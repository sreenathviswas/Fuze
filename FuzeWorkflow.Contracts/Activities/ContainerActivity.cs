using FuzeWorkflow.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts.Activities
{
    public class ContainerActivity : FuzeActivity
    {
        //---- needs to be removed and dynamically added using xaml.
        [Input]
        public int Input1 { get; set; }
        
        [Input]
        public int Input2 { get; set; }
        
        [Output]
        public int Result { get; set; }
        [Output]
        public string ViewName { get; set; }
        //---- needs to be removed and dynamically added using xaml.

        public ContainerActivity()
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
