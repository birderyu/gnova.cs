using GNova.Core.Attribute;
using GNova.Geometry.Model.Pattern;

namespace GNova.Geometry.Model
{
    public interface IMultiPoint : IGeometryCollection<IPoint>, IPuntal
    {
        [NotNull]
        new IPoint this[int index] { get; }

        [NotNull]
        new IMultiPoint Reverse();

        [NotNull]
        new IMultiPoint Normalize();

        [NotNull]
        new IMultiPoint Clone();

    }
}
