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
    public class GameOscillatorsTests
    {
        private Game<Vector> sut;

        [SetUp]
        public void SetUp()
        {
            sut = new Game<Vector>();
        }

        [Test]
        public void Blinker_Blinks_Has_Period_2_For_10_Generations()
        {
            // 1     2

            // x    
            // x    xxx
            // x 

            var start = new[]
            {
                new Vector(-1, 0),
                new Vector(0, 0),
                new Vector(1, 0),
            }.ToHashSet();

            var periodOne = new[]
            {
                new Vector(0, 1),
                new Vector(0, 0),
                new Vector(0, -1),
            }.ToHashSet();

            var periodTwo = start;

            var periodSwitch = false;

            var generation = 0;
            foreach (var pattern in sut.Run(start).Take(10))
            {
                generation++;
                CollectionAssert.AreEquivalent(periodSwitch ? periodTwo : periodOne, pattern, "Wrong pattern in generation: {0}", generation);

                periodSwitch = !periodSwitch;
            }
        }

        [Test]
        public void Toad_Toads_Has_Period_2_For_10_Generations()
        {
            // 1            2

            //                x
            //              x  x
            //  xxx         x  x
            // xxx           x
            //

            var start = new[]
            {
                new Vector(0, 4),
                new Vector(1, 3),
                new Vector(1, 4),
                new Vector(2, 3),
                new Vector(2, 4),
                new Vector(3, 3)
            }.ToHashSet();

            var periodOne = new[]
            {
                new Vector(0, 3),
                new Vector(0, 4),
                new Vector(1, 5),
                new Vector(2, 2),
                new Vector(3, 3),
                new Vector(3, 4)
            }.ToHashSet();

            var periodTwo = start;

            var periodSwitch = false;

            var generation = 0;
            foreach (var pattern in sut.Run(start).Take(10))
            {
                generation++;
                CollectionAssert.AreEquivalent(periodSwitch ? periodTwo : periodOne, pattern, "Wrong pattern in generation: {0}", generation);

                periodSwitch = !periodSwitch;
            }
        }

        [Test]
        public void Beacon_Beacons_Has_Period_2_For_10_Generations()
        {
            // 1        2
            //  1234   1234
            //1 xx   1 xx
            //2 xx   2 x 
            //3   xx 3    x  
            //4   xx 4   xx

            var start = new[]
            {
                new Vector(1,1),
                new Vector(1,2),
                new Vector(2,1),
                new Vector(2,2),
                new Vector(3,3),
                new Vector(3,4),
                new Vector(4,3),
                new Vector(4,4)
            }.ToHashSet();

            var periodOne = new[]
            {
                new Vector(1,1),
                new Vector(1,2),
                new Vector(2,1),
                new Vector(3,4),
                new Vector(4,3),
                new Vector(4,4)
            }.ToHashSet();

            var periodTwo = start;

            var periodSwitch = false;

            var generation = 0;
            foreach (var pattern in sut.Run(start).Take(10))
            {
                generation++;
                CollectionAssert.AreEquivalent(periodSwitch ? periodTwo : periodOne, pattern, "Wrong pattern in generation: {0}", generation);

                periodSwitch = !periodSwitch;
            }
        }
    }
}
