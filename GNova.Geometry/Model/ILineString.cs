using GNova.Core.Attribute;
using GNova.Geometry.Model.Pattern;
using System.Collections.Generic;

namespace GNova.Geometry.Model
{
    public interface ILineString : IGeometry, ILineal, ICollection<IPoint>
    {
        [NotNull]
        IPoint this[int index] { get; }

        [NotNull]
        IPoint StartPoint { get; }

        [NotNull]
        IPoint EndPoint { get; }

        bool IsClosed { get; }

        bool IsRing { get; }

        [NotNull]
        IGeometry Extract(int start, int end);

        [NotNull]
        Coordinate GetCoordinateAt(int n);

        [NotNull]
        new ILineString Reverse();

        [NotNull]
        new ILineString Normalize();

        [NotNull]
        new ILineString Clone();

    }
}
