using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FuzeFormEngine.Controllers
{
    public class FuzeController : Controller
    {
        // GET: Fuze
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(int? workflowID)
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();

            Patient patient = null;
            try
            {
                patient = JsonConvert.DeserializeObject<Patient>(json);
                patient.Age += 1;
            }

            catch (Exception ex)
            {

            }

            FuzeFormEngineModel model = new FuzeFormEngineModel();
            model.WorkFlowID = 1;
            model.ActivityID = 1;
            model.Model = patient;

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetFuzeForm(int? workFlowID, int? activityID)
        {
            FuzeFormEngineModel model = new FuzeFormEngineModel();
            model.WorkFlowID = 1;
            model.ActivityID = 1;
            model.FormContent = @"<fz-textbox name='firstName' bindvalue='{{Model.FirstName}}' placeholder='FirstName'></fz-textbox><br />" +
                "<fz-textbox name='age' bindvalue='{{Model.Age}}' placeholder='Age'></fz-textbox><span>{{Age}}</span>";

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFormData(int? workFlowID, int? activityID)
        {
            FuzeFormEngineModel model = new FuzeFormEngineModel();
            model.WorkFlowID = 1;
            model.ActivityID = 1;
            model.Model = new Patient { FirstName = "John", LastName = "Doe", Age = 25, Gender = "MALE" };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData()
        {
            var inputModel = new Dictionary<string, object>();

            FormPayLoad payLoad = new FormPayLoad();
            payLoad.InputModel.Add("FirstName", "John M Doe");

            //return Json(payLoad, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                Id = 12832,
                Name = "Shoe v2",
                Availability = ViewUtilities.RenderViewToString("FuzeForm", new Availability() { Size = "XL", Stock = 92 }, ControllerContext)
            }, JsonRequestBehavior.AllowGet);
        }
    }

    public class FormPayLoad
    {
        public FormPayLoad()
        {
            InputModel = new Dictionary<string, object>();
            OutputModel = new Dictionary<string, object>();
        }

        public Dictionary<string, object> InputModel { get; set; }

        public Dictionary<string, object> OutputModel { get; set; }
    }

    public static class ViewUtilities
    {
        public static string RenderViewToString(string viewName, object model, ControllerContext controllerContext, ViewDataDictionary viewData = null, TempDataDictionary tempData = null)
        {
            if (viewData == null)
            {
                viewData = new ViewDataDictionary();
            }
            if (tempData == null)
            {
                tempData = new TempDataDictionary();
            }

            // assing model to the viewdata
            viewData.Model = model;

            using (var sw = new StringWriter())
            {
                // try to find the specified view
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                // create the associated context
                ViewContext viewContext = new ViewContext(controllerContext, viewResult.View, viewData, tempData, sw);
                // write the render view with the given context to the stringwriter
                viewResult.View.Render(viewContext, sw);

                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }

    public class Availability
    {
        public string Size { get; set; }
        public int Stock { get; set; }
    }

    public class FuzeFormEngineModel
    {
        public int WorkFlowID { get; set; }
        public int ActivityID { get; set; }
        public string FormContent { get; set; }
        public object Model { get; set; }

    }

    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

    }

}