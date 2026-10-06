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

public partial class BacklogUnit_data : System.Web.UI.Page
{
    public static DataTable Table = new DataTable();
    ArrayList ParameterArray = new ArrayList();
    int[] s = new int[10];
    int unit1, unit2, unit3, unit4, unit5, unitcount;
    int companyid, processid;
    string commalist;
    int UserId, RoleId;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
           
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            
        }
        CreateTemplatedGridView();
    }

    void PopulateDataTable()
    {
        try
        {
            clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();

            companyid = Convert.ToInt32(Request.QueryString["id"]);
            processid = 1; //Convert.ToInt32(Request.QueryString["urlid"]);
            DataSet ds = new DataSet();
            ds = objCompanyEdit.Company_Fetch_Monetary_dynamic(companyid, processid);
            Table = ds.Tables[0];
            unitcount = Convert.ToInt32(ds.Tables[1].Rows[0]["unitcount"]);
            Table.Columns.Remove("seqno");
            commalist = Convert.ToString(ds.Tables[2].Rows[0]["unitlist"]);

            if (Table != null)
            {
                if (Table.Rows.Count <= 1)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Year");
                    for (int col = 1; col <= unitcount; col++)
                    {
                        string unit = "Unit" + " " + col.ToString();
                        dt.Columns.Add(unit);
                    }

                    for (int i = 1; i <= 4; i++)
                    {
                        DataRow oItem = dt.NewRow();
                        int index = 0;
                        foreach (DataColumn dcol in dt.Columns)
                        {
                           if(i == 4 && index == 0 )
                           {
                               oItem[dcol] = "Monetary Limit";
                           }
                            else
                           {
                            oItem[dcol] = "";
                           }

                           index = index + 1;
                        }
                        dt.Rows.Add(oItem);
                    }
                    Table = dt;
                }
                
            }

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
                ItemTmpField.HeaderTemplate = new DynamicallyTemplatedGridViewHandler(ListItemType.Header,
                                                              Table.Columns[i].ColumnName,
                                                              Table.Columns[i].DataType.Name,i);
                // create ItemTemplate
                ItemTmpField.ItemTemplate = new DynamicallyTemplatedGridViewHandler(ListItemType.Item,
                                                              Table.Columns[i].ColumnName,
                                                              Table.Columns[i].DataType.Name,i);
            
                // then add to the GridView
                gvmonetary.Columns.Add(ItemTmpField);

            }

            // bind and display the data

            gvmonetary.DataSource = Table;
            gvmonetary.DataBind();
            //     gvmonetary.Columns[1].Visible = false;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    private string Comapnay_Unit1( int noofunit)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<Turnover>");
      //  str.Append("<Comapny>");

    
        string[] unitid = commalist.Split(new char[] { ',' });
      
        for (int i = 1; i <= noofunit; i++)
        {
            str.Append("<Unit>");
            str.Append("<CompanyId>" + companyid + "</CompanyId>");
            str.Append("<UserId>" + UserId + "</UserId>");
            str.Append("<UnitID>" + unitid[i-1].ToString() + "</UnitID>");
            int index = 1;
            foreach (GridViewRow gvrow in gvmonetary.Rows)
            {
                int con = unitcount;
                string yearval = "Year" + index.ToString();
                string field_value0 = ((TextBox)gvrow.FindControl(Table.Columns[0].ColumnName)).Text;

                if (index == 4)
                {
                    str.Append("<MonetaryLimit>" + field_value0 + "</MonetaryLimit>");
                }
                else
                {
                    str.Append("<" + yearval + ">" + field_value0 + "</" + yearval + ">");
                }

                string field_value1 = ((TextBox)gvrow.FindControl(Table.Columns[i].ColumnName)).Text;
                string yearamtval = "Yearamt" + index.ToString();
                if (index == 4)
                {

                    //str.Append("<MonetaryLimit" + i + ">" + field_value1 + "</MonetaryLimit" + i + ">");
                    str.Append("<MonetaryLimitval>" + field_value1 + "</MonetaryLimitval>");
                }
                else
                {
                    str.Append("<" + yearamtval + ">" + field_value1 + "</" + yearamtval + ">");
                }
                index = index + 1;
            }
            str.Append("</Unit>");
        }
              
          
         
           // str.Append("</Comapny>");
            str.Append("</Turnover>");
            return str.ToString();
        }



    protected void btn_find_Click(object sender, EventArgs e)
    {
       
        string xmldata = "";
     
        bool chkblank = false;
        int index = 1;
        foreach (GridViewRow gvrow in gvmonetary.Rows)
        {  
            string field_value0 = ((TextBox)gvrow.FindControl(Table.Columns[0].ColumnName)).Text;
            string field_value1 = ((TextBox)gvrow.FindControl(Table.Columns[1].ColumnName)).Text;
            if (field_value0 == string.Empty && field_value1 == string.Empty)
            {
                chkblank = true;
            }
            else
            {
                chkblank = false;
            }
            if (index == 4)
            {
                if (field_value0 == "Monetary Limit" && field_value1 == string.Empty)
                {
                    chkblank = true;
                }
                else
                {
                    chkblank = false;
                }
            }

            index = index + 1;
        }

        if (chkblank == true)
        {
            //string message = "Please fill atleast one entry for monetary & turnover";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("alert('");
            //sb.Append(message);
            //sb.Append("');");
            //ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());

            lblMessage.Text = "Please fill atleast one entry for monetary limit & turnover";

            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);
            // Response.Write("<script>alert('Please fill atleast one entry for monetary & turnover');</script>");
            // ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
            return;
        }
        else
        {
            lblMessage.Text = string.Empty;
        }
        xmldata = Comapnay_Unit1(unitcount);

        //if (unitcount == 1)
        //{
        //    xmldata = Comapnay_Unit1();
        //}

        clsCompanyBacklog objcom = new clsCompanyBacklog();
     int error = objcom.BacklogUnit_insert(xmldata,Convert.ToDouble(txt_basicfee.Text),
         Convert.ToDouble(txt_servicetax.Text), Convert.ToDouble(txt_total.Text), txt_registrationdate.Text, txt_referenceno.Text, txt_registrationno.Text);

     if (error == 1)
     {
         Response.Redirect("./Home.aspx", false);
     }

    }

   
}













