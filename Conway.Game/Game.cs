namespace Conway.Game
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
        /// <returns>
        /// An infinite stream of alive cells.
        /// </returns>
        /// <remarks>
        /// Keeps on running even after it's empty.
        /// </remarks>
        public IEnumerable<IEnumerable<TCell>> Run(ICollection<TCell> initialPattern)
        {
            return new PatternQueue(initialPattern);
        }

        private class PatternQueue : IEnumerator<ICollection<TCell>>, IEnumerable<ICollection<TCell>>
        {
            private readonly ICollection<TCell> initialPattern;
            private readonly Queue<ICollection<TCell>> innerQueue = new Queue<ICollection<TCell>>();

            public PatternQueue(ICollection<TCell> initialPattern)
            {
                this.initialPattern = initialPattern;
                innerQueue.Enqueue(initialPattern);
            }

            #region Game of Life

            private IEnumerable<TCell> NextGeneration(ICollection<TCell> pattern)
            {
                return Born(pattern).Union(Survives(pattern));
            }

            private IEnumerable<TCell> AliveNeighbours(TCell cell, ICollection<TCell> pattern)
            {
                return cell.Neighbours().AsParallel().Where(pattern.Contains);
            }

            private IEnumerable<TCell> DeadNeighbours(TCell cell, ICollection<TCell> pattern)
            {
                return cell.Neighbours().AsParallel().Where(n => !pattern.Contains(n));
            }

            private IEnumerable<TCell> Born(ICollection<TCell> pattern)
            {
                var cd = new ConcurrentDictionary<TCell, int>();

                pattern.AsParallel().SelectMany(c => DeadNeighbours(c, pattern)).ForAll(c => cd.AddOrUpdate(c, _ => 1, (_, n) => n + 1));

                //var allDead = pattern.AsParallel().SelectMany(c => DeadNeighbours(c, pattern));
                //
                //Parallel.ForEach(allDead, c => cd.AddOrUpdate(c, _ => 1, (_, n) => n + 1));

                return cd.Where(kvp => kvp.Value == 3).Select(kvp => kvp.Key);
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
                innerQueue.Enqueue(NextGeneration(innerQueue.Dequeue()).ToHashSet());

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
