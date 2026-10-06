using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;

public partial class appAdministrator_Online_Renewal_RejectPaymentDetails : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId=0;
    int count = 1;
    int successId, urlId = 0, amendmentid;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public int Company_Reject_RenewalProcess(int CompanyId,string Reason)
    {

        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Reject_Renewalprocess_ByCompanyId_Online_Renewal", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            
            MySqlComm.Parameters.Add("@Reason", SqlDbType.VarChar, 5000).Value = Reason;
            

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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

          
            Myds = new DataSet();
            int    companyid;


            //ACCESSING FORMTYPEID, COMPANY ID, COMPANY EMAIL
            companyid = Convert.ToInt32(Request.QueryString["uid"]);

          

         

            successId = Company_Reject_RenewalProcess(companyid,txtReason.Text);

           
            if (successId == 2)
            {
               
                Session["companyAction"] = "Status changed successfully!";
                Response.Redirect("Online_RenewalPaymentDetails.aspx"); ;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
}