public class DynamicallyTemplatedGridViewHandler : ITemplate
{
    #region data memebers

    ListItemType ItemType;
    string FieldName;
    string InfoType;
    int Currentrow;

    #endregion

    #region constructor

    public DynamicallyTemplatedGridViewHandler(ListItemType item_type, string field_name, string info_type,int rowno)
    {
        ItemType = item_type;
        FieldName = field_name;
        InfoType = info_type;
        Currentrow = rowno;
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
                header_ltrl.Text = FieldName;
                Container.Controls.Add(header_ltrl);
                break;
            case ListItemType.Item:
                switch (InfoType)
                {
                    case "Command":
                       
                        break;

                    default:

                        

                        TextBox field_txt = new TextBox();
                        field_txt.ID = FieldName;
                        field_txt.Text = "";
                        field_txt.DataBinding += new EventHandler(OnDataBinding);
                        Container.Controls.Add(field_txt);
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
                if (data_item_container.DisplayIndex == 3 && FieldName != "Year")
                {
                    field_ltrl.Attributes.Add("onkeypress", "return checkNumeric(event)");
                }
                else
                {
                    field_ltrl.Attributes.Add("onkeypress", "return checkDECIMAL(event)");
                }
                break;
            case ListItemType.EditItem:
                TextBox field_txtbox = (TextBox)sender;
                field_txtbox.Text = bound_value_obj.ToString();

                break;
        }


    }

    #endregion

}
