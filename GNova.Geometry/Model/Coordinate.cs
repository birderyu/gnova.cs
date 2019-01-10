using GNova.Core.Attribute;
using System;

namespace GNova.Geometry.Model
{
    public sealed class Coordinate : IComparable<Coordinate>, ICloneable
    {

        public static readonly Coordinate NONE = new Coordinate(
            NULL_ORDINATE_VALUE,
            NULL_ORDINATE_VALUE,
            NULL_ORDINATE_VALUE,
            NULL_ORDINATE_VALUE);

        public const double NULL_ORDINATE_VALUE = double.NaN;

        /// <summary>
        /// X坐标的纵轴
        /// </summary>
        public const int ORDINATE_X = 0;

        /// <summary>
        /// Y坐标的纵轴
        /// </summary>
        public const int ORDINATE_Y = 1;

        /// <summary>
        /// Z坐标的纵轴
        /// </summary>
        public const int ORDINATE_Z = 2;

        /// <summary>
        /// 度量值的纵轴
        /// </summary>
        public const int ORDINATE_M = 3;

        public Coordinate(double x, double y)
            : this(x, y , NULL_ORDINATE_VALUE, NULL_ORDINATE_VALUE)
        {
            
        }

        public Coordinate(double x, double y, double z)
            : this(x, y, z, NULL_ORDINATE_VALUE)
        {

        }

        public Coordinate(double x, double y, double z, double m)
        {
            X = x;
            Y = y;
            Z = z;
            M = m;
        }

        public Coordinate([NotNull] Coordinate c)
            : this(c.X, c.Y, c.Z, c.M)
        {

        }

        public bool HasZ
        {
            get
            {
                return !double.IsNaN(Z);
            }
        }

        public bool HasM
        {
            get
            {
                return !double.IsNaN(M);
            }
        }

        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        public double M { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordinateId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public double this[int ordinateId]
        {
            get
            {
                switch (ordinateId)
                {
                    case ORDINATE_X: return X;
                    case ORDINATE_Y: return Y;
                    case ORDINATE_Z: return Z;
                    case ORDINATE_M: return M;
                }
                throw new ArgumentOutOfRangeException("Invalid ordinate index: " + ordinateId);
            }
        }

        public double Distance2D([NotNull] Coordinate c)
        {
            double dx = X - c.X;
            double dy = Y - c.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public double Distance3D([NotNull] Coordinate c)
        {
            if (!HasZ || !c.HasZ)
            {
                return Distance2D(c);
            }
            double dx = X - c.X;
            double dy = Y - c.Y;
            double dz = Z - c.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public double Distance([NotNull] Coordinate c)
        {
            return Distance3D(c);
        }

        public bool Equals2D([NotNull] Coordinate c)
        {
            return X == c.X && Y == c.Y;
        }

        public bool Equals2D([NotNull] Coordinate c, double tolerance)
        {
            return Core.Equals.DoubleEquals(X, c.X, tolerance) &&
                Core.Equals.DoubleEquals(Y, c.Y, tolerance);
        }

        public bool Equals3D([NotNull] Coordinate c)
        {
            return X == c.X && Y == c.Y &&
                ((!HasZ && !c.HasZ) || Z == c.Z);
        }

        public bool Equals3D([NotNull] Coordinate c, double tolerance)
        {
            return Core.Equals.DoubleEquals(X, c.X, tolerance) &&
                Core.Equals.DoubleEquals(Y, c.Y, tolerance) &&
                ((!HasZ && !c.HasZ) || Core.Equals.DoubleEquals(Z, c.Z, tolerance));
        }

        public bool Equals([NotNull] Coordinate c)
        {
            return X == c.X && Y == c.Y &&
                ((!HasZ && !c.HasZ) || Z == c.Z) &&
                ((!HasM && !c.HasM) || M == c.M);
        }

        public bool Equals([NotNull] Coordinate c, double tolerance)
        {
            return Core.Equals.DoubleEquals(X, c.X, tolerance) &&
                Core.Equals.DoubleEquals(Y, c.Y, tolerance) &&
                ((!HasZ && !c.HasZ) || Core.Equals.DoubleEquals(Z, c.Z, tolerance)) &&
                ((!HasM && !c.HasM) || Core.Equals.DoubleEquals(M, c.M, tolerance));
        }

        public int CompareTo2D([NotNull] Coordinate c)
        {
            if (X < c.X) return -1;
            if (X > c.X) return 1;
            if (Y < c.Y) return -1;
            if (Y > c.Y) return 1;
            return 0;
        }

        public int CompareTo3D([NotNull] Coordinate c)
        {
            if (X < c.X) return -1;
            if (X > c.X) return 1;
            if (Y < c.Y) return -1;
            if (Y > c.Y) return 1;
            return CompareToZM(Z, c.Z);
        }

        public Coordinate Clone()
        {
            if (this == NONE)
            {
                return NONE;
            }
            return new Coordinate(this);
        }

        #region object

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (!(obj is Coordinate))
            {
                return false;
            }
            return Equals(obj as Coordinate);
        }

        public override int GetHashCode()
        {
            int result = 1;
            result = 31 * result + X.GetHashCode();
            result = 31 * result + Y.GetHashCode();
            result = 31 * result + (HasZ ? Z.GetHashCode() : 0);
            result = 31 * result + (HasM ? M.GetHashCode() : 0);
            return result;
        }

        public override string ToString()
        {
            if (!HasZ)
            {
                return $"[{X}, {Y}]";
            }
            else if (!HasM)
            {
                return $"[{X}, {Y}, {Z}]";
            }
            else
            {
                return $"[{X}, {Y}, {Z}, {M}]";
            }
        }

        #endregion

        #region IComparable

        public int CompareTo([NotNull] Coordinate c)
        {
            if (X < c.X) return -1;
            if (X > c.X) return 1;
            if (Y < c.Y) return -1;
            if (Y > c.Y) return 1;
            int c2z = CompareToZM(Z, c.Z);
            if (c2z != 0) return c2z;
            return CompareToZM(M, c.M);
        }

        #endregion

        #region ICloneable

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        private static int CompareToZM(double d1, double d2)
        {
            if (double.IsNaN(d1) && double.IsNaN(d2)) return 0;
            if (double.IsNaN(d1)) return -1;
            if (double.IsNaN(d2)) return 1;
            if (d1 < d2) return -1;
            if (d1 > d2) return 1;
            return 0;
        }
 
    }
}
