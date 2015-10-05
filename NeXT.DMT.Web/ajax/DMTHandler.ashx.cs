using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using NeXT.DMT.Entities;
using NeXT.DMT.Web.ReferenceService;
using System.Collections.ObjectModel;

namespace NeXT.DMT.Web.ajax
{
    /// <summary>
    /// Summary description for DMTHandler
    /// </summary>
    public class DMTHandler : IHttpHandler
    {
        /// <summary>
        /// Log4net manager instance
        /// </summary>
        static readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(typeof(DMTHandler));

        ReferenceServiceClient client = new ReferenceServiceClient();
        MessageBE uiMessage = new MessageBE();

        public void ProcessRequest(HttpContext context)
        {
            //get the JSON string which
            //from the UI
            String jsonString = context.Request.Form[0];

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();



            //it is this function that
            //does all the application magic
            //Beware it can make you disappear also!!
            try
            {   
                //deserialized the string to our object
                _Logger.Info(String.Format("Trying To Deserialize({0})", jsonString));
                Entities.JsonBE jsonObject = jsSerializer.Deserialize<Entities.JsonBE>(jsonString);

                uiMessage.Status = "1";
                uiMessage.Message = "Success";
                client.PerformTheMagic(jsonObject);
            }
            catch (Exception ex)
            {
                uiMessage.Status = "2";
                uiMessage.Message = ex.Message;
            }

            //go to sleep ZzzzZZzzz
            //just for the loader animation
            //useless code !!
            System.Threading.Thread.Sleep(2000);

            //set the response type to be JSON
            context.Response.ContentType = "application/json; charset=utf-8";
            
            //write the output back
            context.Response.Write(jsSerializer.Serialize(uiMessage));
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