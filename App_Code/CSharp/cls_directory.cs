using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for cls_directory
/// </summary>
public class cls_director
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    public cls_director()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<Director> FetchCompanyDirector(int CompanyId)
    {
        MyDt = new DataTable();
        clsCompany objcompany = new clsCompany();
        MyDt = objcompany.Company_FetchDirectorList(CompanyId);
        List<Director> DList = new List<Director>();
        DList.Clear();
        if (MyDt != null)
        {

            if (MyDt.Rows.Count > 0)
            {
                foreach (DataRow drow in MyDt.Rows)
                {
                    Director drct = new Director();
                    drct.rowno = Convert.ToInt32(drow["rowno"].ToString());
                    drct.CompanyId = CompanyId.ToString(); ;
                    drct.Name = drow["fGPtbl_Company_Directors_Name"].ToString();
                    drct.Address = drow["fGPtbl_Company_Directors_Address"].ToString();
                    drct.Pincode = drow["fGPtbl_Company_Directors_Zip"].ToString();
                    //   drct.Pincode = drow["fGPtbl_Company_Directors_Zip"].ToString();
                    drct.stateid = drow["fGPtbl_Company_Directors_StateId"].ToString();
                    drct.State = drow["fGPtbl_MS_Name"].ToString();
                    drct.cityId = drow["fGPtbl_Company_Directors_CityId"].ToString();
                    drct.City = drow["fGPtbl_MCty_Name"].ToString();
                    drct.Telephone = drow["fGPtbl_Company_Directors_Phone"].ToString();
                    drct.ShareHolding = drow["fGPtbl_Company_Directors_Shareholder"].ToString();
                    drct.email = drow["fGPtbl_Company_Directors_Email"].ToString();
                    drct.experience = Convert.ToInt16(drow["fGPtbl_Company_Directors_Experience"].ToString());
                    drct.adhar = drow["fGPtbl_Company_Directors_Adhar"].ToString();
                    DList.Add(drct);

                }
            }
            else
            {
                Director drct = new Director();
                drct.rowno = 1;
                drct.CompanyId = CompanyId.ToString();
                drct.Name = "";
                drct.Address = "";
                drct.Pincode = "";
                drct.State = "";
                drct.City = "";
                drct.Telephone = "";
                drct.ShareHolding = "";
                drct.stateid = "";
                drct.cityId = "";
                drct.email = "";
                drct.experience = 0;
                drct.adhar = "";
                DList.Add(drct);
            }
        }
        return DList;
    }


    public List<PerformanceSheet> FetchPerformanceSheet(int CompanyId)
    {
        MyDt = new DataTable();
        clsCompany objcompany = new clsCompany();
        MyDt = objcompany.Company_FetchPerformanceSheetList(CompanyId);
        List<PerformanceSheet> DList = new List<PerformanceSheet>();
        DList.Clear();
        if (MyDt != null)
        {

            if (MyDt.Rows.Count > 0)
            {
                foreach (DataRow drow in MyDt.Rows)
                {
                    PerformanceSheet drct = new PerformanceSheet();
                    drct.PrimaryId = Convert.ToInt32(drow["PrimaryId"].ToString());
                    drct.CompanyId = CompanyId.ToString(); ;
                    drct.OrderNumber = drow["OrderNumber_Date_Authority"].ToString();
                    drct.Date_of_ReceiptOrder = drow["DateofReceipt"].ToString();
                    drct.Store_Produt_Service_Desc = drow["Description_Stores_Product_Services"].ToString();
                    //   drct.Pincode = drow["fGPtbl_Company_Directors_Zip"].ToString();
                    drct.Quantity = drow["Quantity"].ToString();
                    drct.Value = drow["Value"].ToString();
                    drct.Name_Consignee = drow["ConsigneeName"].ToString();
                    drct.DeliveryDueDate = drow["DeliveryDueDate"].ToString();
                    drct.MaterialDeliveryDate = drow["MaterialDeliveryDate"].ToString();
                    drct.DespatchingParticulars = drow["DespatchingParticulars"].ToString();
                    drct.Remarks = drow["Remarks"].ToString();
                   
                    DList.Add(drct);

                }
            }
            else
            {
                PerformanceSheet drct = new PerformanceSheet();
                drct.rowno = 1;
                drct.CompanyId = CompanyId.ToString();
                drct.OrderNumber = "";
                drct.Date_of_ReceiptOrder = "";
                drct.Store_Produt_Service_Desc = "";
                drct.Quantity = "";
                drct.Value = "";
                drct.Name_Consignee = "";
                drct.DeliveryDueDate = "";
                drct.MaterialDeliveryDate = "";
                drct.DespatchingParticulars = "";
                drct.Remarks = "";
                
                DList.Add(drct);
            }
        }
        return DList;
    }

}

[Serializable]

public class Director
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Pincode { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Telephone { get; set; }

    public string ShareHolding { get; set; }
    public string CompanyId { get; set; }

    public string stateid { get; set; }
    public string cityId { get; set; }

    public int rowno { get; set; }
    public int experience { get; set; }
    public string email { get; set; }
    public string adhar { get; set; }
}

[Serializable]
public class PerformanceSheet
{
    public string OrderNumber { get; set; }
    public string Date_of_ReceiptOrder { get; set; }
    public string Store_Produt_Service_Desc { get; set; }
    public string Quantity { get; set; }
    public string Value { get; set; }
    public string Name_Consignee { get; set; }

    public string DeliveryDueDate { get; set; }
    public string MaterialDeliveryDate { get; set; }

    public string DespatchingParticulars { get; set; }
    public string Remarks { get; set; }

    public int rowno { get; set; }

    public int PrimaryId { get; set; }

    public int experience { get; set; }
    public string email { get; set; }
    public string adhar { get; set; }
    public string CompanyId { get; set; }
}