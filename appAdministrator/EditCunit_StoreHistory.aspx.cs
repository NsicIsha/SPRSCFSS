using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Globalization;

public partial class appAdministrator_EditCunit_StoreHistory : System.Web.UI.Page
{
    clsCompany objCompany ;
    int CompanyID, CompanyID_Pk;
    int UserId, RoleId;
    string UserFullName;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
           
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            FunRequest();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            UserFullName = "";
        }
    }

    public void FunRequest()
    {
        try
        {
            if (Request.QueryString.HasKeys())
            {
                CompanyID = Convert.ToInt32(Request.QueryString["Company_ID"].ToString());
                CompanyID_Pk = Convert.ToInt32(Request.QueryString["CompanyIdPk"].ToString());

                Bind_Cunit_store_History(CompanyID, CompanyID_Pk);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }


    public void Bind_Cunit_store_History( int CompanyId, int CompanyId_Pk)
    {
        try
        {
            objCompany = new clsCompany();
            ds = new DataSet();
            ds = objCompany.Company_fetch_history_CunitStores(CompanyId, CompanyId_Pk);
            DataTable Mydt1 = new DataTable();
            Mydt1 = ds.Tables[0];

            DataSet ds_fetch_Cunit_Store = new DataSet();
            ds_fetch_Cunit_Store = objCompany.Company_UnitDetails_Fetch_ByCompanyId(CompanyId);
            if (Mydt1.Rows.Count > 0)
            {
                gvStoredetails.DataSource = Mydt1;
                gvStoredetails.DataBind();
            }
            else
            {
                Mydt1.Dispose();
                gvStoredetails.DataBind();
            }
            if (ds_fetch_Cunit_Store.Tables[1].Rows.Count > 0)
            {
                gvStoredetails_new.DataSource = ds_fetch_Cunit_Store.Tables[1];
                gvStoredetails_new.DataBind();
            }
            else
            {
                ds_fetch_Cunit_Store.Dispose();
                gvStoredetails_new.DataBind();
            }
           /* if (ds.Tables[0].Rows.Count > 0)
            {
                

                DataTable dt_Stores_details = new DataTable();
                dt_Stores_details = ds_fetch_Cunit_Store.Tables[1];// for Amedment
                DataTable dtmerge_storedetails = new DataTable();
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_Store_SNo", typeof(string));
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_1B_Factory_Address", typeof(string));
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_Store_DescriptionStoresProducts", typeof(string));
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_Store_RefOfBISSpecification", typeof(string));
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_Store_ModelBrand", typeof(string));
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_Store_LimitingSizeCapacityRating", typeof(string));
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_Store_AnnualProductionCapacity", typeof(string));
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_Store_NoOfShifts", typeof(string));
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_Store_HsCode", typeof(string));
                dtmerge_storedetails.Columns.Add("fGPtbl_CUnit_Store_Pmps", typeof(string));
                // there are  two column add only main datatable and others are operation
                DataRow add_addstore;
                //  for (int a = 0; a <= Mydt1.Rows.Count - 1; a++)
                //{
                //add_addstore = dtmerge_storedetails.NewRow();

                //  add_addstore["fGPtbl_CUnit_Store_SNo"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_SNo"].ToString();
                //add_addstore["fGPtbl_CUnit_1B_Factory_Address"] = Mydt1.Rows[a]["fGPtbl_CUnit_1B_Factory_Address"].ToString();
                for (int b = 0; b <= dt_Stores_details.Rows.Count - 1; b++)
                {
                    add_addstore = dtmerge_storedetails.NewRow();
                    int a = b;
                    add_addstore["fGPtbl_CUnit_1B_Factory_Address"] = Mydt1.Rows[a]["fGPtbl_CUnit_1B_Factory_Address"].ToString();

                    if (dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_SNo"].ToString() != Mydt1.Rows[a]["fGPtbl_CUnit_Store_SNo"].ToString())
                    {
                        add_addstore["fGPtbl_CUnit_Store_SNo"] = "<span style='color:red;'>" + dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_SNo"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt1.Rows[a]["fGPtbl_CUnit_Store_SNo"].ToString() + "</span>";
                    }
                    else
                    {
                        add_addstore["fGPtbl_CUnit_Store_SNo"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_SNo"].ToString();
                    }

                    if (dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_DescriptionStoresProducts"].ToString() != Mydt1.Rows[a]["fGPtbl_CUnit_Store_DescriptionStoresProducts"].ToString())
                    {
                        add_addstore["fGPtbl_CUnit_Store_DescriptionStoresProducts"] = "<span style='color:red;'>" + dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_DescriptionStoresProducts"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt1.Rows[a]["fGPtbl_CUnit_Store_DescriptionStoresProducts"].ToString() + "</span>";
                    }
                    else
                    {
                        add_addstore["fGPtbl_CUnit_Store_DescriptionStoresProducts"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_DescriptionStoresProducts"].ToString();
                    }
                    if (dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString() != Mydt1.Rows[a]["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString())
                    {
                        add_addstore["fGPtbl_CUnit_Store_RefOfBISSpecification"] = "<span style='color:red;'>" + dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt1.Rows[a]["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString() + "</span>";
                    }
                    else
                    {
                        add_addstore["fGPtbl_CUnit_Store_RefOfBISSpecification"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString();
                    }

                    if (dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_ModelBrand"].ToString() != Mydt1.Rows[a]["fGPtbl_CUnit_Store_ModelBrand"].ToString())
                    {
                        add_addstore["fGPtbl_CUnit_Store_ModelBrand"] = "<span style='color:red;'>" + dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_ModelBrand"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt1.Rows[a]["fGPtbl_CUnit_Store_ModelBrand"].ToString() + "</span>";
                    }
                    else
                    {
                        add_addstore["fGPtbl_CUnit_Store_ModelBrand"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_ModelBrand"].ToString();
                    }

                    if (dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_LimitingSizeCapacityRating"].ToString() != Mydt1.Rows[a]["fGPtbl_CUnit_Store_LimitingSizeCapacityRating"].ToString())
                    {
                        add_addstore["fGPtbl_CUnit_Store_LimitingSizeCapacityRating"] = "<span style='color:red;'>" + dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_LimitingSizeCapacityRating"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt1.Rows[a]["fGPtbl_CUnit_Store_LimitingSizeCapacityRating"].ToString() + "</span>";
                    }
                    else
                    {
                        add_addstore["fGPtbl_CUnit_Store_LimitingSizeCapacityRating"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_LimitingSizeCapacityRating"].ToString();
                    }

                    if (dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_AnnualProductionCapacity"].ToString() != Mydt1.Rows[a]["fGPtbl_CUnit_Store_AnnualProductionCapacity"].ToString())
                    {
                        add_addstore["fGPtbl_CUnit_Store_AnnualProductionCapacity"] = "<span style='color:red;'>" + dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_AnnualProductionCapacity"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt1.Rows[a]["fGPtbl_CUnit_Store_AnnualProductionCapacity"].ToString() + "</span>";
                    }
                    else
                    {
                        add_addstore["fGPtbl_CUnit_Store_AnnualProductionCapacity"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_AnnualProductionCapacity"].ToString();
                    }

                    if (dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_NoOfShifts"].ToString() != Mydt1.Rows[a]["fGPtbl_CUnit_Store_NoOfShifts"].ToString())
                    {
                        add_addstore["fGPtbl_CUnit_Store_NoOfShifts"] = "<span style='color:red;'>" + dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_NoOfShifts"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt1.Rows[a]["fGPtbl_CUnit_Store_NoOfShifts"].ToString() + "</span>";
                    }
                    else
                    {
                        add_addstore["fGPtbl_CUnit_Store_NoOfShifts"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_NoOfShifts"].ToString();
                    }
                    if (dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_HsCode"].ToString() != Mydt1.Rows[a]["fGPtbl_CUnit_Store_HsCode"].ToString())
                    {
                        add_addstore["fGPtbl_CUnit_Store_HsCode"] = "<span style='color:red;'>" + dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_HsCode"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt1.Rows[a]["fGPtbl_CUnit_Store_HsCode"].ToString() + "</span>";
                    }
                    else
                    {
                        add_addstore["fGPtbl_CUnit_Store_HsCode"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_HsCode"].ToString();
                    }
                    if (dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_Pmps"].ToString() != Mydt1.Rows[a]["fGPtbl_CUnit_Store_Pmps"].ToString())
                    {
                        add_addstore["fGPtbl_CUnit_Store_Pmps"] = "<span style='color:red;'>" + dt_Stores_details.Rows[b]["fGPtbl_CUnit_Store_Pmps"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt1.Rows[a]["fGPtbl_CUnit_Store_Pmps"].ToString() + "</span>";
                    }
                    else
                    {
                        add_addstore["fGPtbl_CUnit_Store_Pmps"] = Mydt1.Rows[a]["fGPtbl_CUnit_Store_Pmps"].ToString();
                    }

                    dtmerge_storedetails.Rows.Add(add_addstore);

                }
                //  dtmerge_storedetails.Rows.Add(add_addstore); 

                // }



                gvStoredetails.DataSource = dtmerge_storedetails;//Mydt1;
                gvStoredetails.DataBind();
            }
            else
            {
                ds.Dispose();
                gvStoredetails.DataBind();
            }*/
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }
}
