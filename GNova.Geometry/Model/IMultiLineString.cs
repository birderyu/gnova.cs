using GNova.Core.Attribute;
using GNova.Geometry.Model.Pattern;

namespace GNova.Geometry.Model
{
    public interface IMultiLineString : IGeometryCollection<ILineString>, ILineal
    {
        [NotNull]
        new ILineString this[int index] { get; }

        bool IsClosed { get; }

        [NotNull]
        new IMultiLineString Reverse();

        [NotNull]
        new IMultiLineString Normalize();

        [NotNull]
        new IMultiLineString Clone();

    }
}
