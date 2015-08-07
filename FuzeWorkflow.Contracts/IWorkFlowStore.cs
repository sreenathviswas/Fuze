using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts
{
    public interface IWorkFlowStore
    {
        string GetWorkFlow(int workflowID);
        string GetActivity(int activityID);
        string GetWorkFlowInstance(Guid workflowInstanceID);
        void PersistWorkFlowInstance(Guid workflowInstanceID, string workflow);
    }
}
