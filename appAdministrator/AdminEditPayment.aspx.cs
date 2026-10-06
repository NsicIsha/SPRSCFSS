using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Drawing;

public partial class appAdministrator_AdminEditPayment : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId;
    string LastyearTurnover = string.Empty;
    string UserFullName;
    int urlid = 0;
    clsCompanyInformationEdit objCompanyEdit;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch(Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }

        try
        {
            objCompanyEdit = new clsCompanyInformationEdit();
            FunRequest();
           // CompanyId = 69430;

            LastyearTurnover =objCompanyEdit.Fetch_LastyearTurnover(CompanyId);
            if (LastyearTurnover != "")
            {
                if (!IsPostBack)
                {
                   // lblLastYearTurn.Text = "Rs."+" "+string.Format("{0:0.00}", Convert.ToString( Math.Round( Convert.ToDecimal( LastyearTurnover),2)));
                    lblLastYearTurn.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(LastyearTurnover)));
                    Session["ReferenceNo"] = "7";
                }
            }
            else
            {
                lblLastYearTurn.Text = "Your Application's status is incomplete. <br/>Please use the"+
                "<b>New Unit Registration</b> link to change any information in the application and complete it.";
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  "+ex.StackTrace);
        }
    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
        }
        catch
        {
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            /*to check provisional company. compare old turnover and new. if exceed show pending payment. if less show pay back
             payment ask for continue. fetch last payment amount and payment slab fee for entered turnover
             Also check for negative payment*/

            objCompanyEdit = new clsCompanyInformationEdit();
            DataSet MydsPayment = new DataSet();
            Button btnsub = (Button)sender;
           

            if (btnsub.Text == "Show payment")
            {
                MydsPayment = objCompanyEdit.Fetch_FeepaidTurnoverFee(Convert.ToDecimal(txtLastyearTurnover.Text), CompanyId);
                float lastfee, turnfee;
                lastfee=Convert.ToSingle(MydsPayment.Tables[0].Rows[0]["feepaid"]);
                turnfee = Convert.ToSingle(MydsPayment.Tables[0].Rows[0]["turnoverfee"]);
                lastfee = Convert.ToSingle(Math.Round(lastfee, 2, MidpointRounding.AwayFromZero));
                turnfee = Convert.ToSingle(Math.Round(turnfee, 2, MidpointRounding.AwayFromZero));

                /*ltllastFeepaid.Text = Convert.ToString(MydsPayment.Tables[0].Rows[0]["feepaid"]);
                ltlcurrentturnfee.Text = Convert.ToString(MydsPayment.Tables[0].Rows[0]["turnoverfee"]);*/
                ltllastFeepaid.Text = "Rs."+" "+ Convert.ToString( string.Format("{0:0.00}", Convert.ToDecimal( lastfee)));
                ltlcurrentturnfee.Text = "Rs."+" "+ Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal( turnfee)));

                decimal FeeDifference;
                FeeDifference = Convert.ToDecimal(MydsPayment.Tables[0].Rows[0]["turnoverfee"]) - Convert.ToDecimal(MydsPayment.Tables[0].Rows[0]["feepaid"]);
                FeeDifference = Math.Round(FeeDifference, 2, MidpointRounding.AwayFromZero);
                if (FeeDifference > 0)
                {
                    lblpayfee.Text = "Balance fees to be paid";
                    ltlpayback.Text = "Rs."+" "+ Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal( FeeDifference)));
                    Session["AddSubFees"] = "1";
                }
                else if (FeeDifference < 0)
                {
                    lblpayfee.Text = "Balance fees to be refunded";
                    ltlpayback.Text = "Rs."+""+ Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal( FeeDifference * (-1))));
                    Session["AddSubFees"] = "-1";
                }
                else if (FeeDifference == 0)
                {
                    lblpayfee.Text = "No fees to be paid";
                    ltlpayback.Text = "Rs."+" "+ "0";
                    //btnSubmit.Visible = false;
                    Session["ReferenceNo"] = "8";
                }
                //lblpayfee.Font.Bold = true;

                divcalculate.Visible = true;
                btnsub.Text = "Submit";
            }
            else if (btnsub.Text == "Submit")
            {
                objCompanyEdit = new clsCompanyInformationEdit();
                objCompanyEdit.Update_LastTurnover(CompanyId, Convert.ToSingle(txtLastyearTurnover.Text));

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
                string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
                string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
                string EncryptFormtype = objDecEnc.Encrypt("1");
                string EncryptPayback = objDecEnc.Encrypt(ltlpayback.Text);
                if (Session["ReferenceNo"].ToString() == "8")
                {
                    Response.Redirect("Thankyou.aspx", false);
                }
                else
                {
                    Response.Redirect("AdminEditPaymentDetail.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&pb=" + EncryptPayback, false);
                }
                
            }

            //TurnoverDifference = Convert.ToDecimal(txtLastyearTurnover) - Convert.ToDecimal(LastyearTurnover);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  " + ex.StackTrace);
        }
    }


}
