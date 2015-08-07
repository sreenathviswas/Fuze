using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts
{
    [Serializable]
    public class Validation
    {
        public string ValidationKey { get; set; }
        public string ValidationResult { get; set; }
    }
}
