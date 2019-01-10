using GNova.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.Model.NTS
{
    abstract class AbstractGeometryAdaptor : AbstractGeometry
    {

        [NotNull]
        private readonly GeoAPI.Geometries.IGeometry _ntsGeometry;

        protected AbstractGeometryAdaptor([NotNull] GeoAPI.Geometries.IGeometry ntsGeometry)
            : base(new GeometryFactoryAdaptor(ntsGeometry.Factory))
        {
            _ntsGeometry = ntsGeometry;
        }

        public GeoAPI.Geometries.IGeometry Nts
        {
            get
            {
                return _ntsGeometry;
            }
        }

        [NotNull]
        public new GeometryFactoryAdaptor Factory
        {
            get
            {
                return base.Factory as GeometryFactoryAdaptor;
            }
        }

        [NotNull]
        public override BoundingBox BoundingBox
        {
            get
            {
                GeoAPI.Geometries.Envelope ntsEnvelope = _ntsGeometry.EnvelopeInternal;
                return new BoundingBox(ntsEnvelope.MinX, ntsEnvelope.MaxX, ntsEnvelope.MinY, ntsEnvelope.MaxY);
            }
        }

        public override int BoundaryDimension
        {
            get
            {
                return (int)_ntsGeometry.BoundaryDimension;
            }
        }

        public override int CoordinateDimension
        {
            get
            {
                // NTS是一个二维几何包，因此其坐标不包含Z值
                return 2;
            }
        }

        public override bool IsEmpty
        {
            get
            {
                return _ntsGeometry.IsEmpty;
            }
        }

        public override bool IsSimple
        {
            get
            {
                return _ntsGeometry.IsSimple;
            }
        }

        public override bool IsValid
        {
            get
            {
                return _ntsGeometry.IsValid;
            }
        }

        public override Coordinate Coordinate
        {
            get
            {
                GeoAPI.Geometries.Coordinate ntsCoordinate = _ntsGeometry.Coordinate;
                return new Coordinate(ntsCoordinate.X, ntsCoordinate.Y, ntsCoordinate.Z);
            }
        }

        [NotNull]
        public override IEnumerable<Coordinate> Coordinates
        {
            get
            {
                GeoAPI.Geometries.Coordinate[] ntsCoordinates = _ntsGeometry.Coordinates;
                foreach (GeoAPI.Geometries.Coordinate ntsCoordinate in ntsCoordinates)
                {
                    yield return new Coordinate(ntsCoordinate.X, ntsCoordinate.Y, ntsCoordinate.Z);
                }
            }
        }

        public override int Srid
        {
            get
            {
                return _ntsGeometry.SRID;
            }
        }

        [NotNull]
        public override IGeometry Reverse()
        {
            return Factory.FromNtsGeometry(_ntsGeometry.Reverse());
        }

        [NotNull]
        public override IGeometry Normalize()
        {
            GeoAPI.Geometries.IGeometry newNtsGeometry = _ntsGeometry.Clone() as GeoAPI.Geometries.IGeometry;
            newNtsGeometry.Normalize();
            return Factory.FromNtsGeometry(newNtsGeometry);
        }

        #region ITopologicalOperator

        [NotNull]
        public override IGeometry Boundary
        {
            get
            {
                return Factory.FromNtsGeometry(_ntsGeometry.Boundary);
            }
        }

        [NotNull]
        public override IPoint Centroid
        {
            get
            {
                return Factory.FromNtsPoint(_ntsGeometry.Centroid);
            }
        }

        [NotNull]
        public override IPoint Interior
        {
            get
            {
                return Factory.FromNtsPoint(_ntsGeometry.InteriorPoint);
            }
        }

        [NotNull]
        public override IGeometry ConvexHull()
        {
            return Factory.FromNtsGeometry(_ntsGeometry.ConvexHull());
        }

        [NotNull]
        public override IGeometry Clip([NotNull] BoundingBox bbox)
        {
            return Factory.FromNtsGeometry(_ntsGeometry.ConvexHull());
        }

        [NotNull]
        public override IGeometry Split([NotNull] IGeometry bladeIn)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IGeometry Cut([NotNull] IGeometry bladeIn)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IPolygon Buffer(double distance)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IPolygon Buffer(double distance, int quadrantSegments)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IPolygon Buffer(double distance, int quadrantSegments, int endCapStyle)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IGeometry Intersection([NotNull] IGeometry other)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IGeometry Union([NotNull] IGeometry other)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IGeometry Difference([NotNull] IGeometry other)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IGeometry SymmetricDifference([NotNull] IGeometry other)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IGeometry Simplify(double distanceTolerance)
        {
            // TODO
            return null;
        }

        [NotNull]
        public override IGeometry Triangulation(double distanceTolerance, [NotNull] GeometryType resultType)
        {
            // TODO
            return null;
        }

        #endregion

        [NotNull]
        public override IGeometry Clone()
        {
            return Factory.FromNtsGeometry(_ntsGeometry.Clone() as GeoAPI.Geometries.IGeometry);
        }
    }
}