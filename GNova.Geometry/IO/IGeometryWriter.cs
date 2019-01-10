using GNova.Geometry.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.IO
{
    public interface IGeometryWriter<out T> 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        /// <exception cref="GeometryIOException"></exception>
        T Write(IGeometry geometry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="stream"></param>
        /// <exception cref="GeometryIOException"></exception>
        void Write(IGeometry geometry, Stream stream);
    }
}
