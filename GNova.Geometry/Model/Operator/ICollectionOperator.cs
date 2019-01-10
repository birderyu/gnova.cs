using GNova.Core.Attribute;

namespace GNova.Geometry.Model.Operator
{
    public interface ICollectionOperator
    {

        [NotNull]
        IGeometry Union();

        IMultiLineString LineMerge();

        IMultiPolygon Polygonize();

    }
}
