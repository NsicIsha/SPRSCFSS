using AjaxControlToolkit;
using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 




public partial class appAdministrator_AnnualTargetUpdate : System.Web.UI.Page
{
    clsMasterBranch objBranch;
    clsMasterZone objZone;
    clsTrans_UserBranch objTUB;
    DataSet Myds;
    int accessId;
    int count = 1;
    //int Zoneid;
    //int Byid;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int branchAction = 0;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;
    DataTable dt;
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
        {
            string eventTarget = Request["__EVENTTARGET"];
            string eventArgument = Request["__EVENTARGUMENT"];

            if (eventTarget == "ConfirmResult")
            {
                if (eventArgument == "OK")
                {

                    hdnBranchIdEdit.Value = ddlBranch.SelectedValue;
                    hdnFYIdEdit.Value = ddlFY.SelectedValue;


                    MyDs = new DataSet();
                    Myds = Fetch_AnnualBudget_BranchWise(Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlFY.SelectedValue));
                    if (Myds.Tables[0].Rows.Count > 0)
                    {
                        lblZoneDisplay.Text = Myds.Tables[0].Rows[0]["fGPtbl_MZ_Name"].ToString();
                        lblBranchDisplay.Text = Myds.Tables[0].Rows[0]["fGPtbl_MB_Name"].ToString();
                        AnnualBudgetDisplay.Text = Myds.Tables[0].Rows[0]["AnnualTarget"].ToString();
                        lblFYDisplay.Text = Myds.Tables[0].Rows[0]["FinancialYear"].ToString();
                    }

                    ddlZone.SelectedIndex = -1;
                    ddlBranch.SelectedIndex = -1;
                    ddlFY.SelectedIndex = -1;

                    // SHOWING MODALPOPUPEXTENDER
                    ModalPopupExtender1.Show();
                }
                else if (eventArgument == "Cancel")
                {
                    Response.Redirect("AnnualTargetUpdate.aspx");
                }
            }
        }


        if (Page.IsPostBack != true)
        {
            DropDownZoneBind(ddlZone);
            DataSet dtBranches = GetData();
            grd_report.DataSource = dtBranches;
            grd_report.DataBind();

        }
    }
    public DataSet GetData()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Branch_ByZoneId", DBManager.ConnectToDB());
            // MyAdp.SelectCommand.Parameters.Add("@ZoneId", SqlDbType.VarChar, 1000).Value = ZoneId;
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

    public void DropDownZoneBind(DropDownList ddl)
    {
        Myds = new DataSet();
        objZone = new clsMasterZone();
        Myds = objZone.MasterZone_Fetch_DataSet_Activated();

        ddl.DataTextField = Myds.Tables[0].Columns[1].Caption;
        ddl.DataValueField = Myds.Tables[0].Columns[0].Caption;
        ddl.DataSource = Myds.Tables[0];
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select Zone--", "0"));
    }


    public DataSet Check_Branhc_Entry_Status_AnnualBudget(int BranchId, int FY)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Check_Branhc_Entry_Status_AnnualBudget", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            MyAdp.SelectCommand.Parameters.Add("@FY", SqlDbType.Int).Value = FY;
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

    public DataSet MasterBranch_FetchByZoneId_DataSet_AnnualBudget(int ZoneId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterBranch_ByZoneId_AnnualBudget", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MB_ZoneId", SqlDbType.Int).Value = ZoneId;

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

    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        int status = 0;
        objBranch = new clsMasterBranch();
        MyDs = new DataSet();
        if (ddlBranch.SelectedValue != "0" && ddlFY.SelectedValue != "0")
        {
            MyDs = Check_Branhc_Entry_Status_AnnualBudget(Convert.ToInt32(ddlBranch.SelectedItem.Value), Convert.ToInt32(ddlFY.SelectedItem.Value));// objBranch.MasterBranch_FetchByZoneId_DataSet(Convert.ToInt32(ddlZone.SelectedItem.Value));

            status = Convert.ToInt32(MyDs.Tables[0].Rows[0]["Status"].ToString());
            if (status == 420)
            {
                //  ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "if(confirm('Sorry, but there is already a Primary Stakeholder assigned to this ticket. Would you like to replace the existing data?'))", true);


                //  ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "confirm", "alert('Data is already added for selected Branch & Financial Year. You can only Edit Annual Budget for Selected Branch');", true);


                ScriptManager.RegisterStartupScript(
               this.UpdatePanel1,
               this.GetType(),
               "confirm",
               "if (confirm('Data is already added for selected Branch & Financial Year. Do you want to edit Annual Budget for Selected Branch?')) { " +
                   "__doPostBack('ConfirmResult','OK'); " +
               "} else { " +
                   "__doPostBack('ConfirmResult','Cancel'); }",
               true
           );


                //MyDs = new DataSet();
                //Myds = Fetch_AnnualBudget_BranchWise(Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlFY.SelectedValue));
                //if (Myds.Tables[0].Rows.Count > 0)
                //{
                //    lblZoneDisplay.Text = Myds.Tables[0].Rows[0]["fGPtbl_MZ_Name"].ToString();
                //    lblBranchDisplay.Text = Myds.Tables[0].Rows[0]["fGPtbl_MB_Name"].ToString();
                //    AnnualBudgetDisplay.Text = Myds.Tables[0].Rows[0]["AnnualTarget"].ToString();
                //    lblFYDisplay.Text = Myds.Tables[0].Rows[0]["FinancialYear"].ToString();
                //}

                //ddlZone.SelectedIndex = -1;
                //ddlBranch.SelectedIndex = -1;
                //ddlFY.SelectedIndex = -1;

                //// SHOWING MODALPOPUPEXTENDER
                //ModalPopupExtender1.Show();


            }
        }

    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        objBranch = new clsMasterBranch();
        if (ddlZone.SelectedValue != "0")
        {
            ddlBranch.DataSource = objBranch.MasterBranch_FetchByZoneId_DataSet(Convert.ToInt32(ddlZone.SelectedItem.Value));//MasterBranch_FetchByZoneId_DataSet_AnnualBudget(Convert.ToInt32(ddlZone.SelectedItem.Value));
            ddlBranch.DataValueField = "fGPtbl_MB_Id";
            ddlBranch.DataTextField = "fGPtbl_MB_Name";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
        }

    }
    protected void gvBranch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {

            txtAnnualBudgetEdit.Text = "";
            string[] args = e.CommandArgument.ToString().Split('|');
            string BranchId = args[0];
            string FYId = args[1];

            hdnBranchIdEdit.Value = BranchId;
            hdnFYIdEdit.Value = FYId;
            MyDs = new DataSet();
            Myds = Fetch_AnnualBudget_BranchWise(Convert.ToInt32(BranchId), Convert.ToInt32(FYId));
            if (Myds.Tables[0].Rows.Count > 0)
            {
                lblZoneDisplay.Text = Myds.Tables[0].Rows[0]["fGPtbl_MZ_Name"].ToString();
                lblBranchDisplay.Text = Myds.Tables[0].Rows[0]["fGPtbl_MB_Name"].ToString();
                AnnualBudgetDisplay.Text = Myds.Tables[0].Rows[0]["AnnualTarget"].ToString();
                lblFYDisplay.Text = Myds.Tables[0].Rows[0]["FinancialYear"].ToString();
            }


            ModalPopupExtender1.Show();

        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AnnualTargetUpdate.aspx");
    }
    protected void btnSubmitEdit_Click(object sender, EventArgs e)
    {
        Decimal AnnualTarget = Convert.ToDecimal(txtAnnualBudgetEdit.Text.ToString().Trim());
        ErrorId = Edit_Annualtarget_For_Branch(Convert.ToInt32(hdnBranchIdEdit.Value), Convert.ToInt32(hdnFYIdEdit.Value), AnnualTarget);

        if (ErrorId == 1)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "Alert", "alert('Data has been updated successfully.');", true);
            Clear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "Alert", "alert('System is unable to process your request. Sorry for inconvenience. Please try after some time !');", true);

        }

        ModalPopupExtender1.Hide();

        DataSet dtBranches = GetData();
        grd_report.DataSource = dtBranches;
        grd_report.DataBind();

    }
    protected void gvBranch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Literal ltSno = (Literal)e.Row.FindControl("ltlSno");
            //ltSno.Text = count.ToString();
            //count++;

            Label lblZoneCheck = (Label)e.Row.FindControl("lbl_Zonename");
            Label lblAnnualBudget = (Label)e.Row.FindControl("lblAnnualBudget");
            
            if(lblAnnualBudget.Text== "0.000")
            {
                lblAnnualBudget.Text = "";
            }
            // ImageButton imgAct = (ImageButton)e.Row.FindControl("imgAct");
            ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
            HiddenField hdnzone = (HiddenField)e.Row.FindControl("HiddenField3");

            if (lblZoneCheck.Text != "")
            {
                imgEdit.Visible = false;
            }
            else
            {
                imgEdit.Visible = true;

            }
        }
        //}
        //catch (Exception ex)
        //{
        //    ErrHandler.WriteError(ex.Message);
        //    count = 0;
        //}

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        int status = 0;
        objBranch = new clsMasterBranch();
        MyDs = new DataSet();
        if (ddlBranch.SelectedValue != "0" && ddlFY.SelectedValue != "0")
        {
            MyDs = Check_Branhc_Entry_Status_AnnualBudget(Convert.ToInt32(ddlBranch.SelectedItem.Value), Convert.ToInt32(ddlFY.SelectedItem.Value));// objBranch.MasterBranch_FetchByZoneId_DataSet(Convert.ToInt32(ddlZone.SelectedItem.Value));

            status = Convert.ToInt32(MyDs.Tables[0].Rows[0]["Status"].ToString());
            if (status == 420)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "Alert", "alert('Data is already added for selected Branch & Financial Year.');", true);

                MyDs = new DataSet();
                Myds = Fetch_AnnualBudget_BranchWise(Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlFY.SelectedValue));
                if (Myds.Tables[0].Rows.Count > 0)
                {
                    lblZoneDisplay.Text = Myds.Tables[0].Rows[0]["fGPtbl_MZ_Name"].ToString();
                    lblBranchDisplay.Text = Myds.Tables[0].Rows[0]["fGPtbl_MB_Name"].ToString();
                    AnnualBudgetDisplay.Text = Myds.Tables[0].Rows[0]["AnnualTarget"].ToString();
                    lblFYDisplay.Text = Myds.Tables[0].Rows[0]["FinancialYear"].ToString();
                }
                // SHOWING MODALPOPUPEXTENDER
                ModalPopupExtender1.Show();
                return;
            }
        }




        int Branchid_dghr = 0;
        int branchid = 0;
        int Zoneid = 0;
        int FY = 0;
        string strBranchId = "";

        try
        {

            int ErrorId = 0;
            if (Convert.ToString(ddlZone.SelectedValue) != "0")
            {
                try
                {
                    Zoneid = Convert.ToInt32(ddlZone.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    strBranchId = "0";
                }
            }

            if (Convert.ToInt32(ddlBranch.SelectedValue) != 0)
            {
                try
                {
                    branchid = Convert.ToInt32(ddlBranch.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    branchid = 0;
                }
            }
            if (Convert.ToInt32(ddlFY.SelectedValue) != 0)
            {
                try
                {
                    FY = Convert.ToInt32(ddlFY.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    FY = 0;
                }
            }


            Decimal AnnualTarget = Convert.ToDecimal(txtAnnualTarget.Text.ToString().Trim());
            ErrorId = Add_Edit_Annualtarget_For_Branch(Zoneid, branchid, FY, AnnualTarget);

            if (ErrorId == 1)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "Alert", "alert('Data has been updated successfully.');", true);
                Clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "Alert", "alert('System is unable to process your request. Sorry for inconvenience. Please try after some time !');", true);


            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


        DataSet dtBranches = GetData();
        grd_report.DataSource = dtBranches;
        grd_report.DataBind();



        //ds = new DataSet();
        //ds = Fetch_EMPID_SSO();
        //GridView1.DataSource = ds;
        //GridView1.DataBind();

        //if (!IsPostBack)
        //{
        //    // Example: Fetch distinct zones
        //    DataSet dtZones = GetData();
        //    rptZones.DataSource = dtZones;
        //    rptZones.DataBind();
        //}
    }
    void Clear()
    {
        txtAnnualTarget.Text = "";
        ddlZone.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlFY.SelectedIndex = -1;
    }
    public int Edit_Annualtarget_For_Branch(int Branchid, int FY, Decimal AnnualTarget)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Edit_Annualtarget_For_Branch", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = Branchid;

            MySqlComm.Parameters.Add("@FY", SqlDbType.Int).Value = FY;
            MySqlComm.Parameters.Add("@AnnualTarget", SqlDbType.Decimal).Value = AnnualTarget;
            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);
            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return ErrorId;
    }
    public int Add_Edit_Annualtarget_For_Branch(int Zoneid, int Branchid, int FY, Decimal AnnualTarget)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Add_Edit_Annualtarget_For_Branch", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = Branchid;
            MySqlComm.Parameters.Add("@Zoneid", SqlDbType.Int).Value = Zoneid;
            MySqlComm.Parameters.Add("@FY", SqlDbType.Int).Value = FY;
            MySqlComm.Parameters.Add("@AnnualTarget", SqlDbType.Decimal).Value = AnnualTarget;
            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);
            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return ErrorId;
    }
    public DataSet Fetch_AnnualBudget_BranchWise(int BranchId, int FY)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_AnnualBudget_BranchWise", DBManager.ConnectToDB());
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            MyAdp.SelectCommand.Parameters.Add("@FY", SqlDbType.Int).Value = FY;
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
}