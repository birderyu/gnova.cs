using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Query.Expression
{
    public enum ValueType
    {
        Null,
        Boolean,
        Int32,
        Int64,
        Double,
        String,
        Geometry,
        Placeholder,
        List,
        Key
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IValueExpression : IExpression
    {

        ValueType ValueType { get; }

        object Value { get; }

        bool IsNull { get; }

        bool IsBool { get; }

        bool IsNumber { get; }

        bool IsInt32 { get; }

        bool IsInt64 { get; }

        bool IsDouble { get; }

        bool IsString { get; }

        bool IsGeometry { get; }

        bool IsPlaceholder { get; }

        bool IsList { get; }

        bool IsKey { get; }

        string AsKey();

        string CheckBy(CompareOperator co);

        bool ComparedBy(object left, CompareOperator co);

    }

    
}
