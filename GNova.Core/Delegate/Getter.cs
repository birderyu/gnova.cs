using GNova.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Core.Delegate
{
    /// <summary>
    /// 值获取器
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public delegate object Getter([NotNull]string key);
}
