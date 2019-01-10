using GNova.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.Model.Operator
{
    public interface ITopologicalOperator
    {
        [NotNull]
        IGeometry Boundary { get; }

        [NotNull]
        IPoint Centroid { get; }

        [NotNull]
        IPoint Interior { get; }

        [NotNull]
        IGeometry ConvexHull();

        [NotNull]
        IGeometry Clip([NotNull] BoundingBox bbox);

        [NotNull]
        IGeometry Split([NotNull] IGeometry bladeIn);

        [NotNull]
        IGeometry Cut([NotNull] IGeometry bladeIn);

        [NotNull]
        IPolygon Buffer(double distance);

        [NotNull]
        IPolygon Buffer(double distance, int quadrantSegments);

        [NotNull]
        IPolygon Buffer(double distance, int quadrantSegments, int endCapStyle);

        [NotNull]
        IGeometry Intersection([NotNull] IGeometry other);

        [NotNull]
        IGeometry Union([NotNull] IGeometry other);

        [NotNull]
        IGeometry Difference([NotNull] IGeometry other);

        [NotNull]
        IGeometry SymmetricDifference([NotNull] IGeometry other);

        [NotNull]
        IGeometry Simplify(double distanceTolerance);

        [NotNull]
        IGeometry Triangulation(double distanceTolerance, [NotNull] GeometryType resultType);
    }
}
