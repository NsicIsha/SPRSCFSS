using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using AjaxControlToolkit;

using System.Data.SqlClient;

public partial class appAdministrator_Calculate_Monetary_Limit : System.Web.UI.Page
{
    public static DataTable Table = new DataTable();
    ArrayList ParameterArray = new ArrayList();
    int[] s = new int[10];
    int unit1, unit2, unit3, unit4, unit5, unitcount;
    int companyid, processid;
    string commalist, FACtorylist;
    int UserId, RoleId, DepartmentId;
    //  string plant_machinary = "1";
    string UserFullName = string.Empty;
    public string plant_machinary { get; set; }
    DataSet ds;


    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            FunRequest();

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;

        }


        int companyid;
        companyid = Convert.ToInt32(Request.QueryString["id"]);

        int isprovisional;
        clsCompany objCompany = new clsCompany();
        isprovisional = objCompany.Company_Provisional_Status(companyid);

        ////// OLD ONE COMMENTED ON 04 DECEMBER 2018 WITH REFERANCE TO BAPendingMonetaryLimit.aspx K C INDIA LABORATRY LLP(IN CASE OF PROVISIONAL RENEWAL FY ENTRY PANEL WAS NOT VISIBLE)
        ////if (isprovisional == 1)
        ////{
        ////    Response.Redirect("Home.aspx");
        ////}

        if (RoleId == 4 || RoleId == 8 || RoleId == 1)
        {
            CreateTemplatedGridView();
            ShowHistory();
        }
        else
        {
            Response.Redirect("Home.aspx");
        }

        if (RoleId == 8)
        {
            btnBack.Visible = false;
            btn_calculate.Text = "Submit";
        }
        else
        {
            btnBack.Visible = true;
            btn_calculate.Text = "Calculate";
        }

    }

    protected void fchange(object sender, EventArgs e)
    {
        lit_monetary.Text = "";
        if (!Page.IsPostBack)
        {

            CreateTemplatedGridView();

        }

    }

    void PopulateDataTable()
    {
        try
        {
            clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();

            companyid = Convert.ToInt32(Request.QueryString["id"]);
            processid = Convert.ToInt32(Request.QueryString["urlid"]);
            if (processid != 3)
            {
                processid = 1;
            }
            DataSet ds = new DataSet();
            ds = objCompanyEdit.Company_Fetch_Monetary_dynamic(companyid, processid);
            Table = ds.Tables[0];
            unitcount = Convert.ToInt32(ds.Tables[1].Rows[0]["unitcount"]);



            Table.Columns.Remove("seqno");
            commalist = Convert.ToString(ds.Tables[2].Rows[0]["unitlist"]);
            FACtorylist = Convert.ToString(ds.Tables[3].Rows[0]["unitaddress"]);
            DataTable dt = new DataTable();

            dt.Clear();
            dt.Columns.Add("Year");
            if (rdo_options.SelectedValue == "0")
            {

                string turnover = "Turnover";
                dt.Columns.Add(turnover);
                string profit = "Profit";
                string loss = "Loss";
                dt.Columns.Add(profit);
                dt.Columns.Add(loss);
            }

            else
            {
                for (int col = 1; col <= unitcount; col++)
                {
                    string turnover = "Turnover" + " " + "" + " " + col.ToString();
                    dt.Columns.Add(turnover);
                    string profit = "Profit" + " " + "" + " " + col.ToString();
                    string loss = "Loss" + " " + "" + " " + col.ToString();
                    dt.Columns.Add(profit);
                    dt.Columns.Add(loss);

                }

            }

            for (int i = 1; i <= 3; i++)
            {
                DataRow oItem = dt.NewRow();
                int index = 0;
                foreach (DataColumn dcol in dt.Columns)
                {

                    oItem[dcol] = "";

                    index = index + 1;
                }
                dt.Rows.Add(oItem);
            }
            Table = dt;


        }


        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    void CreateTemplatedGridView()
    {
        try
        {
            // fill the table which is to bound to the GridView
            PopulateDataTable();


            for (int i = 0; i < Table.Columns.Count; i++)
            {
                TemplateField ItemTmpField = new TemplateField();
                // create HeaderTemplate
                ItemTmpField.HeaderTemplate = new DynamicallyGridView(ListItemType.Header,
                                                              Table.Columns[i].ColumnName,
                                                              Table.Columns[i].DataType.Name, i, Table.Columns.Count);
                // create ItemTemplate
                ItemTmpField.ItemTemplate = new DynamicallyGridView(ListItemType.Item,
                                                              Table.Columns[i].ColumnName,
                                                              Table.Columns[i].DataType.Name, i, Table.Columns.Count);

                // then add to the GridView
                gvmonetary.Columns.Add(ItemTmpField);

            }

            // bind and display the data

            gvmonetary.DataSource = Table;
            gvmonetary.DataBind();

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }
    protected void gvmonetary_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            if (Table.Columns.Count > 4)
            {
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "";
                //   HeaderCell.Height = 5;
                HeaderGridRow.Cells.Add(HeaderCell);
                for (int i = 1; i <= unitcount; i++)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Unit " + i.ToString();
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderCell.ColumnSpan = 3;
                    HeaderGridRow.Cells.Add(HeaderCell);
                }
            }
            lit_message.Text = "<br/>";
            gvmonetary.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txt = (TextBox)e.Row.FindControl(Table.Columns[0].ColumnName);

            //MaskedEditExtender mmyear = new MaskedEditExtender();
            //mmyear.ID = "yearmaskedit";
            //mmyear.TargetControlID = txt.ID;
            //mmyear.Mask = "9999-99";

            string fieldval1 = ((TextBox)e.Row.FindControl(Table.Columns[0].ColumnName)).Text;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            string test = "hello";


        }

    }
    public void InsertHistoryTable(int companyId)
    {
        try
        {
            companyId = Convert.ToInt32(Request.QueryString["id"]);
            clsCompany objCompany = new clsCompany();
            DataSet chekamedment = new DataSet();
            // string Amendmentchek = "";
            // chekamedment = objCompany.Company_fetch_Chek_amedment_firsttime_modified(companyId);
            // if (chekamedment.Tables[0].Rows.Count > 0)
            //  {
            // Amendmentchek = chekamedment.Tables[0].Rows[0]["Amedment"].ToString();
            //  }
            // else
            // {
            // chekamedment.Dispose();
            // }
            // if (Amendmentchek == "Not Amedment")
            //  {
            int k = objCompany.Company_Insert_HistoryTable(companyId);
            if (k == 1)
            {

            }
            //}
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    public void InsertTurnoverTableHistory(int companyId)
    {
        try
        {
            companyId = Convert.ToInt32(Request.QueryString["id"]);
            clsCompany objCompany = new clsCompany();
            int r = objCompany.Company_Insert_HistoryTableTurnOverandMonetryLimit(companyId);
            if (r == 1)
            {
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }

    protected void btn_calculate_Click(object sender, EventArgs e)
    {

         
                DataSet dsCom = new DataSet();
                dsCom = Check_PaymentStaus_Companyid(Convert.ToInt32(Request.QueryString["id"]));
                int Result = 0;
                Result = Convert.ToInt32(dsCom.Tables[0].Rows[0]["PaymentStatus"].ToString());

                if(Result==420)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Payment details is not updated properly for the given unit.');", true);
                    return;
                }
                else
                { 
           


        // insert data on amedment history and turnover history table
        int Urlid = Convert.ToInt32(Request.QueryString["urlid"]);
        if (Urlid == 3)
        {
            int companyId_text = Convert.ToInt32(Request.QueryString["id"]);
            InsertHistoryTable(companyId_text);
        }
        else
        {
        }
        if (Urlid == 1)
        {
            int Companyid_reg = Convert.ToInt32(Request.QueryString["id"]);
            if (Request.QueryString.HasKeys())
            {
                if (Request.QueryString.AllKeys.Contains("edit") == false)
                {
                }
                else
                {
                    if (Request.QueryString["edit"].ToString() == "E")
                    {
                        InsertTurnoverTableHistory(Companyid_reg);
                    }
                    else
                    {
                    }
                }
            }

        }

        int colcount = gvmonetary.Columns.Count;
        string[] arr1 = new string[3];

        //  String  arr2 array to compare 3 successive increment profit 

        string[] arr2 = new string[3];

        foreach (GridViewRow gvrow in gvmonetary.Rows)
        {
            string yearval = ((TextBox)gvrow.Cells[0].FindControl(Table.Columns[0].ColumnName)).Text;
            arr1[gvrow.RowIndex] = yearval;
            if (yearval != string.Empty)
            {
                if (Convert.ToInt32(yearval.Length) < 4 || Convert.ToInt32(yearval.Length) > 7)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Turnover year as  2010-11  in row no " +

     (gvrow.RowIndex + 1) + " ');", true);
                    return;
                }

            }


            for (int i = 1; i < colcount; i++)
            {

                string profitcol, losscol;

                profitcol = Table.Columns[i].ColumnName.ToString();
                if (profitcol.Contains("Profit"))
                {
                    string fieldval1 = ((TextBox)gvrow.Cells[i].FindControl(Table.Columns[i].ColumnName)).Text;
                    string fieldval2 = "";
                    if (i < colcount)
                    {
                        fieldval2 = ((TextBox)gvrow.Cells[i].FindControl(Table.Columns[i + 1].ColumnName)).Text;
                    }

                    if (yearval == string.Empty && ((fieldval1 != string.Empty) || (fieldval2 != string.Empty)))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Turnover year as  Profit or Loss have data  in row no " +

(gvrow.RowIndex + 1) + " ');", true);
                        return;
                    }

                    if (yearval != string.Empty)
                    {
                        if ((fieldval1 == string.Empty) && (fieldval2 == string.Empty))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter  Profit  data  as Turnover year have data in row no " +

    (gvrow.RowIndex + 1) + " ');", true);
                            return;
                        }



                    }


                    if ((fieldval1 != string.Empty) && (fieldval2 != string.Empty))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter either Profit or Loss data  in row no " +

(gvrow.RowIndex + 1) + " ');", true);
                        return;
                    }

                    if ((fieldval1 == string.Empty) && (fieldval2 == "0"))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Profit as 0  in row no " +

  (gvrow.RowIndex + 1) + " . Loss cannot be entered as 0.');", true);
                        return;
                    }

                    if (gvrow.RowIndex == 0)
                    {
                        if ((fieldval1 == string.Empty) && (fieldval2 == string.Empty))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill Profit or Loss  in row no " + (gvrow.RowIndex + 1) + " ');", true);
                            return;
                        }
                    }
                }
            }


            for (int j = 1; j < colcount; j++)
            {

                string profitcol, losscol;

                profitcol = Table.Columns[j].ColumnName.ToString();
                if (profitcol.Contains("Turnover"))
                {
                    string fieldval1 = ((TextBox)gvrow.Cells[j].FindControl(Table.Columns[j].ColumnName)).Text;
                    string fieldval2 = "";
                    j = j + 2;
                    if (gvrow.RowIndex == 0)
                    {
                        if (fieldval1 == string.Empty)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill Turnover  in row no " + (gvrow.RowIndex + 1) + " ');", true);
                            return;
                        }
                    }
                }

            }
        }
        if (arr1[0].ToString() != string.Empty && arr1[1].ToString() != string.Empty && arr1[2].ToString() != string.Empty)
        {
            if (arr1[0].ToString().Substring(0, 4) == arr1[1].ToString().Substring(0, 4))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You have filled in Same Year in more than one row.');", true);
                return;
            }
            if (Convert.ToInt32(arr1[0].ToString().Substring(0, 4)) > Convert.ToInt32(arr1[1].ToString().Substring(0, 4)))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Enter the turnover in ascending order of the Financial Year.');", true);
                return;
            }
            if (Convert.ToInt32(arr1[1].ToString().Substring(0, 4)) > Convert.ToInt32(arr1[2].ToString().Substring(0, 4)))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Enter the turnover in ascending order of the Financial Year.');", true);
                return;
            }
            if (Convert.ToInt32(arr1[0].ToString().Substring(0, 4)) > Convert.ToInt32(arr1[2].ToString().Substring(0, 4)))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Enter the turnover in ascending order of the Financial Year.');", true);
                return;
            }

        }
        if (arr1[0].ToString() != string.Empty && arr1[2].ToString() != string.Empty)
        {
            if (arr1[0].ToString().Substring(0, 4) == arr1[2].ToString().Substring(0, 4))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You have filled in Same Year in more than one row.');", true);
                return;
            }
        }

        if (arr1[1].ToString() != string.Empty && arr1[2].ToString() != string.Empty)
        {
            if (arr1[1].ToString().Substring(0, 4) == arr1[2].ToString().Substring(0, 4))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You have filled in Same Year in more than one row.');", true);
                return;
            }
        }



        string html = Comapnay_Unit1(unitcount);
        //lit_monetary.Text = html;

        string Profitsplit = string.Empty;
        //   return;
        clsCompany objcom = new clsCompany();
        DataSet ds = new DataSet();
        companyid = Convert.ToInt32(Request.QueryString["id"]);
        processid = Convert.ToInt32(Request.QueryString["urlid"]);

        //  return;

        ds = objcom.Company_Turnover_Monetary(html, companyid, Convert.ToInt32(rdo_options.SelectedValue), processid, UserId);

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Profitsplit = ds.Tables[0].Rows[0]["mon"].ToString();
                    // by default ML should 5 for any type start:isha-->
                    if (!string.IsNullOrEmpty(Profitsplit))
                    {
                        double ML = Convert.ToDouble(Profitsplit.Split(',')[0]);
                        if (ML < 5)
                            Profitsplit = "5,";
                    }
                    //by default ML should 5 for any type end:isha<--
                    Calulate_monatary(Profitsplit);
                    lit_monetary.Text = lit_monetary.Text;
                    //  btn_calculate.Enabled = false;
                    btnBack.Enabled = true;
                }
                else
                {
                    //  btn_calculate.Enabled = true;
                    btnBack.Enabled = false;
                    ShowHistory();
                }
            }

        }

          }
    }
    private void Calulate_monatary(string monetarylist)
    {
        double width = gvmonetary.Width.Value;
        string monetaryhtml = "";
        float colwidth = 50; //100 / ((units * 3) + 1);
        StringBuilder str = new StringBuilder();
        string[] words = monetarylist.Split(',');
        decimal consolidated;
        consolidated = 0;
        if (rdo_options.SelectedValue == "0")
        {
            str.Append("<table border='1' width ='100%'  cellpadding='3'cellspacing='0' style='background-color: #ffffff; margin-left:0px;  '>");
            str.Append("<tr><td style ='width:25%;'><b> Monetary Limit : </b> </td> <td colspan ='3'><b> " + words[0].ToString() + "</b><br/> " + " </td> </tr>");
            str.Append("</table>");
            str.Append("<br/><b> Consolidated Monetary Limit  : </b>  " + words[0].ToString() + " ");
        }

        else
        {

            int tablewidth = ((words.Length - 1) * 3 * 50) + 46;
            str.Append("<table  width ='" + tablewidth + "px;'  cellpadding='5'cellspacing='0' style='background-color: #ffffff; margin-left:0px;  '>");
            str.Append("<tr>");
            str.Append("<td style='width :100px; '> <b> Monetary Limit : </b></td>");

            for (int i = 0; i < words.Length - 1; i++)
            {
                str.Append("<td colspan ='3' syle ='width:200px;' align ='left'> <b> Unit " + (i + 1) + " :</b> " + words[i].ToString() + "   </td>");
                consolidated = consolidated + Convert.ToDecimal(words[i].ToString());
            }
            str.Append("</tr>");
            str.Append("</table>");
            str.Append("<br/><b> Consolidated Monetary Limit  : </b>  " + consolidated + " ");
        }

        lit_monetary.Text = str.ToString();
    }


    public DataSet Check_PaymentStaus_Companyid(int CompanyId)
    {
        DataSet MyDs = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();

        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
            MyAdp = new SqlDataAdapter("GPSp_Check_PaymentStaus_Companyid", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (CompanyId != 0) MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyId;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }


    private string Comapnay_Unit1(int noofunit)
    {
        plant_machinary = rdo_plant_machinary.SelectedItem.Value.ToString();
        if (plant_machinary == "")
        {
            plant_machinary = "1";
        }


        Session["previousprofit"] = "";
        StringBuilder str = new StringBuilder();
        str.Append("<Company>");

        string[] unitid = commalist.Split(new char[] { ',' });

        int index = 1;



        //  int k = 1;

        // 3i-2
        if (rdo_options.SelectedValue != "0")
        {
            foreach (GridViewRow gvrow in gvmonetary.Rows)
            {
                int con = unitcount;
                for (int i = 1; i <= noofunit; i++)
                {

                    string field_value0 = ((TextBox)gvrow.FindControl(Table.Columns[0].ColumnName)).Text;

                    if (field_value0 != string.Empty)
                    {
                        str.Append("<Unit>");
                        str.Append("<UnitID>" + unitid[i - 1].ToString() + "</UnitID>");

                        string yearval = "Yearval";// +index.ToString();

                        //    str.Append("<year>");
                        str.Append("<" + yearval + ">");
                        str.Append(field_value0.Substring(0, 4));
                        str.Append("</" + yearval + ">");

                        string turnover = "Turnover";// +index.ToString();
                        string turnoverval = ((TextBox)gvrow.FindControl(Table.Columns[3 * i - 2].ColumnName)).Text;

                        string profit = "Profit";// +index.ToString();
                        string profitval = ((TextBox)gvrow.FindControl(Table.Columns[3 * i - 2 + 1].ColumnName)).Text;

                        string Lossval = ((TextBox)gvrow.FindControl(Table.Columns[3 * i - 2 + 2].ColumnName)).Text;


                        string previousprofitval = "";
                        if (gvrow.RowIndex > 0)
                        {
                            //previousprofitval = ((TextBox)gvmonetary.Rows[gvrow.RowIndex - 1].FindControl(Table.Columns[3 * i - 2 + 1].ColumnName)).Text;
                            previousprofitval = ((TextBox)gvmonetary.Rows[gvrow.RowIndex - 1].FindControl(Table.Columns[3 * i - 2].ColumnName)).Text;
                        }
                        if (gvrow.RowIndex == 0)
                        {
                            if (Lossval == string.Empty)
                            {
                                str.Append("<PreviousProfit>T</PreviousProfit>");
                            }
                            else
                            {
                                str.Append("<PreviousProfit>F</PreviousProfit>");
                            }

                        }
                        else
                        {
                            if (Lossval != string.Empty)
                            {
                                if (turnoverval != string.Empty && previousprofitval != string.Empty)
                                    str.Append("<PreviousProfit>F</PreviousProfit>");
                            }
                            else
                            {
                                if (turnoverval != string.Empty && previousprofitval != string.Empty && profitval != string.Empty)
                                {

                                    if (Convert.ToDecimal(turnoverval) > Convert.ToDecimal(previousprofitval))
                                    {
                                        str.Append("<PreviousProfit>T</PreviousProfit>");
                                    }

                                    else
                                    {
                                        str.Append("<PreviousProfit>F</PreviousProfit>");
                                    }
                                }
                            }

                        }

                        Session["previousprofit"] = profitval;

                        string loss = "Loss";// +index.ToString();


                        str.Append("<" + turnover + ">" + turnoverval + "</" + turnover + ">");
                        str.Append("<" + profit + ">" + profitval + "</" + profit + ">");
                        str.Append("<" + loss + ">" + Lossval + "</" + loss + ">");
                        str.Append("<Plant>" + plant_machinary + "</Plant>");
                        str.Append("</Unit>");

                    }


                }
            }
        }
        else
        {
            string previousvalue = "";
            string checkprofit = "T";
            foreach (GridViewRow gvrow in gvmonetary.Rows)
            {
                for (int i = 1; i <= noofunit; i++)
                {
                    string yearval = "Yearval";// +index.ToString();
                    string field_value0 = ((TextBox)gvrow.FindControl(Table.Columns[0].ColumnName)).Text;

                    if (field_value0 != string.Empty)
                    {
                        str.Append("<Unit>");
                        str.Append("<UnitID>" + unitid[i - 1].ToString() + "</UnitID>");


                        //    str.Append("<year>");

                        str.Append("<" + yearval + ">");
                        str.Append(field_value0.Substring(0, 4));
                        str.Append("</" + yearval + ">");

                        string turnover = "Turnover";// +index.ToString();
                        string turnoverval = ((TextBox)gvrow.FindControl(Table.Columns[1].ColumnName)).Text;

                        string profit = "Profit";// +index.ToString();
                        string profitval = ((TextBox)gvrow.FindControl(Table.Columns[2].ColumnName)).Text;

                        string loss = "Loss";// +index.ToString();
                        string Lossval = ((TextBox)gvrow.FindControl(Table.Columns[3].ColumnName)).Text;



                        if (gvrow.RowIndex > 0)
                        {
                            if (Lossval != string.Empty)
                            {
                                checkprofit = "F";
                            }
                            else
                            {
                                if (previousvalue != string.Empty && i == 1 && turnoverval != string.Empty && profitval != string.Empty)
                                {
                                    if (Convert.ToDecimal(turnoverval) > Convert.ToDecimal(previousvalue))
                                    {
                                        checkprofit = "T";
                                    }
                                    else
                                    {
                                        checkprofit = "F";
                                    }
                                }
                            }


                        }
                        else
                        {
                            if (Lossval != string.Empty)
                            {
                                checkprofit = "F";
                            }
                            else
                            {
                                checkprofit = "T";
                            }
                        }
                        if (i == 1)
                        {
                            previousvalue = ((TextBox)gvrow.FindControl(Table.Columns[1].ColumnName)).Text; ;
                        }

                        string checkfalse = "F";
                        string Previousyearprofitval = ((TextBox)gvrow.FindControl(Table.Columns[1].ColumnName)).Text;


                        str.Append("<" + turnover + ">" + turnoverval + "</" + turnover + ">");
                        str.Append("<" + profit + ">" + profitval + "</" + profit + ">");
                        if (gvrow.RowIndex > 0)
                        {
                            str.Append("<PreviousProfit>" + checkprofit + "</PreviousProfit>");
                        }
                        else
                        {

                            str.Append("<PreviousProfit>" + checkprofit + "</PreviousProfit>");
                        }

                        str.Append("<" + loss + ">" + Lossval + "</" + loss + ">");
                        str.Append("<Plant>" + plant_machinary + "</Plant>");
                        str.Append("</Unit>");
                    }


                }
            }
        }
        index = index + 1;
        //    str.Append("</year>");


        str.Append("</Company>");
        return str.ToString();


    }
    private void processgridview()
    {
        int i = 1;
        for (i = 1; i <= gv_display.Rows.Count - 1; i++)
        {
            if (i == 3)
            {
                string text = gv_display.Rows[i].Cells[0].Text;
                gv_display.Rows[i].Cells[0].BorderWidth = 0;
                //  gv_display.Rows[i].Cells[2].Attributes.Add("style", " border-width:0;");
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (RoleId == 1)
        {
            int type = Convert.ToInt32(Request.QueryString["type"]);
            Response.Redirect("Convertprovisonaltofreshregistration.aspx?i=" + Convert.ToInt32(type), true);
        }
        else
        {
            processid = Convert.ToInt32(Request.QueryString["urlid"]);
            Response.Redirect("BAPendingMonetaryLimit.aspx?i=" + Convert.ToInt32(processid), true);
        }
    }
    void ShowHistory()
    {
        try
        {
            clsCompany objcom = new clsCompany();
            ds = new DataSet();
            companyid = Convert.ToInt32(Request.QueryString["id"]);
            processid = Convert.ToInt32(Request.QueryString["urlid"]);
            ds = objcom.FetchTurnover_history(Convert.ToInt32(companyid), Convert.ToInt32(processid));
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    // rdo_options.SelectedValue = ds.Tables[0].Rows[0]["type"].ToString();
                    ds.Tables[1].Columns.Remove("id");
                    DataTable dt = new DataTable();
                    dt = ds.Tables[1];
                    if (dt.Rows.Count > 1)
                    {
                        gv_display.DataSource = dt;
                        gv_display.DataBind();
                        // gv_display.Columns[0].Visible = false;
                        processgridview();
                    }

                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdo_plant_machinary_SelectedIndexChanged(object sender, EventArgs e)
    {

        plant_machinary = rdo_plant_machinary.SelectedItem.Value.ToString();
        if (plant_machinary == "")
        {
            plant_machinary = "1";
        }

    }
}





public class DynamicallyGridView : ITemplate
{
    #region data memebers

    ListItemType ItemType;
    string FieldName;
    string InfoType;
    int colindex;
    int indexinfotype = 0;
    int totalcolaumn = 0;

    #endregion

    #region constructor

    public DynamicallyGridView(ListItemType item_type, string field_name, string info_type, int index, int columns)
    {
        ItemType = item_type;
        FieldName = field_name;
        InfoType = info_type;
        colindex = index;
        totalcolaumn = columns;
    }

    #endregion


    #region Methods

    public void InstantiateIn(System.Web.UI.Control Container)
    {

        switch (ItemType)
        {
            case ListItemType.Header:
                Literal header_ltrl = new Literal();
                //   header_ltrl.Text = "<b>" + FieldName + "</b>";
                FieldName = Regex.Replace(FieldName, "[0-9]", "");

                header_ltrl.Text = FieldName;
                Container.Controls.Add(header_ltrl);
                break;
            case ListItemType.Item:
                switch (InfoType)
                {
                    case "Command":

                        break;

                    default:
                        // int index = colindex;

                        TextBox field_txt = new TextBox();
                        field_txt.ID = FieldName;
                        if (totalcolaumn > 4)
                        {
                            if (colindex == 0)
                            {

                                field_txt.Width = 100;
                            }
                            else
                            {
                                field_txt.MaxLength = 15;
                                field_txt.Width = 50;
                            }

                        }


                        field_txt.Text = "";


                        field_txt.DataBinding += new EventHandler(OnDataBinding);



                        Container.Controls.Add(field_txt);

                        //if (HttpContext.Current.Session["key"] != null)
                        //{
                        //    HttpContext.Current.Session["key"] = index;
                        //}
                        //else
                        //{
                        //    HttpContext.Current.Session["key"] = index + (int)HttpContext.Current.Session["key"];
                        //}

                        //  string value = HttpContext.Current.Session["key"].ToString();
                        if (colindex != 0)
                        {

                            field_txt.Attributes.Add("onkeypress", "return onlyDotsAndNumbers(this,event)");
                        }
                        else
                        {
                            //field_txt.Attributes.Add("onkeypress", "return Yearvalidation(this,event)");
                            //field_txt.Attributes.Add("onchange()", "return yearonchange()");
                            field_txt.Attributes.Add("onblur", "return masking(this.value,this,event)");
                            field_txt.Attributes.Add("onKeyPress", "return masking(this.value,this,event)");
                        }
                        // field_txt.Attributes.Add("onkeypress", "return onlyDotsAndNumbers(this,event)");
                        //  field_txt.Attributes.Add("onkeypress", "return isNumber(event)");
                        break;
                }
                break;
            case ListItemType.EditItem:
                if (InfoType == "Command")
                {


                }
                else// for other 'non-command' i.e. the key and non key fields, bind textboxes with corresponding field values
                {


                }
                break;

        }

    }

    #endregion

    #region Event Handlers

    //just sets the insert flag OFF so that we ll be able to decide in OnRowUpdating event whether to insert or update 
    protected void edit_button_Click(Object sender, EventArgs e)
    {
        new Page().Session["InsertFlag"] = 0;
    }


    private void OnDataBinding(object sender, EventArgs e)
    {

        object bound_value_obj = null;
        Control ctrl = (Control)sender;
        IDataItemContainer data_item_container = (IDataItemContainer)ctrl.NamingContainer;
        bound_value_obj = DataBinder.Eval(data_item_container.DataItem, FieldName);

        switch (ItemType)
        {
            case ListItemType.Item:
                //    Label field_ltrl = (Label)sender;
                TextBox field_ltrl = (TextBox)sender;
                field_ltrl.Text = bound_value_obj.ToString();
                // indexinfotype = indexinfotype + 1;

                //if (field_ltrl.ID.Contains("Year"))
                //{
                //   // MaskedTextBox txtmask = new MaskedTextBox();
                //    MaskedEditExtender txtmask = new MaskedEditExtender();
                //    txtmask.ID = "YearMask";
                //    txtmask.TargetControlID = field_ltrl.ID;
                //    txtmask.MaskType = MaskedEditType.Number;
                //    txtmask.Mask = "9999-99";
                //}

                // field_ltrl.Text = indexinfotype.ToString() + field_ltrl.ID;
                break;
            case ListItemType.EditItem:
                TextBox field_txtbox = (TextBox)sender;
                field_txtbox.Text = bound_value_obj.ToString();

                break;
        }


    }

    #endregion

}
