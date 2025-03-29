using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.Models
{
    public class OpenIdAuthModel
    {
        public string Ns { get; set; }
        public string Mode { get; set; }
        public string OpEndpoint { get; set; }
        public string ClaimedId { get; set; }
        public string Identity { get; set; }
        public string ReturnTo { get; set; }
        public string ResponseNonce { get; set; }
        public string AssocHandle { get; set; }
        public string Signed { get; set; }
        public string Sig { get; set; }

    }
}
