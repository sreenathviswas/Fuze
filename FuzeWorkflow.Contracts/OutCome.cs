using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts
{
    [Serializable]
    public class OutCome
    {
        public string Name { get; set; }
        public FuzeActivity Activity { get; set; }
    }
}
