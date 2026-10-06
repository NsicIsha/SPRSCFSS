using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

public partial class appAdministrator_SSOUpdate : System.Web.UI.Page
{
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
        if (!Page.IsPostBack)
        {

            ds = new DataSet();
            ds = MasterBranch_FetchActivated_DataSet();

            ddlbranchEdit.DataSource = ds;
            ddlbranchEdit.DataTextField = "fGPtbl_MB_Name"; ds.Tables[0].Rows[0]["fGPtbl_MB_Name"].ToString();// ds.Columns["fGPtbl_MB_Name"].ToString();
            ddlbranchEdit.DataValueField = "ID";// ds.Tables[0].Rows[0]["ID"].ToString();// dt.Columns["BRANCHID_DGHR"].ToString();
            ddlbranchEdit.DataBind();
            ddlbranchEdit.Items.Insert(0, new ListItem("--Select Branch--", ""));


            ds = new DataSet();
            ds = Fetch_EMPID_SSO();
            GridView1.DataSource = ds;
            GridView1.DataBind();


        }
    }

    public DataSet MasterBranch_FetchActivated_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterBranch_Activated_SSO", DBManager.ConnectToDB());
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


    public DataSet Fetch_EMPID_SSO()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_EMPID_SSO", DBManager.ConnectToDB());
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
    protected void btnSubmitEdit_Click(object sender, EventArgs e)
    {

        int Branchid_dghr = 0;
        int branchid = 0;
        int UserType = 0;
        string strBranchId = "";

        try
        {

            int ErrorId = 0;
            if (Convert.ToString(ddlbranchEdit.SelectedValue) != "0")
            {
                try
                {
                    strBranchId = ddlbranchEdit.SelectedValue.ToString();
                }
                catch (Exception ex)
                {
                    strBranchId = "0";
                }
            }

            string[] parts = strBranchId.Split(new char[] { ',' });

            branchid = Convert.ToInt32(parts[0]);
            Branchid_dghr = Convert.ToInt32(parts[1]);



            if (Convert.ToInt32(ddlUserType.SelectedValue) != 0)
            {
                try
                {
                    UserType = Convert.ToInt32(ddlUserType.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    UserType = 0;
                }
            }


            string empcode = txtEmpCode.Text.ToString().Trim();
            ErrorId = Update_PF_Code(branchid, Branchid_dghr, UserType, empcode);

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
        ds = new DataSet();
        ds = Fetch_EMPID_SSO();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    void Clear()
    {
        txtEmpCode.Text = "";

        ddlbranchEdit.SelectedIndex = -1;
        ddlUserType.SelectedIndex = -1;




    }

    public int Update_PF_Code(int Branchid, int branch_dghrid, int usertype, string EMPCode)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_EMPID_SSO", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = Branchid;
            MySqlComm.Parameters.Add("@branch_dghrid", SqlDbType.Int).Value = branch_dghrid;
            MySqlComm.Parameters.Add("@usertype", SqlDbType.Int).Value = usertype;
            MySqlComm.Parameters.Add("@EMPCode", SqlDbType.VarChar, 8).Value = EMPCode;
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
}