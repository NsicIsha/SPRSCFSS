using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;

public partial class appAdministrator_MasterMailerView : System.Web.UI.Page
{
    clsMasterMailer objMailer;
    DataSet Myds;
    int successId;

    int UserId, RoleId, DepartmentId;
    string UserFullName;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;

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

            //SETTING SUBMIT BUTTON AS DEFAULT BUTTON
            txtFrom.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtSubject.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            RichTextEditor_1_1.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");


            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    try
                    {
                        if (Request.QueryString["id"] != null)
                        {
                            objMailer = new clsMasterMailer();
                            Myds = new DataSet();

                            //FETCHING CONTENT TO DATASET
                            Myds = MasterMailer_Fetch_DataSet(Convert.ToInt32(Request.QueryString["id"]));// objMailer.MasterMailer_Fetch_DataSet(Convert.ToInt32(Request.QueryString["id"]));

                            txtFrom.Text = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
                            txtSubject.Text = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
                            RichTextEditor_1_1.txtRichTextBox_1 = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();

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
                txtFrom.Visible = false;
                txtSubject.Visible = false;
                RichTextEditor_1_1.Visible = false;
                btnBack.Visible = false;
                btnSubmit.Visible = false;

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


    public DataSet MasterMailer_Fetch_DataSet(int MasterMailerId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterMailer_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MM_Id", SqlDbType.Int).Value = MasterMailerId;

            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterMailer.aspx", false);
    }
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        objMailer = new clsMasterMailer();

        try
        {
            if (Request.QueryString["id"] != null)
            {
                successId = objMailer.MasterMailer_InsertUpdate(Convert.ToInt32(Request.QueryString["id"]), txtFrom.Text, txtSubject.Text, RichTextEditor_1_1.txtRichTextBox_1, UserId);
            }

            //use select case
            switch (successId)
            {
                case 3:// error in operation
                    Session["mailerAction"] = "Server is busy";
                    break;
                case 4://data updated successfully
                    Session["mailerAction"] = "Updated successfully";
                    break;
            }

            Response.Redirect("MasterMailer.aspx", false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
