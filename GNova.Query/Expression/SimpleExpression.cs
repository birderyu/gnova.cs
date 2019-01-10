using GNova.Core.Attribute;
using GNova.Core.Delegate;

namespace GNova.Query.Expression
{
    public class SimpleExpression : LogicalExpression, ISimpleExpression
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="co"></param>
        /// <param name="rv"></param>
        public SimpleExpression([NotNull]IValueExpression lv, [NotNull]CompareOperator co, [NotNull]IValueExpression rv)
        {
            LeftValue = lv;
            CompareOperator = co;
            RightValue = rv;
        }

        [NotNull]
        public IValueExpression LeftValue { get; }

        [NotNull]
        public CompareOperator CompareOperator { get; }

        [NotNull]
        public IValueExpression RightValue { get; }

        public override bool IsSimple
        {
            get
            {
                return true;
            }
        }

        public override int PlaceholderCount
        {
            get
            {
                return RightValue.PlaceholderCount;
            }
        }

        public override bool Fit([NotNull] Getter getter)
        {
            if (LeftValue.IsKey)
            {
                object left = getter(LeftValue.AsKey());
                return RightValue.ComparedBy(left, CompareOperator);
            }
            else if (IsAlwaysTrue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
