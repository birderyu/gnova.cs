using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Query.Expression
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMultiExpression : ILogicalExpression
    {

        LogicalExpression LeftExpression { get; }

        LogicalOperator LogicalOperator { get; }

        LogicalExpression RightExpression { get; }

    }
}
