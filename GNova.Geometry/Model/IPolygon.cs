using GNova.Core.Attribute;
using GNova.Geometry.Model.Pattern;
using System.Collections.Generic;

namespace GNova.Geometry.Model
{
    public interface IPolygon : IGeometry, IPolygonal, ICollection<ILineString>
    {
        [NotNull]
        ILinearRing this[int index] { get; }

        [NotNull]
        ILinearRing ExteriorRing { get; }

        int InteriorRingCount { get; }

        [NotNull]
        ILinearRing GetInteriorRingAt(int index);

        double Area { get; }

        [NotNull]
        new IPolygon Reverse();

        [NotNull]
        new IPolygon Normalize();

        [NotNull]
        new IPolygon Clone();

    }
}
