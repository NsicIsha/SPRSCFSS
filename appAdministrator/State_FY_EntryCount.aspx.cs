using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Text;
using System.Data.SqlClient;

public partial class appAdministrator_State_FY_EntryCount : System.Web.UI.Page
{
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
           
            ErrHandler.WriteError("" + ex.StackTrace);
        }
        if(!Page.IsPostBack)
        {
            funfill_ddlState();
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (ddlState.SelectedValue !="0" && ddlFY.SelectedValue !="0")
        {
            Search(Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlFY.SelectedValue));
        }
    }

    private void funfill_ddlState()
    {
        //STATE DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlState_Activated();
        ddlState.DataSource = dt;
        ddlState.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
        ddlState.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("--Select State--", ""));

        

        dt.Clear();
        dt.Dispose();
    }

    void Search(int StateId, int FYId)
    {
        DataSet dsss = new DataSet();
        dsss = Count_Details(StateId, FYId);
        lbl_total_Count.Text= dsss.Tables[0].Rows[0]["TotalCount"].ToString();
    }

    public DataSet Count_Details(int StateId, int FYId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_fetch_UNIT_Count_By_State_FY", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            // MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch;
            MyAdp.SelectCommand.Parameters.Add("@StateId", SqlDbType.VarChar).Value = StateId;
            MyAdp.SelectCommand.Parameters.Add("@FY", SqlDbType.VarChar).Value = FYId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }

        return MyDs;
    }
}