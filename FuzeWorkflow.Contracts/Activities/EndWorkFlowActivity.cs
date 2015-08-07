using FuzeWorkflow.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts.Activities
{
    public class EndWorkFlowActivity : FuzeActivity
    {
        public EndWorkFlowActivity()
        {
            this.OutComes = new List<OutCome>();
            this.InputMappings = new List<Mapping>();
            this.OutputMappings = new List<Mapping>();
            this.ValidationErros = new List<Validation>();
            this.WorkFlowActivities = new List<FuzeActivity>();  
        }
        public override void Execute()
        {
            
        }
    }
}
