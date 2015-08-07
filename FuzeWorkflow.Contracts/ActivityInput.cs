using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts
{
    public class ActivityInput
    {
        public int WorkflowID { get; set; }
        public Guid WorkflowInstanceID { get; set; }

        public int ActivityID { get; set; }

        public Dictionary<string, object> Inputs { get; set; }
    }
}
