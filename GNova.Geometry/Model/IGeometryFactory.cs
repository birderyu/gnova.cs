using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.Model
{
    public interface IGeometryFactory
    {
        int Srid { get; }
    }
}
