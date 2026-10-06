using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;

public partial class Final_Verify : System.Web.UI.Page
{
    int Primary_Id;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;

    protected void Page_Load(object sender, EventArgs e)
    {
        //SqlCommand sqlcom = new SqlCommand();
        //try
        //{
        //    sqlcom.Connection = DBManager.ConnectToDB();
        //    sqlcom.CommandText = "select count(fGPtbl_Company_Id) from dbo.GPtbl_Company where fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0 and fGPtbl_Company_Status!=5";
        //    string comcount = Convert.ToString(sqlcom.ExecuteScalar());
        //    Label lblUnits = Master.FindControl("lblUnits") as Label;
        //    lblUnits.Text = "CURRENT REGISTERED UNITS: " + comcount;
        //    sqlcom.Connection.Close();
        //}
        //catch { }
        //finally
        //{
        //    sqlcom.Dispose();
        //    DBManager.CloseConnection();
        //}

        Label lblUnits = Master.FindControl("lblUnits") as Label;
        lblUnits.Text = "CURRENT REGISTERED UNITS: " + "60256";

        // Label lblUnits = Master.FindControl("lblUnits") as Label;
        // lblUnits.Text = "CURRENT REGISTERED UNITS: " + "60256";
        UpdateUnitCount();
    }

    public void UpdateUnitCount()
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand MySqlComm;

