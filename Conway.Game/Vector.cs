namespace Conway.Game
{
    using System;
    using System.Collections.Generic;

    public struct Vector : ICell<Vector>, IEquatable<Vector>
    {
        private readonly int x;
        private readonly int y;

        public Vector(int x, int y)
            : this()
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Vector other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.Equals(vector2);
        }

        public static bool operator !=(Vector colorVector1, Vector colorVector2)
        {
            return !(colorVector1 == colorVector2);
        }

        public override bool Equals(object obj)
        {
            var vector = obj as Vector?;
            return vector.HasValue && this.Equals(vector.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<Vector> Neighbours()
        {
            for (var x = this.x - 1; x <= this.x + 1; x++)
                for (var y = this.y - 1; y <= this.y + 1; y++)
                    if (x != this.x && y != this.y)
                        yield return new Vector(x, y);
        }
    }
}
