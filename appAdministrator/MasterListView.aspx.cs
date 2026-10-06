using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appAdministrator_MasterListView : System.Web.UI.Page
{
    clsMasterList objList;
    DataSet Myds;
    int successId;

    int count = 1;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int listAction = 0;


    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {

            FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    try
                    {
                        if (Request.QueryString["id"] != null)
                        {
                            //FILLING RICHTEXTBOX TO EDIT
                            objList = new clsMasterList();
                            Myds = new DataSet();
                            //TO SHOW LABEL IN EDIT MODE
                            listAction = Convert.ToInt32(Request.QueryString["id"]);
                            Myds = objList.MasterList_Fetch_DataSet(Convert.ToInt32(Request.QueryString["id"]));
                            lblName.Text = Myds.Tables[0].Rows[0][1].ToString();

                            RichTextEditor_1_1.txtRichTextBox_1 = Myds.Tables[0].Rows[0][2].ToString();

                        }
                    }
                    catch (Exception ex)
                    {
                        ErrHandler.WriteError(ex.Message);
                    }
                }
            }
            else
            {
                RichTextEditor_1_1.Visible = false;
                btnBack.Visible = false;
                btnSubmit.Visible = false;
                txtName.Visible = false;
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }

    }
   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objList = new clsMasterList();
            //TO USE IN EDIT MODE
            if (Request.QueryString["id"] != null)
            {
                int listid = Convert.ToInt32(Request.QueryString["id"]);

                successId = objList.MasterList_InsertUpdate(listid, lblName.Text, RichTextEditor_1_1.txtRichTextBox_1, UserId);
            }
            

            //use select case
            switch (successId)
            {
                case 1://data already exist
                    Session["listAction"] = "Name already exist";
                    break;
                case 2://data inserted successfully
                    Session["listAction"] = "Added successfully";
                    break;
                case 3:// error in operation
                    Session["listAction"] = "Server is busy";
                    break;
                case 4://data updated successfully
                    Session["listAction"] = "Updated successfully";
                    break;
            }

            Response.Redirect("../appAdministrator/MasterList.aspx");

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    //BACK BUTTON
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../appAdministrator/MasterList.aspx");
    }
}
 