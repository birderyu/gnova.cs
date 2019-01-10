using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNova.Core.Attribute;
using GNova.Core.Delegate;

namespace GNova.Query.Expression
{
    public abstract class LogicalExpression : ILogicalExpression
    {

        public bool IsValue
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsAlwaysTrue
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsAlwaysFalse
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsInvariable
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSimple
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsMulti
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsNon
        {
            get
            {
                return false;
            }
        }

        public abstract int PlaceholderCount { get; }

        public abstract bool Fit([NotNull] Getter getter);
    }
}
