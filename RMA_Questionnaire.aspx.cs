using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
public partial class RMA_Questionnaire : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        { 
        SetInitialRow();
            SetInitialRow_SubRawMaterials();
        }
    }

    private string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int rowIndex = 0;
        StringCollection sc = new StringCollection();
        if (ViewState["RMBDetails"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["RMBDetails"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values  
                    TextBox txtNameofRawMaterial = (TextBox)grdRMB_Details.Rows[rowIndex].Cells[1].FindControl("txtNameofRawMaterial");
                    TextBox txtMaterialPrice = (TextBox)grdRMB_Details.Rows[rowIndex].Cells[2].FindControl("txtMaterialPrice");
                    TextBox txtQuantity = (TextBox)grdRMB_Details.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                    TextBox txtSupName = (TextBox)grdRMB_Details.Rows[rowIndex].Cells[4].FindControl("txtSupName");
                    TextBox txtAltSupName = (TextBox)grdRMB_Details.Rows[rowIndex].Cells[5].FindControl("txtAltSupName");
                    



                    DropDownList ddlMode = (DropDownList)grdRMB_Details.Rows[rowIndex].Cells[6].FindControl("ddlMode");
                    string sss = ddlMode.SelectedValue;
                   
                    
                    sc.Add(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", txtUAM.Text, txtAadhaarNumber.Text, txtNameofRawMaterial.Text, txtMaterialPrice.Text, txtQuantity.Text, txtSupName.Text, txtAltSupName.Text, ddlMode.SelectedItem.Text));
                    rowIndex++;
                }
                //Call the method for executing inserts  
                InsertRecords(sc);
            }
        }

        // Sub Raw Material Details

        int rowIndex_Sub = 0;
        StringCollection sc_Sub = new StringCollection();
        if (ViewState["RMB_SubRawMaterialsDetails"] != null)
        {
            DataTable dtCurrentTable_Sub = (DataTable)ViewState["RMB_SubRawMaterialsDetails"];
            if (dtCurrentTable_Sub.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable_Sub.Rows.Count; i++)
                {
                    //extract the TextBox values  
                    TextBox txtNameofComponents = (TextBox)grdRMB_SubRaw_Details.Rows[rowIndex_Sub].Cells[1].FindControl("txtNameofComponents");
                    TextBox txtSpecification = (TextBox)grdRMB_SubRaw_Details.Rows[rowIndex_Sub].Cells[2].FindControl("txtSpecification");
                    TextBox txtQuantityPPerMonth = (TextBox)grdRMB_SubRaw_Details.Rows[rowIndex_Sub].Cells[3].FindControl("txtQuantityPPerMonth");
                    TextBox txtSupNameAddress = (TextBox)grdRMB_SubRaw_Details.Rows[rowIndex_Sub].Cells[4].FindControl("txtSupNameAddress");
                    TextBox txtAltSupSource = (TextBox)grdRMB_SubRaw_Details.Rows[rowIndex_Sub].Cells[5].FindControl("txtAltSupSource");


                    sc_Sub.Add(string.Format("{0},{1},{2},{3},{4},{5},{6}", txtUAM.Text, txtAadhaarNumber.Text, txtNameofComponents.Text, txtSpecification.Text, txtQuantityPPerMonth.Text, txtSupNameAddress.Text, txtAltSupSource.Text));
                    rowIndex_Sub++;
                }
                //Call the method for executing inserts  
                InsertRecords_SubRawMaterials(sc_Sub);
            }
        }





    }

    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {

        // AddNewRowToGrid();
        if (ViewState["RMBDetails"] != null)
        {
            grdRMB_Details.DataSource = ViewState["RMBDetails"];
            grdRMB_Details.DataBind();
        }


        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        if (row != null)
        {
            string selectedValue = ((DropDownList)(row.FindControl("ddlMode"))).SelectedValue;
        }

    }

    private void InsertRecords(StringCollection sc)
    {
        StringBuilder sb = new StringBuilder(string.Empty);
        string[] splitItems = null;
        const string sqlStatement = "INSERT INTO RMB_RawMaterialDetails (RMB_UAM, RMB_AadhaarNumber, RMB_RawMaterialName, RMB_Price, RMB_Quantity, RMB_Supplier, RMB_AltSupplier, RMB_Mode_Sourcing) VALUES";
        foreach (string item in sc)
        {
            if (item.Contains(","))
            {
                splitItems = item.Split(",".ToCharArray());
                sb.AppendFormat("{0}('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'); ", sqlStatement, splitItems[0], splitItems[1], splitItems[2], splitItems[3], splitItems[4], splitItems[5], splitItems[6], splitItems[7]);
            }
        }

        using (SqlConnection connection = new SqlConnection("server=DESKTOP-H31IATS; database=NSICSPR_LIVE; UID=sa; Pwd=nsic; Max Pool Size=10000; Connection Timeout=7200;"))
        {
            connection.Open();
            using (SqlCommand cmd = new SqlCommand(sb.ToString(), connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
      //  lblMsg.Text = "Records successfully saved!";
        ScriptManager.RegisterStartupScript(this.updSubmt, typeof(string), "Alert", "alert('Data has been added successfully.');", true);

        SetInitialRow();

    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    private void AddNewRowToGrid()
    {

        if (ViewState["RMBDetails"] != null)
        {

            DataTable dtCurrentTable = (DataTable)ViewState["RMBDetails"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   

                ViewState["RMBDetails"] = dtCurrentTable;


                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {

                    //extract the TextBox values   

                    TextBox txtNameofRawMaterial = (TextBox)grdRMB_Details.Rows[i].Cells[1].FindControl("txtNameofRawMaterial");
                    TextBox txtMaterialPrice = (TextBox)grdRMB_Details.Rows[i].Cells[2].FindControl("txtMaterialPrice");

                    TextBox txtQuantity = (TextBox)grdRMB_Details.Rows[i].Cells[3].FindControl("txtQuantity");
                    TextBox txtSupName = (TextBox)grdRMB_Details.Rows[i].Cells[4].FindControl("txtSupName");
                    TextBox txtAltSupName = (TextBox)grdRMB_Details.Rows[i].Cells[5].FindControl("txtAltSupName");
                    


                    dtCurrentTable.Rows[i]["Column1"] = txtNameofRawMaterial.Text;
                    dtCurrentTable.Rows[i]["Column2"] = txtMaterialPrice.Text;
                    dtCurrentTable.Rows[i]["Column3"] = txtQuantity.Text;
                    dtCurrentTable.Rows[i]["Column4"] = txtSupName.Text;
                    dtCurrentTable.Rows[i]["Column5"] = txtAltSupName.Text;

                    //extract the DropDownList Selected Items   

                    DropDownList ddlMode = (DropDownList)grdRMB_Details.Rows[i].Cells[6].FindControl("ddlMode");
                    

                    // Update the DataRow with the DDL Selected Items   

                    dtCurrentTable.Rows[i]["Column6"] = ddlMode.SelectedItem.Text;
                   

                }

                //Rebind the Grid with the current data to reflect changes   
                grdRMB_Details.DataSource = ViewState["RMBDetails"];
                grdRMB_Details.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");

        }
        //Set Previous Data on Postbacks   
        SetPreviousData();
    }

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));//for DropDownList selected item   
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));//for DropDownList selected item  
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));//for DropDownList selected item 
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));//for DropDownList selected item 
         

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        //dr["Column2"] = string.Empty;

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["RMBDetails"] = dt;

        //Bind the Gridview   
        grdRMB_Details.DataSource = dt;
        grdRMB_Details.DataBind();

        ////After binding the gridview, we can then extract and fill the DropDownList with Data   
        //DropDownList ddl1 = (DropDownList)Gridview1.Rows[0].Cells[3].FindControl("DropDownList1");
        //DropDownList ddl2 = (DropDownList)Gridview1.Rows[0].Cells[4].FindControl("DropDownList2");
        //FillDropDownList(ddl1);
        //FillDropDownList(ddl2);
    }
    private void SetPreviousData()
    {

        int rowIndex = 0;
        if (ViewState["RMBDetails"] != null)
        {

            DataTable dt = (DataTable)ViewState["RMBDetails"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox txtNameofRawMaterial = (TextBox)grdRMB_Details.Rows[i].Cells[1].FindControl("txtNameofRawMaterial");
                    TextBox txtMaterialPrice = (TextBox)grdRMB_Details.Rows[i].Cells[2].FindControl("txtMaterialPrice");
                    TextBox txtQuantity = (TextBox)grdRMB_Details.Rows[i].Cells[3].FindControl("txtQuantity");
                    TextBox txtSupName = (TextBox)grdRMB_Details.Rows[i].Cells[4].FindControl("txtSupName");
                    TextBox txtAltSupName = (TextBox)grdRMB_Details.Rows[i].Cells[5].FindControl("txtAltSupName");


                    DropDownList ddl1 = (DropDownList)grdRMB_Details.Rows[rowIndex].Cells[6].FindControl("ddlMode");
                    

                    ////Fill the DropDownList with Data   
                    //FillDropDownList(ddl1);
                    //FillDropDownList(ddl2);

                    if (i < dt.Rows.Count - 1)
                    {

                        //Assign the value from DataTable to the TextBox   
                        txtNameofRawMaterial.Text = dt.Rows[i]["Column1"].ToString();
                        txtMaterialPrice.Text = dt.Rows[i]["Column2"].ToString();

                        txtQuantity.Text = dt.Rows[i]["Column3"].ToString();
                        txtSupName.Text = dt.Rows[i]["Column4"].ToString();
                        txtAltSupName.Text = dt.Rows[i]["Column5"].ToString();
                        


                        //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                        ddl1.ClearSelection();
                        ddl1.Items.FindByText(dt.Rows[i]["Column6"].ToString()).Selected = true;

                        //ddl2.ClearSelection();
                        //ddl2.Items.FindByText(dt.Rows[i]["Column4"].ToString()).Selected = true;

                    }

                    rowIndex++;
                }
            }
        }
    }


    protected void ButtonAddSubRaw_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid_SubRawMaterials();
    }

    private void AddNewRowToGrid_SubRawMaterials()
    {

        if (ViewState["RMB_SubRawMaterialsDetails"] != null)
        {

            DataTable dtCurrentTable_Sub = (DataTable)ViewState["RMB_SubRawMaterialsDetails"];
            DataRow drCurrentRow_Sub = null;

            if (dtCurrentTable_Sub.Rows.Count > 0)
            {
                drCurrentRow_Sub = dtCurrentTable_Sub.NewRow();
                drCurrentRow_Sub["RowNumber"] = dtCurrentTable_Sub.Rows.Count + 1;

                //add new row to DataTable   
                dtCurrentTable_Sub.Rows.Add(drCurrentRow_Sub);
                //Store the current data to ViewState for future reference   

                ViewState["RMB_SubRawMaterialsDetails"] = dtCurrentTable_Sub;


                for (int i = 0; i < dtCurrentTable_Sub.Rows.Count - 1; i++)
                {

                    //extract the TextBox values   

                    TextBox txtNameofComponents = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[1].FindControl("txtNameofComponents");
                    TextBox txtSpecification = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[2].FindControl("txtSpecification");

                    TextBox txtQuantityPPerMonth = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[3].FindControl("txtQuantityPPerMonth");
                    TextBox txtSupNameAddress = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[4].FindControl("txtSupNameAddress");
                    TextBox txtAltSupSource = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[5].FindControl("txtAltSupSource");



                    dtCurrentTable_Sub.Rows[i]["Column1"] = txtNameofComponents.Text;
                    dtCurrentTable_Sub.Rows[i]["Column2"] = txtSpecification.Text;
                    dtCurrentTable_Sub.Rows[i]["Column3"] = txtQuantityPPerMonth.Text;
                    dtCurrentTable_Sub.Rows[i]["Column4"] = txtSupNameAddress.Text;
                    dtCurrentTable_Sub.Rows[i]["Column5"] = txtAltSupSource.Text;

                    


                }

                //Rebind the Grid with the current data to reflect changes   
                grdRMB_SubRaw_Details.DataSource = ViewState["RMB_SubRawMaterialsDetails"];
                grdRMB_SubRaw_Details.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");

        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Sub();
    }

    private void SetInitialRow_SubRawMaterials()
    {

        DataTable dt_Sub = new DataTable();
        DataRow dr_Sub = null;

        dt_Sub.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt_Sub.Columns.Add(new DataColumn("Column1", typeof(string)));//for TextBox value   
        dt_Sub.Columns.Add(new DataColumn("Column2", typeof(string)));//for TextBox value   
        dt_Sub.Columns.Add(new DataColumn("Column3", typeof(string)));//for DropDownList selected item   
        dt_Sub.Columns.Add(new DataColumn("Column4", typeof(string)));//for DropDownList selected item  
        dt_Sub.Columns.Add(new DataColumn("Column5", typeof(string)));//for DropDownList selected item 



        dr_Sub = dt_Sub.NewRow();
        dr_Sub["RowNumber"] = 1;
        dr_Sub["Column1"] = string.Empty;
        dr_Sub["Column2"] = string.Empty;
        dr_Sub["Column3"] = string.Empty;
        dr_Sub["Column4"] = string.Empty;
        dr_Sub["Column5"] = string.Empty;


        dt_Sub.Rows.Add(dr_Sub);

        //Store the DataTable in ViewState for future reference   
        ViewState["RMB_SubRawMaterialsDetails"] = dt_Sub;

        //Bind the Gridview   
        grdRMB_SubRaw_Details.DataSource = dt_Sub;
        grdRMB_SubRaw_Details.DataBind();

        ////After binding the gridview, we can then extract and fill the DropDownList with Data   
        //DropDownList ddl1 = (DropDownList)Gridview1.Rows[0].Cells[3].FindControl("DropDownList1");
        //DropDownList ddl2 = (DropDownList)Gridview1.Rows[0].Cells[4].FindControl("DropDownList2");
        //FillDropDownList(ddl1);
        //FillDropDownList(ddl2);
    }

    private void InsertRecords_SubRawMaterials(StringCollection sc_Sub)
    {
        StringBuilder sb_Sub = new StringBuilder(string.Empty);
        string[] splitItems = null;
        const string sqlStatement = "INSERT INTO RMB_Sub_RawMaterialDetails (RMB_UAM, RMB_AadhaarNumber, RMB_RawMaterialName, RMB_Price, RMB_Quantity, RMB_Supplier, RMB_AltSupplier) VALUES";
        foreach (string item in sc_Sub)
        {
            if (item.Contains(","))
            {
                splitItems = item.Split(",".ToCharArray());
                sb_Sub.AppendFormat("{0}('{1}','{2}','{3}','{4}','{5}','{6}','{7}'); ", sqlStatement, splitItems[0], splitItems[1], splitItems[2], splitItems[3], splitItems[4], splitItems[5], splitItems[6] );
            }
        }

        using (SqlConnection connection = new SqlConnection("server=DESKTOP-H31IATS; database=NSICSPR_LIVE; UID=sa; Pwd=nsic; Max Pool Size=10000; Connection Timeout=7200;"))
        {
            connection.Open();
            using (SqlCommand cmd = new SqlCommand(sb_Sub.ToString(), connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        //  lblMsg.Text = "Records successfully saved!";
        ScriptManager.RegisterStartupScript(this.updSubmt, typeof(string), "Alert", "alert('Data has been added successfully.');", true);

        SetInitialRow_SubRawMaterials();

    }
    private void SetPreviousData_Sub()
    {

        int rowIndex = 0;
        if (ViewState["RMB_SubRawMaterialsDetails"] != null)
        {

            DataTable dt = (DataTable)ViewState["RMB_SubRawMaterialsDetails"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox txtNameofComponents = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[1].FindControl("txtNameofComponents");
                    TextBox txtSpecification = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[2].FindControl("txtSpecification");
                    TextBox txtQuantityPPerMonth = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[3].FindControl("txtQuantityPPerMonth");
                    TextBox txtSupNameAddress = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[4].FindControl("txtSupNameAddress");
                    TextBox txtAltSupSource = (TextBox)grdRMB_SubRaw_Details.Rows[i].Cells[5].FindControl("txtAltSupSource");
                     

                    if (i < dt.Rows.Count - 1)
                    {

                        //Assign the value from DataTable to the TextBox   
                        txtNameofComponents.Text = dt.Rows[i]["Column1"].ToString();
                        txtSpecification.Text = dt.Rows[i]["Column2"].ToString();

                        txtQuantityPPerMonth.Text = dt.Rows[i]["Column3"].ToString();
                        txtSupNameAddress.Text = dt.Rows[i]["Column4"].ToString();
                        txtAltSupSource.Text = dt.Rows[i]["Column5"].ToString(); 
                    }

                    rowIndex++;
                }
            }
        }
    }
}