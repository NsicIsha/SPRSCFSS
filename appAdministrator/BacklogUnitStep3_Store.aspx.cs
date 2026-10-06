using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;
using System.Collections.Generic;
using System.Web.Services;
using System.Text;
using System.Web.Script.Services;
using System.Data.SqlClient;

[System.Web.Script.Services.ScriptService]
public partial class appAdministrator_BacklogUnitStep3_Store : System.Web.UI.Page
{

    clsCompany objCompany;

    int UserId, RoleId, DepartmentId;
    DataTable Mydt=new DataTable();
    DataTable dtRep;

    int successId;
    string UserFullName;
    string emailid;
    string filepath;
    string expression = string.Empty;
    int urlid = 0;//USED FOR FORM TYPE ID 1 OR 3 FOR REGISTRATION OR AMENDMENT
    int SuccessInspectionComplete, companyid;
    DataRow[] factoryrow;
    DataRow[] foundrow;

    public delegate DataTable RepeaterDelegate(int compid);

    public delegate DataTable GridviewDelgate(int compid, int userid);

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (RoleId == 8)
            {
                if (Request.QueryString["id"] != null)
                {
                    companyid = Convert.ToInt32(Request.QueryString["id"].ToString());
                }

                // BINDING STORE REPEATER
                objCompany = new clsCompany();

                //DECLARING MULTIPLE DELEGATES
                RepeaterDelegate repeatdelreg2;
                //System.Data.DataTable Dt = new System.Data.DataTable();

                //ASSIGNING FUNCTION TO DELEGATE
                repeatdelreg2 = objCompany.Company_Fetch_FactoryLocation;

                //Mydt = objCompany.Company_Fetch_FactoryLocation(companyid);
                //CALLING FUNCTION THROUGH DELEGATE
                Mydt = repeatdelreg2(companyid);
                hdn_enterprise.Value = Mydt.Rows[0]["fGPtbl_CUnit_Enterprise"].ToString();
                if (!IsPostBack)
                {
                    //TO BIND REPEATERS

                    //FILL REPEATER++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    try
                    {
                        

                        repeaterPmps.DataSource = Mydt;
                        repeaterPmps.DataBind();
                       
                        GridViewBind();
                      //  Bind_Searchgrid();
                    }
                    catch(Exception ex)
                    {
                        ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }

    }


    private void GridViewBind()
    {
        try
        {
            dtRep = new DataTable();
            clsCompanyBacklog objCompanyBacklog = new clsCompanyBacklog();
            mvStore.SetActiveView(viewGrid);

            dtRep = objCompanyBacklog.Company_Fetch_StoreByCompanyId(companyid);
            gvStoreDetails.DataSource = dtRep;
            gvStoreDetails.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            dtRep.Dispose();
        }

    }

    protected void btnAddStore_Click(object sender, EventArgs e)
    {
        var val = hdn_enterprise.Value;
        if (val == "1")
        {
            rdo_option1.Visible = false;
            rdo_option2.Visible = false;
            rdo_option3.Visible = false;
            rdo_option4.Visible = false;
            lbl_heading.Text = "Profile HS Code";
        }
        else if (val == "2")
        {

            rdo_option1.Visible = false;
            rdo_option2.Visible = false;
            rdo_option3.Visible = false;
            rdo_option4.Visible = false;
            lbl_heading.Text = "Profile NPC Code";
        }
        else
        {
            rdo_option1.Visible = true;
            rdo_option2.Visible = true;
            rdo_option3.Visible = true;
            rdo_option4.Visible = true;
            lbl_heading.Text = "Profile HS/NPC Code"; 
        }
        mvStore.SetActiveView(viewRepeater);
        RepeaterBind();
    }

    private void RepeaterBind()
    {
        try
        {
            dtRep = new DataTable();

            dtRep.Rows.Clear();
            dtRep.Columns.Clear();
            dtRep.Clear();

            dtRep.Columns.Add();
            dtRep.Columns.Add();
            dtRep.Columns.Add();
            dtRep.Columns.Add();
            dtRep.Columns.Add();
            dtRep.Columns.Add();
            dtRep.Columns.Add();
            dtRep.Columns.Add();
            dtRep.Columns.Add();
            dtRep.Columns.Add();
            dtRep.Columns.Add();

            int i = 0;
            while (i < 40)
            {
                dtRep.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                i++;
            }

            StoreRepeaterBranch.DataSource = dtRep;
            StoreRepeaterBranch.DataBind();

            mvStore.SetActiveView(viewRepeater);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }



    public void btnStoreSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            clsCompanyBacklog objCompanyBacklog = new clsCompanyBacklog();         

            TextBox description, specification, brand, rating, production, shift, quality, quantity, storecode;

            DropDownList ddlhscode, ddlfloc;
            HiddenField hfstoreid,hdnhscode,hdnnpcorhs ;
            string allvalue = string.Empty;
            //DICTIONARY TO ADD ALL ITEMS OF REPEATER
            Dictionary<int, string> vall = new Dictionary<int, string>();
            int key = 0;

            foreach (RepeaterItem item in StoreRepeaterBranch.Items)
            {

                //Hscode = (DropDownList)(item.FindControl("ddlHSCode"));

                description = (TextBox)(item.FindControl("txtDescription"));
                specification = (TextBox)(item.FindControl("txtBis"));
                brand = (TextBox)(item.FindControl("txtModel"));
                rating = (TextBox)(item.FindControl("txtLimiting"));
                production = (TextBox)(item.FindControl("txtAnnual"));
                shift = (TextBox)(item.FindControl("txtShift"));
                quality = (TextBox)(item.FindControl("txtQuality"));
                quantity = (TextBox)(item.FindControl("txtQuantity"));
            //    ddlhscode = (DropDownList)(item.FindControl("ddlHSCode"));
                ddlfloc = (DropDownList)(item.FindControl("ddlFactLoc"));
                storecode = (TextBox)(item.FindControl("txt_serial_no"));
                hdnhscode = (HiddenField)(item.FindControl("hdn_hstextbox"));
                string hscode = hdnhscode.Value;
                hdnnpcorhs = (HiddenField)(item.FindControl("hdn_hsornpc"));
                string hsornpc = "0";
                if (hdn_enterprise.Value == "3")
                {
                    if (hdnnpcorhs.Value != "")
                    {
                        hsornpc = hdnnpcorhs.Value;
                    }
                }
                else
                {
                    hsornpc = hdn_enterprise.Value;
                }
               



                //if (ddlhscode.SelectedIndex > 0)
                //{
                //    hscode = ddlhscode.SelectedValue.ToString();
                //}


                // CONCATENATE ALL VALUES IN A STRING
                //DELIMITER CHANGES FROM ',' TO '^'
                if (description.Text != "0")
                {
                    allvalue = ddlfloc.SelectedValue.ToString() + "^" + description.Text.Trim() + "^"
                       + specification.Text.Trim() + "^" + brand.Text.Trim() + "^" + rating.Text.Trim()
                       + "^" + production.Text.Trim() + "^" + shift.Text.Trim() + "^" + quality.Text.Trim()
                       + "^" + quantity.Text.Trim() + "^" + storecode.Text.Trim() + "^" + hscode + "^" + hsornpc + "@";

                    if (allvalue != "0^0^0^0^0^0^0^0^0^0^0^0@")
                    {
                        if (allvalue != ddlfloc.SelectedValue.ToString() + "^0^0^0^0^0^0^0^0^0^0^0@")
                        {
                            vall.Add(key, allvalue);
                            key++;
                        }
                    }
                }
               

               
            }
            allvalue = string.Empty;
            HiddenField Hfunitidpmps;
            RadioButtonList rdlist;
            //DICTIONARY TO ADD ALL ITEMS OF REPEATER
            Dictionary<int, string> vallpmps = new Dictionary<int, string>();
            key = 0;

            foreach (RepeaterItem item in repeaterPmps.Items)
            {

                //Hscode = (DropDownList)(item.FindControl("ddlHSCode"));

                Hfunitidpmps = (HiddenField)(item.FindControl("hfUnitidpmps"));

                rdlist = (RadioButtonList)(item.FindControl("radlist"));

                // CONCATENATE CHECKBOX VALUES IN A STRING
                if (rdlist.SelectedItem.Value == "Ps")
                {
                    allvalue = Hfunitidpmps.Value + ",ps@";
                }
                else if (rdlist.SelectedItem.Value == "Pmps")
                {
                    allvalue = Hfunitidpmps.Value + ",pmps@";
                }
                else if (rdlist.SelectedItem.Value == "PA")
                {
                    allvalue = Hfunitidpmps.Value + ",PA@";
                }

                vallpmps.Add(key, allvalue);
                key++;
            }
            string comid = "0";
            StringBuilder str = new StringBuilder();
            if (Request.QueryString.HasKeys())
            {
                comid = Request.QueryString["id"].ToString();
                str.Append("<CompanyHSData>");
                //if (Request.Form[txt_hscode1.UniqueID] != "")
                //{
                    str.Append("<Company>");
                  str.Append("<ID>1</ID>");
                  str.Append("<HSCODE>" + Request.Form[txt_hscode1.UniqueID] + "</HSCODE>");
                    if (hdn_enterprise.Value == "3")
                    {
                        str.Append("<HSTYPE>" + rdo_option1.SelectedValue + "</HSTYPE>");
                    }
                    else
                    {
                        str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
                    }
                    str.Append("</Company>");
               // }
                // if (Request.Form[txt_hscode2.UniqueID] != "")
                //{
                    str.Append("<Company>");
                 str.Append("<ID>2</ID>");
                 str.Append("<HSCODE>" + Request.Form[txt_hscode2.UniqueID] + "</HSCODE>");
                    if (hdn_enterprise.Value == "3")
                    {
                        str.Append("<HSTYPE>" + rdo_option2.SelectedValue + "</HSTYPE>");
                    }
                    else
                    {
                        str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
                    }
                    str.Append("</Company>");
                //}
                // if (Request.Form[txt_hscode3.UniqueID] != "")
                //{
                   str.Append("<Company>");
                 str.Append("<ID>3</ID>");
                 str.Append("<HSCODE>" + Request.Form[txt_hscode3.UniqueID] + "</HSCODE>");
                    if (hdn_enterprise.Value == "3")
                    {
                        str.Append("<HSTYPE>" + rdo_option3.SelectedValue + "</HSTYPE>");
                    }
                    else
                    {
                        str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
                    }
                    str.Append("</Company>");
                //}
                // if (Request.Form[txt_hscode4.UniqueID] != "")
                //{
                    str.Append("<Company>");
                str.Append("<ID>4</ID>");
                str.Append("<HSCODE>" + Request.Form[txt_hscode4.UniqueID] + "</HSCODE>");
                    if (hdn_enterprise.Value == "3")
                    {
                        str.Append("<HSTYPE>" + rdo_option4.SelectedValue + "</HSTYPE>");
                    }
                    else
                    {
                        str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
                    }
                    str.Append("</Company>");
              //  }
                str.Append("</CompanyHSData>");
                if (vall.Values != null)
                {
                    try
                    {
                       
                       Store_Insert_Modified(vall.Values, vallpmps.Values, UserId, str.ToString(), comid);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                   
                }
            }

           

         

            

            GridViewBind();

            //=======================================================

/*
            if (successId == 2)
            {
                 Response.Redirect("ICRegistration.aspx?urlid=1", false);
            }
 * */
           
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        

    }

    private void Store_Insert_Modified(Dictionary<int, string>.ValueCollection vall1, Dictionary<int, string>.ValueCollection vallpmps, int userid, string str, string companyID)
    {
        SqlCommand MySqlComm;

        try
        {
            string allval = string.Empty, allvalpmps = string.Empty;//, all1;
            foreach (string s in vall1)
            {
                allval += s;
            }
            //all1 = allval;

            foreach (string s1 in vallpmps)
            {
                allvalpmps += s1;
            }
            
            //MySqlComm = new SqlCommand("GPsp_Company_Insert_Store_Step3_All_Modified_Backlog", DBManager.ConnectToDB());
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Store_Step3_All_Modified_Backlog_27feb", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@AllStores", SqlDbType.Text).Value = allval;
            MySqlComm.Parameters.Add("@AllPmps", SqlDbType.VarChar, 1000).Value = allvalpmps;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@xmldata", SqlDbType.Xml).Value = str;
            MySqlComm.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Convert.ToInt32(companyID);

            SqlParameter outVal = new SqlParameter("@ErrorId", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();

            int errorid = Convert.ToInt32(outVal.Value);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
          
        }

    }

    protected void StoreRepeaterBranch_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataTable dt = new DataTable();

        try
        {
            dt = clsDDL.ddlHscode();
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
             //   DropDownList ddlhscode = (DropDownList)e.Item.FindControl("ddlHSCode");
                DropDownList ddlfaclo = (DropDownList)e.Item.FindControl("ddlFactLoc");

                //System.Data.DataTable dt = new System.Data.DataTable();
                //dt = clsDDL.ddlHscode();

                //ddlhscode.DataSource = dt;
                //ddlhscode.DataTextField = dt.Columns["fGPtbl_HS_HScode"].ToString();
                //ddlhscode.DataValueField = dt.Columns["fGPtbl_HS_HScode"].ToString();
                //ddlhscode.DataBind();
                //ddlhscode.Items.Insert(0, new ListItem("--Select--", ""));
                RadioButtonList rdo = (RadioButtonList)e.Item.FindControl("rdooption_repeater");
                //Button btn = (Button)e.Item.FindControl("btn_search_repeater");
                //btn.Attributes.Add("onClick", "javascript:ShowPopup();");

                if (hdn_enterprise.Value != "3")
                {
                    rdo.Visible = false;
                
                }
                else
                {
                    //if (hdn_enterprise.Value == "1")
                    //{
                    //    rdo.SelectedValue = "1";
                    //}
                    //else
                    //{
                    //    rdo.SelectedValue = "2";
                    //}
                    rdo.Visible = true;
                }

                dt.Clear();
                dt.Dispose();

                ddlfaclo.DataSource = Mydt;
                ddlfaclo.DataTextField = Mydt.Columns["fAddress"].ToString();
                ddlfaclo.DataValueField = Mydt.Columns["fGPtbl_CUnit_Id"].ToString();
                ddlfaclo.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }


    }

    
    

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //TO RESTRICT USER TO FILL STORE DETAILS
            dtRep = new DataTable();
            objCompany = new clsCompany();
            //mvStore.SetActiveView(viewGrid);
            dtRep = objCompany.Company_Fetch_StoreByCompanyId(companyid);
            if (dtRep.Rows.Count > 0)
            {
                Response.Redirect("BacklogUnit_data.aspx?id=" + companyid.ToString(), false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }


    protected void gvStoreDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            string strtext = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                dtRep = new DataTable();
                objCompany = new clsCompany();

                dtRep = objCompany.Company_Fetch_FactoryLocation(companyid);


                HiddenField hdnuntid = (HiddenField)e.Row.FindControl("hdnUnitid");
                if (hdnuntid != null)
                {
                    Literal location = (Literal)e.Row.FindControl("ltlLocation");
                    string expression = "fGPtbl_CUnit_Id=" + hdnuntid.Value.ToString();

                    foundrow = dtRep.Select(expression);
                    location.Text = foundrow[0][1].ToString();
                    
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in gvStoreDetails_RowDataBound() " + ex.Message);
        }
    }

   
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static HsCodeList[] Bind_Searchgrid(string searchitem,string codetype)
    {
        DataTable dt = new DataTable();
        List<HsCodeList> details = new List<HsCodeList>();
    //    clsCompanyBacklog objback = new clsCompanyBacklog();
        string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        SqlConnection con = new SqlConnection(connectionstr);
        try
        {
            SqlCommand cmd = new SqlCommand("GP_sp_searchHSCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Product", SqlDbType.VarChar, 5000).Value = searchitem == "" ? null : searchitem;
            cmd.Parameters.Add("@tppe", SqlDbType.Int).Value = codetype == "" ? null : codetype;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
             if (dt != null)
             {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dtrow in dt.Rows)
                {
                    HsCodeList hs = new HsCodeList();
                    hs.HSID = dtrow["Id"].ToString();
                    hs.HSCode = dtrow["Productcode"].ToString();
                    hs.HSCodedescription = dtrow["Productdescription"].ToString();
                    details.Add(hs);
                }
            }
        }
        }
        catch (Exception ex)
        { 
        
        }
     return details.ToArray();
       
        }
     
 


    public class HsCodeList
    {
        public string HSID { get; set; }
        public string HSCode { get; set; }
        public string HSCodedescription { get; set; }
    }


}
