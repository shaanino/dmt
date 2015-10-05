using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using NeXT.DMT.Entities;
using NeXT.DMT.Entities.Json;
using System.Collections.ObjectModel;

namespace NeXT.DMT.Web.ajax
{
    /// <summary>
    /// Summary description for AjaxHandler
    /// </summary>
    public class AjaxHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            JsonBE jsonObject = new JsonBE();
            FirstStep firstStep = new FirstStep();

            firstStep.ApplicationQuadri = "IPRI";
            firstStep.ITFNumber = "ITF123";
            firstStep.DeltaOrFull = "0";

            Collection<String> colString = new Collection<String>();

            colString.Add("0");
            colString.Add("1");
            colString.Add("2");



            firstStep.Deliverables = colString;
            firstStep.Environments = colString;



            jsonObject.Step1 = firstStep;

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            var ser = jsSerializer.Serialize(jsonObject);

            var test = context.Request.Form[0];

            var test2 = jsSerializer.Deserialize<JsonBE>(ser);

            var test3 = jsSerializer.Deserialize<JsonBE>(test);


            context.Response.ContentType = "text/plain";
            context.Response.Write(ser);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}