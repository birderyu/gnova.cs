using GNova.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.Model.Operator
{
    public interface IRelationalOperator
    {

        bool Contains([NotNull] IGeometry other);

        bool Crosses([NotNull] IGeometry other);

        bool Equals([NotNull] IGeometry other);

        bool Touches([NotNull] IGeometry other);

        bool Disjoint([NotNull] IGeometry other);

        bool Intersects([NotNull] IGeometry other);

        bool Within([NotNull] IGeometry other);

        bool Overlaps([NotNull] IGeometry other);

        bool Covers([NotNull] IGeometry other);

        bool CoveredBy([NotNull] IGeometry other);

    }
}
