namespace Conway.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Game;
    using NUnit.Framework;

    [TestFixture]
    public class GameStillLifesTests
    {
        private Game<Vector> sut;

        [SetUp]
        public void SetUp()
        {
            sut = new Game<Vector>();
        }

        [Test]
        public void Block_Stays_As_Block_10_Generations()
        {
            // xx
            // xx

            var startAndTarget = new[]
            {
                new Vector(0, 0),
                new Vector(0, 1),
                new Vector(1, 1),
                new Vector(1, 0)
            }.ToHashSet();

            foreach (var pattern in sut.Run(startAndTarget).Take(10))
                CollectionAssert.AreEquivalent(startAndTarget, pattern, "Pattern changed");
        }

        [Test]
        public void Beehive_Stays_Beehive_10_Generations()
        {
            //  xx
            // x  x
            //  xx

            var startAndTarget = new[]
            {
                new Vector(3, 4),
                new Vector(4, 4),
                new Vector(5, 5),
                new Vector(2, 5),
                new Vector(3, 6),
                new Vector(4, 6)
            }.ToHashSet();

            foreach (var pattern in sut.Run(startAndTarget).Take(10))
                CollectionAssert.AreEquivalent(startAndTarget, pattern, "Pattern changed");
        }

        [Test]
        public void Loaf_Stays_Loaf_10_Generations()
        {
            //  xx 
            // x  x
            //  x x
            //   x

            var startAndTarget = new[]
            {
                new Vector(6, 1),
                new Vector(7, 1),
                new Vector(5, 2),
                new Vector(8, 2),
                new Vector(6, 3),
                new Vector(8, 3),
                new Vector(7, 4)
            }.ToHashSet();

            foreach (var pattern in sut.Run(startAndTarget).Take(10))
                CollectionAssert.AreEquivalent(startAndTarget, pattern, "Pattern changed");
        }

        [Test]
        public void Boat_Stays_Boat_10_Generations()
        {
            // xx
            // x x
            //  x

            var startAndTarget = new[]
            {
                new Vector(1, 1),
                new Vector(2, 1),
                new Vector(1, 2),
                new Vector(2, 3),
                new Vector(3, 2),
            }.ToHashSet();

            foreach (var pattern in sut.Run(startAndTarget).Take(10))
                CollectionAssert.AreEquivalent(startAndTarget, pattern, "Pattern changed");
        }
    }
}
