namespace Conway.Game
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("Vector X: {x} Y: {y}")]
    public struct Vector : ICell<Vector>, IEquatable<Vector>
    {
        public readonly int X;
        public readonly int Y;

        public Vector(int x, int y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(Vector other)
        {
            return this.X == other.X && this.Y == other.Y;
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
                return 397 * X ^ Y;
            }
        }

        public override string ToString()
        {
            return string.Format("X: {0} Y: {1}", X, Y);
        }

        public IEnumerable<Vector> Neighbours()
        {
            for (var x = this.X - 1; x <= this.X + 1; x++)
                for (var y = this.Y - 1; y <= this.Y + 1; y++)
                    if (x != this.X || y != this.Y)
                        yield return new Vector(x, y);
        }
    }
}
