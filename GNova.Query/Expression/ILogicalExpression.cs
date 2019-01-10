using GNova.Core.Attribute;
using GNova.Core.Delegate;

namespace GNova.Query.Expression
{
    /// <summary>
    /// 逻辑表达式
    /// </summary>
    public interface ILogicalExpression : IExpression
    {

        bool IsAlwaysTrue { get; }

        bool IsAlwaysFalse { get; }

        bool IsInvariable { get; }

        bool IsSimple { get; }

        bool IsMulti { get; }

        bool IsNon { get; }

        bool Fit([NotNull]Getter getter);

    }
}
