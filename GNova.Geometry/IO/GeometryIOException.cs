using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Geometry.IO
{
    public class GeometryIOException : IOException
    {

        public GeometryIOException()
            : base()
        {

        }
        
        public GeometryIOException(string message)
            : base(message)
        {

        }

        public GeometryIOException(string message, int hresult)
            : base(message, hresult)
        {

        }

        public GeometryIOException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected GeometryIOException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
