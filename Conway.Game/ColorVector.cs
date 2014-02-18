namespace Conway.Game
{
    using System;
    using System.Collections.Generic;

    public struct ColorVector : ICell<ColorVector>, IEquatable<ColorVector>
    {
        private readonly int x;
        private readonly int y;
        private readonly Color color;

        public ColorVector(Color color, int x, int y)
            : this()
        {
            this.color = color;

            this.x = x;
            this.y = y;
        }

        public bool Equals(ColorVector other)
        {
            return this.color == other.color && this.x == other.x && this.y == other.y;
        }

        public static bool operator ==(ColorVector colorVector1, ColorVector colorVector2)
        {
            return colorVector1.Equals(colorVector2);
        }

        public static bool operator !=(ColorVector colorVector1, ColorVector colorVector2)
        {
            return !(colorVector1 == colorVector2);
        }

        public override bool Equals(object obj)
        {
            var colorVector = obj as ColorVector?;
            return colorVector.HasValue && this.Equals(colorVector.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<ColorVector> Neighbours()
        {
            for (var x = this.x - 1; x <= this.x + 1; x++)
                for (var y = this.y - 1; y <= this.y + 1; y++)
                    if (x != this.x && y != this.y)
                        switch (color)
                        {
                            case Color.Red:
                                {
                                    yield return new ColorVector(Color.Orange, x, y);
                                    yield return new ColorVector(Color.Red, x, y);
                                    break;
                                }
                            case Color.Orange:
                                {
                                    yield return new ColorVector(Color.Red, x, y);
                                    yield return new ColorVector(Color.Orange, x, y);
                                    yield return new ColorVector(Color.Yellow, x, y);
                                    break;
                                }
                            case Color.Yellow:
                                {
                                    yield return new ColorVector(Color.Orange, x, y);
                                    yield return new ColorVector(Color.Yellow, x, y);
                                    yield return new ColorVector(Color.Green, x, y);
                                    break;
                                }
                            case Color.Green:
                                {
                                    yield return new ColorVector(Color.Yellow, x, y);
                                    yield return new ColorVector(Color.Green, x, y);
                                    yield return new ColorVector(Color.Blue, x, y);
                                    break;
                                }
                            case Color.Blue:
                                {
                                    yield return new ColorVector(Color.Green, x, y);
                                    yield return new ColorVector(Color.Blue, x, y);
                                    yield return new ColorVector(Color.Indigo, x, y);
                                    break;
                                }
                            case Color.Indigo:
                                {
                                    yield return new ColorVector(Color.Blue, x, y);
                                    yield return new ColorVector(Color.Indigo, x, y);
                                    yield return new ColorVector(Color.Violett, x, y);
                                    break;
                                }
                            case Color.Violett:
                                {
                                    yield return new ColorVector(Color.Indigo, x, y);
                                    yield return new ColorVector(Color.Violett, x, y);
                                    break;
                                }
                        }
        }
    }
}
