using GNova.Core.Attribute;

namespace GNova.Query.Expression
{
    /// <summary>
    /// 表达式
    /// </summary>
    [Immutable]
    public interface IExpression
    {
        /// <summary>
        /// 获取表达式中占位符的数量
        /// </summary>
        /// <returns></returns>
        int PlaceholderCount { get; }

        /// <summary>
        /// 是否是值表达式
        /// </summary>
        bool IsValue { get; }

    }
}
