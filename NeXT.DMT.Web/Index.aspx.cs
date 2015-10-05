using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NeXT.DMT.Web.ReferenceService;
using System.Collections.ObjectModel;
using NeXT.DMT.Entities;

namespace NeXT.DMT.Web
{
    public partial class Index : System.Web.UI.Page
    {

        ReferenceServiceClient client = new ReferenceServiceClient();

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindApplicationList();
            }
        }

        /// <summary>
        /// Fetch all the applications 
        /// and bind it to the dropdownlist
        /// </summary>
        protected void BindApplicationList()
        {
            this.DropDownListApplicationList.DataTextField = "Name";
            this.DropDownListApplicationList.DataValueField = "Quadri";

            //get all the applications and order them by name
            this.DropDownListApplicationList.DataSource = client.GetAllApplications().OrderBy(app => app.Name);
            this.DropDownListApplicationList.DataBind();
        }

        #endregion
    }
}