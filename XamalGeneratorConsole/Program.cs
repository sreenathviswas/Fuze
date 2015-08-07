using FuzeWorkflow.Contracts;
using FuzeWorkflow.Contracts.Activities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace XamalGeneratorConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            //EndWorkFlowActivity endActivity = new EndWorkFlowActivity();
            //endActivity.Name = "EndWorkflowActivity";
            //endActivity.WorkFlowID = 1;
            //endActivity.ActivityID = 4;
            //endActivity.ActivityType = "END";

            FormActivity formActivity2 = new FormActivity();
            formActivity2.Name = "FormActivity";
            formActivity2.WorkFlowID = 1;
            formActivity2.ActivityID = 5;
            formActivity2.ActivityResult = "ActivityPage";
            formActivity2.ActivityType = "FORM";
            formActivity2.InputMappings.Add(new Mapping() { InputArgument = "ActivityResult", MappingArgument = "Result" });
            formActivity2.OutputMappings.Add(new Mapping() { InputArgument = "ActivityResult", MappingArgument = "Result" });
           // formActivity2.OutComes.Add(new OutCome() {Activity= endActivity, Name="END"});

            AddActivity addActivity = new AddActivity();
            addActivity.Name = "AddActivity";
            addActivity.WorkFlowID = 1;
            addActivity.ActivityID = 2;
            addActivity.ActivityType = "CUSTOM";
            addActivity.InputMappings.Add(new Mapping(){InputArgument="Number1",MappingArgument= "Input1"});
            addActivity.InputMappings.Add(new Mapping() { InputArgument = "Number2", MappingArgument = "Input2" });
            addActivity.OutputMappings.Add(new Mapping() { InputArgument = "Result", MappingArgument = "Result" });
            addActivity.OutComes.Add(new OutCome() {Activity= formActivity2, Name="RES"});

            MultiplyActivity muliplyActivity = new MultiplyActivity();
            muliplyActivity.Name = "MultiplyActivity";
            muliplyActivity.WorkFlowID = 1;
            muliplyActivity.ActivityID = 3;
            muliplyActivity.ActivityType = "CUSTOM";
            muliplyActivity.InputMappings.Add(new Mapping() { InputArgument = "Number1", MappingArgument = "Input1" });
            muliplyActivity.InputMappings.Add(new Mapping() { InputArgument = "Number2", MappingArgument = "Input2" });
            muliplyActivity.OutputMappings.Add(new Mapping() { InputArgument = "Result", MappingArgument = "Result" });
            muliplyActivity.OutComes.Add(new OutCome() {Activity= formActivity2, Name="RES"});

            
            FormActivity formActivity = new FormActivity();
            formActivity.Name = "Form2Activity";
            formActivity.WorkFlowID = 1;
            formActivity.ActivityID = 1;
            formActivity.ActivityType = "FORM";
            formActivity.ActivityResult = "ActivityPage";
            formActivity.OutComes.Add(new OutCome() {Activity= addActivity, Name="ADD"});
            formActivity.OutComes.Add(new OutCome() { Activity = muliplyActivity, Name = "MUL" });
           // formActivity.OutComes.Add(new OutCome() { Activity = endActivity, Name = "CANCEL" });

            // Create the container.
            ContainerActivity container = new ContainerActivity();
            container.Name = "ContainerActivity";
            container.WorkFlowID = 1;
            container.ActivityID = 0;
            container.ActivityType = "CONTAINER";
            container.WorkFlowActivities.Add(formActivity);
            container.WorkFlowActivities.Add(addActivity);
            container.WorkFlowActivities.Add(muliplyActivity);
           // container.WorkFlowActivities.Add(endActivity);
            container.WorkFlowActivities.Add(formActivity2);

            string bookFileName = "book.xaml";
            using (TextWriter writer = File.CreateText(bookFileName))
            {
                XamlServices.Save(writer, container);
            }
        }
    }
}
