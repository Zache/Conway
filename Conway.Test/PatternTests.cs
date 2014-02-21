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
    public class PatternTests
    {
        private Game<Vector> sut;

        [SetUp]
        public void SetUp()
        {
            sut = new Game<Vector>();
        }

        [Test]
        public void Die_Hard_Dies_After_130_Generations()
        {
            var diehard = new[]
            {
                new Vector(3, 2),
                new Vector(4, 4),
                new Vector(3, 4),
                new Vector(2, 4),
                new Vector(-2, 3),
                new Vector(-2, 4),
                new Vector(-3, 3),
            }.ToHashSet();

            var dead = sut.Run(diehard).Skip(129).Take(1).First();

            Assert.IsFalse(dead.Any(), "Not dead");
        }

        [Test]
        public void Die_Hard_Alive_After_129_Generations()
        {
            var diehard = new[]
            {
                new Vector(3, 2),
                new Vector(4, 4),
                new Vector(3, 4),
                new Vector(2, 4),
                new Vector(-2, 3),
                new Vector(-2, 4),
                new Vector(-3, 3),
            }.ToHashSet();

            var dead = sut.Run(diehard).Skip(128).Take(1).First();

            Assert.IsTrue(dead.Any(), "Not dead");
        }
    }
}
