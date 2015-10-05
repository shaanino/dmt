using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using NeXT.DMT.Entities;
using NeXT.DMT.Web.ReferenceService;
using System.Web.Script.Serialization;
using NeXT.DMT.Utilities;
using NeXT.DMT.Services;

namespace NeXT.DMT.Web.ajax
{
    /// <summary>
    /// Summary description for AutoSuggestHandler
    /// </summary>
    public class AutoSuggestHandler : IHttpHandler
    {
        /// <summary>
        /// Log4net manager instance
        /// </summary>
        static readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(typeof(AutoSuggestHandler));

        public void ProcessRequest(HttpContext context)
        {

            //cache the results
            if (HttpContext.Current.Cache["personCol"] == null)
            {
                ReferenceServiceClient client = new ReferenceServiceClient();

                HttpContext.Current.Cache["personColCache"] = client.GetAllPersons();
            }

            //get the JSON string which
            //from the UI and convert it to lower case
            //for filtering
            String personName = context.Request.Form[0].ToLower();

            //casting it to our collection
            Collection<Entities.PersonBE> personCol = (Collection<Entities.PersonBE>)HttpContext.Current.Cache["personColCache"];

            //don't do any filtering if char lenght less than two
            //if (personName.Length > 2)
            //{ 
            personCol = personCol.Where(p => p.name.ToLower().Contains(personName)).ToCollection();
            //}

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            String jsonString = jsonString = jsSerializer.Serialize(personCol);

            //set the response type to be JSON
            context.Response.ContentType = "application/json; charset=utf-8";
            
            context.Response.Write(jsonString);
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}