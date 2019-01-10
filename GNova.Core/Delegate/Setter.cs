using GNova.Core.Attribute;

namespace GNova.Core.Delegate
{
    /// <summary>
    /// 值设置器
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public delegate void Setter([NotNull]string key, object value);
}
