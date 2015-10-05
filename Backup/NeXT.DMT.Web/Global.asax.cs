using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace NeXT.DMT.Web
{
    public class Global : System.Web.HttpApplication
    {

        /// <summary>
        /// Handles the Start event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            //getting the configuration for log4net 
            //from the config file
            log4net.Config.XmlConfigurator.Configure();

            log4net.ILog logger = log4net.LogManager.GetLogger("Global");
            logger.Info("Application start");
        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
            string err = String.Concat("Error Caught in Application_Error event\n", objErr);
            log4net.ILog logger = log4net.LogManager.GetLogger("Global");
            logger.Error(err, objErr);
        }

        /// <summary>
        /// Handles the End event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_End(object sender, EventArgs e)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("Global");
            logger.Info("Application end");
        }
    }
}