using GNova.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Query.Expression
{
    public interface ISimpleExpression : ILogicalExpression
    {

        [NotNull]
        IValueExpression LeftValue { get; }

        [NotNull]
        CompareOperator CompareOperator { get; }

        [NotNull]
        IValueExpression RightValue { get; }
    }
}
