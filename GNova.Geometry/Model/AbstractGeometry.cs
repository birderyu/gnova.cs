using GNova.Core.Attribute;
using System;
using System.Collections.Generic;

namespace GNova.Geometry.Model
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractGeometry : IGeometry
    {
        private IGeometryFactory _factory;

        public AbstractGeometry(IGeometryFactory factory)
        {
            _factory = factory;
        }

        [NotNull]
        public virtual IGeometryFactory Factory
        {
            get
            {
                return _factory;
            }
        }

        public virtual int Srid
        {
            get
            {
                return _factory.Srid;
            }
        }

        [NotNull]
        public string ToGeometryJSON()
        {
            // TODO
            return null;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IGeometry))
            {
                return false;
            }
            return Equals(obj as IGeometry);
        }

        public override string ToString()
        {
            return ToGeometryJSON();
        }

        #region IGeometry

        [NotNull]
        public abstract GeometryType Type { get; }

        [NotNull]
        public abstract BoundingBox BoundingBox { get; }

        public abstract int Dimension { get; }

        public abstract int BoundaryDimension { get; }

        public abstract int CoordinateDimension { get; }

        public abstract bool IsEmpty { get; }

        public abstract bool IsSimple { get; }

        public abstract bool IsValid { get; }

        [NotNull]
        public abstract Coordinate Coordinate { get; }

        [NotNull]
        public abstract IEnumerable<Coordinate> Coordinates { get; }

        [NotNull]
        public abstract IGeometry Reverse();

        [NotNull]
        public abstract IGeometry Normalize();

        #endregion

        #region ITopologicalOperator

        [NotNull]
        public abstract IGeometry Boundary { get; }

        [NotNull]
        public abstract IPoint Centroid { get; }

        [NotNull]
        public abstract IPoint Interior { get; }

        [NotNull]
        public abstract IGeometry ConvexHull();

        [NotNull]
        public abstract IGeometry Clip([NotNull] BoundingBox bbox);

        [NotNull]
        public abstract IGeometry Split([NotNull] IGeometry bladeIn);

        [NotNull]
        public abstract IGeometry Cut([NotNull] IGeometry bladeIn);

        [NotNull]
        public abstract IPolygon Buffer(double distance);

        [NotNull]
        public abstract IPolygon Buffer(double distance, int quadrantSegments);

        [NotNull]
        public abstract IPolygon Buffer(double distance, int quadrantSegments, int endCapStyle);

        [NotNull]
        public abstract IGeometry Intersection([NotNull] IGeometry other);

        [NotNull]
        public abstract IGeometry Union([NotNull] IGeometry other);

        [NotNull]
        public abstract IGeometry Difference([NotNull] IGeometry other);

        [NotNull]
        public abstract IGeometry SymmetricDifference([NotNull] IGeometry other);

        [NotNull]
        public abstract IGeometry Simplify(double distanceTolerance);

        [NotNull]
        public abstract IGeometry Triangulation(double distanceTolerance, [NotNull] GeometryType resultType);

        #endregion

        #region IRelationalOperator

        public abstract bool Contains([NotNull] IGeometry other);

        public abstract bool Crosses([NotNull] IGeometry other);

        public abstract bool Equals([NotNull] IGeometry other);

        public abstract bool Touches([NotNull] IGeometry other);

        public abstract bool Disjoint([NotNull] IGeometry other);

        public abstract bool Intersects([NotNull] IGeometry other);

        public abstract bool Within([NotNull] IGeometry other);

        public abstract bool Overlaps([NotNull] IGeometry other);

        public abstract bool Covers([NotNull] IGeometry other);

        public abstract bool CoveredBy([NotNull] IGeometry other);

        #endregion

        #region IComparable

        public abstract int CompareTo(IGeometry other);

        #endregion

        #region ICloneable

        public abstract IGeometry Clone();

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

    }
}
