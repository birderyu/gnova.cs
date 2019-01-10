using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.Model.NTS
{
    class GeometryFactoryAdaptor : IGeometryFactory
    {
        private readonly GeoAPI.Geometries.IGeometryFactory _ntsGeometryFactory;

        public GeometryFactoryAdaptor()
        {
            _ntsGeometryFactory = new NetTopologySuite.Geometries.GeometryFactory();
        }

        public GeometryFactoryAdaptor(int srid)
        {
            _ntsGeometryFactory = new NetTopologySuite.Geometries.GeometryFactory(
                new NetTopologySuite.Geometries.PrecisionModel(), srid);
        }

        public GeometryFactoryAdaptor(GeoAPI.Geometries.IGeometryFactory ntsGeometryFactory)
        {
            _ntsGeometryFactory = ntsGeometryFactory;
        }

        public int Srid
        {
            get
            {
                return _ntsGeometryFactory.SRID;
            }
        }

        public IGeometry FromNtsGeometry(GeoAPI.Geometries.IGeometry ntsGeometry)
        {
            // TODO
            return null;
        }

        public IPoint FromNtsPoint(GeoAPI.Geometries.IPoint ntsPoint)
        {
            // TODO
            return null;
        }
    }
}
