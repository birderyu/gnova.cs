using GNova.Core.Attribute;
using GNova.Geometry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.Index
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGeometryIndex<T>
    {
        int Count { get; }

        bool IsEmpty { get; }

        void Insert([NotNull] BoundingBox bbox, T item);

        bool Delete([NotNull] BoundingBox bbox, T item);

        ICollection<T> Query([NotNull] BoundingBox bbox);

        void Query([NotNull] BoundingBox bbox, [NotNull] Action<T> visitor);

        void Query([NotNull] BoundingBox bbox, [NotNull] Func<T, bool> visitor);
    }
}
