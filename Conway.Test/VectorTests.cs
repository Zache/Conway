namespace Conway.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Policy;
    using System.Text;
    using System.Threading.Tasks;
    using Game;
    using NUnit.Framework;

    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void Vector_Has_8_Neighbours()
        {
            var v = new Vector(0, 0);

            var neighbours = v.Neighbours();

            Assert.AreEqual(8, neighbours.Count(), "Wrong number of neighbours");
        }

        [Test]
        public void Vector_0_0_Has_Correct_Neighbours()
        {
            var v = new Vector(0, 0);

            var neighbours = v.Neighbours();

            CollectionAssert.AreEquivalent(
                new[]
                {
                    new Vector(0,1),
                    new Vector(1,1),
                    new Vector(1,0),
                    new Vector(-1,0),
                    new Vector(-1,-1),
                    new Vector(0,-1),
                    new Vector(-1,1),
                    new Vector(1,-1)
                }
                , neighbours, "Wrong number of neighbours");
        }
    }
}
