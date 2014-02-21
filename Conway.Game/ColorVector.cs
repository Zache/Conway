namespace Conway.Game
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("Vector X: {x} Y: {y} Color: {color}")]
    public struct ColorVector : ICell<ColorVector>, IEquatable<ColorVector>
    {
        public readonly int X;
        public readonly int Y;
        public readonly RogbivColor Color;

        public ColorVector(RogbivColor rogbivColor, int x, int y)
            : this()
        {
            this.Color = rogbivColor;

            this.X = x;
            this.Y = y;
        }

        public bool Equals(ColorVector other)
        {
            return this.Color == other.Color && this.X == other.X && this.Y == other.Y;
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
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<ColorVector> Neighbours()
        {
            for (var x = this.X - 1; x <= this.X + 1; x++)
                for (var y = this.Y - 1; y <= this.Y + 1; y++)
                    if (x != this.X && y != this.Y)
                        switch (Color)
                        {
                            case RogbivColor.Red:
                                {
                                    yield return new ColorVector(RogbivColor.Orange, x, y);
                                    yield return new ColorVector(RogbivColor.Red, x, y);
                                    break;
                                }
                            case RogbivColor.Orange:
                                {
                                    yield return new ColorVector(RogbivColor.Red, x, y);
                                    yield return new ColorVector(RogbivColor.Orange, x, y);
                                    yield return new ColorVector(RogbivColor.Yellow, x, y);
                                    break;
                                }
                            case RogbivColor.Yellow:
                                {
                                    yield return new ColorVector(RogbivColor.Orange, x, y);
                                    yield return new ColorVector(RogbivColor.Yellow, x, y);
                                    yield return new ColorVector(RogbivColor.Green, x, y);
                                    break;
                                }
                            case RogbivColor.Green:
                                {
                                    yield return new ColorVector(RogbivColor.Yellow, x, y);
                                    yield return new ColorVector(RogbivColor.Green, x, y);
                                    yield return new ColorVector(RogbivColor.Blue, x, y);
                                    break;
                                }
                            case RogbivColor.Blue:
                                {
                                    yield return new ColorVector(RogbivColor.Green, x, y);
                                    yield return new ColorVector(RogbivColor.Blue, x, y);
                                    yield return new ColorVector(RogbivColor.Indigo, x, y);
                                    break;
                                }
                            case RogbivColor.Indigo:
                                {
                                    yield return new ColorVector(RogbivColor.Blue, x, y);
                                    yield return new ColorVector(RogbivColor.Indigo, x, y);
                                    yield return new ColorVector(RogbivColor.Violett, x, y);
                                    break;
                                }
                            case RogbivColor.Violett:
                                {
                                    yield return new ColorVector(RogbivColor.Indigo, x, y);
                                    yield return new ColorVector(RogbivColor.Violett, x, y);
                                    break;
                                }
                        }
        }
    }
}
