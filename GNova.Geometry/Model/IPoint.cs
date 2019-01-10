using GNova.Core.Attribute;
using GNova.Geometry.Model.Pattern;

namespace GNova.Geometry.Model
{
    public interface IPoint : IGeometry, IPuntal
    {

        double X { get; }

        double Y { get; }

        double Z { get; }

        [NotNull]
        new IPoint Reverse();

        [NotNull]
        new IPoint Normalize();

        [NotNull]
        new IPoint Clone();

    }
}
