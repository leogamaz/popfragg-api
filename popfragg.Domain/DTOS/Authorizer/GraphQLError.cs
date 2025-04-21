using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.DTOS.Authorizer
{
    public class GraphQLError
    {
        public string Message { get; set; } = default!;
        public List<string>? Path { get; set; }
    }
}
