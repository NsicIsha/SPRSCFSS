using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;
 

public partial class SPR_CHECK_REGISTRATION : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        clsCompany objcom = new clsCompany();
        DataSet ds = new DataSet();
        string registrationno;
        registrationno = txtSearchbox.Text;
        if(registrationno != string.Empty)
        {
            ds = objcom.Company_SPR_CHECK_REGISTRATION(registrationno);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_companies.DataSource = ds;
                    gv_companies.DataBind();
                }
                else
                {
                    gv_companies.DataSource = null;
                    gv_companies.DataBind();
                    gv_companies.EmptyDataText = "No record found";
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
                Label lbl_old_registration_number_text = (Label)e.Row.FindControl("lbl_old_registration_number");
                Label lbl_accessid_check = (Label)e.Row.FindControl("lbl_Accessid_type");
                Label lbl_registration_number = (Label)e.Row.FindControl("lbl_new_registration_number");
                Label lbl_Prodct = (Label)e.Row.FindControl("lbl_product_name");
                Label lbl_validity_text = (Label)e.Row.FindControl("lbl_validity_text");
                Label lbl_validitydate_last = (Label)e.Row.FindControl("lbl_last_validity_text");
                Label lbl_registration_date = (Label)e.Row.FindControl("lbl_registratondate");
                Label lblprovisionalfresh = (Label)e.Row.FindControl("lbl_provisinol_fresh");
                Label lbl_renewaldate = (Label)e.Row.FindControl("lbl_renewal_date");
                if (lbl_old_registration_number_text.Text == "")
                {
                }
                else
                {
                    if (lbl_accessid_check.Text == "4" && lbl_registration_number.Text=="")
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
                    if (lblprovisionalfresh.Text == "0")
                    {
                        DateTime RegistrationendDate = RegistrationStartDate.AddYears(2).AddDays(-1);
                        DateTime now = System.DateTime.Today.Date;
                        if ((now > RegistrationStartDate) && (now < RegistrationendDate))
                        {
                            lbl_validity_text.Visible = true;
                            lbl_validitydate_last.Visible = true;
                            lbl_validitydate_last.Text = RegistrationendDate.ToString("dd/MM/yyyy");
                        }
                    }
                    else
                    {
                        DateTime RegistrationendDate_pro = RegistrationStartDate.AddYears(1).AddDays(-1);
                        DateTime now = System.DateTime.Today.Date;
                        if ((now > RegistrationStartDate) && (now < RegistrationendDate_pro))
                        {
                            lbl_validity_text.Visible = true;
                            lbl_validitydate_last.Visible = true;
                            lbl_validitydate_last.Text = RegistrationendDate_pro.ToString("dd/MM/yyyy");
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
}
