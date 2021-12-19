using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsAccess.Models;

namespace WebFormsAccess
{
    public partial class Repeater : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void repeaterUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int userID = 0;

            int.TryParse(e.CommandArgument.ToString(), out userID);

            if(userID > 0)
            {
                switch(e.CommandName)
                {
                    case "deleteUser":
                        deleteUser(userID);
                        break;
                    case "editUser":
                    {
                        UserViewModel userToEdit = getUserByID(userID);
                        editUser(userToEdit);

                        //hiddenEditUserID.Value = userID.ToString();
                        SetEditUserID(userID);
                        restoreDataSourceSelectCommand();
                           
                        break;
                    }
                }
            }
        }

        private void SetEditUserID(int userID)
        {
            Session["EditUserID"] = userID;
        }

        private int GetEditUserID()
        {
            int editUserID = 0;

            int.TryParse(Session["EditUserID"].ToString(), out editUserID);
            
            return editUserID;
        }

        private void restoreDataSourceSelectCommand()
        {
            dsUsers.SelectCommand = $"SELECT * FROM [Users]";
            dsUsers.SelectParameters.Clear();
        }

        private UserViewModel getUserByID(int userID)
        {
            UserViewModel userToEdit = null;
            dsUsers.SelectCommand = $"SELECT FirstName, LastName, Password FROM [Users] WHERE [UserID] = @UserID";
            dsUsers.SelectParameters.Add(
                        new Parameter(
                            "UserID",
                        System.Data.DbType.Int32,
                    userID.ToString()
                )
            );
            //dsUsers.SelectParameters["UserID"].DefaultValue = userID.ToString();
            try
            {
                var userDataRows =  dsUsers.Select(DataSourceSelectArguments.Empty);
                
                if(userDataRows != null)
                {
                    foreach (var dataRow in userDataRows)
                    {
                        object[] curUserData = (dataRow as DataRowView).Row.ItemArray;

                        userToEdit = parseUserFromDbRow(curUserData);
                    }
                }
            }
            catch 
            { }
            return userToEdit;
        }

        private UserViewModel parseUserFromDbRow(object[] curUserData)
        {
            UserViewModel userFromDb = new UserViewModel();

            userFromDb.FirstName = curUserData[0].ToString();
            userFromDb.LastName = curUserData[1].ToString();
            userFromDb.Password = curUserData[2].ToString();

            return userFromDb;
        }

        private void editUser(UserViewModel userToEdit)
        {
            enableEditMode();

            txtFirstName.Text = userToEdit.FirstName;
            txtLastName.Text = userToEdit.LastName;
            txtPassword.Text = userToEdit.Password;

        }

        private void deleteUser(int userID)
        {
            dsUsers.DeleteParameters["UserID"].DefaultValue = userID.ToString();

            try
            {
                dsUsers.Delete();
            }
            catch
            {
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        { 
            UserViewModel newUser = new UserViewModel()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Password = txtPassword.Text,
            };

            dsUsers.InsertParameters["FirstName"].DefaultValue = newUser.FirstName;
            dsUsers.InsertParameters["LastName"].DefaultValue = newUser.LastName;
            dsUsers.InsertParameters["Password"].DefaultValue = newUser.Password;

            try
            {
                dsUsers.Insert();
                
                cleanAddEditForm();
            }
            catch
            {
            }

        }

        private void enableAddMode()
        {
            btnEditUser.Visible = false;
            btnAddUser.Visible = true;
        }

        private void enableEditMode()
        {
            btnEditUser.Visible = true;
            btnAddUser.Visible = false;
        }

        private void cleanAddEditForm()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
        }

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            UserViewModel userToSave = new UserViewModel()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Password = txtPassword.Text,
            };

            int editUserID = GetEditUserID();

            if(editUserID > 0)
            {
                dsUsers.UpdateParameters["FirstName"].DefaultValue = userToSave.FirstName;
                dsUsers.InsertParameters["LastName"].DefaultValue = userToSave.LastName;
                dsUsers.UpdateParameters["Password"].DefaultValue = "123";
                //dsUsers.InsertParameters["Password"].DefaultValue = userToSave.Password;
                dsUsers.InsertParameters["UserID"].DefaultValue = editUserID.ToString();
            }

            try
            {
                dsUsers.Update();

                enableAddMode();
            }
            catch
            {
            }
        }
    }
}