using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.Model.NTS
{
    static class GeometryFactoryAdaptorEx
    {

        public static GeoAPI.Geometries.IGeometry ToNtsGeometry(this GeoAPI.Geometries.IGeometryFactory ntsGeometryFactory, IGeometry geometry)
        {
            if (geometry is AbstractGeometryAdaptor)
            {
                return (geometry as AbstractGeometryAdaptor).Nts;
            }
            switch (geometry.Type)
            {
                case GeometryType.Point:
                    break;
                case GeometryType.LineString:
                    break;
                case GeometryType.LinearRing:
                    break;
                case GeometryType.Polygon:
                    break;
                case GeometryType.MultiPoint:
                    break;
                case GeometryType.MultiLineString:
                    break;
                case GeometryType.MultiPolygon:
                    break;
                case GeometryType.GeometryCollection:
                    break;
            }
            return null;
        }

    }
}
