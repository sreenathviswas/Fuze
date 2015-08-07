using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts
{
    public class ActivityResult
    {
        public int WorkflowID { get; set; }
        public Guid WorkflowInstanceID { get; set; }

        public Dictionary<string, object> Results { get; set; }

        public Dictionary<string, string> ValidationResults { get; set; }
    }
}
