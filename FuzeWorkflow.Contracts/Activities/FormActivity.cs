using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts.Activities
{
    public class FormActivity:FuzeActivity
    {
        [Output]
        public string ActivityResult { get; set; }
        [Output]
        public int Result { get; set; }
        public FormActivity()
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
