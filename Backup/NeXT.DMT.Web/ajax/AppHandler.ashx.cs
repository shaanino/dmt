using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeXT.DMT.Web.ReferenceService;
using System.Web.Script.Serialization;
using NeXT.DMT.Entities;
using System.Collections.ObjectModel;
using NeXT.DMT.Utilities;

namespace NeXT.DMT.Web.ajax
{
    /// <summary>
    /// Summary description for AppHandler
    /// </summary>
    public class AppHandler : IHttpHandler
    {

        ReferenceServiceClient client = new ReferenceServiceClient();

        public void ProcessRequest(HttpContext context)
        {
            //get the JSON string which
            //from the UI
            String jsonString = context.Request.Form[0];

            //get only the deliverable for the application
            Collection<String> appDeliverablesCol = client.GetApplication(jsonString).Deliverables;

            //all deliverables collection
            Collection<String> allDeliverablesCol = new Collection<String>();
            allDeliverablesCol.Add("WEB");
            allDeliverablesCol.Add("WS");
            allDeliverablesCol.Add("K2WS");
            allDeliverablesCol.Add("DB");
            allDeliverablesCol.Add("K2PROCESS");
            allDeliverablesCol.Add("REPORT");
            allDeliverablesCol.Add("BATCH");

            //get all deliverables that the application
            //is not conserned with
            Collection<String> notDeliverablesCol = allDeliverablesCol.Except(appDeliverablesCol).ToCollection();

            //set the response type to be JSON
            context.Response.ContentType = "application/json; charset=utf-8";

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            //write the output back
            context.Response.Write(jsSerializer.Serialize(notDeliverablesCol));
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}