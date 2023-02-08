using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweaj.Patterns.Requests
{
    public interface IWebRequest
    {
        Guid RequestId { get; }

    }

    public class WebRequest : IWebRequest
    {
        private WebRequest() 
        {
            RequestId = Guid.NewGuid();
        }

        public Guid RequestId { get; }

        public static WebRequest Create()
        {
            return new WebRequest();
        }
    }
}
