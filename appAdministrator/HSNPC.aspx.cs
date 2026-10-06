using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.IO;
using System.Text;

public partial class appAdministrator_HSNPC : System.Web.UI.Page
{
    clsCompany objcompany;
    DataTable dt;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
           
            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
            

        
                if (Page.IsPostBack != true)
                {
                    //FILLING GRIDVIEW
                
                }
           
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
           
        }
        //End Read C
    }


    // Feth Data when Click Fill company data------------------



    public void Bind_listbox( int Type, string Product)
    {
        try
        {
            DataTable dt_master = new DataTable();
            objcompany = new clsCompany();
            dt_master = objcompany.fetch_master_product(Type, Product);
            if (dt_master.Rows.Count > 0)
            {
               
                //lst_box_HSCode.DataSource = dt_master;
                //lst_box_HSCode.DataTextField = "Productcode"+ " "+"Productdescription";
                //lst_box_HSCode.DataValueField = "Id";
                //lst_box_HSCode.DataBind();
                lst_box_HSCode.Items.Clear();
                for (int m = 0; m <= dt_master.Rows.Count - 1; m++)
                {
                      ListItem li=new ListItem(
                                                string.Concat(dt_master.Rows[m]["Productcode"].ToString(), " ",
                                                              dt_master.Rows[m]["Productdescription"].ToString()),
                                                              dt_master.Rows[m]["Id"].ToString());
                      lst_box_HSCode.Items.Add(li);
                }

                lbl_search_record_text.Text = "";
            }
               
            else
            {
                lbl_search_record_text.Text = "<span style='color:red;'>" + " No Record Found " + "</span>";
                lst_box_HSCode.DataBind();
                dt_master.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    public void bind_gridview_table()
    {
        try
        {
            lbl_message.Text = "";
            int type = 1;
            type = Convert.ToInt32(rdl_button.SelectedValue.ToString());
              string Product="";


              if (txt_product.Text.ToString() == "")
              {
                  Product = "";
              }
              else
              {
                  Product = txt_product.Text.ToString();

                 

              }

             

              DataTable dt_grd = new DataTable();
              objcompany = new clsCompany();
              dt_grd = objcompany.Fetch_Hscode_Company(Product, Convert.ToString(type));
              if (dt_grd.Rows.Count > 0)
              {
                  grd_view.DataSource = dt_grd;
                  grd_view.DataBind();

                  if (Product != "")
                  {
                      pnlpopup.Visible = true;
                      btn_submit.Visible = true;
                      if (type == 1)
                      {
                          hs_code_npc.Text = "HS CODE";
                      }
                      else
                      {
                          hs_code_npc.Text = "NPC CODE";
                      }
                     
                      Bind_listbox(type, Product);

                  }
                  else
                  {
                      hs_code_npc.Text = "";
                  }
              }
              else
              {
                  btn_submit.Visible = false;
                  pnlpopup.Visible = false;
                  grd_view.DataBind();
                  dt.Dispose();
              }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }
    void Popup(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
        }
        else
        {
            builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
        }
    }
    protected void grd_view_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "action")
        {
            string id = e.CommandArgument.ToString();
            if (objcompany == null)
            {
                objcompany = new clsCompany();

            }


          //  Popup(true);
        }

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        bind_gridview_table();
    }
    protected void on_update(object sender, EventArgs e)
    {

    }
    public void insert_update_hs_code()
    {
        lbl_message.Text = "";
        try
        {
            int CompanyID;
            string HSCode = "";
            int Type;
            string Companyid = "";
            if (Request.Form["conchk"] != null)
            {
                Companyid = Convert.ToString(Request.Form["conchk"].ToString());
            }

            


            foreach (ListItem item in lst_box_HSCode.Items)
            {
                if (item.Selected)
                {
                    string[] Hscodetext = item.Text.Split(' ');
                 //   for (int m = 0; m <= Hscodetext.Length - 1; m++)
                  //  {

                        HSCode = Hscodetext[0].ToString();
                  //  }
                 


                 //   HSCode = item.Text;

                }
            }

            Type = Convert.ToInt32(rdl_button.SelectedValue.ToString());
            if (Type == 1)
            {
                if (HSCode.ToString().Length > 4)
                {
                  
                    return;
                }
            }


            string[] Company_split = Companyid.Split(',');

            objcompany = new clsCompany();
            int K=1;
            for (int s = 0; s <= Company_split.Length - 1; s++)
            {
              K = objcompany.insert_upadte_HsCode(  Convert.ToInt32(Company_split[s].ToString()), HSCode, Type);
            }
            bind_gridview_table();
            if (K == 1)
            {
               


                lbl_message.Text = "<span style='color:red;'>" + "Data saved successfully" + "</span>";
                foreach (ListItem li in lst_box_HSCode.Items)
                {
                    li.Selected = false;
                }
              
                lst_box_HSCode.SelectedIndex = -1;
               // txt_product.Text = "";
                       
            }
            else
            {
                lbl_message.Text = "";
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        insert_update_hs_code();
    }
}
