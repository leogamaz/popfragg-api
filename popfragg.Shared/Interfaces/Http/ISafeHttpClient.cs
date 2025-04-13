using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Interfaces.Http
{
    public interface ISafeHttpClient
    {
        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string errorMessage);
    }
}
