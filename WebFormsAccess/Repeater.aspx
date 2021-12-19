<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Repeater.aspx.cs" Inherits="WebFormsAccess.Repeater" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
</head>
<body>
    <form id="frmViewUsers" runat="server">
        <asp:SqlDataSource ID="dsUsers"
            runat="server"
            ConnectionString='<%$ ConnectionStrings:CompanyDB %>'
            SelectCommand="SELECT * FROM [Users]"
            DeleteCommand="DELETE FROM [Users] WHERE [UserID] = @UserID"
            InsertCommand="INSERT INTO Users(FirstName, LastName, Password) VALUES (@FirstName, @LastName, @Password)"
            UpdateCommand="UPDATE [Users] SET [FirstName] = @FirstName, [LastName] = @LastName, [Password] = @Password WHERE [UserID] = @UserID"
            >
             <DeleteParameters>
                <asp:Parameter Name="UserID" Type="Int32" />
            </DeleteParameters>
             <InsertParameters>
                <asp:Parameter Name="FirstName" Type="String" />
                <asp:Parameter Name="LastName" Type="String" />
                <asp:Parameter Name="Password" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="FirstName" Type="String"></asp:Parameter>
                <asp:Parameter Name="LastName" Type="String"></asp:Parameter>
                <asp:Parameter Name="Password" Type="String"></asp:Parameter>
                <asp:Parameter Name="UserID" Type="Int32"></asp:Parameter>
            </UpdateParameters>
        </asp:SqlDataSource>
        <div>
            <asp:Repeater ID="repeaterUsers" runat="server" DataSourceID="dsUsers" OnItemCommand="repeaterUsers_ItemCommand">
            <HeaderTemplate>
                <table>
                    <thead>
                        <tr>
                            <th>User ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Password</th>
                            <th>Post</th>
                            <th>Actions</th>
                        </tr>
                    </thead>   
                    <tbody>   
            </HeaderTemplate>  
            <ItemTemplate>
                <tr>
                    <td><%# Eval("UserID") %></td>
                    <td><%# Eval("FirstName") %></td>
                    <td><%# Eval("LastName") %></td>
                    <td><%# Eval("Password") %></td>
                    <td><%# Eval("Post") %></td>
                    <td>
                        <asp:ImageButton ID="imgBtnDelete"
                            runat="server"
                            CommandName="deleteUser"
                            CommandArgument=<%# Eval("UserID") %>
                            ImageUrl="~/img/delete.png"
                            />
                        <asp:ImageButton ID="imgBtnEdit"
                            runat="server" 
                            CommandName="editUser"
                            CommandArgument=<%# Eval("UserID") %>
                            ImageUrl="~/img/edit.png"
                            />
                    </td>  
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>

         </asp:Repeater>
        </div>
        <div>
            <%--<asp:HiddenField ID="hiddenEditUserID" runat="server" />--%>
            <table>
            <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Password</th>
                </tr>
                 </thead> 
                <tr>
                    <td><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr> 
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnAddUser" runat="server" OnClick="btnAddUser_Click"
                        Text="Add User"/>
                    <asp:Button ID="btnEditUser" runat="server" OnClick="btnEditUser_Click"
                        Text="Edit User" Visible="false"/>
                </td>
            </tr>
        </table>
        </div>
    </form>
    <%--<script>
        var passwordField = document.querySelector("#txtPassword");
        passwordField.value 
    </script>--%>
</body>
</html>
