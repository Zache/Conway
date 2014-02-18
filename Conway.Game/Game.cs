namespace Conway.Game
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Game<TCell> where TCell : struct, ICell<TCell>, IEquatable<TCell>
    {
        public IEnumerable<TCell> NextGeneration(ISet<TCell> pattern)
        {
            return Born(pattern).Union(Survives(pattern));
        }

        private IEnumerable<TCell> AliveNeighbours(TCell cell, ISet<TCell> pattern)
        {
            return cell.Neighbours().AsParallel().Where(pattern.Contains);
        }

        private IEnumerable<TCell> DeadNeighbours(TCell cell, ISet<TCell> pattern)
        {
            return cell.Neighbours().AsParallel().Where(n => !pattern.Contains(n));
        }

        private IEnumerable<TCell> Born(ISet<TCell> pattern)
        {
            var cd = new ConcurrentDictionary<TCell, int>();

            pattern.AsParallel().SelectMany(c => DeadNeighbours(c, pattern)).ForAll(c => cd.AddOrUpdate(c, _ => 1, (_, n) => n + 1));

            //var allDead = pattern.AsParallel().SelectMany(c => DeadNeighbours(c, pattern));
            //
            //Parallel.ForEach(allDead, c => cd.AddOrUpdate(c, _ => 1, (_, n) => n + 1));

            return cd.Where(kvp => kvp.Value == 3).Select(kvp => kvp.Key);
        }

        private IEnumerable<TCell> Survives(ISet<TCell> pattern)
        {
            return from c in pattern.AsParallel()
                   let nbC = AliveNeighbours(c, pattern).Count()
                   where nbC >= 2 && nbC <= 3
                   select c;
        }
    }
}
