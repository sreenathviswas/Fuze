using FuzeWorkflow.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts.Activities
{
    public class MultiplyActivity : FuzeActivity
    {
         [Input]
        public int Number1 { get; set; }
        
        [Input]
        public int Number2 { get; set; }

        [Output]
        public int Result { get; set; }

        public MultiplyActivity()
        {
            this.OutComes = new List<OutCome>();
            this.InputMappings = new List<Mapping>();
            this.OutputMappings = new List<Mapping>();
            this.ValidationErros = new List<Validation>();
            this.WorkFlowActivities = new List<FuzeActivity>();  
        }
        public override void Execute()
        {
            if(Number1 == 0 || Number2 == 0)
            {
                var validation = new Validation();
                validation.ValidationKey = "Number";
                validation.ValidationResult = "Please enter a number other than zero";
                this.ValidationErros.Add(validation);
                return;
            }

            Result = Number1 * Number2;
        }
    }
}
