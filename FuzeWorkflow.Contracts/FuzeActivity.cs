using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow.Contracts
{
    [Serializable]
    public abstract class FuzeActivity
    {
        public string Name { get; set; }
        public Guid WorkflowInstanceID { get; set; }
        public int ActivityID { get; set; }
        public int WorkFlowID { get; set; }
        public string Expression { get; set; }
        public string ActivityType { get; set; }
        public int CurrentActivityID { get; set; }
        public int NextActivityID { get; set; }

        public List<FuzeActivity> WorkFlowActivities { get; set; }
        public List<OutCome> OutComes { get; set; }
        public List<Validation> ValidationErros { get; set; }
        public List<Mapping> InputMappings { get; set; }
        public List<Mapping> OutputMappings { get; set; } 
        public abstract void Execute();

        public FuzeActivity GetOutCome(string outComeType)
        {
            if(string.IsNullOrEmpty(outComeType))
            {
                return OutComes.ElementAt(0).Activity;
            }

            return OutComes.Where(c=>c.Name.ToUpper() == outComeType.ToUpper()).First().Activity;
        }
    }
}
