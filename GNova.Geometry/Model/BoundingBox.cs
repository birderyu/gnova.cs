using GNova.Core.Attribute;
using System;

namespace GNova.Geometry.Model
{
    [Immutable]
    public sealed class BoundingBox : IComparable<BoundingBox>, ICloneable
    {
        public static readonly BoundingBox NONE = new BoundingBox(Coordinate.NONE, Coordinate.NONE);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bbox"></param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public BoundingBox(string bbox)
        {
            if (string.IsNullOrWhiteSpace(bbox))
            {
                throw new ArgumentException("bbox is empty.");
            }
            bbox = bbox.Trim();
            if (!bbox.StartsWith("[") || !bbox.EndsWith("]"))
            {
                throw new ArgumentException("bbox must start with [ and end with ].");
            }
            bbox = bbox.Substring(1, bbox.Length - 1);
            string[] values = bbox.Split(',');
            if (values.Length != 4 && values.Length != 6)
            {
                throw new ArgumentException("values must be 4 doubles or 6 doubles");
            }

            if (values.Length == 4)
            {
                MinX = double.Parse(values[0].Trim());
                MaxX = double.Parse(values[2].Trim());
                MinY = double.Parse(values[1].Trim());
                MaxY = double.Parse(values[3].Trim());
                MinZ = Coordinate.NULL_ORDINATE_VALUE;
                MaxZ = Coordinate.NULL_ORDINATE_VALUE;
            }
            else
            {
                MinX = double.Parse(values[0].Trim());
                MaxX = double.Parse(values[4].Trim());
                MinY = double.Parse(values[1].Trim());
                MaxY = double.Parse(values[5].Trim());
                MinZ = double.Parse(values[2].Trim());
                MaxZ = double.Parse(values[3].Trim());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vs"></param>
        /// <exception cref="ArgumentException"></exception>
        public BoundingBox(params double[] vs)
        {
            if (vs == null || (vs.Length != 4 && vs.Length != 6))
            {
                throw new ArgumentException("values must be 4 doubles or 6 doubles");
            }
            if (vs.Length == 4)
            {
                MinX = vs[0];
                MaxX = vs[2];
                MinY = vs[1];
                MaxY = vs[3];
                MinZ = Coordinate.NULL_ORDINATE_VALUE;
                MaxZ = Coordinate.NULL_ORDINATE_VALUE;
            }
            else
            {
                MinX = vs[0];
                MaxX = vs[4];
                MinY = vs[1];
                MaxY = vs[5];
                MinZ = vs[2];
                MaxZ = vs[3];
            }
        }

        public BoundingBox(double x, double y)
        {
            MinX = MaxX = x;
            MinY = MaxY = y;
            MinZ = Coordinate.NULL_ORDINATE_VALUE;
            MaxZ = Coordinate.NULL_ORDINATE_VALUE;
        }

        public BoundingBox(double x, double y, double z)
        {
            MinX = MaxX = x;
            MinY = MaxY = y;
            MinZ = MaxZ = z;
        }

        public BoundingBox(double x1, double x2, double y1, double y2)
        {
            MinX = Math.Min(x1, x2);
            MaxX = Math.Max(x1, x2);
            MinY = Math.Min(y1, y2);
            MaxY = Math.Max(y1, y2);
            MinZ = Coordinate.NULL_ORDINATE_VALUE;
            MaxZ = Coordinate.NULL_ORDINATE_VALUE;
        }

        public BoundingBox(double x1, double x2, double y1, double y2, double z1, double z2)
        {
            MinX = Math.Min(x1, x2);
            MaxX = Math.Max(x1, x2);
            MinY = Math.Min(y1, y2);
            MaxY = Math.Max(y1, y2);
            MinZ = Math.Min(z1, z2);
            MaxZ = Math.Max(z1, z2);
        }

        public BoundingBox([NotNull] Coordinate c1, [NotNull] Coordinate c2)
        {
            MinX = Math.Min(c1.X, c2.X);
            MaxX = Math.Max(c1.X, c2.X);
            MinY = Math.Min(c1.Y, c2.Y);
            MaxY = Math.Max(c1.Y, c2.Y);
            if (c1.HasZ && c2.HasZ)
            {
                MinZ = Math.Min(c1.Z, c2.Z);
                MaxZ = Math.Max(c1.Z, c2.Z);
            }
            else
            {
                MinZ = Coordinate.NULL_ORDINATE_VALUE;
                MaxZ = Coordinate.NULL_ORDINATE_VALUE;
            }
        }

        public BoundingBox([NotNull] BoundingBox bbox)
        {
            MinX = bbox.MinX;
            MaxX = bbox.MaxX;
            MinY = bbox.MinY;
            MaxY = bbox.MaxY;
            MinZ = bbox.MinZ;
            MaxZ = bbox.MaxZ;
        }

        public double MinX { get; private set; }

        public double MaxX { get; private set; }

        public double MinY { get; private set; }

        public double MaxY { get; private set; }

        public double MinZ { get; private set; }

        public double MaxZ { get; private set; }

        public bool HasZ
        {
            get
            {
                return !double.IsNaN(MinZ);
            }
        }

        public double Length
        {
            get
            {
                return MaxX - MinX;
            }
        }

        public double Width
        {
            get
            {
                return MaxY - MinY;
            }
        }

        public double Height
        {
            get
            {
                return MaxZ - MinZ;
            }
        }

        public double Area
        {
            get
            {
                return Length * Width;
            }
        }

        public double Volume
        {
            get
            {
                return Area * Height;
            }
        }

        [NotNull]
        public Coordinate Center
        {
            get
            {
                return new Coordinate((MinX + MaxX) / 2.0, (MinY + MaxY) / 2.0, (MinZ + MaxZ) / 2.0);
            }
        }

        public bool Intersects(double x, double y)
        {
            return !(x > MaxX || x < MinX ||
                    y > MaxY || y < MinY);
        }

        public bool Intersects(double x, double y, double z)
        {
            if (!HasZ)
            {
                return false;
            }
            return !(x > MaxX || x < MinX ||
                    y > MaxY || y < MinY ||
                    z > MaxZ || z < MinZ);
        }

        public bool Intersects([NotNull] Coordinate c)
        {
            return c.HasZ ?
                    Intersects(c.X, c.Y, c.Z) :
                    Intersects(c.X, c.Y);
        }

        public bool Intersects([NotNull] BoundingBox bbox)
        {

            if ((HasZ && !bbox.HasZ) || (!HasZ && bbox.HasZ))
            {
                return false;
            }

            if (HasZ)
            {
                return !(bbox.MinX > MaxX || bbox.MaxX < MinX ||
                        bbox.MinY > MaxY || bbox.MaxY < MinY ||
                        bbox.MinZ > MaxZ || bbox.MaxZ < MinZ);
            }
            else
            {
                return !(bbox.MinX > MaxX || bbox.MaxX < MinX ||
                        bbox.MinY > MaxY || bbox.MaxY < MinY);
            }
        }

        public bool Contains(double x, double y)
        {
            return Covers(x, y);
        }

        public bool Contains(double x, double y, double z)
        {
            return Covers(x, y, z);
        }

        public bool Contains([NotNull] Coordinate coord)
        {
            return Covers(coord);
        }

        public bool Contains([NotNull] BoundingBox bbox)
        {
            return Covers(bbox);
        }

        public bool Covers(double x, double y)
        {
            return x >= MinX && x <= MaxX &&
                    y >= MinY && y <= MaxY;
        }

        public bool Covers(double x, double y, double z)
        {
            if (!HasZ)
            {
                return false;
            }
            return x >= MinX && x <= MaxX &&
                    y >= MinY && y <= MaxY &&
                    z >= MinZ && z <= MaxZ;
        }

        public bool Covers([NotNull] Coordinate c)
        {
            return c.HasZ ?
                    Covers(c.X, c.Y, c.Z) :
                    Covers(c.X, c.Y);
        }

        public bool Covers([NotNull] BoundingBox bbox)
        {
            if ((HasZ && !bbox.HasZ) || (!HasZ && bbox.HasZ))
            {
                return false;
            }
            if (HasZ)
            {
                return bbox.MinX >= MinX && bbox.MaxX <= MaxX &&
                        bbox.MinY >= MinY && bbox.MaxY <= MaxY &&
                        bbox.MinZ >= MinZ && bbox.MaxZ <= MaxZ;
            }
            else
            {
                return bbox.MinX >= MinX && bbox.MaxX <= MaxX &&
                        bbox.MinY >= MinY && bbox.MaxY <= MaxY;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bbox"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double Distance([NotNull] BoundingBox bbox)
        {
            if ((HasZ && !bbox.HasZ) || (!HasZ && bbox.HasZ))
            {
                throw new ArgumentException($"this bbox {this} can not distance to giving bbox {bbox}");
            }
            if (Intersects(bbox)) return 0;

            double dx = 0.0;
            if (MaxX < bbox.MinX) dx = bbox.MinX - MaxX;
            else if (MinX > bbox.MaxX) dx = MinX - bbox.MaxX;

            double dy = 0.0;
            if (MaxY < bbox.MinY) dy = bbox.MinY - MaxY;
            else if (MinY > bbox.MaxY) dy = MinY - bbox.MaxY;

            if (HasZ)
            {
                double dz = 0.0;
                if (MaxZ < bbox.MinZ) dz = bbox.MinZ - MaxZ;
                else if (MinZ > bbox.MaxZ) dz = MinZ - bbox.MaxZ;
                return Math.Sqrt(dx * dx + dy * dy + dz * dz);
            }
            else
            {
                if (dx == 0.0) return dy;
                if (dy == 0.0) return dx;
                return Math.Sqrt(dx * dx + dy * dy);
            }
        }

        [NotNull]
        public BoundingBox ExpandToInclude(double x, double y)
        {
            double newMinX = Math.Min(x, MinX);
            double newMaxX = Math.Max(x, MaxX);
            double newMinY = Math.Min(y, MinY);
            double newMaxY = Math.Max(y, MaxY);
            return new BoundingBox(newMinX, newMaxX, newMinY, newMaxY, MinZ, MaxZ);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [NotNull]
        public BoundingBox ExpandToInclude(double x, double y, double z)
        {
            if (HasZ)
            {
                throw new ArgumentException($"this bbox do not has z: {this}");
            }
            double newMinX = Math.Min(x, MinX);
            double newMaxX = Math.Max(x, MaxX);
            double newMinY = Math.Min(y, MinY);
            double newMaxY = Math.Max(y, MaxY);
            double newMinZ = Math.Min(z, MinZ);
            double newMaxZ = Math.Max(z, MaxZ);
            return new BoundingBox(newMinX, newMaxX, newMinY, newMaxY, newMinZ, newMaxZ);
        }

        [NotNull]
        public BoundingBox ExpandToInclude([NotNull] Coordinate c)
        {
            return c.HasZ ?
                    ExpandToInclude(c.X, c.Y, c.Z) :
                    ExpandToInclude(c.X, c.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bbox"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [NotNull]
        public BoundingBox ExpandToInclude([NotNull] BoundingBox bbox)
        {
            if ((HasZ && !bbox.HasZ) || (!HasZ && bbox.HasZ))
            {
                throw new ArgumentException($"this bbox {this} can not expand to giving bbox {bbox}");
            }
            double newMinX = Math.Min(bbox.MinX, MinX);
            double newMaxX = Math.Max(bbox.MaxX, MaxX);
            double newMinY = Math.Min(bbox.MinY, MinY);
            double newMaxY = Math.Max(bbox.MaxY, MaxY);
            double newMinZ = MinZ;
            double newMaxZ = MaxZ;
            if (HasZ)
            {
                newMinZ = Math.Min(bbox.MinZ, MinZ);
                newMaxZ = Math.Max(bbox.MaxZ, MaxZ);
            }
            return new BoundingBox(newMinX, newMaxX, newMinY, newMaxY, newMinZ, newMaxZ);
        }

        [NotNull]
        public BoundingBox ExpandBy(double delta)
        {
            return ExpandBy(delta, delta, delta);
        }

        [NotNull]
        public BoundingBox ExpandBy(double deltaX, double deltaY)
        {
            return new BoundingBox(MinX - deltaX, MaxX + deltaX, MinY - deltaY, MaxY + deltaY, MinZ, MaxZ);
        }

        [NotNull]
        public BoundingBox ExpandBy(double deltaX, double deltaY, double deltaZ)
        {
            if (HasZ)
            {
                return ExpandBy(deltaX, deltaY);
            }
            return new BoundingBox(MinX - deltaX, MaxX + deltaX, MinY - deltaY, MaxY + deltaY, MinZ - deltaZ, MaxZ + deltaZ);
        }

        [NotNull]
        public BoundingBox Translate(double trans)
        {
            return Translate(trans, trans, trans);
        }

        [NotNull]
        public BoundingBox Translate(double transX, double transY)
        {
            return new BoundingBox(MinX + transX, MaxX + transX, MinY + transY, MaxY + transY, MinZ, MaxZ);
        }

        [NotNull]
        public BoundingBox Translate(double transX, double transY, double transZ)
        {
            if (!HasZ)
            {
                return Translate(transX, transY);
            }
            return new BoundingBox(MinX + transX, MaxX + transX, MinY + transY, MaxY + transY, MinZ + transZ, MaxZ + transZ);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bbox"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [NotNull]
        public BoundingBox Intersection([NotNull] BoundingBox bbox)
        {

            if ((HasZ && !bbox.HasZ) || (!HasZ && bbox.HasZ))
            {
                throw new ArgumentException($"this bbox {this} can not intersects to giving bbox {bbox}");
            }
            double nexMinX = Math.Min(MinX, bbox.MinX);
            double nexMaxX = Math.Max(MaxX, bbox.MaxX);
            double nexMinY = Math.Min(MinY, bbox.MinY);
            double nexMaxY = Math.Max(MaxY, bbox.MaxY);
            double nexMinZ = MinZ;
            double nexMaxZ = MaxZ;
            if (HasZ)
            {
                nexMinZ = Math.Min(MinZ, bbox.MinZ);
                nexMaxZ = Math.Max(MaxZ, bbox.MaxZ);
            }
            return new BoundingBox(nexMinX, nexMaxX, nexMinY, nexMaxY, nexMinZ, nexMaxZ);
        }

        public bool Equals([NotNull] BoundingBox bbox, double tolerance)
        {
            if ((HasZ && !bbox.HasZ) || (!HasZ && bbox.HasZ))
            {
                return false;
            }
            if (HasZ)
            {
                return Core.Equals.DoubleEquals(MinX, bbox.MinX, tolerance) &&
                        Core.Equals.DoubleEquals(MaxX, bbox.MaxX, tolerance) &&
                        Core.Equals.DoubleEquals(MinY, bbox.MinY, tolerance) &&
                        Core.Equals.DoubleEquals(MaxY, bbox.MaxY, tolerance) &&
                        Core.Equals.DoubleEquals(MinZ, bbox.MinZ, tolerance) &&
                        Core.Equals.DoubleEquals(MaxZ, bbox.MaxZ, tolerance);
            }
            else
            {
                return Core.Equals.DoubleEquals(MinX, bbox.MinX, tolerance) &&
                        Core.Equals.DoubleEquals(MaxX, bbox.MaxX, tolerance) &&
                        Core.Equals.DoubleEquals(MinY, bbox.MinY, tolerance) &&
                        Core.Equals.DoubleEquals(MaxY, bbox.MaxY, tolerance);
            }
        }

        public bool Equals([NotNull] BoundingBox bbox)
        {
            if ((HasZ && !bbox.HasZ) || (!HasZ && bbox.HasZ))
            {
                return false;
            }
            if (HasZ)
            {
                return MinX == bbox.MinX &&
                        MaxX == bbox.MaxX &&
                        MinY == bbox.MinY &&
                        MaxY == bbox.MaxY &&
                        MinZ == bbox.MinZ &&
                        MaxZ == bbox.MaxZ;
            }
            else
            {
                return MinX == bbox.MinX &&
                        MaxX == bbox.MaxX &&
                        MinY == bbox.MinY &&
                        MaxY == bbox.MaxY;
            }
        }

        public BoundingBox Clone()
        {
            if (this == NONE)
            {
                return NONE;
            }
            return new BoundingBox(this);
        }

        #region object

        public override bool Equals(object obj)
        {
            if (!(obj is BoundingBox))
            {
                return false;
            }
            return Equals(obj as BoundingBox);
        }

        public override int GetHashCode()
        {
            int result = 1;
            result = 31 * result + MinX.GetHashCode();
            result = 31 * result + MaxX.GetHashCode();
            result = 31 * result + MinY.GetHashCode();
            result = 31 * result + MaxY.GetHashCode();
            result = 31 * result + (HasZ ? MinZ.GetHashCode() : 0);
            result = 31 * result + (HasZ ? MaxZ.GetHashCode() : 0);
            return result;
        }

        public override string ToString()
        {
            if (HasZ)
            {
                return $"[{MinX}, {MinY}, {MinZ}, {MaxX}, {MaxY}, {MaxZ}]";
            }
            else
            {
                return $"[{MinX}, {MinY}, {MaxX}, {MaxY}]";
            }
        }

        #endregion

        #region IComparable

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bbox"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public int CompareTo([NotNull] BoundingBox bbox)
        {
            if ((HasZ && !bbox.HasZ) || (!HasZ && bbox.HasZ))
            {
                throw new ArgumentException($"this bbox {this} can not compare to giving bbox {bbox}");
            }
            if (MinX < bbox.MinX) return -1;
            if (MinX > bbox.MinX) return 1;
            if (MinY < bbox.MinY) return -1;
            if (MinY > bbox.MinY) return 1;
            if (HasZ)
            {
                if (MinZ < bbox.MinZ) return -1;
                if (MinZ > bbox.MinZ) return 1;
            }
            if (MaxX < bbox.MaxX) return -1;
            if (MaxX > bbox.MaxX) return 1;
            if (MaxY < bbox.MaxY) return -1;
            if (MaxY > bbox.MaxY) return 1;
            if (HasZ)
            {
                if (MaxZ < bbox.MaxZ) return -1;
                if (MaxZ > bbox.MaxZ) return 1;
            }
            return 0;
        }

        #endregion

        #region ICloneable

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

    }
}
