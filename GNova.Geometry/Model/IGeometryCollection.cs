using GNova.Core.Attribute;
using GNova.Geometry.Model.Operator;
using System.Collections.Generic;

namespace GNova.Geometry.Model
{
    public interface IGeometryCollection<G> : IGeometry, ICollection<G>, ICollectionOperator
        where G : IGeometry
    {
        [NotNull]
        G this[int index] { get; }

        [NotNull]
        new IGeometryCollection<G> Reverse();

        [NotNull]
        new IGeometryCollection<G> Normalize();

        [NotNull]
        new IGeometryCollection<G> Clone();

    }
}
