using GNova.Core.Attribute;
using GNova.Geometry.Model.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.Model
{
    public interface IMultiPolygon : IGeometryCollection<IPolygon>, IPolygonal
    {
        [NotNull]
        new IPolygon this[int index] { get; }

        [NotNull]
        new IMultiPolygon Reverse();

        [NotNull]
        new IMultiPolygon Normalize();

        [NotNull]
        new IMultiPolygon Clone();
    }
}
