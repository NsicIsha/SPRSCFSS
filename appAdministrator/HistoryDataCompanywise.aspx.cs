using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Text;

public partial class appAdministrator_HistoryDataCompanywise : System.Web.UI.Page
{
    clsCompany objCompany = new clsCompany();
    DataSet ds;
    DataSet dsAmedment;
    DataSet dsrenewal;
    string Amedment = "";
    string Renewal = "";
    int UserId, RoleId;
    StringBuilder sb;
    StringBuilder sb1;
    string Text = "";
    string Text_Date = "";
    string Text_Renewal = "";
    string Text_renewal_Date = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }

    }

    public void Bindlist()
    {
        try
        {
            int ddlvalue = Convert.ToInt32(ddlSearchCriteria.SelectedValue.ToString());
            string serachtext = string.Empty;
            string IncompleteStatus = string.Empty;
            if (txt_search.Text == "")
            {
                serachtext = "";
            }
            else
            {
                serachtext = txt_search.Text.ToString();
            }
            objCompany = new clsCompany();
            ds = objCompany.Company_fetch_history_Companywise(ddlvalue, serachtext);
            if (ds.Tables[0].Rows.Count > 0)
            {
                sb = new StringBuilder();
                sb.Append("<table border='1' cellpadding='3'cellspacing='3' style='background-color: #ffffff; margin-left:0px; min-width:800px; ' >");  // main table we have closed it
                sb.Append("<tr class='GrdviewHeader'><th >Company Name</th><th >Status</th>");
                sb.Append("<th >Registration Date </th> <th>Amedment Date</th>  <th> Renewal Date </th></tr>");

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {


                    sb.Append("<tr><td align='left' valign='top'>" + ds.Tables[0].Rows[i]["CompanyName"].ToString() + "</td>");
                  //  sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["Status"].ToString() + "</td>");
                    IncompleteStatus = ds.Tables[0].Rows[i]["Status"].ToString();
                    sb.Append("<td align='left' valign='top'>");
                    sb.Append("<table border='0'cellpadding='2'cellspacing='2' style='background-color: #ffffff; margin-left:0px; width:350px;'>");

                    if (IncompleteStatus != "Expired")
                    {
                        switch (IncompleteStatus)
                        {
                            case "Incomplete":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2' valign='top'> <b>Application Form incomplete.</b></td></tr>");
                                break;
                            case "Waiting":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2' valign='top'> <b> Application Form waiting to be accepted by Branch Office .</b></td></tr>");

                                break;
                            case "Inprocess":
                                if (ds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Agency Selected")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Application Form sent to Inspection Agency for inspection.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Request Generated")
                                {
                                    //show username and agency id
                                    DataSet ds_agency = new DataSet();
                                    int AgencyUserId = Convert.ToInt32(ds.Tables[0].Rows[i]["AgencyUserid"].ToString());
                                    string AgencyName = "N.A";
                                    string AgencyUsername = "N.A";
                                    if (AgencyUserId != 0)
                                    {
                                        ds_agency = objCompany.Fetch_Inspection_Agency_and_user_name(AgencyUserId);
                                        if (ds_agency.Tables[0].Rows.Count > 0)
                                        {
                                            AgencyName = ds_agency.Tables[0].Rows[0]["InspectionAgencyName"].ToString();
                                            AgencyUsername = ds_agency.Tables[0].Rows[0]["UserName"].ToString();
                                        }
                                        else
                                        {
                                            ds_agency.Dispose();
                                        }
                                    }

                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2' valign='top'> <b>Application Form forwarded to Inspection Agency for Inspection Date to be allocated.</b><br/><b>Inspection Agency:</b> <b>" + AgencyName.ToString() + ".<br> Agency User:" + AgencyUsername.ToString() + " </b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Inspection Date already Issued but Inspection is Pending")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2' valign='top'> <b>Inspection Date already issued but Inspection is pending.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Inspection Complete")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td> Status:</td>");
                                    sb.Append("<td colspan='2'> <b>Inspection Complete.  Waiting for branch office approval for store details.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Not complete Inspection")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Server Error.</b></td></tr>");
                                }

                                break;

                            case "Inspection Passed":
                                //lbl_branch_account_chek.Text = "You not set Monetary limit";
                                //break;
                                
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Monetary Limit to be set by Branch Account.</b></td></tr>");
                              

                               
                                break;

                            case "Confirm":

                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2'> <b> Registration Complete .</b></td></tr>");
                                break;


                        }




                        // else if(Inspectionstatus
                    }




                    else
                    {
                        sb.Append("<tr>");
                        sb.Append("<td valign='top'></td>");
                        sb.Append("<td colspan='2' valign='top'> <b>Expired</b></td></tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");

                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["RegistrationDate"].ToString() + "</td>");
                      Amedment=BindAmedmentData(Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyID"].ToString()));
                      Renewal = BindRenewalData(Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyID"].ToString()));
                    sb.Append("<td align='left' valign='top'>" +Amedment+ "</td>"); //AmedmentDate

                    sb.Append("<td align='left' valign='top'>"+Renewal +"</td>");// Renewal Date

                    sb.Append("</tr>");
                }

                sb.Append("</table>");
                lbl_grid_view.Text = sb.ToString();


            }

            else
            {
                ds.Dispose();
                lbl_grid_view.Text = "No Record Found!!!!!";
                lbl_grid_view.Style.Add("color", "red");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }
    protected void btn_serch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridview();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }
    protected void ddlSearchCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (txt_search.Text != "")
            {
                txt_search.Text = "";
            }
        }
        catch (Exception ex)
        {
        }
    }


    public string BindAmedmentData(int Companyid)
    {
        try
        {
            objCompany = new clsCompany();
            dsAmedment = new DataSet();
            dsAmedment = objCompany.Company_fetch_history_AmedmentDate(Companyid);
            if (dsAmedment.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j <= dsAmedment.Tables[0].Rows.Count - 1; j++)
                {
                    Amedment +=   "<br/>" + dsAmedment.Tables[0].Rows[j]["AmedmentDate"].ToString();
                }
            }
            else
            {
                dsAmedment.Dispose();
                Amedment = "";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        return Amedment;
    }

    public string BindRenewalData(int Companyid)
    {
        try
        {
            objCompany = new clsCompany();
            dsrenewal = new DataSet();
            dsrenewal = objCompany.Company_fetch_history_RenewalDate(Companyid);
            if (dsrenewal.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k <= dsrenewal.Tables[0].Rows.Count - 1; k++)
                {
                    Renewal += "<br/>" + dsrenewal.Tables[0].Rows[k]["RenewalDate"].ToString();
                }
            }
            else
            {
                dsrenewal.Dispose();
                Renewal = "";
            }
            return Renewal;
        }
        catch (Exception ex)
        {
        }
        return Renewal;
    }
    protected void grd_view_history_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_companyid = (Label)e.Row.FindControl("lbl_Company_id");
                Label lbl_company_name = (Label)e.Row.FindControl("lbl_Company_Name");
                Label lbl_statusttext = (Label)e.Row.FindControl("lbl_comapny_name_text");
                if (lbl_company_name.Text != "")
                {
                    lbl_company_name.Text = "<b>" + lbl_company_name.Text.ToString() +"</b>";
                }
                Label lbl_Company_Address = (Label)e.Row.FindControl("lbl_Comapny_Address");
                if (lbl_Company_Address.Text != "")
                {
                    lbl_Company_Address.Text =lbl_Company_Address.Text.ToString();
                }
                Label lbl_phone = (Label)e.Row.FindControl("lbl_phone");
                if (lbl_phone.Text != "")
                {
                    lbl_phone.Text = "<b>" + "Phone :</b>" + lbl_phone.Text.ToString();
                }
                Label lbl_email = (Label)e.Row.FindControl("lbl_email");
                if (lbl_email.Text != "")
                {
                    lbl_email.Text = "<b>" + "Email :</b>" + lbl_email.Text.ToString();
                }
                Label lbl_status = (Label)e.Row.FindControl("lbl_status");


                if (lbl_status.Text != "")
                {
                    if (lbl_status.Text.ToString() == "Incomplete")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Application Form incomplete.";
                    }
                    if (lbl_status.Text.ToString() == "Waiting")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Application Form waiting to be accepted by Branch Office .";
                    }
                    if (lbl_status.Text.ToString() == "Inprocess")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Application Form sent to Inspection Agency for inspection.";
                    }
                    if (lbl_status.Text.ToString() == "Inspection Passed")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Monetary Limit to be set by Branch Account.";
                    }
                    if (lbl_status.Text.ToString() == "Registerd")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Registered";
                    }

                   // lbl_status.Text = "<b>" + "Status :</b>" + lbl_status.Text.ToString();
                }
                Label lbl_Registrationno = (Label)e.Row.FindControl("lbl_registration_number");
                if (lbl_Registrationno.Text != "")
                {
                    lbl_Registrationno.Text = "<b>" + "Registration No:</b>" + lbl_Registrationno.Text.ToString();
                }
                Label lbl_registration_date = (Label)e.Row.FindControl("lbl_registration_date");
                if (lbl_registration_date.Text != "01/01/1900" && lbl_registration_date.Text!="")
                    {
                         lbl_registration_date.Text="<b>"+"Registration Date :</b>"+ lbl_registration_date.Text.ToString();
                   }
                Label lblAmedment_renewal_date=(Label)e.Row.FindControl("lbl_renewl_amedment_date");
               
                  sb1=new StringBuilder();

                 DataSet ds_renewal_amedment=new DataSet();
                  objCompany=new clsCompany();

                  string ImageId = string.Empty;
                  string ImageidRenewal = string.Empty;
                
                  if (lbl_statusttext.Text.ToString() == "Registerd")
                        {
                       Label lbl_amedment_id = (Label)e.Row.FindControl("lbl_Amendmet_id");
                       
                 ds_renewal_amedment=objCompany.Company_fetch_history_AmedmentRenewalDate(Convert.ToInt32(lbl_companyid.Text.ToString()));
                  if(ds_renewal_amedment.Tables[0].Rows.Count>0)
                  {
                   sb1.Append("<table cellpadding='5' cellspacing='5' border='0'>"); 
                          for(int m=0; m<=ds_renewal_amedment.Tables[0].Rows.Count-1;m++)
                          {
                              lbl_amedment_id.Text = ds_renewal_amedment.Tables[0].Rows[m]["fGPtbl_Company_Amendment_Id"].ToString();
                              if (lbl_amedment_id.Text != "")
                              {
                              }
                              else
                              {
                                  lbl_amedment_id.Text = "0";
                              }
                              if (ds_renewal_amedment.Tables[0].Rows[m]["AMEDMENT"].ToString() == "A")
                              {
                                
                              string   Text_Date_amedment =  ds_renewal_amedment.Tables[0].Rows[m]["AmedmentDate"].ToString();
                                    if(Text_Date_amedment!="")
                                    {
                                          Text =  Text+ "<b>Amedment Date</b>" + "<br/>"; 
                                    }
                                    else
                                    {
                                         Text="";
                                    }
                                    ImageId = "../appAdministrator/AmedmentHistoryPage.aspx?Company_ID=" + Convert.ToInt32(lbl_companyid.Text.ToString()) + "&Date=" + Text_Date_amedment + "&Amedmentid=" + ds_renewal_amedment.Tables[0].Rows[m]["fGPtbl_Company_Amendment_Id"].ToString(); 
                                  Text_Date =  Text_Date+ "<a href='" + ImageId + "' target='_blank'>" + ds_renewal_amedment.Tables[0].Rows[m]["AmedmentDate"].ToString() + "</a>" +"<br/>" ;
                                //  Text_Date = Text_Date + "<br/>";

                              }
                             

                              else  if (ds_renewal_amedment.Tables[0].Rows[m]["AMEDMENT"].ToString() == "R")
                              {
                                  Text_Renewal = Text_Renewal + "<b>Renewal Date</b>" + "<br/>";
                                  Text_renewal_Date = Text_renewal_Date+ ds_renewal_amedment.Tables[0].Rows[m]["AmedmentDate"].ToString()+ "<br/>";
                             //  ImageidRenewal = "../appAdministrator/RenewalHistoryPage.aspx?Company_ID=" + Convert.ToInt32(lbl_companyid.Text.ToString()) + "&Date=" + Text_renewal_Date_renewal;
                                //  Text_renewal_Date = "<a href='" + ImageidRenewal + "' target='_blank'>" +  ds_renewal_amedment.Tables[0].Rows[m]["AmedmentDate"].ToString()+"</a>" ;
                                //  Text_renewal_Date = Text_renewal_Date + "<br/>";
                              }
                              else
                              {
                                  Text = "";
                                  Text_Date = "";
                                  Text_Renewal = "";
                                   Text_renewal_Date="";
                              }
                             
                          }
                          sb1.Append("<tr><td align='left' valign='top'>" + Text + Text_Renewal + "</td>");
                          sb1.Append("<td align='left'valign='top'>" + Text_Date + Text_renewal_Date + "</td></tr>");
                          sb1.Append("</table>");
                          lblAmedment_renewal_date.Text = sb1.ToString();
                          Text = "";
                          Text_Renewal = "";
                          Text_Date = "";
                          Text_renewal_Date = "";
                  }
                  else
                  {
                       ds_renewal_amedment.Dispose();
                       lblAmedment_renewal_date.Text = "";
                      
                  }
         }

            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }
    public void BindGridview()
    {
        try
        {
            int ddlvalue = Convert.ToInt32(ddlSearchCriteria.SelectedValue.ToString());
            string serachtext = string.Empty;
            if (txt_search.Text == "")
            {
                serachtext = "";
            }
            else
            {
                serachtext = txt_search.Text.ToString();
            }
            objCompany = new clsCompany();
            ds = objCompany.Company_fetch_history_Companywise(ddlvalue, serachtext);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_view_history.DataSource = ds.Tables[0];
                grd_view_history.DataBind();
            }
            else
            {
                ds.Dispose();
                grd_view_history.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }
    protected void grd_view_history_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_view_history.PageIndex = e.NewPageIndex;
        BindGridview();
    }
}
