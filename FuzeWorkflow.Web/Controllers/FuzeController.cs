using FuzeWorkflow;
using FuzeWorkflow.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FuzeFormEngine.Controllers
{
    public class FuzeController : Controller
    {
        WorkflowService service = new WorkflowService();

        // GET: Fuze
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(int? workflowID, Guid? workflowInstanceID)
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            ActivityInput input = new ActivityInput();
            input.WorkflowID = workflowID.Value;
            input.WorkflowInstanceID = workflowInstanceID.Value;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> inputObj = (Dictionary<string, object>)serializer.DeserializeObject(json);
            input.Inputs = inputObj;

            var result = service.Resume(input);

            FuzeFormEngineModel model = new FuzeFormEngineModel();
            model.WorkFlowID = workflowID.Value;
            model.WorkflowInstanceID = workflowInstanceID.Value;
            //model.FormName = result.Results["ViewName"].ToString();
            model.Model = result.Results; //input.Inputs;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFuzeForm(string formName)
        {
            FuzeFormModel model = new FuzeFormModel();
            model.Layout = FileHelper.GetFileContent("CALCULATOR");

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFormData(int? workFlowID)
        {
            ActivityInput input = new ActivityInput();

            input.WorkflowID = 1;

            var result = service.Invoke(input);

            FuzeFormEngineModel model = new FuzeFormEngineModel();
            model.WorkFlowID = result.WorkflowID;
            model.WorkflowInstanceID = result.WorkflowInstanceID;
            model.FormName = result.Results["ViewName"].ToString();
            model.Model = new { Input1 = "", Input2 = "", Result = "" };

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
    public class FuzeFormEngineModel
    {
        public int WorkFlowID { get; set; }
        public Guid WorkflowInstanceID { get; set; }
        public object Model { get; set; }
        public string FormName { get; set; }
    }

    public class FuzeFormModel
    {
        public string Layout { get; set; }
    }

    public static class FileHelper
    {
        public static string GetFileContent(string formName)
        {
            string path = HostingEnvironment.MapPath(string.Format(@"~/Widgets/{0}.html", formName));

            if (File.Exists(path))
            {
                return File.ReadAllText(path);

            }
            return string.Empty;
        }
    }

}