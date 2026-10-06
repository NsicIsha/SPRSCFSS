using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.Text;

public partial class appAdministrator_MasterServiceTax : System.Web.UI.Page
{
    DataSet MyDs;
    int accessId;
    int count = 1;
    //int Zoneid;
    //int Byid;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

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
        try
        {
            FunRequest();
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            if (RoleId == 1 || RoleId == 2)
            {
                if (Page.IsPostBack != true)
                {
                    Bind_gridview();
                }
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void Bind_gridview()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = Company_fetch_servicetax();
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_servicetax.DataSource = ds.Tables[0];
                grd_servicetax.DataBind();
            }
            else
            {
                ds.Dispose();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    public DataSet Company_fetch_servicetax()
    {
        SqlDataAdapter MyAdp;
        MyAdp = new SqlDataAdapter();
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPSP_fetch_masterServiceTax", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
           

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

    public DataSet servicetax_fetch_edit(int servicetaxid)
    {
        SqlDataAdapter MyAdp;
        MyAdp = new SqlDataAdapter();
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_fetch_servicetext_edit", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_ST_Id", SqlDbType.Int).Value = servicetaxid;


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

    protected void grd_servicetax_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int act;
        try
        {
            if (e.CommandName == "edt")
            {
                lblAddEdit.Text = "Edit Service Tax";

                DataSet ds_edit = new DataSet();

                //TO RECOGNIZE WHAT ACTION TO DO IN SUBMIT BUTTON CLICK
                hdnfield.Value = "edit";
                HiddenField1.Value = e.CommandArgument.ToString();
                ds_edit = servicetax_fetch_edit(Convert.ToInt32(HiddenField1.Value));
                if (ds_edit.Tables[0].Rows.Count > 0)
                {
                    txtservice_text.Text = ds_edit.Tables[0].Rows[0]["Servicetax"].ToString();
                    txt_proposal_date.Text = ds_edit.Tables[0].Rows[0]["ProposalDate"].ToString();
                    txt_Proposal_end_Date.Text = ds_edit.Tables[0].Rows[0]["ProposalEndDate"].ToString();
                    ModalPopupExtender1.Show();
                }
                else
                {
                    ds_edit.Dispose();
                }

                ModalPopupExtender1.Show();
            }

            else if (e.CommandName == "Act")
            {
                 
                HiddenField1.Value = e.CommandArgument.ToString();
                int id = delete_service_tax(Convert.ToInt32(HiddenField1.Value));
                Bind_gridview();

                  
            }
        }
        catch (Exception ex)
        {
        }
    }

    public int Edit_service_tax(int servicetaxid, string Servicetax, string ProposalDate, string proposalEndDate)
    {
        int ErrorId = 0;
        SqlCommand MySqlComm = new SqlCommand();
        try
        {
            MySqlComm = new SqlCommand("Gp_sp_update_service_tax", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_ST_Id", SqlDbType.Int).Value = servicetaxid;
            MySqlComm.Parameters.Add("@fGPtbl_ST_ServiceTax", SqlDbType.VarChar, 50).Value = Servicetax;
            MySqlComm.Parameters.Add("@fGPtbl_ProposalDate", SqlDbType.VarChar, 50).Value = ProposalDate;
            MySqlComm.Parameters.Add("@fGPtbl_ProposalDateEnd", SqlDbType.VarChar, 50).Value = proposalEndDate;
            
                 
            ErrorId = MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
         finally
            {
                DBManager.CloseConnection();
                MySqlComm.Dispose();   
            }
        return ErrorId;
    }
    public int Add_service_tax( string Servicetax, string ProposalDate, string proposalEndDate)
    {
        int ErrorId = 0;
        SqlCommand MySqlComm = new SqlCommand();
        try
        {
            MySqlComm = new SqlCommand("Gp_sp_Add_service_tax", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
          
            MySqlComm.Parameters.Add("@fGPtbl_ST_ServiceTax", SqlDbType.VarChar, 50).Value = Servicetax;
            MySqlComm.Parameters.Add("@fGPtbl_ProposalDate", SqlDbType.VarChar, 50).Value = ProposalDate;
            MySqlComm.Parameters.Add("@fGPtbl_ProposalDateEnd", SqlDbType.VarChar, 50).Value = proposalEndDate;


            ErrorId = MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return ErrorId;
    }

    public int delete_service_tax(int servicetaxid)
    {
        int ErrorId = 0;
        SqlCommand MySqlComm = new SqlCommand();
        try
        {
            MySqlComm = new SqlCommand("Gp_sp_deactive", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_ST_Id", SqlDbType.Int).Value = servicetaxid;
            


            ErrorId = MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return ErrorId;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfield.Value == "edit")
            {

                int id = Edit_service_tax(Convert.ToInt32(HiddenField1.Value), txtservice_text.Text ,txt_proposal_date.Text, txt_Proposal_end_Date.Text);
            }
            else if (hdnfield.Value == "add")
            {
                int id = Add_service_tax(txtservice_text.Text, txt_proposal_date.Text, txt_Proposal_end_Date.Text);
            }
            Bind_gridview();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }
    protected void grd_servicetax_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltSno = (Literal)e.Row.FindControl("ltlSno");
                ltSno.Text = count.ToString();
                count++;
                ImageButton imgAct = (ImageButton)e.Row.FindControl("imgAct");
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
               // imgAct.ImageUrl = "../Images/Admin/Active.png";
            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void btnAddBranch_Click(object sender, EventArgs e)
    {
        lblAddEdit.Text = "Add New Service Tax";
        txt_Proposal_end_Date.Text = "";
        txt_proposal_date.Text = "";
        txtservice_text.Text = "";
        hdnfield.Value = "add";
        ModalPopupExtender1.TargetControlID = "btnAddBranch";
        ModalPopupExtender1.Show();
    }
}
