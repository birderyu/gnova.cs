using GNova.Core.Attribute;
using GNova.Geometry.Model.Operator;
using System;
using System.Collections.Generic;

namespace GNova.Geometry.Model
{
    [Immutable]
    public interface IGeometry : ITopologicalOperator, IRelationalOperator, IComparable<IGeometry>, ICloneable
    {

        [NotNull]
        GeometryType Type { get; }

        [NotNull]
        BoundingBox BoundingBox { get; }

        [NotNull]
        IGeometryFactory Factory { get; }

        int Dimension { get; }

        int BoundaryDimension { get; }

        int CoordinateDimension { get; }

        bool IsEmpty { get; }

        bool IsSimple { get; }

        bool IsValid { get; }

        [NotNull]
        Coordinate Coordinate { get; }

        [NotNull]
        IEnumerable<Coordinate> Coordinates { get; }

        int Srid { get; }

        [NotNull]
        string ToGeometryJSON();

        [NotNull]
        IGeometry Reverse();

        [NotNull]
        IGeometry Normalize();

        [NotNull]
        new IGeometry Clone();
    }
}
