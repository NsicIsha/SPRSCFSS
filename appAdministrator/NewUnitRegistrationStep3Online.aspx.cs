using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Xml.Linq;

public partial class appAdministrator_NewUnitRegistrationStep3Online : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, Edit;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany;
    int ErrorId;
    int ConstitutionId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //try
        //{
        //    UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        //    RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        //    DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        //    UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        //}
        //catch (Exception ex)
        //{
        //    UserId = 0;
        //    RoleId = 0;
        //    DepartmentId = 0;
        //    UserFullName = "";
        //}
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        FunRequest();
        FunFillControls();
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

          ConstitutionId= Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["con"].Replace(" ", "+")).ToString());

            Edit = Convert.ToInt32(Request.QueryString["Edit"].Replace(" ", "+"));



        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in FunRequest() " + ex.StackTrace);
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
            Edit = 0;
        }
    }

    public void FunFillControls()
    {
        if (!Page.IsPostBack)
        {
            //FILL RADIOBUTTON LIST
            objCompany = new clsCompany();
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = objCompany.Company_Fetch_RegisteredUnder();
            rd3d.DataTextField = "fGPtbl_CUnit_RegisteredUnder_Name";
            rd3d.DataValueField = "fGPtbl_CUnit_RegisteredUnder_Id";
            rd3d.DataSource = Dt;
            rd3d.DataBind();

            rd3d.SelectedIndex = 0;
        }
        if (!Page.IsPostBack)
        {
            FunFillData();
        }

    }

    private void FunFillData()
    {

        //cls_director objdir = new cls_director();
        //List<PerformanceSheet> director = new List<PerformanceSheet>();
        //director = objdir.FetchPerformanceSheet(CompanyId);

        // Bind_Director_gridview(director);



        if (Status == 5 || Edit == 1)    //INCOMPLETE
        {
            //UNIT TABLE
            objCompany = new clsCompany();
            System.Data.SqlClient.SqlDataReader MyDr;
            MyDr = objCompany.Company_Fetch_ByUnit_Step3(UnitId);
            while (MyDr.Read())
            {
                try
                {
                    rd3d.SelectedValue = MyDr["fGPtbl_CUnit_3D"].ToString();
                }
                catch { }

                txt_3e.Text = MyDr["fGPtbl_CUnit_3E"].ToString();
                txt_3f.Text = MyDr["fGPtbl_CUnit_3F"].ToString();
                txt_3g.Text = MyDr["fGPtbl_CUnit_3G"].ToString();
                txt_3h.Text = MyDr["fGPtbl_CUnit_3H"].ToString();
               // txt_3i_new.Text = MyDr["fGPtbl_CUnit_3I"].ToString();
                txt_3j.Text = MyDr["fGPtbl_CUnit_3J"].ToString();
                txt_3k.Text = MyDr["fGPtbl_CUnit_3K1"].ToString() != "" ? MyDr["fGPtbl_CUnit_3K1"].ToString() : "N.A.";
            //    txt_3l.Text = MyDr["fGPtbl_CUnit_3L"].ToString();
              //  txt_3m.Text = MyDr["fGPtbl_CUnit_3M"].ToString();
                txt_3n.Text = MyDr["fGPtbl_CUnit_3N"].ToString();
               // txt_3o.Text = MyDr["fGPtbl_CUnit_3O"].ToString();
                txt_4a.Text = MyDr["fGPtbl_CUnit_4A"].ToString();
                txt_4b.Text = MyDr["fGPtbl_CUnit_4B"].ToString();
            }
            MyDr.Close();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        objCompany = new clsCompany();

        System.Data.DataTable dt = new System.Data.DataTable();
        dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);

        //  ListtoXml();

        foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
        {

            int i = 0;

            foreach (var item in row.ItemArray) // Loop over the items.
            {
                if (i == 0)
                {
                    int bsRd3d = 0;
                    string bsTxt_3e = "";
                    string bsTxt_3f = "";
                    string bsTxt_3g = "";
                    string bstxt_3h = "";
                    string bstxt_3i = "";
                    string bstxt_3j = "";
                    string bstxt_3k = "";
                    string bstxt_3l = "";
                    string bstxt_3m = "";
                    string bstxt_3n = "";
                    string bstxt_3o = "";
                    string bstxt_4a = "";
                    string bstxt_4b = "";

                    try
                    {
                        bsRd3d = Convert.ToInt32(rd3d.SelectedValue.ToString());
                    }
                    catch { }

                    bsTxt_3e = txt_3e.Text.ToString().Trim();
                    bsTxt_3f = txt_3f.Text.ToString().Trim();
                    bsTxt_3g = txt_3g.Text.ToString().Trim();
                    bstxt_3h = txt_3h.Text.ToString().Trim();
                    bstxt_3i = "N.A.";//txt_3i_new.Text.ToString().Trim();
                    bstxt_3j = txt_3j.Text.ToString().Trim();
                    bstxt_3k = txt_3k.Text.ToString().Trim();
                    bstxt_3l = "N.A.";// txt_3l.Text.ToString().Trim();
                    bstxt_3m = "N.A.";//txt_3m.Text.ToString().Trim();
                    bstxt_3n = txt_3n.Text.ToString().Trim();
                    bstxt_3o = "N.A.";// txt_3o.Text.ToString().Trim();
                    bstxt_4a = txt_4a.Text.ToString().Trim();
                    bstxt_4b = txt_4b.Text.ToString().Trim();


                    //fGPtbl_CUnit_Id
                    UnitId = Convert.ToInt32(item);
                    objCompany = new clsCompany();
                    ErrorId = objCompany.Company_Insert_Step3(UnitId, CompanyId, bsRd3d, bsTxt_3e,
                                bsTxt_3f, bsTxt_3g, bstxt_3h, bstxt_3i,
                                bstxt_3j, bstxt_3k, bstxt_3l,
                                bstxt_3m, bstxt_3n, bstxt_3o, bstxt_4a,
                                bstxt_4b, UserId);
                }

                i++;

            }
        }

        dt.Clear();
        dt.Dispose();



        if (ErrorId == 1)   //INSERT/UPDATE SUCCESSFULL
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());

            string EncryptConstitutionId= objDecEnc.Encrypt(ConstitutionId.ToString()); 
            string EncryptPan = objDecEnc.Encrypt(objDecEnc.Decrypt(Request.QueryString["p"].Replace(" ", "+"))).ToString();

            if (Edit == 1)  // TO SKIP STORE IF EDITED BY ADMIN
            {
                Response.Redirect("NewUnitRegistrationStep4Online.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&con=" + EncryptConstitutionId + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=1", false);
            }
            else
            {
                Response.Redirect("NewUnitRegistrationStep3StoreOnline.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&con=" + EncryptConstitutionId + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=0", false);
            }

            //Response.Redirect("NewUnitRegistrationStep3Store.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId, false);
        }
        else               //INSERT/UPDATE FAIL
        {

        }
    }



    protected void btnBack_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
        string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
        string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());

        string EncryptPan = objDecEnc.Encrypt(objDecEnc.Decrypt(Request.QueryString["p"].Replace(" ", "+"))).ToString();
        string EncryptFormtype = objDecEnc.Encrypt("1");

        string EncryptConstitutionId = objDecEnc.Encrypt(ConstitutionId.ToString());


        if (Edit == 1)
        {
            Response.Redirect("NewUnitRegistrationStep2Online.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&con=" + EncryptConstitutionId + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&PanCard=" + EncryptPan + "&Edit=1", false);
        }
        else
        {
            Response.Redirect("NewUnitRegistrationStep2Online.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&con=" + EncryptConstitutionId + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&PanCard=" + EncryptPan + "&Edit=0", false);
        }
    }



    protected void gv_director_RowDataBound(object sender, GridViewRowEventArgs e)
    {






    }
    ////protected void AddnewDirector(object sender, EventArgs e)
    ////{
    ////    string ordernumber, Date_of_ReceiptOrder, Store_Produt_Service_Desc, Quantity, Value, Name_Consignee, DeliveryDueDate, MaterialDeliveryDate, DespatchingParticulars, Remarks;


    ////    ordernumber = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_OrderNumber")).Text;
    ////    Date_of_ReceiptOrder = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Date_of_ReceiptOrder")).Text;
    ////    Store_Produt_Service_Desc = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Store_Produt_Service_Desc")).Text;
    ////    Quantity = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Quantity")).Text;
    ////    Value = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Value")).Text;
    ////    Name_Consignee = ((TextBox)gv_director.FooterRow.FindControl("txt_Name_Consignee")).Text;
    ////    DeliveryDueDate = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_DeliveryDueDate")).Text;
    ////    MaterialDeliveryDate = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_MaterialDeliveryDate")).Text;
    ////    DespatchingParticulars = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_DespatchingParticulars")).Text;
    ////    Remarks = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Remarks")).Text.Trim();


    ////    List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];

    ////    if (mydir == null)
    ////    {
    ////        List<PerformanceSheet> mydir1 = new List<PerformanceSheet>();
    ////        PerformanceSheet dir = new PerformanceSheet();
    ////        dir.rowno = 1;
    ////        dir.OrderNumber = ordernumber;
    ////        dir.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
    ////        dir.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
    ////        dir.Quantity = Quantity;
    ////        dir.Value = Value;
    ////        dir.Name_Consignee = Name_Consignee;
    ////        dir.DeliveryDueDate = DeliveryDueDate;
    ////        dir.MaterialDeliveryDate = MaterialDeliveryDate;
    ////        dir.DespatchingParticulars = DespatchingParticulars;
    ////        dir.Remarks = Remarks;

    ////        mydir1.Add(dir);
    ////        Bind_Director_gridview(mydir1);
    ////    }
    ////    else
    ////    {
    ////        PerformanceSheet dir = new PerformanceSheet();
    ////        dir.rowno = mydir.Count + 1;
    ////        dir.OrderNumber = ordernumber;
    ////        dir.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
    ////        dir.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
    ////        dir.Quantity = Quantity;
    ////        dir.Value = Value;
    ////        dir.Name_Consignee = Name_Consignee;
    ////        dir.DeliveryDueDate = DeliveryDueDate;
    ////        dir.MaterialDeliveryDate = MaterialDeliveryDate;
    ////        dir.DespatchingParticulars = DespatchingParticulars;
    ////        dir.Remarks = Remarks;
    ////        mydir.Add(dir);
    ////        Bind_Director_gridview(mydir);
    ////    }

    ////}

    ////private void Bind_Director_gridview(List<PerformanceSheet> Directors)
    ////{

    ////    gv_director.DataSource = Directors;
    ////    gv_director.DataBind();
    ////    ViewState["PerformanceSheet"] = Directors;

    ////    if (Directors[0].OrderNumber == "" && Directors[0].Date_of_ReceiptOrder == "" && Directors[0].Store_Produt_Service_Desc == "" && Directors[0].Quantity == "" && Directors[0].Value == "")
    ////    {
    ////        gv_director.Rows[0].Visible = false;

    ////    }
    ////}
    ////protected void gv_director_RowEditing(object sender, GridViewEditEventArgs e)
    ////{
    ////    gv_director.EditIndex = e.NewEditIndex;
    ////    List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
    ////    gv_director.DataSource = mydir;
    ////    gv_director.DataBind();
    ////}
    ////protected void gv_director_RowUpdating(object sender, GridViewUpdateEventArgs e)
    ////{
    ////    int userid = Convert.ToInt32(gv_director.DataKeys[e.RowIndex].Value.ToString());
    ////    // Response.Write(userid);
    ////    // Fin Gridview Record 
    ////    string ordernumber, Date_of_ReceiptOrder, Store_Produt_Service_Desc, Quantity, Value, Name_Consignee, DeliveryDueDate, MaterialDeliveryDate, DespatchingParticulars, Remarks;


    ////    ordernumber = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_OrderNumber")).Text;
    ////    Date_of_ReceiptOrder = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Date_of_ReceiptOrder")).Text;
    ////    Store_Produt_Service_Desc = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Store_Produt_Service_Desc")).Text;
    ////    Quantity = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Quantity")).Text;
    ////    Value = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Value")).Text;
    ////    Name_Consignee = ((TextBox)gv_director.FooterRow.FindControl("txt_Name_Consignee")).Text;
    ////    DeliveryDueDate = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_DeliveryDueDate")).Text;
    ////    MaterialDeliveryDate = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_MaterialDeliveryDate")).Text;
    ////    DespatchingParticulars = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_DespatchingParticulars")).Text;
    ////    Remarks = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Remarks")).Text.Trim();



    ////    List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];

    ////    if (mydir != null)
    ////    {
    ////        var item = mydir.Where(d => d.rowno == userid).FirstOrDefault();
    ////        if (item != null)
    ////        {
    ////            item.OrderNumber = ordernumber;
    ////            item.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
    ////            item.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
    ////            item.Quantity = Quantity;
    ////            item.Value = Value;
    ////            item.Name_Consignee = Name_Consignee;
    ////            item.DeliveryDueDate = DeliveryDueDate;
    ////            item.MaterialDeliveryDate = MaterialDeliveryDate;
    ////            item.DespatchingParticulars = DespatchingParticulars;
    ////            item.Remarks = Remarks;

    ////        }
    ////    }
    ////    gv_director.EditIndex = -1;
    ////    Bind_Director_gridview(mydir);

    ////}
    ////private void ListtoXml()
    ////{
    ////    List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
    ////    if (mydir != null)
    ////    {
    ////        var xDir = new XElement("PerformanceSheets", from dir in mydir
    ////                                             select new XElement("PerformanceSheet", new XAttribute("ID", dir.rowno),
    ////                                                 new XElement("OrderNumber", dir.OrderNumber),
    ////                                                 new XElement("Date_of_ReceiptOrder", dir.Date_of_ReceiptOrder),
    ////                                                 new XElement("Store_Produt_Service_Desc", dir.Store_Produt_Service_Desc),
    ////                                                 new XElement("Quantity", dir.Quantity),
    ////                                                 new XElement("Value", dir.Value),
    ////                                                 new XElement("Name_Consignee", dir.Name_Consignee),
    ////                                                 new XElement("DeliveryDueDate", dir.DeliveryDueDate),
    ////                                                 new XElement("MaterialDeliveryDate", dir.MaterialDeliveryDate),
    ////         new XElement("DespatchingParticulars", dir.DespatchingParticulars),

    ////         new XElement("Remarks", dir.Remarks)));


    ////        objCompany = new clsCompany();
    ////        objCompany.Company_PerformanceSheet_insert(xDir.ToString(), CompanyId);
    ////    }

    ////}
    ////protected void gv_director_RowDeleting(object sender, GridViewDeleteEventArgs e)
    ////{
    ////    int userid = Convert.ToInt32(gv_director.DataKeys[e.RowIndex].Value.ToString());
    ////    List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
    ////    PerformanceSheet dir = mydir.Where(s => s.rowno == userid).Single();
    ////    if (mydir != null)
    ////    {
    ////        mydir.Remove(dir);
    ////    }
    ////    Bind_Director_gridview(mydir);
    ////}
}