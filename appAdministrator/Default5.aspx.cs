using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

public partial class appAdministrator_Default5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        if (!IsPostBack)

        {
            int CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            int Formtypeid = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());
            int BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            clsRenewal objRenewal = new clsRenewal();
            DataTable dtPropdate = new DataTable();
            dtPropdate = objRenewal.Company_Fetch_CompanyStaus(CompanyId);
            string proposaldate = dtPropdate.Rows[0]["proposaldate"].ToString();
            string renewaldate = dtPropdate.Rows[0]["renewaldate"].ToString();

            // renewaldate = "08/11/2019";
            //proposaldate = "09/12/2021";


            //DateTime newdate = Convert.ToDateTime(renewaldate).AddDays(760);
            txtUnitName.Text = dtPropdate.Rows[0]["fGPtbl_Company_Name"].ToString();
            txtPAN.Text = dtPropdate.Rows[0]["fGPtbl_Company_PAN"].ToString();
            // txtStartDate.Text = dtPropdate.Rows[0]["renewaldate"].ToString();
            DateTime date = DateTime.ParseExact(dtPropdate.Rows[0]["renewaldate"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            txtStartDate.Text = date.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
            
            bool Isprovisional = Convert.ToBoolean(dtPropdate.Rows[0]["isProvisional"]);

            DateTime parsedDate = DateTime.ParseExact(renewaldate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime newdate = Isprovisional ? parsedDate.AddDays(364):parsedDate.AddDays(730);

            txtEndDate.Text = newdate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

            //bool Isprovisional = Convert.ToBoolean(dtPropdate.Rows[0]["isProvisional"]);

            //DateTime newdate = Isprovisional ? Convert.ToDateTime(renewaldate).AddDays(395) : Convert.ToDateTime(renewaldate).AddDays(760);
            //txtEndDate.Text = newdate.ToString();
            DateTime futureEndDate = newdate.AddDays(730);
            DateTime futureStartDate = newdate.AddDays(1);
            lblop1.Text= futureStartDate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture) + " To " + futureEndDate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
            //lblop2.Text = futureStartDate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture) + " To " + futureEndDate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

            string Company_Id = objDecEnc.Encrypt(CompanyId.ToString());
            string Formtypeid_renewal = objDecEnc.Encrypt("2");
            string Branch_Id = objDecEnc.Encrypt(BranchId.ToString());

            string imageidRenewal = "Online_UnitRenewalStep1.aspx?c=" + Company_Id.ToString() + "&f=" + Formtypeid_renewal.ToString() + "&b=" + Branch_Id.ToString();
            lnkRenewal.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidRenewal + "');");

            //string fresh = "NewUnitRegistrationOnline.aspx";
            //lnkFresh.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + fresh + "');");
        }

    }

    protected void lnkFresh_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string CompanyId = (objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
        DeleteTheUnit(CompanyId, "Expired for More than Two years. ");
        Response.Redirect("NewUnitRegistrationOnline.aspx", false);
    }

    private void DeleteTheUnit(string compID, string reason)
    {
        using (SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"]))
        {
            sqlConn.Open();
            using (SqlTransaction transaction = sqlConn.BeginTransaction())
            {
                try
                {
                    SqlCommand command = sqlConn.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GP_companies_deletion_UsingQuery";
                    command.CommandTimeout = 120;

                    command.Parameters.AddWithValue("@Companyid", compID);
                    command.Parameters.AddWithValue("@ErrorId", 0);
                    command.Parameters.AddWithValue("@Reason", reason);

                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
                }
                finally
                {

                }
            }
        }
    }
}