using FuzeWorkflow.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzeWorkflow
{
    public interface IFuzeWorkFlow
    {
        ActivityResult Invoke(ActivityInput input);
        ActivityResult Resume(ActivityInput input);
    }
}
