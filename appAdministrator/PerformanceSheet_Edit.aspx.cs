using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;

public partial class appAdministrator_PerformanceSheet_Edit : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int ErrorId;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;
    DateTime dtold;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

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
            UserEmailID = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }


     
    public DataSet Company_Fetch_PerformanceSheet(string PAN)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            ////MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_PerformanceSheetData", DBManager.ConnectToDB());
            MyAdp = new SqlDataAdapter("GPsp_Edit_PerformanceSheetData", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@strPAN", SqlDbType.VarChar).Value = PAN;            
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }
    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        ////DataSet ds = new DataSet();
        ////if(txt_PAN.Text !="")
        ////{
        ////    ds = Company_Fetch_PerformanceSheet(txt_PAN.Text);

        ////    if(ds.Tables[0].Rows.Count > 0)
        ////    {
        ////        CompanyId=Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyId"].ToString().Trim());
        ////        gv_PerformanceSheet.DataSource = ds;
        ////        gv_PerformanceSheet.DataBind();
        //////cls_director objdir1 = new cls_director();
        //////List<PerformanceSheet> director1 = new List<PerformanceSheet>();
        //////director1 = objdir1.FetchPerformanceSheet(CompanyId);

        ////        //Bind_PerformanceSheet_gridview(director1);
        ////        trSubmit.Visible = true;
        ////    }

        ////}

        BindGriddata();
    }


    void BindGriddata()
    {
        DataSet ds = new DataSet();
        if (txt_PAN.Text != "")
        {
            ds = Company_Fetch_PerformanceSheet(txt_PAN.Text);

            if (ds.Tables[0].Rows.Count > 0)
            {
                CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyId"].ToString().Trim());
                gv_PerformanceSheet.DataSource = ds;
                gv_PerformanceSheet.DataBind();
                //cls_director objdir1 = new cls_director();
                //List<PerformanceSheet> director1 = new List<PerformanceSheet>();
                //director1 = objdir1.FetchPerformanceSheet(CompanyId);

                //Bind_PerformanceSheet_gridview(director1);
                //trSubmit.Visible = true;
            }

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];


        //if (PerformanceSheet[0].OrderNumber == "" && PerformanceSheet[0].Date_of_ReceiptOrder == "" && PerformanceSheet[0].Store_Produt_Service_Desc == "" && PerformanceSheet[0].Quantity == "" && PerformanceSheet[0].Value == "")

        if (gv_PerformanceSheet.Rows.Count == 1)
        {
            if (mydir[0].OrderNumber == "" && mydir[0].Date_of_ReceiptOrder == "" && mydir[0].Store_Produt_Service_Desc == "" && mydir[0].Quantity == "" && mydir[0].Value == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Performance Statement');", true);
                return;
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Director Details');", true);
            //return;
        }          

            ListtoXml_PerFormanceSheet();


       

    }
    protected void AddnewPerformanceSheet(object sender, EventArgs e)
    {
        string ordernumber, Date_of_ReceiptOrder, Store_Produt_Service_Desc, Quantity, Value, Name_Consignee, DeliveryDueDate, MaterialDeliveryDate, DespatchingParticulars, Remarks;


        ordernumber = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_OrderNumber")).Text;
        Date_of_ReceiptOrder = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Date_of_ReceiptOrder")).Text;
        Store_Produt_Service_Desc = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Store_Produt_Service_Desc")).Text;
        Quantity = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Quantity")).Text;
        Value = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Value")).Text;
        Name_Consignee = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_Name_Consignee")).Text;
        DeliveryDueDate = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_DeliveryDueDate")).Text;
        MaterialDeliveryDate = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_MaterialDeliveryDate")).Text;
        DespatchingParticulars = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_DespatchingParticulars")).Text;
        Remarks = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Remarks")).Text.Trim();


        List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];

        if (mydir == null)
        {
            List<PerformanceSheet> mydir1 = new List<PerformanceSheet>();
            PerformanceSheet dir = new PerformanceSheet();
            dir.PrimaryId = 1;
            dir.OrderNumber = ordernumber;
            dir.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
            dir.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
            dir.Quantity = Quantity;
            dir.Value = Value;
            dir.Name_Consignee = Name_Consignee;
            dir.DeliveryDueDate = DeliveryDueDate;
            dir.MaterialDeliveryDate = MaterialDeliveryDate;
            dir.DespatchingParticulars = DespatchingParticulars;
            dir.Remarks = Remarks;

            mydir1.Add(dir);
            Bind_PerformanceSheet_gridview(mydir1);


        }
        else
        {
            PerformanceSheet dir = new PerformanceSheet();
            dir.PrimaryId = mydir.Count + 1;
            dir.OrderNumber = ordernumber;
            dir.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
            dir.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
            dir.Quantity = Quantity;
            dir.Value = Value;
            dir.Name_Consignee = Name_Consignee;
            dir.DeliveryDueDate = DeliveryDueDate;
            dir.MaterialDeliveryDate = MaterialDeliveryDate;
            dir.DespatchingParticulars = DespatchingParticulars;
            dir.Remarks = Remarks;
            mydir.Add(dir);
            Bind_PerformanceSheet_gridview(mydir);

            //RequiredFieldValidator rfdval = ((RequiredFieldValidator)gv_PerformanceSheet.FooterRow.FindControl("reqpisncodValue"));
            //rfdval.Enabled = false;
        }

    }
    private void Bind_PerformanceSheet_gridview(List<PerformanceSheet> PerformanceSheet)
    {

        gv_PerformanceSheet.DataSource = PerformanceSheet;
        gv_PerformanceSheet.DataBind();
        ViewState["PerformanceSheet"] = PerformanceSheet;

        //RequiredFieldValidator rfdval = ((RequiredFieldValidator)gv_PerformanceSheet.FooterRow.FindControl("reqpisncodValue"));
        //rfdval.Enabled = false;


        if (PerformanceSheet[0].OrderNumber == "" && PerformanceSheet[0].Date_of_ReceiptOrder == "" && PerformanceSheet[0].Store_Produt_Service_Desc == "" && PerformanceSheet[0].Quantity == "" && PerformanceSheet[0].Value == "")
        {
            gv_PerformanceSheet.Rows[0].Visible = false;
        }
    }
    protected void gv_PerformanceSheet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_PerformanceSheet.EditIndex = e.NewEditIndex;
        this.BindGriddata();


        //gv_PerformanceSheet.EditIndex = e.NewEditIndex;
        //List<PerformanceSheet> myPS = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
        //gv_PerformanceSheet.DataSource = myPS;
        //gv_PerformanceSheet.DataBind();


    }
    protected void gv_PerformanceSheet_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int userid = Convert.ToInt32(gv_PerformanceSheet.DataKeys[e.RowIndex].Value.ToString());

        // Response.Write(userid);
        // Fin Gridview Record 
        string ordernumber, Date_of_ReceiptOrder, Store_Produt_Service_Desc, Quantity, Value, Name_Consignee, DeliveryDueDate, MaterialDeliveryDate, DespatchingParticulars, Remarks;


        ordernumber = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_OrderNumber")).Text.Trim();
        Date_of_ReceiptOrder = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Date_of_ReceiptOrder")).Text.Trim();
        Store_Produt_Service_Desc = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Store_Produt_Service_Desc")).Text.Trim();
        Quantity = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Quantity")).Text.Trim();
        Value = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Value")).Text.Trim();
        Name_Consignee = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Name_Consignee")).Text.Trim();
        DeliveryDueDate = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_DeliveryDueDate")).Text.Trim();
        MaterialDeliveryDate = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_MaterialDeliveryDate")).Text.Trim();
        DespatchingParticulars = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_DespatchingParticulars")).Text.Trim();
        Remarks = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Remarks")).Text.Trim();

        int rcd = Company_PerformanceSheet_Edit_IndividualData(userid, ordernumber,
           Date_of_ReceiptOrder, Store_Produt_Service_Desc,
            Convert.ToString(Quantity), Convert.ToString(Value),
            Name_Consignee, DeliveryDueDate,
           MaterialDeliveryDate,
            DespatchingParticulars, Remarks
            );
        gv_PerformanceSheet.EditIndex = -1;
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Details Updated Successfully');", true);
        
        this.BindGriddata();

       


        //List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];

        //if (mydir != null)
        //{
        //    var item = mydir.Where(d => d.PrimaryId == userid).FirstOrDefault();
        //    if (item != null)
        //    {
        //        item.OrderNumber = ordernumber;
        //        item.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
        //        item.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
        //        item.Quantity = Quantity;
        //        item.Value = Value;
        //        item.Name_Consignee = Name_Consignee;
        //        item.DeliveryDueDate = DeliveryDueDate;
        //        item.MaterialDeliveryDate = MaterialDeliveryDate;
        //        item.DespatchingParticulars = DespatchingParticulars;
        //        item.Remarks = Remarks;

        //    }
        //}
        //gv_PerformanceSheet.EditIndex = -1;
        //Bind_PerformanceSheet_gridview(mydir);

    }
    private void ListtoXml_PerFormanceSheet()
    {
        DataSet ds = new DataSet();
        ds = Company_Fetch_PerformanceSheet(txt_PAN.Text);

        if (ds.Tables[0].Rows.Count > 0)
        {
            CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyId"].ToString().Trim());
        }


        List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
        if (mydir != null)
        {

           

            var xDir = new XElement("PerformanceSheets", from dir in mydir
                                                         select new XElement("PerformanceSheet", new XAttribute("ID", dir.PrimaryId),
                                                             new XElement("OrderNumber", dir.OrderNumber),
                                                             new XElement("Date_of_ReceiptOrder", dir.Date_of_ReceiptOrder),
                                                             new XElement("Store_Produt_Service_Desc", dir.Store_Produt_Service_Desc),
                                                             new XElement("Quantity", dir.Quantity),
                                                             new XElement("Value", dir.Value),
                                                             new XElement("Name_Consignee", dir.Name_Consignee),
                                                             new XElement("DeliveryDueDate", dir.DeliveryDueDate),
                                                             new XElement("MaterialDeliveryDate", dir.MaterialDeliveryDate),
                     new XElement("DespatchingParticulars", dir.DespatchingParticulars),

                     new XElement("Remarks", dir.Remarks)));


           

            objCompany = new clsCompany();
            //// ErrorId= objCompany.Company_PerformanceSheet_insert_Edit(xDir.ToString(), CompanyId);

            ErrorId = Company_PerformanceSheet_insert_Edit(xDir.ToString(), CompanyId);

            //lbl_branch_account_chek.Text = "Details Updated Successfully";

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Details Updated Successfully');", true);


        }

    }


    public int Company_PerformanceSheet_insert_Edit(string xmldata, int companyid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            con.Open();
            cmd = new SqlCommand("GP_sp_Insert_PerformanceSheetdata_Edit_RA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Xmldata", xmldata);
            cmd.Parameters.AddWithValue("@CompanyId", companyid);
            ErrorId = cmd.ExecuteNonQuery();
            // ErrorId = Convert.ToInt32(outError.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            con.Close();

        }

        return ErrorId;
    }



    public int Company_PerformanceSheet_insert(string xmldata, int companyid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            con.Open();
            cmd = new SqlCommand("GP_sp_Insert_PerformanceSheetdata_RA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Xmldata", xmldata);
            cmd.Parameters.AddWithValue("@CompanyId", companyid);

            ErrorId = cmd.ExecuteNonQuery();
            // ErrorId = Convert.ToInt32(outError.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            con.Close();

        }

        return ErrorId;
    }
    public int Company_PerformanceSheet_Edit_IndividualData(
        int Primaryid,
        string ordernumber,
        string Date_of_ReceiptOrder,
        string Store_Produt_Service_Desc,
        string Quantity,
        string Value,
        string Name_Consignee,
        string DeliveryDueDate,
        string MaterialDeliveryDate,
        string DespatchingParticulars,
        string Remarks
        )
        //, int companyid
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            con.Open();
            cmd = new SqlCommand("GP_sp_Insert_PerformanceSheetdata_Edit_Individual_Record_RA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Primaryid", Primaryid);
            //cmd.Parameters.AddWithValue("@companyid", companyid);

            cmd.Parameters.AddWithValue("@ordernumber", ordernumber);
            cmd.Parameters.AddWithValue("@Date_of_ReceiptOrder", Date_of_ReceiptOrder);
            cmd.Parameters.AddWithValue("@Store_Produt_Service_Desc", Store_Produt_Service_Desc);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.AddWithValue("@Value", Value);
            cmd.Parameters.AddWithValue("@Name_Consignee", Name_Consignee);
            cmd.Parameters.AddWithValue("@DeliveryDueDate", DeliveryDueDate);
            cmd.Parameters.AddWithValue("@MaterialDeliveryDate", MaterialDeliveryDate);
            cmd.Parameters.AddWithValue("@DespatchingParticulars", DespatchingParticulars);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            

            ErrorId = cmd.ExecuteNonQuery();
            // ErrorId = Convert.ToInt32(outError.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            con.Close();

        }

        return ErrorId;
    }

    protected void gv_PerformanceSheet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userid = Convert.ToInt32(gv_PerformanceSheet.DataKeys[e.RowIndex].Value.ToString());
        List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
        PerformanceSheet dir = mydir.Where(s => s.rowno == userid).Single();
        if (mydir != null)
        {
            mydir.Remove(dir);
        }
        Bind_PerformanceSheet_gridview(mydir);
    }
}