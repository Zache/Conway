﻿namespace Conway.Game
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class Game<TCell> where TCell : struct, ICell<TCell>, IEquatable<TCell>
    {
        /// <summary>
        /// Starts an infinite lazy Conway's Game of Life.
        /// </summary>
        /// <param name="initialPattern">
        /// The initial pattern used to seed the game.
        /// </param>
        /// <param name="loop">
        /// Should the game loop once every cell is dead?
        /// </param>
        /// <returns>
        /// An infinite stream of alive cells.
        /// </returns>
        /// <remarks>
        /// Keeps on running even after it's empty.
        /// </remarks>
        public IEnumerable<IEnumerable<TCell>> Run(ICollection<TCell> initialPattern, bool loop = false)
        {
            return new PatternQueue(initialPattern, loop);
        }

        private class PatternQueue : IEnumerator<ICollection<TCell>>, IEnumerable<ICollection<TCell>>
        {
            private readonly bool loop;

            private readonly ICollection<TCell> initialPattern;
            private readonly Queue<ICollection<TCell>> innerQueue = new Queue<ICollection<TCell>>();

            public PatternQueue(ICollection<TCell> initialPattern, bool loop = false)
            {
                this.initialPattern = initialPattern;
                this.loop = loop;
                innerQueue.Enqueue(initialPattern);
            }

            #region Game of Life

            private IEnumerable<TCell> NextGeneration(ICollection<TCell> pattern)
            {
                return Survives(pattern).Union(Born(pattern));
            }

            private IEnumerable<TCell> AliveNeighbours(TCell cell, ICollection<TCell> pattern)
            {
                return from c in cell.Neighbours() 
                       where pattern.Contains(c) 
                       select c;
            }

            private IEnumerable<TCell> DeadNeighbours(TCell cell, ICollection<TCell> pattern)
            {
                return from c in cell.Neighbours() 
                       where !pattern.Contains(c) 
                       select c;
            }

            private IEnumerable<TCell> Born(ICollection<TCell> pattern)
            {
                var cd = new ConcurrentDictionary<TCell, int>();

                pattern
                    .AsParallel()
                    .SelectMany(c => DeadNeighbours(c, pattern))
                    .ForAll(c => cd.AddOrUpdate(c, _ => 1, (_, n) => n + 1));

                return from kvp in cd
                       where kvp.Value == 3
                       select kvp.Key;
            }

            private IEnumerable<TCell> Survives(ICollection<TCell> pattern)
            {
                return from c in pattern.AsParallel()
                       let nbC = AliveNeighbours(c, pattern).Count()
                       where nbC >= 2 && nbC <= 3
                       select c;
            }

            #endregion Game of Life

            #region IEnumerator<TCell>

            public bool MoveNext()
            {
                var nextGeneration = NextGeneration(innerQueue.Dequeue()).ToHashSet();
                if (nextGeneration.Any())
                    innerQueue.Enqueue(nextGeneration);
                else
                    innerQueue.Enqueue(loop ? initialPattern : new HashSet<TCell>());

                return true;
            }

            public void Reset()
            {
                innerQueue.Clear();
                innerQueue.Enqueue(initialPattern);
            }

            public ICollection<TCell> Current { get { return innerQueue.Peek(); } }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
            }

            #endregion IEnumerator<TCell>

            #region IEnumerable<TCell>

            public IEnumerator<ICollection<TCell>> GetEnumerator()
            {
                return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion IEnumerable<TCell>
        }
    }
}
