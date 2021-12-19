using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebFormsAccess.Models;

namespace WebFormsAccess
{
    public partial class ManageDbData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            UserViewModel newUser = new UserViewModel()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Password = txtPassword.Text,
            };

            dataSourceUsers.InsertParameters["FirstName"].DefaultValue = newUser.FirstName;
            dataSourceUsers.InsertParameters["LastName"].DefaultValue = newUser.LastName;
            dataSourceUsers.InsertParameters["Password"].DefaultValue = newUser.Password;

            try
            {
                dataSourceUsers.Insert();
                txtFirstName.Text = "";
                txtLastName.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorText.Text = $"ErrorText: {ex.Message}. {ex.StackTrace}";
            }
            
        }
    }
}