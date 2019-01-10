using GNova.Geometry.Model;
using System.IO;

namespace GNova.Geometry.IO
{
    public interface IGeometryReader<in T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="GeometryIOException"></exception>
        IGeometry Read(T obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="GeometryIOException"></exception>
        IPoint ReadPoint(T obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="GeometryIOException"></exception>
        IGeometry Read(Stream stream);

    }
}
