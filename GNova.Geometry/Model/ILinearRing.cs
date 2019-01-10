using GNova.Core.Attribute;

namespace GNova.Geometry.Model
{
    public interface ILinearRing : ILineString
    {

        [NotNull]
        new ILinearRing Reverse();

        [NotNull]
        new ILinearRing Normalize();

        [NotNull]
        new ILinearRing Clone();

    }
}
