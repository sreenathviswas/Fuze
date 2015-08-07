using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts
{
    [Serializable]
    public class Mapping
    {
        public string InputArgument { get; set; }
        public string MappingArgument { get; set; }
    }
}