            int CountStatus = 0;
            Label lblUnits = Master.FindControl("lblUnits") as Label;
            if (File.Exists((Server.MapPath("/Document/Upload/XMLData/" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml"))))
            {
                XmlDocument xmlDocument = new XmlDocument();
                //Read the XML File  
                xmlDocument.Load((Server.MapPath("/Document/Upload/XMLData/" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml")));
                //Create a XML Node List with XPath Expression  
                XmlNode xmlNode = xmlDocument.SelectSingleNode("/UnitCount");
                lblUnits.Text = "CURRENT REGISTERED UNITS: " + xmlNode.InnerText;
            }

            else
            {
                MySqlComm = new SqlCommand("GP_SP_HomePageSPRSCount", DBManager.ConnectToDB());
                MySqlComm.CommandType = CommandType.StoredProcedure;
                SqlParameter outCount = new SqlParameter("@UnitCount", SqlDbType.Int);
                outCount.Direction = ParameterDirection.Output;
                MySqlComm.Parameters.Add(outCount);
                MySqlComm.ExecuteNonQuery();
                CountStatus = Convert.ToInt32(outCount.Value);
                if (CountStatus != 0)
                {
                    XmlTextWriter writer = new XmlTextWriter(Server.MapPath("/Document/Upload/XMLData/" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml"), System.Text.Encoding.UTF8);
                    //Start XM DOcument
                    writer.WriteStartDocument(true);
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    //ROOT Element
                    writer.WriteStartElement("UnitCount");
                    writer.WriteString(CountStatus.ToString());
                    writer.WriteEndElement();
                    //End XML Document
                    writer.WriteEndDocument();
                    //Close writer
                    writer.Close();
                    lblUnits.Text = "CURRENT REGISTERED UNITS: " + CountStatus.ToString();
                }
                else
                {
                    lblUnits.Text = "CURRENT REGISTERED UNITS: ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }


    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        clsCompany objcom = new clsCompany();
        DataSet ds = new DataSet();
        string registrationno;
        registrationno = txtSearchbox.Text;
        ////if (registrationno != "NSIC/GP/BAL/2023/0051738" && registrationno != "NSIC/GP/NTP/2023/0051746" && registrationno != "NSIC/GP/FAR/2022/0051725")
        ////{
            ds = objcom.Company_SPR_CHECK_REGISTRATION(registrationno);
            int CompanyId;
            int a;
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = "";
                    CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());
                    a = Company_Insert_Feedback(txtSearchbox.Text, CompanyId, txtVAgencyName.Text);

                    gv_companies.DataSource = ds;
                    gv_companies.DataBind();


                }
                else
                {
                    gv_companies.DataSource = null;
                    gv_companies.DataBind();
                    gv_companies.EmptyDataText = "No record found";

                    a = Company_Insert_Feedback(txtSearchbox.Text, 0, txtVAgencyName.Text);

                }
            }

            else

            {
                lblMsg.Text = "No record found";
            }
        ////}
        ////else

        ////{
        ////    gv_companies.DataSource = null;
        ////    gv_companies.DataBind();
        ////    gv_companies.EmptyDataText = "No record found";
        ////    lblMsg.Text = "No record found";
        ////}

    }

    private void funopen(string companyid)
    {
        string imageid = "appAdministrator/CompanyCertificatePrintPreviewPageVerify.aspx?id=" + companyid;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + imageid + "', null, 'height=700,width=760,status=yes,toolbar=yes,scrollbars=yes,menubar=no,resizable=yes,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

    }

    protected void gvCertificateDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "verifycertificate")//REQUEST FOR TAKING REQUEST
        {
            int CompanyIdd = Convert.ToInt32(e.CommandArgument.ToString());
            funopen(CompanyIdd.ToString());



            clsCompany objcom = new clsCompany();
            DataSet ds = new DataSet();
            string registrationno;
            registrationno = txtSearchbox.Text;
            if (registrationno != string.Empty)
            {
                ds = objcom.Company_SPR_CHECK_REGISTRATION(registrationno);
                int CompanyId;
                int a;
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());
                        a = Company_Insert_Feedback(txtSearchbox.Text, CompanyId, txtVAgencyName.Text);

                        gv_companies.DataSource = ds;
                        gv_companies.DataBind();


                    }
                    else
                    {
                        gv_companies.DataSource = null;
                        gv_companies.DataBind();
                        gv_companies.EmptyDataText = "No record found";

                        a = Company_Insert_Feedback(txtSearchbox.Text, 0, txtVAgencyName.Text);

                    }
                }
            }
        }
    }
    protected void grd_gvcompaniesonrowdatabound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string imgdraft = string.Empty;

                HiddenField hdncompId = (HiddenField)e.Row.FindControl("HiddenFieldCompanyId");

                //LinkButton btnDraftPrint = (LinkButton)e.Row.FindControl("lbnDraftPrint");

                //imgdraft = "appAdministrator/CompanyCertificatePrintPreviewPageVerify.aspx?id=" + hdncompId.Value;


                //btnDraftPrint.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imgdraft + "');");



                Label lbl_old_registration_number_text = (Label)e.Row.FindControl("lbl_old_registration_number");
                Label lbl_accessid_check = (Label)e.Row.FindControl("lbl_Accessid_type");
                Label lbl_registration_number = (Label)e.Row.FindControl("lbl_new_registration_number");
                Label lbl_Prodct = (Label)e.Row.FindControl("lbl_product_name");
                Label lbl_validity_text = (Label)e.Row.FindControl("lbl_validity_text");
                Label lbl_validitydate_last = (Label)e.Row.FindControl("lbl_last_validity_text");
                Label lbl_registration_date = (Label)e.Row.FindControl("lbl_registratondate");
                Label lblprovisionalfresh = (Label)e.Row.FindControl("lbl_provisinol_fresh");
                Label lbl_renewaldate = (Label)e.Row.FindControl("lbl_renewal_date");

                Label lbl_Last_validitydate = (Label)e.Row.FindControl("Lbl_last_validity");


                if (lbl_old_registration_number_text.Text == "")
                {
                }
                else
                {
                    if (lbl_accessid_check.Text == "4" && lbl_registration_number.Text == "")
                    {
                        lbl_old_registration_number_text.Text = lbl_old_registration_number_text.Text;
                    }
                    else
                    {
                        lbl_old_registration_number_text.Text = "(" + " Old Registration No: " + lbl_old_registration_number_text.Text + ")";
                    }
                }

                if (lbl_renewaldate.Text == "1/1/1900 12:00:00 AM")
                {
                    lbl_validity_text.Visible = false;
                    lbl_validitydate_last.Visible = false;
                }
                else
                {
                    CultureInfo ci = new CultureInfo("en-GB");
                    DateTime RegistrationStartDate = Convert.ToDateTime(lbl_registration_date.Text);
                    DateTime LastValidityStartDate = Convert.ToDateTime(lbl_Last_validitydate.Text);
                    if (lblprovisionalfresh.Text == "0")
                    {
                        // DateTime RegistrationendDate = RegistrationStartDate.AddYears(2).AddDays(-1);//lbl_validitydate_last

                        DateTime RegistrationendDate = LastValidityStartDate.AddYears(2).AddDays(0);
                        DateTime now = System.DateTime.Today.Date;


                        if ((now > RegistrationStartDate) && (now < RegistrationendDate))
                        {
                            lbl_validity_text.Visible = true;
                            lbl_validitydate_last.Visible = true;
                            lbl_validitydate_last.Text = RegistrationendDate.ToString("dd/MM/yyyy");

                            //Commented on 01 March 2021  lbl_validitydate_last.Text
                            lbl_validitydate_last.Visible = false;
                        }
                    }
                    else
                    {
                        // DateTime RegistrationendDate_pro = RegistrationStartDate.AddYears(1).AddDays(-1);
                        DateTime RegistrationendDate_pro = LastValidityStartDate.AddYears(1).AddDays(-1);
                        DateTime now = System.DateTime.Today.Date;
                        if ((now > RegistrationStartDate) && (now < RegistrationendDate_pro))
                        {
                            lbl_validity_text.Visible = true;
                            lbl_validitydate_last.Visible = true;
                            lbl_validitydate_last.Text = RegistrationendDate_pro.ToString("dd/MM/yyyy");
                            //Commented on 01 March 2021  lbl_validitydate_last.Text
                            lbl_validitydate_last.Visible = false;
                        }
                    }
                }
                if (lbl_Prodct.Text.Contains("\r\n"))
                {
                    lbl_Prodct.Text = lbl_Prodct.Text.Replace("\r\n", "<br/>");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public int Company_Insert_Feedback(string RegNo, int CompanyId, string VerifyingBy)
    {
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Insert_CertificateVerification_Details", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@RegNo", SqlDbType.VarChar, 5000).Value = RegNo;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.VarChar, 150).Value = CompanyId;
            MySqlComm.Parameters.Add("@VerifyingBy", SqlDbType.VarChar, 150).Value = VerifyingBy;

            SqlParameter fGPtbl_POI_Id = new SqlParameter("@Id", SqlDbType.Int);
            fGPtbl_POI_Id.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(fGPtbl_POI_Id);
            MySqlComm.ExecuteNonQuery();
            Primary_Id = Convert.ToInt32(fGPtbl_POI_Id.Value);
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

        return Primary_Id;
    }
}