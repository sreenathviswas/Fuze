using FuzeWorkflow.Contracts;
using FuzeWorkflow.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace FuzeWorkflow
{
    public class WorkflowService : IFuzeWorkFlow
    {
        FuzeWorkflowEntities dbContext = new FuzeWorkflowEntities();
        public Contracts.ActivityResult Invoke(Contracts.ActivityInput input)
        {
            Contracts.ActivityResult activityResult = null;
            dynamic workflow;
            if (input.WorkflowInstanceID == Guid.Empty)
            {
                workflow = dbContext.FuzeWorkflows.Where(c => c.WorkflowID == input.WorkflowID).FirstOrDefault();
            }
            else
            {
                workflow = dbContext.FuzeWorkflowInstances.AsNoTracking().Where(c => c.WorkflowInstanceID == input.WorkflowInstanceID).FirstOrDefault();
            }

            // instansiate and execute the first workflow.
            // create the workflow instance. for that load workflow from store..

            if (workflow != null)
            {
                string xaml = workflow.WorkFlow;
                FuzeActivity container;
                // load the xaml as object.

                container = (FuzeActivity)XamlServices.Parse(xaml);

                // check whether the container has any input properties, then map those propeties to the container from input.
                MapInputParameters(input, container);

                // Execute workflow from start.
                if (container.NextActivityID == 0)
                {
                    activityResult = ProcessWorkFlow(container, container.WorkFlowActivities.First(), input);
                }
                else
                {
                    activityResult = ProcessWorkFlow(container, container.WorkFlowActivities.Where(c => c.ActivityID == container.NextActivityID).First(), input);
                }


            }

            return activityResult;
        }

        public ActivityResult ProcessWorkFlow(FuzeActivity container, FuzeActivity currentActivity, ActivityInput input)
        {
            Contracts.ActivityResult activityResult = null;
            container.CurrentActivityID = currentActivity.ActivityID;

            // map all input to the activity for activity execution.
            SetInputMappingsFromWorkFlow(container, currentActivity);

            // execute the workflow
            currentActivity.Execute();

            // map if the item has any output mappings.
            SetOutputMappingsToWorkFlow(container, currentActivity);


            // check the activity type
            if (currentActivity.ActivityType == "FORM")
            {
                // map all output to the activity result.
                activityResult = new ActivityResult();
                activityResult.Results = new Dictionary<string, object>();
              //  activityResult.Results.Add("FormResult", currentActivity.GetType().GetProperty("ActivityResult").GetValue(currentActivity, null));
                MapOutPutMappingToActivityResult(container, currentActivity, activityResult);
                activityResult.ValidationResults = currentActivity.ValidationErros.ToDictionary(c => c.ValidationKey, x => x.ValidationResult);

                // check for persistance
                PersistActivity(container, input);
                activityResult.WorkflowInstanceID = input.WorkflowInstanceID;
                if (currentActivity.OutComes == null || currentActivity.OutComes.Count() == 0)
                {
                    RemoveActivityInstance(input.WorkflowInstanceID);
                }

                return activityResult;
            }
            else if (currentActivity.ActivityType == "CUSTOM")
            {
                if (currentActivity.OutComes == null || currentActivity.OutComes.Count() == 0)
                {
                    RemoveActivityInstance(input.WorkflowInstanceID);
                    return activityResult;
                }

                container.NextActivityID = currentActivity.OutComes.First().Activity.ActivityID;
                return ProcessWorkFlow(container, currentActivity.OutComes.First().Activity, input);
            }
            return activityResult;
        }
        public Contracts.ActivityResult Resume(Contracts.ActivityInput input)
        {
            if (input.Inputs != null && input.Inputs.Count() > 0)
            {
                var workflow = dbContext.FuzeWorkflowInstances.AsNoTracking().Where(c => c.WorkflowInstanceID == input.WorkflowInstanceID).FirstOrDefault();
                // load the workflow.

                if (workflow != null)
                {
                    string xaml = workflow.WorkFlow;
                    FuzeActivity container;
                    // load the xaml as object.

                    container = (FuzeActivity)XamlServices.Parse(xaml);

                    var currentActivity = container.WorkFlowActivities.Where(c => c.ActivityID == container.CurrentActivityID).First();
                    var nextActivity = currentActivity.GetOutCome(input.Inputs["OUTCOME"].ToString());
                    container.NextActivityID = nextActivity.ActivityID;
                    PersistActivity(container, input);
                    return Invoke(input);
                }

            }

            return null;
        }

        private void MapInputParameters(Contracts.ActivityInput input, FuzeActivity activity)
        {
            if (input.Inputs != null)
            {
                // get all input properties of the workflow.
                var properties = activity.GetType().GetProperties()
                                 .Where(prop => prop.IsDefined(typeof(InputAttribute), false));

                foreach (var property in properties)
                {
                    if (input.Inputs[property.Name] != null)
                    {
                        property.SetValue(activity, Convert.ChangeType(input.Inputs[property.Name], property.PropertyType), null);
                    }
                }

            }
        }

        private void SetInputMappingsFromWorkFlow(FuzeActivity container, FuzeActivity currentActivity)
        {
            if (currentActivity.InputMappings != null)
            {
                foreach (Mapping mapping in currentActivity.InputMappings)
                {
                    PropertyInfo propertyInfo = currentActivity.GetType().GetProperty(mapping.InputArgument);
                    propertyInfo.SetValue(currentActivity, Convert.ChangeType(container.GetType().GetProperty(mapping.MappingArgument).GetValue(container, null), propertyInfo.PropertyType), null);
                }
            }
        }

        private void SetOutputMappingsToWorkFlow(FuzeActivity container, FuzeActivity currentActivity)
        {
            if (currentActivity.OutputMappings != null)
            {
                foreach (Mapping mapping in currentActivity.OutputMappings)
                {
                    PropertyInfo propertyInfo = container.GetType().GetProperty(mapping.MappingArgument);
                    propertyInfo.SetValue(container, Convert.ChangeType(currentActivity.GetType().GetProperty(mapping.InputArgument).GetValue(currentActivity, null), propertyInfo.PropertyType), null);
                }
            }
        }

        private void MapOutPutMappingToActivityResult(FuzeActivity container, FuzeActivity currentActivity, Contracts.ActivityResult activityResult)
        {
            if (currentActivity.OutputMappings != null)
            {
                foreach (Mapping mapping in currentActivity.OutputMappings)
                {

                    PropertyInfo propertyInfo = container.GetType().GetProperty(mapping.MappingArgument);
                    activityResult.Results.Add(mapping.MappingArgument, propertyInfo.GetValue(container));

                }
            }
        }

        private void PersistActivity(FuzeActivity container, ActivityInput input)
        {
            bool isUpdate = false;
            if (input.WorkflowInstanceID == Guid.Empty)
            {
                input.WorkflowInstanceID = Guid.NewGuid();
            }
            else
            {
                isUpdate = true;
            }
            StringBuilder instanceXaml = new StringBuilder();
            using (StringWriter writer = new StringWriter(instanceXaml))
            {
                XamlServices.Save(writer, container);
            }

            var workflowInstance = new FuzeWorkflowInstance()
            {
                WorkflowInstanceID = input.WorkflowInstanceID,
                WorkFlow = instanceXaml.ToString()
            };



            if (isUpdate)
            {
                RemoveActivityInstance(workflowInstance.WorkflowInstanceID);
            }

            dbContext.FuzeWorkflowInstances.Add(workflowInstance);
            dbContext.SaveChanges();
        }

        private void RemoveActivityInstance(Guid WorkflowInstanceID)
        {

            // if we reach the end of workflow, remove the workflow instance from store and return the result.
            if (WorkflowInstanceID != Guid.Empty)
            {
                // remove the workflow instance.
                dbContext.FuzeWorkflowInstances.Remove(dbContext.FuzeWorkflowInstances.First(c => c.WorkflowInstanceID == WorkflowInstanceID));
                dbContext.SaveChanges();
            }
        }
    }
}
