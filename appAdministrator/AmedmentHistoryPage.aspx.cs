using NSICGP_VBClassLibrary;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class appAdministrator_AmedmentHistoryPage : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    int UserId, RoleId, companyId, DepartmentId, UnitId, Status, BranchId, FormType;
    //  int successId, urlId = 0, amendmentid;
    int Unitid, urlId = 1;
    int Amedmentid;
    string filepath;
    int storecount = 0, GridFactoryCount = 0, FactoryDetailsCount = 0,
        CapitalDetailsCount = 0, TechnicalDetailsCount = 0;

    string UserFullName;
    string AmedmentDate = "";

    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            // DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
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
                companyId = Convert.ToInt32(Request.QueryString["Company_ID"].ToString());
                AmedmentDate = Convert.ToString(Request.QueryString["Date"].ToString());
                Amedmentid = Convert.ToInt32(Request.QueryString["Amedmentid"].ToString());
                Showdata(companyId, Amedmentid);

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    public void Showdata(int CompanyId, int AmedmentID)
    {
        try
        {
            objCompany = new clsCompany();
            objAmendment = new clsAmendment();
            DataSet Myds = new DataSet();
            System.Data.SqlClient.SqlDataReader MydrSSI;
            System.Data.SqlClient.SqlDataReader MydrDIR;

            DataTable Mydt, Mydt1, Mydt2, Mydt3, Mydt4 = new DataTable();

            objCompany = new clsCompany();
            DataSet chekamedment = new DataSet();
            string Amendmentchek = "";
            chekamedment =  objCompany.Company_fetch_Chek_amedment_firsttime(companyId, Amedmentid);
            if (chekamedment.Tables[0].Rows.Count > 0)
            {
                Amendmentchek = chekamedment.Tables[0].Rows[0]["Amedment"].ToString();
            }
            else
            {
                chekamedment.Dispose();
            }
            //string date = AmedmentDate;
            //string date  = DateTime.ParseExact( AmedmentDate, "yyyy-dd-mm hh:mm:tt", CultureInfo.InvariantCulture).ToString();
            // if (Amendmentchek == "Not Amedment")
            // {
            // Myds = objCompany.Company_fetch_history_AmedmentChange_new_value_historytable(companyId);
            Myds = objCompany.Company_fetch_history_AmedmentChange_new_value_historytable_modified(companyId, AmedmentID);
            // }
            //  else
            // {
            // Myds = objCompany.Company_UnitDetails_Fetch_ByCompanyId(CompanyId);
            // }


            Mydt = Myds.Tables[0];
            Mydt1 = Myds.Tables[1];
            Mydt2 = Myds.Tables[2];
            Mydt3 = Myds.Tables[3];
            Mydt4 = Myds.Tables[4];


            //  ds = objCompany.Company_fetch_history_AmedmentChange_new_value(companyId, AmedmentID);
            ds = objCompany.Company_UnitDetails_Fetch_ByCompanyId(companyId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblPAN.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
                lblCompany.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
                lblAddress.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
                lblPhone.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString();
                lblFax.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString();
                lblEmail.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();
                lblCity.Text = ds.Tables[0].Rows[0]["CompanyCity"].ToString();
                lblState.Text = ds.Tables[0].Rows[0]["CompanyState"].ToString();
                lblPin.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString();
                // Changes Table history 
                //  Showdata_history(companyId, Amedmentid);


                if ((lblPAN.Text != Mydt.Rows[0]["fGPtbl_Company_PAN"].ToString()))
                {
                    lblPAN.ForeColor = Color.Red;
                    lblPAN.Text = lblPAN.Text + "" + "<span style='ForeColor:blue;'>" + Mydt.Rows[0]["fGPtbl_Company_PAN"].ToString() + "</span>";
                }
                else
                {
                    lblPAN.Text = Mydt.Rows[0]["fGPtbl_Company_PAN"].ToString();
                }
                if ((lblCompany.Text != Mydt.Rows[0]["fGPtbl_Company_Name"].ToString()))
                {
                    // lblCompany.ForeColor = Color.Red;
                    lblCompany.Text = "<span style='color:Red;'>" + ds.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_Company_Name"].ToString() + "</span>";
                    //lblCompany.Text = "<span style='color:Red;'>" + Mydt.Rows[0]["fGPtbl_Company_Name"].ToString() + "</span>" + "<br/>" + "<span style='color:Blue;'>" + ds.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString() + "</span>";
                }
                else
                {
                    lblCompany.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
                }

                if ((ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString() != Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString()))
                {
                    lblAddress.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString() + "</span>";
                    //lblAddress.Text = "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString() + "</span>" + " <br/> " + "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString() + "</span>";
                }
                else
                {
                    lblAddress.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
                }

                if (ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString() != Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString())
                {
                    lblPin.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString() + "</span>";
                }
                else
                {
                    lblPin.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString();
                }


                if (ds.Tables[0].Rows[0]["CompanyState"].ToString() != Mydt.Rows[0]["CompanyState"].ToString())
                {
                    lblState.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["CompanyState"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["CompanyState"].ToString() + "</span>";
                }
                else
                {
                    lblState.Text = Mydt.Rows[0]["CompanyState"].ToString();
                }


                if (ds.Tables[0].Rows[0]["CompanyCity"].ToString() != Mydt.Rows[0]["CompanyCity"].ToString())
                {
                    lblCity.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["CompanyCity"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["CompanyCity"].ToString() + "</span>";
                }
                else
                {
                    lblCity.Text = Mydt.Rows[0]["CompanyCity"].ToString();
                }


                if (ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString() != Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString())
                {
                    lblPhone.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString() + "</span>";
                }
                else
                {
                    lblPhone.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString();
                }


                if (ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString() != Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString())
                {
                    lblFax.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString() + "</span>";
                }
                else
                {
                    lblFax.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString();
                }



                if (ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString() != Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString())
                {
                    lblEmail.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString() + "</span>";
                }
                else
                {
                    lblEmail.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();
                }


                if (Myds.Tables[0].Rows.Count > 0)
                {
                    if (Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"].ToString() != null)
                    {
                        Unitid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"]);
                    }
                    else
                    {

                    }
                }

                /* try
                 {
                     //BINDING STORE DETAILS GRIDVIEW
                     DataTable dt_Stores_details = new DataTable();
                     dt_Stores_details = ds.Tables[1];// for Amedment
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
                 catch { }*/
                try
                {
                    gvStoredetails_new.DataSource = ds.Tables[1];
                    gvStoredetails_new.DataBind();
                    gvStoredetails.DataSource = Mydt1;
                    gvStoredetails.DataBind();

                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
                }
                try
                {
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        GriFactory1New.DataSource = ds.Tables[3];
                        GriFactory1New.DataBind();
                    }
                    else
                    {
                        GriFactory1New.DataBind();
                    }
                    if (Mydt3.Rows.Count > 0)
                    {
                        GridFactory.DataSource = Mydt3;
                        GridFactory.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
                }

                /*  try
                   {
                        DataTable dtFactory_bind=new DataTable();
                      dtFactory_bind = ds.Tables[3]; // For amedment
                      DataTable dtFactory_bindfactory_merge = new DataTable();
                      dtFactory_bindfactory_merge.Columns.Add("fGPtbl_CUnit_1B_Factory_Address", typeof(string));
                      dtFactory_bindfactory_merge.Columns.Add("fGPtbl_CUnit_1B_Factory_Zip", typeof(string));
                      dtFactory_bindfactory_merge.Columns.Add("UnitState", typeof(string));
                      dtFactory_bindfactory_merge.Columns.Add("UnitCity", typeof(string));
                      dtFactory_bindfactory_merge.Columns.Add("fGPtbl_CUnit_1B_Factory_Phone", typeof(string));
                      dtFactory_bindfactory_merge.Columns.Add("fGPtbl_CUnit_1B_Factory_Fax", typeof(string));
                      dtFactory_bindfactory_merge.Columns.Add("fGPtbl_CUnit_1B_Factory_Email", typeof(string));
                      DataRow addw;


                     // getDifferentRecords(dtFactory_bind, Mydt3);
                      //for (int k = 0; k <= Mydt3.Rows.Count - 1; k++)
                      //{

                      for (int m = 0; m <= dtFactory_bind.Rows.Count - 1; m++)
                          {
                              addw = dtFactory_bindfactory_merge.NewRow();
                          for (int k = 0; k < Mydt3.Rows.Count - 1 ;  k++)
                             {
                             // int k = m;

                              if (k==m)
                                   {

                                  if (Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Address"].ToString() != dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Address"].ToString())
                                  {

                                      addw["fGPtbl_CUnit_1B_Factory_Address"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Address"].ToString() + "</span>";
                                  }
                                  else
                                  {
                                      addw["fGPtbl_CUnit_1B_Factory_Address"] = Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Address"].ToString();
                                  }
                                  if (dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Zip"].ToString() != Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Zip"].ToString())
                                  {
                                      addw["fGPtbl_CUnit_1B_Factory_Zip"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Zip"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Zip"].ToString() + "</span>";
                                  }
                                  else
                                  {
                                      addw["fGPtbl_CUnit_1B_Factory_Zip"] = Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Zip"].ToString();
                                  }

                                  if (dtFactory_bind.Rows[m]["UnitState"].ToString() != Mydt3.Rows[k]["UnitState"].ToString())
                                  {
                                      addw["UnitState"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["UnitState"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt3.Rows[k]["UnitState"].ToString() + "</span>";
                                  }
                                  else
                                  {
                                      addw["UnitState"] = Mydt3.Rows[k]["UnitState"].ToString();
                                  }
                                  if (dtFactory_bind.Rows[m]["UnitCity"].ToString() != Mydt3.Rows[k]["UnitCity"].ToString())
                                  {
                                      addw["UnitCity"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["UnitCity"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt3.Rows[k]["UnitCity"].ToString() + "</span>";
                                  }
                                  else
                                  {
                                      addw["UnitCity"] = Mydt3.Rows[k]["UnitCity"].ToString();
                                  }
                                  if (dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Phone"].ToString() != Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Phone"].ToString())
                                  {
                                      addw["fGPtbl_CUnit_1B_Factory_Phone"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Phone"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Phone"].ToString() + "</span>";
                                  }
                                  else
                                  {
                                      addw["fGPtbl_CUnit_1B_Factory_Phone"] = Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Phone"].ToString();
                                  }
                                  if (dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Fax"].ToString() != Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Fax"].ToString())
                                  {
                                      addw["fGPtbl_CUnit_1B_Factory_Fax"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Fax"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Fax"].ToString() + "</span>";
                                  }
                                  else
                                  {
                                      addw["fGPtbl_CUnit_1B_Factory_Fax"] = Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Fax"].ToString();
                                  }
                                  if (dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Email"].ToString() != Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Email"].ToString())
                                  {
                                      addw["fGPtbl_CUnit_1B_Factory_Email"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Email"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Email"].ToString() + "</span>";
                                  }
                                  else
                                  {
                                      addw["fGPtbl_CUnit_1B_Factory_Email"] = Mydt3.Rows[k]["fGPtbl_CUnit_1B_Factory_Email"].ToString();
                                  }
                                  //k++;    
                            }


                          else
                                   {
                                       addw["fGPtbl_CUnit_1B_Factory_Address"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Address"].ToString() + "</span>";
                                       addw["fGPtbl_CUnit_1B_Factory_Zip"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Zip"].ToString() + "</span>";
                                       addw["UnitState"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["UnitState"].ToString() + "</span>";
                                       addw["UnitCity"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["UnitCity"].ToString() + "</span>";
                                       addw["fGPtbl_CUnit_1B_Factory_Phone"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Phone"].ToString() + "</span>";
                                       addw["fGPtbl_CUnit_1B_Factory_Fax"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Fax"].ToString() + "</span>";
                                       addw["fGPtbl_CUnit_1B_Factory_Email"] = "<span style='color:red;'>" + dtFactory_bind.Rows[m]["fGPtbl_CUnit_1B_Factory_Email"].ToString() + "</span>";
                                   }
                            // }

                              dtFactory_bindfactory_merge.Rows.Add(addw);     
                          }

                     }




                      GridFactory.DataSource = dtFactory_bindfactory_merge; //Mydt3;// Myds.Tables[3];original
                       GridFactory.DataBind();
                   }
                   catch { }*/
                //BINDING HORSE POWER GRIDVIEW
                /*  try
                  {
                      DataTable dtFactoryDetails_bind = new DataTable();
                      dtFactoryDetails_bind = ds.Tables[2]; // For amedment
                      DataTable dtFactory_bindfactory_mergeFactory_Datails = new DataTable();
                      dtFactory_bindfactory_mergeFactory_Datails.Columns.Add("fGPtbl_CUnit_1B_Factory_Address", typeof(string));
                      dtFactory_bindfactory_mergeFactory_Datails.Columns.Add("fGPtbl_CUnit_5A1", typeof(string));
                      dtFactory_bindfactory_mergeFactory_Datails.Columns.Add("fGPtbl_CUnit_5A2", typeof(string));
                      dtFactory_bindfactory_mergeFactory_Datails.Columns.Add("fGPtbl_CUnit_5BB1", typeof(string));
                      dtFactory_bindfactory_mergeFactory_Datails.Columns.Add("fGPtbl_CUnit_5BB2", typeof(string));
                      dtFactory_bindfactory_mergeFactory_Datails.Columns.Add("fGPtbl_CUnit_5C", typeof(string));
                      DataRow drfactory;
                      //for (int c = 0; c <= Mydt2.Rows.Count - 1; c++)
                     // {

                          for (int d = 0; d <=dtFactoryDetails_bind.Rows.Count - 1; d++)
                          {
                              drfactory = dtFactory_bindfactory_mergeFactory_Datails.NewRow();
                              int c = d;
                              if (Mydt2.Rows[c]["fGPtbl_CUnit_1B_Factory_Address"].ToString() != dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_1B_Factory_Address"].ToString())
                              {

                                  drfactory["fGPtbl_CUnit_1B_Factory_Address"] = "<span style='color:red;'>" + dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_1B_Factory_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[c]["fGPtbl_CUnit_1B_Factory_Address"].ToString() + "</span>";
                              }
                              else
                              {
                                  drfactory["fGPtbl_CUnit_1B_Factory_Address"] = Mydt2.Rows[d]["fGPtbl_CUnit_1B_Factory_Address"].ToString();
                              }

                              if (Mydt2.Rows[c]["fGPtbl_CUnit_5A1"].ToString() != dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5A1"].ToString())
                              {

                                  drfactory["fGPtbl_CUnit_5A1"] = "<span style='color:red;'>" + dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5A1"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[c]["fGPtbl_CUnit_5A1"].ToString() + "</span>";
                              }
                              else
                              {
                                  drfactory["fGPtbl_CUnit_5A1"] = Mydt2.Rows[d]["fGPtbl_CUnit_5A1"].ToString();
                              }
                              if (Mydt2.Rows[c]["fGPtbl_CUnit_5A2"].ToString() != dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5A2"].ToString())
                              {

                                  drfactory["fGPtbl_CUnit_5A2"] = "<span style='color:red;'>" + dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5A2"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[c]["fGPtbl_CUnit_5A2"].ToString() + "</span>";
                              }
                              else
                              {
                                  drfactory["fGPtbl_CUnit_5A2"] = Mydt2.Rows[d]["fGPtbl_CUnit_5A2"].ToString();
                              }

                              if (Mydt2.Rows[c]["fGPtbl_CUnit_5BB1"].ToString() != dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5BB1"].ToString())
                              {

                                  drfactory["fGPtbl_CUnit_5BB1"] = "<span style='color:red;'>" + dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5BB1"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[c]["fGPtbl_CUnit_5BB1"].ToString() + "</span>";
                              }
                              else
                              {
                                  drfactory["fGPtbl_CUnit_5BB1"] = Mydt2.Rows[d]["fGPtbl_CUnit_5BB1"].ToString();
                              }

                              if (Mydt2.Rows[c]["fGPtbl_CUnit_5BB2"].ToString() != dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5BB2"].ToString())
                              {

                                  drfactory["fGPtbl_CUnit_5BB2"] = "<span style='color:red;'>" + dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5BB2"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[c]["fGPtbl_CUnit_5BB2"].ToString() + "</span>";
                              }
                              else
                              {
                                  drfactory["fGPtbl_CUnit_5BB2"] = Mydt2.Rows[d]["fGPtbl_CUnit_5BB2"].ToString();
                              }
                              if (Mydt2.Rows[c]["fGPtbl_CUnit_5C"].ToString() != dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5C"].ToString())
                              {

                                  drfactory["fGPtbl_CUnit_5C"] = "<span style='color:red;'>" + dtFactoryDetails_bind.Rows[d]["fGPtbl_CUnit_5C"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[c]["fGPtbl_CUnit_5C"].ToString() + "</span>";
                              }
                              else
                              {
                                  drfactory["fGPtbl_CUnit_5C"] = Mydt2.Rows[d]["fGPtbl_CUnit_5C"].ToString();
                              }
                              dtFactory_bindfactory_mergeFactory_Datails.Rows.Add(drfactory);
                          }
                       //   dtFactory_bindfactory_mergeFactory_Datails.Rows.Add(drfactory); 

                      //}


                      gvFactoryDetails.DataSource = dtFactory_bindfactory_mergeFactory_Datails;//Mydt2;// Myds.Tables[2];
                      gvFactoryDetails.DataBind();
                  }
                  catch { }*/
                try
                {

                    gvFactoryDetails.DataSource = Mydt2;
                    gvFactoryDetails.DataBind();
                    gvFactoryDetails_new.DataSource = ds.Tables[2];
                    gvFactoryDetails_new.DataBind();

                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
                }

                //BINDING CAPITAL DETAILS GRIDVIEW
                /*  try
                  {
                      DataTable dtCapitalDetails_bind = new DataTable();
                      dtCapitalDetails_bind = ds.Tables[2]; // For amedment
                      DataTable dtFactory_bindCapital_merge_Datails = new DataTable();
                      dtFactory_bindCapital_merge_Datails.Columns.Add("fGPtbl_CUnit_1B_Factory_Address", typeof(string));
                      dtFactory_bindCapital_merge_Datails.Columns.Add("fGPtbl_CUnit_6A1", typeof(string));
                      dtFactory_bindCapital_merge_Datails.Columns.Add("fGPtbl_CUnit_6A2", typeof(string));
                      dtFactory_bindCapital_merge_Datails.Columns.Add("fGPtbl_CUnit_6B", typeof(string));

                      DataRow drCapital;
                     // for (int f = 0; f <= Mydt2.Rows.Count - 1; f++)
                      //{
                        //  drCapital = dtFactory_bindCapital_merge_Datails.NewRow();
                          for (int h = 0; h <= dtCapitalDetails_bind.Rows.Count - 1; h++)
                          {
                              drCapital = dtFactory_bindCapital_merge_Datails.NewRow();
                              int f = h;
                              if (Mydt2.Rows[f]["fGPtbl_CUnit_1B_Factory_Address"].ToString() != dtCapitalDetails_bind.Rows[h]["fGPtbl_CUnit_1B_Factory_Address"].ToString())
                              {

                                  drCapital["fGPtbl_CUnit_1B_Factory_Address"] = "<span style='color:red;'>" + dtCapitalDetails_bind.Rows[h]["fGPtbl_CUnit_1B_Factory_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[f]["fGPtbl_CUnit_1B_Factory_Address"].ToString() + "</span>";
                              }
                              else
                              {
                                  drCapital["fGPtbl_CUnit_1B_Factory_Address"] = Mydt2.Rows[f]["fGPtbl_CUnit_1B_Factory_Address"].ToString();
                              }
                              if (Mydt2.Rows[f]["fGPtbl_CUnit_6A1"].ToString() != dtCapitalDetails_bind.Rows[h]["fGPtbl_CUnit_6A1"].ToString())
                              {

                                  drCapital["fGPtbl_CUnit_6A1"] = "<span style='color:red;'>" + dtCapitalDetails_bind.Rows[h]["fGPtbl_CUnit_6A1"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[f]["fGPtbl_CUnit_6A1"].ToString() + "</span>";
                              }
                              else
                              {
                                  drCapital["fGPtbl_CUnit_6A1"] = Mydt2.Rows[f]["fGPtbl_CUnit_6A1"].ToString();
                              }

                              if (Mydt2.Rows[f]["fGPtbl_CUnit_6A2"].ToString() != dtCapitalDetails_bind.Rows[h]["fGPtbl_CUnit_6A2"].ToString())
                              {

                                  drCapital["fGPtbl_CUnit_6A2"] = "<span style='color:red;'>" + dtCapitalDetails_bind.Rows[h]["fGPtbl_CUnit_6A2"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[f]["fGPtbl_CUnit_6A2"].ToString() + "</span>";
                              }
                              else
                              {
                                  drCapital["fGPtbl_CUnit_6A2"] = Mydt2.Rows[f]["fGPtbl_CUnit_6A2"].ToString();
                              }

                              if (Mydt2.Rows[f]["fGPtbl_CUnit_6B"].ToString() != dtCapitalDetails_bind.Rows[h]["fGPtbl_CUnit_6B"].ToString())
                              {

                                  drCapital["fGPtbl_CUnit_6B"] = "<span style='color:red;'>" + dtCapitalDetails_bind.Rows[h]["fGPtbl_CUnit_6B"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[f]["fGPtbl_CUnit_6B"].ToString() + "</span>";
                              }
                              else
                              {
                                  drCapital["fGPtbl_CUnit_6B"] = Mydt2.Rows[f]["fGPtbl_CUnit_6B"].ToString();
                              }
                              dtFactory_bindCapital_merge_Datails.Rows.Add(drCapital);
                          }
                         // dtFactory_bindCapital_merge_Datails.Rows.Add(drCapital);
                      //}



                      gvCapitalDetails.DataSource = dtFactory_bindCapital_merge_Datails; //Mydt2;// Myds.Tables[2];
                      gvCapitalDetails.DataBind();
                  }
                  catch { }*/

                try
                {
                    gvCapitalDetails.DataSource = Mydt2;// Myds.Tables[2];
                    gvCapitalDetails.DataBind();
                    gvFactoryDetails_new.DataSource = ds.Tables[2];
                    gvFactoryDetails_new.DataBind();
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
                }

                //BINDING TECHNICAL DETAILS GRIDVIEW
                /*  try
                  {
                      DataTable dttechnical_bind = new DataTable();
                      dttechnical_bind = ds.Tables[2]; // For amedment
                      DataTable dttechnical_merge_Datails = new DataTable();
                      dttechnical_merge_Datails.Columns.Add("fGPtbl_CUnit_1B_Factory_Address", typeof(string));
                      dttechnical_merge_Datails.Columns.Add("fGPtbl_CUnit_7A", typeof(string));
                      dttechnical_merge_Datails.Columns.Add("fGPtbl_CUnit_7B", typeof(string));
                      dttechnical_merge_Datails.Columns.Add("fGPtbl_CUnit_7C", typeof(string));
                      dttechnical_merge_Datails.Columns.Add("fGPtbl_CUnit_7D", typeof(string));
                      dttechnical_merge_Datails.Columns.Add("fGPtbl_CUnit_7E", typeof(string));
                      DataRow drtechnical;
                    //  for (int p = 0; p <= Mydt2.Rows.Count - 1; p++)
                      //{

                          for (int q = 0; q <= dttechnical_bind.Rows.Count - 1; q++)
                          {
                              drtechnical = dttechnical_merge_Datails.NewRow();
                              int p = q;
                              if (Mydt2.Rows[p]["fGPtbl_CUnit_1B_Factory_Address"].ToString() != dttechnical_bind.Rows[q]["fGPtbl_CUnit_1B_Factory_Address"].ToString())
                              {


                                  drtechnical["fGPtbl_CUnit_1B_Factory_Address"] = "<span style='color:red;'>" + dttechnical_bind.Rows[q]["fGPtbl_CUnit_1B_Factory_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[p]["fGPtbl_CUnit_1B_Factory_Address"].ToString() + "</span>";
                              }
                              else
                              {
                                  drtechnical["fGPtbl_CUnit_1B_Factory_Address"] = Mydt2.Rows[p]["fGPtbl_CUnit_1B_Factory_Address"].ToString();
                              }

                              if (Mydt2.Rows[p]["fGPtbl_CUnit_7A"].ToString() != dttechnical_bind.Rows[q]["fGPtbl_CUnit_7A"].ToString())
                              {

                                  drtechnical["fGPtbl_CUnit_7A"] = "<span style='color:red;'>" + dttechnical_bind.Rows[q]["fGPtbl_CUnit_7A"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[p]["fGPtbl_CUnit_7A"].ToString() + "</span>";
                              }
                              else
                              {
                                  drtechnical["fGPtbl_CUnit_7A"] = Mydt2.Rows[p]["fGPtbl_CUnit_7A"].ToString();
                              }

                              if (Mydt2.Rows[p]["fGPtbl_CUnit_7B"].ToString() != dttechnical_bind.Rows[q]["fGPtbl_CUnit_7B"].ToString())
                              {

                                  drtechnical["fGPtbl_CUnit_7B"] = "<span style='color:red;'>" + dttechnical_bind.Rows[q]["fGPtbl_CUnit_7B"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[p]["fGPtbl_CUnit_7B"].ToString() + "</span>";
                              }
                              else
                              {
                                  drtechnical["fGPtbl_CUnit_7B"] = Mydt2.Rows[p]["fGPtbl_CUnit_7B"].ToString();
                              }
                              if (Mydt2.Rows[p]["fGPtbl_CUnit_7C"].ToString() != dttechnical_bind.Rows[q]["fGPtbl_CUnit_7C"].ToString())
                              {

                                  drtechnical["fGPtbl_CUnit_7C"] = "<span style='color:red;'>" + dttechnical_bind.Rows[q]["fGPtbl_CUnit_7C"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[p]["fGPtbl_CUnit_7C"].ToString() + "</span>";
                              }
                              else
                              {
                                  drtechnical["fGPtbl_CUnit_7C"] = Mydt2.Rows[p]["fGPtbl_CUnit_7C"].ToString();
                              }

                              if (Mydt2.Rows[p]["fGPtbl_CUnit_7D"].ToString() != dttechnical_bind.Rows[q]["fGPtbl_CUnit_7D"].ToString())
                              {

                                  drtechnical["fGPtbl_CUnit_7D"] = "<span style='color:red;'>" + dttechnical_bind.Rows[q]["fGPtbl_CUnit_7D"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[p]["fGPtbl_CUnit_7D"].ToString() + "</span>";
                              }
                              else
                              {
                                  drtechnical["fGPtbl_CUnit_7D"] = Mydt2.Rows[p]["fGPtbl_CUnit_7D"].ToString();
                              }
                              if (Mydt2.Rows[p]["fGPtbl_CUnit_7E"].ToString() != dttechnical_bind.Rows[q]["fGPtbl_CUnit_7E"].ToString())
                              {

                                  drtechnical["fGPtbl_CUnit_7E"] = "<span style='color:red;'>" + dttechnical_bind.Rows[q]["fGPtbl_CUnit_7E"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt2.Rows[p]["fGPtbl_CUnit_7E"].ToString() + "</span>";
                              }
                              else
                              {
                                  drtechnical["fGPtbl_CUnit_7E"] = Mydt2.Rows[p]["fGPtbl_CUnit_7E"].ToString();
                              }
                               dttechnical_merge_Datails.Rows.Add(drtechnical);
                          }
                          //dttechnical_merge_Datails.Rows.Add(drtechnical);
                    //  }


                      gvTechnicalDetails.DataSource = dttechnical_merge_Datails;//Mydt2;// Myds.Tables[2];
                      gvTechnicalDetails.DataBind();
                  }
                  catch { }*/

                // Bind Factory Address

                // Bind all label 
                try
                {
                    gvTechnicalDetails.DataSource = Mydt2;
                    gvTechnicalDetails.DataBind();
                    gvTechnicalDetails_new.DataSource = ds.Tables[2];
                    gvTechnicalDetails_new.DataBind();
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
                }

                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_CA_Name"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_CA_Name"].ToString())
                {
                    lblCatarea.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_CA_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_CA_Name"].ToString() + "</span>";
                }
                else
                {
                    lblCatarea.Text = Mydt.Rows[0]["fGPtbl_CUnit_CA_Name"].ToString();
                }



                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_CC_Name"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_CC_Name"].ToString())
                {
                    lblCatcast.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_CC_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_CC_Name"].ToString() + "</span>";
                }
                else
                {
                    lblCatcast.Text = Mydt.Rows[0]["fGPtbl_CUnit_CC_Name"].ToString();
                }


                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_CUS_Name"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_CUS_Name"].ToString())
                {
                    lblCatusize.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_CUS_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_CUS_Name"].ToString() + "</span>";
                }
                else
                {
                    lblCatusize.Text = Mydt.Rows[0]["fGPtbl_CUnit_CUS_Name"].ToString();
                }



                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_Constitution_Name"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_Constitution_Name"].ToString())
                {
                    lblConst.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_Constitution_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_Constitution_Name"].ToString() + "</span>";
                }
                else
                {
                    lblConst.Text = Mydt.Rows[0]["fGPtbl_CUnit_Constitution_Name"].ToString();
                }
                //  }

                if (ds.Tables[0].Rows[0]["IncDate"].ToString() != Mydt.Rows[0]["IncDate"].ToString())
                {
                    lblIncorp.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["IncDate"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["IncDate"].ToString() + "</span>";
                }
                else
                {
                    lblIncorp.Text = Mydt.Rows[0]["IncDate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProdDate"].ToString() != Mydt.Rows[0]["ProdDate"].ToString())
                {
                    lblBuss.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["ProdDate"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["ProdDate"].ToString() + "</span>";
                }
                else
                {
                    lblBuss.Text = Mydt.Rows[0]["ProdDate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3C5"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3C5"].ToString())
                {
                    lbl3c5Part.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3C5"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3C5"].ToString() + "</span>";
                }
                else
                {
                    lbl3c5Part.Text = Mydt.Rows[0]["fGPtbl_CUnit_3C5"].ToString();
                }

                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_RegisteredUnder_Name"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_RegisteredUnder_Name"].ToString())
                {
                    lbl3dregunder.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_RegisteredUnder_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_RegisteredUnder_Name"].ToString() + "</span>";
                }
                else
                {
                    lbl3dregunder.Text = Mydt.Rows[0]["fGPtbl_CUnit_RegisteredUnder_Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3E"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3E"].ToString())
                {
                    lbl3eAssociate.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3E"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3E"].ToString() + "</span>";
                }
                else
                {
                    lbl3eAssociate.Text = Mydt.Rows[0]["fGPtbl_CUnit_3E"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3F"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3F"].ToString())
                {
                    lbl3flParent.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3F"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3F"].ToString() + "</span>";
                }
                else
                {
                    lbl3flParent.Text = Mydt.Rows[0]["fGPtbl_CUnit_3F"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3G"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3G"].ToString())
                {
                    lbl3gDirector.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3G"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3G"].ToString() + "</span>";
                }
                else
                {
                    lbl3gDirector.Text = Mydt.Rows[0]["fGPtbl_CUnit_3G"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3H"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3H"].ToString())
                {
                    lbl3hCapital.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3H"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3H"].ToString() + "</span>";
                }
                else
                {
                    lbl3hCapital.Text = Mydt.Rows[0]["fGPtbl_CUnit_3H"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3I"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3I"].ToString())
                {
                    lbl3iInter.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3I"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3I"].ToString() + "</span>";
                }
                else
                {
                    lbl3iInter.Text = Mydt.Rows[0]["fGPtbl_CUnit_3I"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3J"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3J"].ToString())
                {
                    lbl3jLoans.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3J"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3J"].ToString() + "</span>";
                }
                else
                {
                    lbl3jLoans.Text = Mydt.Rows[0]["fGPtbl_CUnit_3J"].ToString();
                }

                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3K1"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3K1"].ToString())
                {
                    lbl3k1Icircle.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3K1"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3K1"].ToString() + "</span>";
                }
                else
                {
                    lbl3k1Icircle.Text = Mydt.Rows[0]["fGPtbl_CUnit_3K1"].ToString();
                }

                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3L"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3L"].ToString())
                {
                    lbl3lSalestax.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3L"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3L"].ToString() + "</span>";
                }
                else
                {
                    lbl3lSalestax.Text = Mydt.Rows[0]["fGPtbl_CUnit_3L"].ToString();
                }

                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3M"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3M"].ToString())
                {
                    lbl3mNsicreg.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3M"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3M"].ToString() + "</span>";
                }
                else
                {
                    lbl3mNsicreg.Text = Mydt.Rows[0]["fGPtbl_CUnit_3M"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3N"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3N"].ToString())
                {
                    lbl3nnsicbuz.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3N"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3N"].ToString() + "</span>";
                }
                else
                {
                    lbl3nnsicbuz.Text = Mydt.Rows[0]["fGPtbl_CUnit_3N"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_3O"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_3O"].ToString())
                {
                    lbl3oProsecute.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_3O"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_3O"].ToString() + "</span>";
                }
                else
                {
                    lbl3oProsecute.Text = Mydt.Rows[0]["fGPtbl_CUnit_3O"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_4A"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_4A"].ToString())
                {
                    lbl4aDGSD.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_4A"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_4A"].ToString() + "</span>";
                }
                else
                {
                    lbl4aDGSD.Text = Mydt.Rows[0]["fGPtbl_CUnit_4A"].ToString();
                }

                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_4B"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_4B"].ToString())
                {
                    lbl4bDGSDsupplied.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_4B"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_4B"].ToString() + "</span>";
                }
                else
                {
                    lbl4bDGSDsupplied.Text = Mydt.Rows[0]["fGPtbl_CUnit_4B"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_8"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_8"].ToString())
                {
                    lbl8Typetest.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_8"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_8"].ToString() + "</span>";
                }
                else
                {
                    lbl8Typetest.Text = Mydt.Rows[0]["fGPtbl_CUnit_8"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_9"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_9"].ToString())
                {
                    lbl9Serviceprod.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_9"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_9"].ToString() + "</span>";
                }
                else
                {
                    lbl9Serviceprod.Text = Mydt.Rows[0]["fGPtbl_CUnit_9"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_10"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_10"].ToString())
                {
                    lbl10Threeyrs.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_10"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_10"].ToString() + "</span>";
                }
                else
                {
                    lbl10Threeyrs.Text = Mydt.Rows[0]["fGPtbl_CUnit_10"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_11"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_11"].ToString())
                {
                    lbl11Banker.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_11"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_11"].ToString() + "</span>";
                }
                else
                {
                    lbl11Banker.Text = Mydt.Rows[0]["fGPtbl_CUnit_11"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_12A"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_12A"].ToString())
                {
                    lbl12aBis.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_12A"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_12A"].ToString() + "</span>";
                }
                else
                {
                    lbl12aBis.Text = Mydt.Rows[0]["fGPtbl_CUnit_12A"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_12B"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_12B"].ToString())
                {
                    lbl12bIso.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_12B"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_12B"].ToString() + "</span>";
                }
                else
                {
                    lbl12bIso.Text = Mydt.Rows[0]["fGPtbl_CUnit_12B"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_13A"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_13A"].ToString())
                {
                    lbl13aSignatory.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_13A"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_13A"].ToString() + "</span>";
                }
                else
                {
                    lbl13aSignatory.Text = Mydt.Rows[0]["fGPtbl_CUnit_13A"].ToString();
                }

                if (ds.Tables[0].Rows[0]["fGPtbl_CUnit_13B"].ToString() != Mydt.Rows[0]["fGPtbl_CUnit_13B"].ToString())
                {
                    lbl13bDesignation.Text = "<span style='color:red;'>" + ds.Tables[0].Rows[0]["fGPtbl_CUnit_13B"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt.Rows[0]["fGPtbl_CUnit_13B"].ToString() + "</span>";
                }
                else
                {
                    lbl13bDesignation.Text = Mydt.Rows[0]["fGPtbl_CUnit_13B"].ToString();
                }



                //Bind Directors 

                if (ds.Tables[4].Rows.Count > 0)
                {
                    if (Mydt4.Rows.Count > 1)
                    {
                        if (ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Name"].ToString() != Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Name"].ToString())
                        {
                            lbl_3ca1Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca1Name.Text = ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Address"].ToString() != Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Address"].ToString())
                        {
                            lbl_3ca1Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Address"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca1Address.Text = Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Address"].ToString();
                        }
                        if (ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Zip"].ToString() != Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Zip"].ToString())
                        {
                            lbl_3ca1Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Zip"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca1Pin.Text = ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Zip"].ToString();
                        }
                        if (ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Phone"].ToString() != Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Phone"].ToString())
                        {
                            lbl_3ca1Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Phone"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca1Tele.Text = ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Phone"].ToString();
                        }
                        if (ds.Tables[4].Rows[0]["fGPtbl_MS_Name"].ToString() != Mydt4.Rows[0]["fGPtbl_MS_Name"].ToString())
                        {
                            lblState_3ca1.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_MS_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[0]["fGPtbl_MS_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblState_3ca1.Text = ds.Tables[4].Rows[0]["fGPtbl_MS_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[0]["fGPtbl_MCty_Name"].ToString() != Mydt4.Rows[0]["fGPtbl_MCty_Name"].ToString())
                        {
                            lblCity_3ca1.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_MCty_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[0]["fGPtbl_MCty_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblCity_3ca1.Text = ds.Tables[4].Rows[0]["fGPtbl_MCty_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Shareholder"].ToString() != Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Shareholder"].ToString())
                        {
                            lbl_3ca1Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[0]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca1Share.Text = ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Shareholder"].ToString();
                        }
                    }
                    else
                    {
                        lbl_3ca1Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>";
                        lbl_3ca1Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>";

                        lbl_3ca1Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>";

                        lbl_3ca1Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>";

                        lblState_3ca1.Text = "<span style= 'color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_MS_Name"].ToString() + "</span>";

                        lblCity_3ca1.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_MCty_Name"].ToString() + "</span>";

                        lbl_3ca1Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[0]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>";
                    }
                }

                if (ds.Tables[4].Rows.Count > 1)
                {

                    if (Mydt4.Rows.Count > 1)
                    {
                        if (ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Name"].ToString() != Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Name"].ToString())
                        {
                            lbl_3ca2Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca2Name.Text = ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Address"].ToString() != Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Address"].ToString())
                        {
                            lbl_3ca2Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Address"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca2Address.Text = ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Address"].ToString();
                        }
                        if (ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Zip"].ToString() != Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Zip"].ToString())
                        {
                            lbl_3ca2Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Zip"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca2Pin.Text = ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Zip"].ToString();
                        }
                        if (ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Phone"].ToString() != Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Phone"].ToString())
                        {
                            lbl_3ca2Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Phonee"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca2Tele.Text = ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Phone"].ToString();
                        }
                        if (ds.Tables[4].Rows[1]["fGPtbl_MS_Name"].ToString() != Mydt4.Rows[1]["fGPtbl_MS_Name"].ToString())
                        {
                            lblState_3ca2.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_MS_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[1]["fGPtbl_MS_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblState_3ca2.Text = ds.Tables[4].Rows[1]["fGPtbl_MS_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[1]["fGPtbl_MCty_Name"].ToString() != Mydt4.Rows[1]["fGPtbl_MCty_Name"].ToString())
                        {
                            lblCity_3ca2.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_MCty_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[1]["fGPtbl_MCty_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblCity_3ca2.Text = ds.Tables[4].Rows[1]["fGPtbl_MCty_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Shareholder"].ToString() != Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Shareholder"].ToString())
                        {
                            lbl_3ca2Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[1]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca2Share.Text = ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Shareholder"].ToString();
                        }
                    }
                    else
                    {
                        lbl_3ca2Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>";
                        lbl_3ca2Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>";

                        lbl_3ca2Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>";

                        lbl_3ca2Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>";

                        lblState_3ca2.Text = "<span style= 'color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_MS_Name"].ToString() + "</span>";

                        lblCity_3ca2.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_MCty_Name"].ToString() + "</span>";

                        lbl_3ca2Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[1]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>";
                    }
                }
                if (ds.Tables[4].Rows.Count > 2)
                {
                    if (Mydt4.Rows.Count > 2)
                    {
                        if (ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Name"].ToString() != Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Name"].ToString())
                        {
                            lbl_3ca3Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca3Name.Text = ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Address"].ToString() != Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Address"].ToString())
                        {
                            lbl_3ca3Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Address"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca3Address.Text = ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Address"].ToString();
                        }
                        if (ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Zip"].ToString() != Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Zip"].ToString())
                        {
                            lbl_3ca3Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Zip"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca3Pin.Text = ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Zip"].ToString();
                        }
                        if (ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Phone"].ToString() != Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Phone"].ToString())
                        {
                            lbl_3ca3Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Phone"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca3Tele.Text = ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Phone"].ToString();
                        }
                        if (ds.Tables[4].Rows[2]["fGPtbl_MS_Name"].ToString() != Mydt4.Rows[2]["fGPtbl_MS_Name"].ToString())
                        {
                            lblState_3ca3.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_MS_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[2]["fGPtbl_MS_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblState_3ca3.Text = ds.Tables[4].Rows[2]["fGPtbl_MS_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[2]["fGPtbl_MCty_Name"].ToString() != Mydt4.Rows[2]["fGPtbl_MCty_Name"].ToString())
                        {
                            lblCity_3ca3.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_MCty_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[2]["fGPtbl_MCty_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblCity_3ca3.Text = ds.Tables[4].Rows[2]["fGPtbl_MCty_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Shareholder"].ToString() != Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Shareholder"].ToString())
                        {
                            lbl_3ca3Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[2]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca3Share.Text = ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Shareholder"].ToString();
                        }
                    }
                    else
                    {
                        lbl_3ca3Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>";
                        lbl_3ca3Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>";

                        lbl_3ca3Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>";

                        lbl_3ca3Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>";

                        lblState_3ca3.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_MS_Name"].ToString() + "</span>";

                        lblCity_3ca3.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_MCty_Name"].ToString() + "</span>";

                        lbl_3ca3Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[2]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>";
                    }
                }

                if (ds.Tables[4].Rows.Count > 3)
                {
                    if (Mydt4.Rows.Count > 3)
                    {
                        if (ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Name"].ToString() != Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Name"].ToString())
                        {
                            lbl_3ca4Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca4Name.Text = ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Address"].ToString() != Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Address"].ToString())
                        {
                            lbl_3ca4Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Address"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca4Address.Text = ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Address"].ToString();
                        }
                        if (ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Zip"].ToString() != Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Zip"].ToString())
                        {
                            lbl_3ca4Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Zip"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca4Pin.Text = ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Zip"].ToString();
                        }
                        if (ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Phone"].ToString() != Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Phone"].ToString())
                        {
                            lbl_3ca4Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Phone"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca4Tele.Text = ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Phone"].ToString();
                        }
                        if (ds.Tables[4].Rows[3]["fGPtbl_MS_Name"].ToString() != Mydt4.Rows[3]["fGPtbl_MS_Name"].ToString())
                        {
                            lblState_3ca4.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_MS_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[3]["fGPtbl_MS_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblState_3ca4.Text = ds.Tables[4].Rows[3]["fGPtbl_MS_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[3]["fGPtbl_MCty_Name"].ToString() != Mydt4.Rows[3]["fGPtbl_MCty_Name"].ToString())
                        {
                            lblCity_3ca4.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_MCty_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[3]["fGPtbl_MCty_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblCity_3ca4.Text = ds.Tables[4].Rows[3]["fGPtbl_MCty_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Shareholder"].ToString() != Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Shareholder"].ToString())
                        {
                            lbl_3ca4Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[3]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca4Share.Text = ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Shareholder"].ToString();
                        }
                    }
                    else
                    {
                        lbl_3ca4Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>";
                        lbl_3ca4Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>";

                        lbl_3ca4Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>";

                        lbl_3ca4Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>";

                        lblState_3ca4.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_MS_Name"].ToString() + "</span>";

                        lblCity_3ca4.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_MCty_Name"].ToString() + "</span>";

                        lbl_3ca4Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>";

                    }
                }

                if (ds.Tables[4].Rows.Count > 4)
                {
                    if (Mydt4.Rows.Count > 4)
                    {
                        if (ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Name"].ToString() != Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Name"].ToString())
                        {
                            lbl_3ca5Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca5Name.Text = ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Address"].ToString() != Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Address"].ToString())
                        {
                            lbl_3ca5Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Address"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca5Address.Text = ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Address"].ToString();
                        }
                        if (ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Zip"].ToString() != Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Zip"].ToString())
                        {
                            lbl_3ca5Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Zip"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca5Pin.Text = ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Zip"].ToString();
                        }
                        if (ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Phone"].ToString() != Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Phone"].ToString())
                        {
                            lbl_3ca5Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Phone"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca5Tele.Text = ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Phone"].ToString();
                        }
                        if (ds.Tables[4].Rows[4]["fGPtbl_MS_Name"].ToString() != Mydt4.Rows[4]["fGPtbl_MS_Name"].ToString())
                        {
                            lblState_3ca5.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_MS_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[4]["fGPtbl_MS_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblState_3ca5.Text = ds.Tables[4].Rows[4]["fGPtbl_MS_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[4]["fGPtbl_MCty_Name"].ToString() != Mydt4.Rows[4]["fGPtbl_MCty_Name"].ToString())
                        {
                            lblCity_3ca5.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_MCty_Name"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[4]["fGPtbl_MCty_Name"].ToString() + "<span>";
                        }
                        else
                        {
                            lblCity_3ca5.Text = ds.Tables[4].Rows[4]["fGPtbl_MCty_Name"].ToString();
                        }
                        if (ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Shareholder"].ToString() != Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Shareholder"].ToString())
                        {
                            lbl_3ca5Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>" + " " + "<span style='color:Blue;'>" + Mydt4.Rows[4]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "<span>";
                        }
                        else
                        {
                            lbl_3ca5Share.Text = ds.Tables[4].Rows[3]["fGPtbl_CUnit_Directors_Shareholder"].ToString();
                        }


                    }
                    else
                    {
                        lbl_3ca5Name.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Name"].ToString() + "</span>";
                        lbl_3ca5Address.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Address"].ToString() + "</span>";

                        lbl_3ca5Pin.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Zip"].ToString() + "</span>";

                        lbl_3ca5Tele.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Phone"].ToString() + "</span>";

                        lblState_3ca5.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_MS_Name"].ToString() + "</span>";

                        lblCity_3ca5.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_MCty_Name"].ToString() + "</span>";

                        lbl_3ca5Share.Text = "<span style='color:red;'>" + ds.Tables[4].Rows[4]["fGPtbl_CUnit_Directors_Shareholder"].ToString() + "</span>";

                    }
                }

                //}
            }
            else
            {
                ds.Dispose();
            }
            Bindturnover();
            Bindturnover_old();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    //public void Showdata_history(int Companyid, int Amedmentid)
    //{
    //    try
    //    {
    //        objCompany = new clsCompany();
    //         DataSet ds_old_changes = new DataSet();
    //         ds_old_changes = objCompany.Company_fetch_Amedmentchange_view_history_table(Companyid, Amedmentid);
    //         if (ds_old_changes.Tables[0].Rows.Count > 0)
    //         {
    //             lblPAn_old.Text = ds_old_changes.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
    //             lbl_Comapny_old.Text = ds_old_changes.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
    //             lbl_Address_old.Text = ds_old_changes.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
    //             lblPhone_old.Text = ds_old_changes.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString();
    //             lblFax_old.Text = ds_old_changes.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString();
    //             lblEmail_old.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();
    //             lblCity_old.Text = ds_old_changes.Tables[0].Rows[0]["CompanyCity"].ToString();
    //             lblState_old.Text = ds_old_changes.Tables[0].Rows[0]["CompanyState"].ToString();
    //             lblPin_old.Text = ds_old_changes.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString();

    //         }
    //         else
    //         {
    //             ds_old_changes.Dispose();
    //         }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
    //    }

    //}
    protected void GridFactory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_factory_address_add = (Label)e.Row.FindControl("fGPtbl_CUnit_1B_Factory_Address_first");
                // ds = new DataSet();
                // ds = objCompany.Company_fetch_history_AmedmentChange_new_value(companyId, Amedmentid);
                //if(lbl_factory_address_add.Text!=ds.Tables[3].Rows[0]
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }


    public void Bindturnover()
    {
        try
        {
            objCompany = new clsCompany();
            int CompanyID2 = Convert.ToInt32(companyId);//3;
            DataSet ds_report = new DataSet();
            // objCompany.FetchTurnover_history(CompanyID2, 1);
            // ds_report = objCompany.Company_ReportView_TUrnOver(CompanyID2);
            ds_report = objCompany.FetchTurnover_history(CompanyID2, 1);
            if (ds_report.Tables[1].Rows.Count > 0)
            {
                //if (ds_report.Tables[0].Rows.Count > 1)
                // {
                grdTurnOver.DataSource = ds_report.Tables[1];
                grdTurnOver.DataBind();
                // }
            }
            else
            {
                grdTurnOver.Dispose();
                grdTurnOver.DataBind();
                ds_report.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }
    public void Bindturnover_old()
    {
        try
        {
            objCompany = new clsCompany();
            int CompanyID2 = Convert.ToInt32(companyId);//3;
            DataSet ds_report = new DataSet();
            // objCompany.FetchTurnover_history(CompanyID2, 1);
            // ds_report = objCompany.Company_ReportView_TUrnOver(CompanyID2);
            ds_report = objCompany.FetchTurnover_history_Turnover(CompanyID2, 1, Amedmentid);
            if (ds_report.Tables[1].Rows.Count > 0)
            {
                //if (ds_report.Tables[0].Rows.Count > 1)
                // {
                grdTurnOver_old.DataSource = ds_report.Tables[1];
                grdTurnOver_old.DataBind();
                // }
            }
            else
            {
                grdTurnOver_old.Dispose();
                grdTurnOver_old.DataBind();
                ds_report.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }



}
