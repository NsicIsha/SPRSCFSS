using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_ConfirmPaymentDetails : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, PaymentDetailsId , urlid;

    double AmountPayable;
    int UserId, RoleId, DepartmentId;
    string PaymentDetailsIdstring;
    string UserFullName;

    clsCompany objCompany;
    clsAmendment objAmedment;
    clsRenewal objRenewal;

    int ErrorId;
    string ReferenceNo;
    string OriginalFilePath;
    string filePath;
    int FileStatus_1;

    string newFileName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";

        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        OriginalFilePath = "./Document/Upload/Registration/";
        filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";

        FunRequest();
        FunFillData();

    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            PaymentDetailsIdstring = (objDecEnc.Decrypt(Request.QueryString["pd"].Replace(" ", "+")).ToString());
            //UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            //Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
         //   BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["i"].Replace(" ", "+")).ToString());
            //PaymentDetailsIdstring = (objDecEnc.Decrypt(Request.QueryString["pd"].Replace(" ", "+")).ToString());

            //PaymentDetailsId = (objDecEnc.Decrypt(Request.QueryString["pd"].Replace(" ", "+")).ToString());
            urlid = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["i"].Replace(" ", "+")).ToString());
        }
        catch
        {
            PaymentDetailsIdstring = "";
            //UnitId = 0;
            //Status = 0;
            //BranchId = 0;
        }
    }


    private void FunFillData()
    {
        System.Data.SqlClient.SqlDataReader MyDr = null;
        //if (Status == 5 || Status == 123)    //INCOMPLETE
        // {
        //UNIT TABLE
        string FirstvalueAmedment = "";
        string str = null;
        string[] strArr = null;
        int count = 0;
        int Unitamnt = 0, Agncyamnt = 0;
        double servicetax = 0.0;
        //str = "CSharp split test";
        char[] splitchar = { ',' };
        strArr = PaymentDetailsIdstring.Split(splitchar);
        if (urlid != 2)
        {
            for (count = 0; count <= (strArr.Length - 1); count++)
            {
                //MessageBox.Show(strArr[count]);
                PaymentDetailsId = Convert.ToInt32(strArr[count]);

                if (urlid == 1)
                {
                    objCompany = new clsCompany();

                    MyDr = objCompany.Company_Fetch_UnconfirmedTransaction_ByPaymentid(PaymentDetailsId);
                }
                else if (urlid == 3)
                {
                    objAmedment = new clsAmendment();
                    //System.Data.SqlClient.SqlDataReader MyDr;
                    MyDr = objAmedment.Company_Fetch_UnconfirmedTransactionAmedment_ByPaymentid(PaymentDetailsId);
                }



                while (MyDr.Read())
                {
                    if (count == 0)
                    {
                        //lblUnitSlabName.Text = MyDr["fGPtbl_PD_UnitSlabName"].ToString();
                        //lblUnitInstruction.Text = MyDr["fGPtbl_PD_UnitSlabInstruction"].ToString();
                        lblUnitAmnt.Text = MyDr["fGPtbl_PD_UnitAmount"].ToString();
                        Unitamnt = Convert.ToInt32(MyDr["fGPtbl_PD_UnitAmount"].ToString());

                    }
                    //if (count == 0)
                    //{
                    //    lblAgencySlabName.Text = MyDr["fGPtbl_PD_AgencySlabName"].ToString();
                    //}
                    //else
                    //{
                    //    lblAgencySlabName.Text = lblAgencySlabName.Text + " , " + MyDr["fGPtbl_PD_AgencySlabName"].ToString();
                    //}

                    //lblAgencySlabInst.Text = MyDr["fGPtbl_PD_AgencySlabInstruction"].ToString();
                    //  lblUnitAmnt.Text = MyDr["fGPtbl_PD_UnitAmount"].ToString();
                    if (urlid == 3)
                    {

                        lblUnitAmnt.Text = MyDr["fGPtbl_PD_UnitAmount"].ToString();
                        Unitamnt = Convert.ToInt32(MyDr["fGPtbl_PD_UnitAmount"].ToString());
                        if (Unitamnt != 0)
                        {
                            FirstvalueAmedment = Unitamnt.ToString();
                        }
                    }

                    lblAgencyAmnt.Text = MyDr["fGPtbl_PD_AgencyAmount"].ToString();
                    Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
                    servicetax = servicetax + Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());
                    Unitamnt =Convert.ToInt32(MyDr["fGPtbl_PD_UnitAmount"].ToString());
                    lblTotAmnt.Text = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString())).ToString();
                    AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()));

                    //count = count + 1;

                    lblmode.Text = MyDr["fGPtbl_MPT_Name"].ToString();
                    if (lblmode.Text == "Cash")
                    {
                        //  txtCashRecpt.Text=MyDr["fGPtbl_PD_CashReceiptNo"].ToString();  

                    }
                    else
                    {
                        lblddno.Text = MyDr["fGPtbl_PD_ChequeDDNo"].ToString();
                    }
                }

                MyDr.Close();
            }
            if (urlid == 3)
            {
                lblUnitAmnt.Text = FirstvalueAmedment.ToString();
            }
            else
            {
                lblUnitAmnt.Text = Unitamnt.ToString();
            }
            lblAgencyAmnt.Text = Convert.ToString(Agncyamnt);
            // Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
            lblSubTot.Text = (Unitamnt + Agncyamnt).ToString();
            // (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString())).ToString();
            //AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()));
            lblTotAmnt.Text = (Unitamnt + Agncyamnt + Math.Round(servicetax, 0)).ToString();
            AmountPayable = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(servicetax, 1));
            lblServiceTax.Text = (Math.Round(servicetax, 0)).ToString();
        }

        else
        {


            for (count = 0; count <= (strArr.Length - 1); count++)
            {
                //MessageBox.Show(strArr[count]);
                PaymentDetailsId = Convert.ToInt32(strArr[count]);
                objRenewal = new clsRenewal();
                MyDr = objRenewal.Company_Fetch_UnconfirmedTransactionRenewal_ByPaymentid(PaymentDetailsId);
                while (MyDr.Read())
                {
                    if (count == 0)
                    {

                        lblUnitAmnt.Text = MyDr["fGPtbl_PD_UnitAmount"].ToString();
                        Unitamnt = Convert.ToInt32(MyDr["fGPtbl_PD_UnitAmount"].ToString());


                    }

                    //  lblAgencyAmnt.Text = MyDr["fGPtbl_PD_AgencyAmount"].ToString();
                    // Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
                    servicetax = servicetax + Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString());

                    lblTotAmnt.Text = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString())).ToString();
                    AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()));

                    //count = count + 1;

                    lblmode.Text = MyDr["fGPtbl_MPT_Name"].ToString();
                    if (lblmode.Text == "Cash")
                    {
                        //  txtCashRecpt.Text=MyDr["fGPtbl_PD_CashReceiptNo"].ToString();  

                    }
                    else
                    {
                        lblddno.Text = MyDr["fGPtbl_PD_ChequeDDNo"].ToString();
                    }
                }





                MyDr.Close();
            }
            //  lblAgencyAmnt.Text = Convert.ToString(Agncyamnt);
            // Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
            lblSubTot.Text = Unitamnt.ToString();
            // (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString())).ToString();
            //AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()));
            lblTotAmnt.Text = (Unitamnt + Math.Round(servicetax, 0)).ToString();
            AmountPayable = Convert.ToDouble(Unitamnt + Math.Round(servicetax, 1));
            lblServiceTax.Text = (Math.Round(servicetax, 0)).ToString();
        }


        if (lblmode.Text == "Cash")
        {
            trcash.Visible = true;
            trcheck.Visible = false;
        }
        else
        {
            trcash.Visible = false;
            trcheck.Visible = true;

        }

        //}

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
         string str = null;
         string[] strArr = null;
         string cashreceipt = "";
         int count = 0;
         int Unitamnt = 0, Agncyamnt = 0;
         double servicetax = 0.0;
            //str = "CSharp split test";
         char[] splitchar = { ',' };
         strArr = PaymentDetailsIdstring.Split(splitchar);
         if (trcash.Visible == true)
         {
             cashreceipt = txtCashRecpt.Text;
         }

         for (count = 0; count <= (strArr.Length - 1); count++)
         {
            
                 objCompany = new clsCompany();
                 PaymentDetailsId = Convert.ToInt32(strArr[count]);
                 ErrorId = objCompany.Company_Update_UnconfirmedTransaction(urlid, PaymentDetailsId, 1, cashreceipt, UserId);
           
             
             
         }

         Response.Redirect("ConfirmPayment.aspx?i=" + urlid.ToString());
        
    }
}
