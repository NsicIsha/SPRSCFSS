using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data;
//using static AjaxControlToolkit.AsyncFileUpload.Constants;

public partial class appAdministrator_Online_Unit_Ren_Amnd_Step6 : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    string UnitIdMultiple = string.Empty;
    string[] SI;
    int i = 1;
    clsCompany objCompany;
    clsAmendment objAmendment;
    string CnCount = string.Empty;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    //FormInputReplace objInputReplace;
    int outVariable = 0;

    public delegate int step6delegate(int UnitId, int FeeId, int AgencyId, int SlabId, int FormTypeId, int UserId, int BranchId, decimal Amount);

    public delegate int step6delegate_RA(int UnitId, int FeeId, int FormTypeId, int UserId, decimal Amount);

    //TO FETCH CHANGES ON RECORDS
    public DataSet CompanyAmendment_CompanyChangesDetails_Fetch_ByCompanyId(int companyid, int formtypeid)
    {
        try
        {
            MyDs = new DataSet();
            if (formtypeid == 3)//FOR AMENDMENT
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_FetchChanges_ByCompanyId_Renewal_Amendment_Online", DBManager.ConnectToDB());
            }
            else if (formtypeid == 2)//FOR RENEWAL
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_FetchChanges_ByCompanyId", DBManager.ConnectToDB());
            }
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = companyid;
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

    //TO FETCH DETAILS OF CUNIT AMENDMENT WITH WAITING STATUS // Uttam sir 
    public DataTable CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(int companyid, List<int> Changedunit)
    {
        int unit1 = 0, unit2 = 0, unit3 = 0, unit4 = 0, unit5 = 0;
        try
        {
            for (int i = 0; i < Changedunit.Count(); i++)
            {
                if (i == 0)
                    unit1 = Changedunit[0];
                else if (i == 1)
                    unit2 = Changedunit[1];
                else if (i == 2)
                    unit3 = Changedunit[2];
                else if (i == 3)
                    unit4 = Changedunit[3];
                else if (i == 4)
                    unit5 = Changedunit[4];
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Waiting_Fetch_CUnit_ByCompanyid_Renewal_Amendment_Online_New", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            if (unit1 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit1", SqlDbType.Int).Value = unit1;
            if (unit2 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit2", SqlDbType.Int).Value = unit2;
            if (unit3 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit3", SqlDbType.Int).Value = unit3;
            if (unit4 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit4", SqlDbType.Int).Value = unit4;
            if (unit5 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit5", SqlDbType.Int).Value = unit5;

            MyAdp.Fill(MyDt);
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
        return MyDt;
    }
    DataTable ddIA = new DataTable();
    public DataTable CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(int companyid)
    {
        DataSet MydsChange = new DataSet();
        List<int> UnitIdchangedStore = new List<int>();
        List<int> UnitIdchanged = new List<int>();
        DataTable dt, dtStore = new DataTable();
        DataTable dt2 = new DataTable();


        try
        {
            MydsChange = CompanyAmendment_CompanyChangesDetails_Fetch_ByCompanyId(companyid, 3);

            dt = MydsChange.Tables[1];
            if (dt.Rows.Count > 0)
            {
                UnitIdchanged = (from r in dt.AsEnumerable()
                                 where
                                 r.Field<int>("fGPtbl_CUnit_5A1") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_Address") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_Zip") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_StateId") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_CityId") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5A2") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5BA") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5BB1") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5BB2") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5C") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_6A1") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_6A2") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_6B") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7A") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7B") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7C") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7D") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7E") == 0
                                 select r.Field<int>("fGPtbl_CUnit_Id")).ToList();

                /* foreach (var onl in UnitId)
                         {
                     unitid1 += onl.ToString() + ",";
               
                      }*/
            }

            ddIA = dtStore = MydsChange.Tables[2];

            if (dtStore.Rows.Count > 0)
            {
                UnitIdchangedStore = (from r in dtStore.AsEnumerable()
                                      where
                                      r.Field<int>("fGPtbl_CUnit_Store_DescriptionStoresProducts") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_RefOfBISSpecification") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_ModelBrand") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_AnnualProductionCapacity") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_NoOfShifts") == 0
                                      select r.Field<int>("fGPtbl_CUnit_Store_UnitId")).ToList();

                /* foreach (var onl in UnitId)
                         {
                     unitid1 += onl.ToString() + ",";
               
                      }*/
            }

            UnitIdchanged.AddRange(UnitIdchangedStore);

            IEnumerable<int> distinctUnit = UnitIdchanged.Distinct();
            List<int> distinctUnit1 = new List<int>();
            foreach (int un in distinctUnit)
            {
                distinctUnit1.Add(un);
            }

            dt2 = CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(companyid, distinctUnit1);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        return dt2;

    }

    public DataSet Fetch_TurnOver_CFSS(int companyid, int formtypeid)
    {
        try
        {
            MyDs = new DataSet();
            
            MyAdp = new SqlDataAdapter("GPsp_R_RA_LastYear_FetchTurnover_Online_CFSS", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = companyid;
            MyAdp.SelectCommand.Parameters.Add("@formtypeid", SqlDbType.Int).Value = formtypeid;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ////try
        ////{
        ////    UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        ////    RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        ////    DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        ////    UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        ////}
        ////catch (Exception ex)
        ////{
        ////    UserId = 0;
        ////    RoleId = 0;
        ////    DepartmentId = 0;
        ////    UserFullName = "";
        ////}
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        try
        {
            //checkButtonDoubleClick(btnSubmit);

            ModalPopupExtender2.Show();
            FunRequest();

            #region CFSS
            DataSet dsTO = new DataSet();
            dsTO = Fetch_TurnOver_CFSS(CompanyId,3);
            decimal LYTurnOver = 0;
            LYTurnOver = Convert.ToDecimal(dsTO.Tables[0].Rows[0]["fGPtbl_Company_TO_LastYearTurnover"]);

            if (LYTurnOver == 0)
            {
                txtTurnover.Enabled = true;
                lblpopmsg.Text = "Please Enter Turnover";
                ModalPopupExtender2.Show();
                
            }
            else
            {
                txtTurnover.Text = LYTurnOver.ToString();
                txtTurnover.Enabled = false;
                lblpopmsg.Text = "Please Enter Turnover";
                ModalPopupExtender2.Show();
                
            }

            #endregion



            //FunfillPaymentSlab();

            objCompany = new clsCompany();
            objAmendment = new clsAmendment();

            System.Data.DataTable dt = new System.Data.DataTable();

            // dt = objAmendment.CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(CompanyId);
            dt = CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(CompanyId);


            if (dt.Rows.Count > 0)
            {
                CnCount = dt.Rows.Count.ToString();
                tr_agency.Visible = true;
            }
            else
            {
                // Message write and return first page of amedment
                tr_agency.Visible = false;
            }


            DataSet dsf = new DataSet();
            dsf = Fetch_ChangesType_Renewal_Amendment_Online(CompanyId);
            int InspectionfeeStatus = 0;
            InspectionfeeStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);

            // InspectionfeeStatus = 240;
            if (InspectionfeeStatus == 240)
            {
                tr_agency.Visible = true;
                List<int> UnitIdchangedStore = new List<int>();
                List<int> UnitIdchanged = new List<int>();
                if (ddIA.Rows.Count > 0)
                {
                    UnitIdchangedStore = (from r in ddIA.AsEnumerable()
                                          where
                                          r.Field<int>("fGPtbl_CUnit_Store_DescriptionStoresProducts") == 0 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_RefOfBISSpecification") == 0 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_ModelBrand") == 0 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") == 0 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_AnnualProductionCapacity") == 0 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_NoOfShifts") == 0 ||

                                           r.Field<int>("fGPtbl_CUnit_Store_DescriptionStoresProducts") == 1 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_RefOfBISSpecification") == 1 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_ModelBrand") == 1 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") == 1 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_AnnualProductionCapacity") == 1 ||
                                          r.Field<int>("fGPtbl_CUnit_Store_NoOfShifts") == 1

                                          select r.Field<int>("fGPtbl_CUnit_Store_UnitId")).ToList();


                }

                UnitIdchanged.AddRange(UnitIdchangedStore);

                IEnumerable<int> distinctUnit = UnitIdchanged.Distinct();
                List<int> distinctUnit1 = new List<int>();
                foreach (int un in distinctUnit)
                {
                    distinctUnit1.Add(un);
                }
                DataTable dataTable = new DataTable();
                dataTable = CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(CompanyId, distinctUnit1);

                foreach (System.Data.DataRow row in dataTable.Rows) // Loop over the rows.
                {
                    //PASSING ADDRESS AND UNIT ID FUNFILLAGENCY(ADDRESS, UNITID)
                    FunfillAgency(row[2].ToString(), Convert.ToInt32(row[1]));

                    UnitId = Convert.ToInt32(row[1]);
                    UnitIdMultiple += UnitId.ToString() + ',';

                    hdn_UnitID.Value = UnitIdMultiple;
                    string AllValue = hdn_UnitID.Value;
                    string sub = AllValue.Substring(0, AllValue.Length - 1);
                    SI = sub.Split(',');
                    hdn_unitID_Count.Value = SI.Length.ToString();
                }

            }
            else
            {
                tr_agency.Visible = false;
            }

            dt.Clear();
            dt.Dispose();

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
        }
        catch
        {
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }

    private void FunfillPaymentSlab(SqlDataReader MyDr)
    {
        TableRow trow;
        TableCell tcell;
        int Cnt = 0;

        //FETCHING PAYMENT SLAB FOR AMENDTMENT FORMTYPEID=3
        //MyDr = objCompany.Company_FetchBranchPayment_ByFormtypeId_Step6(3);

        while (MyDr.Read())
        {
            Table tbl_1 = new Table();
            tbl_1.CellPadding = 0;
            tbl_1.CellSpacing = 0;
            tbl_1.Width = Unit.Percentage(100);
            if (Cnt == 0)
            {
                trow = new TableRow();
                trow.CssClass = "dynRowHeader";

                tcell = new TableCell();
                tcell.Text = "S.<br/>No.";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Payment Slab";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Amount";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Instruction";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tbl_1.Rows.Add(trow);
            }
            Cnt += 1;

            trow = new TableRow();
            trow.Style.Add("padding-top", "10px");

            tcell = new TableCell();
            tcell.Text = Cnt.ToString() + ".";
            tcell.Width = Unit.Percentage(5);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tcell = new TableCell();
            tcell.Text = "<input id='rdReg' name='rdReg' value='" + MyDr["fGPtbl_MFee_Id"].ToString() + "' type='radio' checked='checked' />";
            tcell.Width = Unit.Percentage(2);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tcell = new TableCell();
            tcell.Text = "<b>Payment</b>";
            tcell.Width = Unit.Percentage(28);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tcell = new TableCell();
            HdnFieldAmount.Value = Convert.ToString(Convert.ToInt32(MyDr["fGPtbl_MFee_Amount"]));
            tcell.Text = "<b>Rs. " + HdnFieldAmount.Value + "</b>";
            tcell.Width = Unit.Percentage(10);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tcell = new TableCell();
            tcell.Text = MyDr["fGPtbl_MFee_Desc"].ToString();
            tcell.Width = Unit.Percentage(55);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tbl_1.Rows.Add(trow);

            TableRow trowMain = new TableRow();
            TableCell tcellMain = new TableCell();
            tcellMain.Controls.Add(tbl_1);
            trowMain.Controls.Add(tcellMain);
            Table2.Controls.Add(trowMain);
        }
        MyDr.Close();
    }

    private void FunfillAgency(string Address, int unitid)
    {
        clsMasterInspectionAgency objAgency = new clsMasterInspectionAgency();
        try
        {
            int AgencyId = 0;
            int SlabId = 0;
            //int tablesub = 1;
            TableRow trow;
            TableCell tcell;
            int Cnt = 0;
            System.Data.SqlClient.SqlDataReader MyDr;
            objCompany = new clsCompany();
            //MyDr = objCompany.Company_FetchAgency_ByBranch_Step6(BranchId);
            string query = "select fGPtbl_IsAyush from [GPtbl_Company] where [fGPtbl_Company_Id]='" + CompanyId + "'";
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();


            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            bool result = false;
            while (reader.Read())
            {
                result = (reader[0].ToString().Equals("True")) ? true : false;
            }
            MyDr = objAgency.MasterInspectionAgency_User_Fetch_step6(result);

            //int i = 1;

            while (MyDr.Read())
            {
                Table tbl_1 = new Table();
                tbl_1.CellPadding = 0;
                tbl_1.CellSpacing = 0;

                tbl_1.Width = Unit.Percentage(100);
                if (Cnt == 0)
                {
                    trow = new TableRow();
                    trow.CssClass = "dynRowHeader";
                    tcell = new TableCell();
                    tcell.Text = "Select inspection agency for " + Address;
                    //for (int i = 1; i < Convert.ToInt32(CnCount); i++)
                    //{
                    tcell.ID = "SN0id" + i.ToString();
                    i++;
                    //}
                    tcell.CssClass = "clstblCellHead";
                    tcell.ColumnSpan = 3;
                    trow.Cells.Add(tcell);

                    tbl_1.Rows.Add(trow);

                    trow = new TableRow();
                    trow.CssClass = "dynRowHeader";

                    tcell = new TableCell();
                    tcell.Text = "S.<br/>No.";

                    tcell.CssClass = "clstblCellHead";
                    trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = "Inspection Agency";
                    tcell.CssClass = "clstblCellHead";
                    trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = "Payment Slab";
                    tcell.CssClass = "clstblCellHead";
                    trow.Cells.Add(tcell);

                    tbl_1.Rows.Add(trow);
                }



                Cnt += 1;
                AgencyId = Convert.ToInt32(MyDr["AgencyId"].ToString());



                trow = new TableRow();
                trow.Style.Add("padding-top", "10px");

                tcell = new TableCell();
                tcell.Text = Cnt.ToString() + ".";
                tcell.Width = Unit.Percentage(5);
                tcell.CssClass = "clstblCell";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "<b>" + MyDr["AgencyName"].ToString() + "</b><br />";
                tcell.Text += MyDr["fGPtbl_TIAB_Instructions"];
                tcell.Text = tcell.Text.ToString().Replace("\r\n", "<br />");
                tcell.Width = Unit.Percentage(40);
                tcell.CssClass = "clstblCell";
                trow.Cells.Add(tcell);

                Table tbl = new Table();
                tbl.CellPadding = 0;
                tbl.CellSpacing = 0;
                tbl.Width = Unit.Percentage(100);
                TableRow tblRow;
                TableCell tblCell;
                clsCompany objCmp = new clsCompany();
                System.Data.SqlClient.SqlDataReader Dr;


                if (AgencyId == 11 || AgencyId == 12 || AgencyId==9)
                {
                    DataSet dsf = new DataSet();
                    dsf = FeeVariationForIA(CompanyId);
                    int InspectionfeeStatus = 0;
                    InspectionfeeStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);

                    // InspectionfeeStatus = 240;
                    if (InspectionfeeStatus == 420)//Nostore or fatory change
                    {
                        if (AgencyId == 11)
                        {
                            int PreAgencyID = LastInspectionAgencyDoneBy(unitid);
                            if (PreAgencyID == 11)
                            {
                                Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 5);
                            }
                            else
                            {
                                Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 3);
                            }
                        }
                        else
                        {
                            Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 5);
                        }
                        if (AgencyId == 9)
                        {
                            int PreAgencyID = LastInspectionAgencyDoneBy(unitid);
                            if (PreAgencyID == 9)
                            {
                                Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 391);
                            }
                            else
                            {
                                Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 392);
                            }
                        }
                        //else
                        //{
                        //    Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 5);
                        //}
                    }
                    else
                    {
                        Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 3);
                    }


                }
                else if (AgencyId == 26 || AgencyId == 13 || AgencyId == 22 || AgencyId == 23)
                {
                    Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 5);
                }
                else
                {
                    Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 3);
                }
                int Cntr = 0;
                while (Dr.Read())
                {
                    Cntr += 1;
                    //++++++++++++++++++++++
                    if (Cntr != 1)
                    {
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Text = "";

                        tblCell.ColumnSpan = 3;
                        tblCell.Width = Unit.Percentage(100);
                        tblCell.Height = Unit.Pixel(1);
                        tblCell.BackColor = System.Drawing.Color.FromName("#22518F");
                        tblRow.Cells.Add(tblCell);
                        tbl.Rows.Add(tblRow);
                    }

                    //++++++++++++++++++++++
                    tblRow = new TableRow();

                    SlabId = Convert.ToInt32(Dr["fGPtbl_IPS_Id"].ToString());
                    string rdVal = AgencyId.ToString() + "/" + SlabId.ToString();
                    tblCell = new TableCell();
                    tblCell.Text = "<input id='rd_" + unitid + "' name='rd_" + unitid + "' value='" + rdVal + "' type='radio' />";
                    tblCell.CssClass = "clstblCell";
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.Text = "<b>" + Dr["fGPtbl_IPS_SlabName"].ToString() + "</b>";
                    tblCell.Width = Unit.Percentage(80);
                    tblCell.CssClass = "clstblCell";
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.Text = "<b>Rs. " + Dr["fGPtbl_IPS_Amount"].ToString() + "</b>";
                    tblCell.Width = Unit.Percentage(20);
                    tblCell.CssClass = "clstblCell";
                    tblRow.Cells.Add(tblCell);

                    tbl.Rows.Add(tblRow);

                    //++++++++++++++++++++++
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    tblCell.Text = "";
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    string slbInst = Dr["fGPtbl_IPS_Instructions"].ToString();
                    slbInst = FormInputReplace.FormStrInputReplace(slbInst);
                    slbInst = slbInst.ToString().Replace("\r\n", "<br />");
                    if (slbInst.Length > 150)
                    {
                        tblCell.Text = slbInst.Substring(0, 150) + "...";
                    }
                    else
                    {
                        tblCell.Text = slbInst;
                    }

                    tblCell.Attributes.Add("onmouseover", "Tip('" + slbInst + "')");
                    tblCell.Attributes.Add("onmouseout", "UnTip()");
                    tblCell.ColumnSpan = 2;
                    tblCell.CssClass = "clstblCell";
                    tblRow.Cells.Add(tblCell);
                    tbl.Rows.Add(tblRow);


                }
                Dr.Close();

                tcell = new TableCell();
                tcell.Controls.Add(tbl);
                tcell.Width = Unit.Percentage(55);
                tcell.CssClass = "clstblCell";
                trow.Cells.Add(tcell);

                tbl_1.Rows.Add(trow);

                TableRow trowMain = new TableRow();
                TableCell tcellMain = new TableCell();
                tcellMain.Controls.Add(tbl_1);
                trowMain.Controls.Add(tcellMain);
                Table1.Controls.Add(trowMain);
            }
            MyDr.Close();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public int LastInspectionAgencyDoneBy(int unitid)
    {
        int AgencyID = 0;
        try
        {
            MySqlComm = new SqlCommand("CheckLastIA", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = unitid;
            SqlParameter outVal = new SqlParameter("@AgencyID", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            AgencyID = Convert.ToInt32(outVal.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return AgencyID;
    }

    public DataSet Fetch_ChangesType_Renewal_Amendment_Online(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_ChangesType_Renewal_Amendment_Online", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    public DataSet FeeVariationForIA(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_FeeVariationForIAinRenewal_Amendment_Online", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    public int CompanyRenewal_Insert_Step6_RA(int UnitId, int FeeId, int FormTypeId, int UserId, decimal amount)
    {
        int tblId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step6_RA_Online", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = UnitId;
            MySqlComm.Parameters.Add("@FeeId", SqlDbType.Int).Value = FeeId;
            MySqlComm.Parameters.Add("@FormTypeId", SqlDbType.Int).Value = FormTypeId;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amount;
            SqlParameter outError = new SqlParameter("@tblId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
            tblId = Convert.ToInt32(outError.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return tblId;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int tblId = 0;
        string paymentid = "";
        int AgencyId = 0;
        int SlabId = 0;
        int FeeId = 0;
        string prevUnitID = "";
        DataSet dsf = new DataSet();
        dsf = Fetch_ChangesType_Renewal_Amendment_Online(CompanyId);
        int InspectionfeeStatus = 0;
        InspectionfeeStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);

        // InspectionfeeStatus = 240;
        if (InspectionfeeStatus == 240)
        {

            string bsRDreg = "";
            string bsRDagency = "";
            char[] separator = new char[] { '/' };
            string[] strSplitArr;
            try
            {
                objCompany = new clsCompany();
                step6delegate step6del = new step6delegate(objCompany.Company_Insert_Step6);
                System.Data.DataTable dt = new System.Data.DataTable();
                // dt = objAmendment.CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(CompanyId);
                dt = CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(CompanyId);
                int cnt = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
                    {
                        //TO INPUT UNIT AMOUNT ONCE IN PAYMENT DETAIL TABLE
                        cnt++;
                        UnitId = Convert.ToInt32(row[1]);
                        bsRDreg = Request["rdReg"].ToString();
                        FeeId = Convert.ToInt32(bsRDreg);
                        bsRDagency = Request["rd_" + UnitId].ToString();
                        strSplitArr = bsRDagency.Split(separator);
                        AgencyId = Convert.ToInt32(strSplitArr[0].ToString());
                        SlabId = Convert.ToInt32(strSplitArr[1].ToString());
                        if (cnt == 1)
                        {
                            prevUnitID = Convert.ToString(row[1]);
                            if (AgencyId == 11 || AgencyId == 12 || AgencyId==9)
                            {
                                DataSet dsf1 = new DataSet();
                                dsf1 = FeeVariationForIA(CompanyId);
                                int InspectionfeeStatus1 = 0;
                                InspectionfeeStatus1 = Convert.ToInt32(dsf1.Tables[0].Rows[0]["ResultType"]);

                                // InspectionfeeStatus = 240;
                                if (InspectionfeeStatus1 == 420)//Nostore or fatory change
                                {
                                    if (AgencyId == 11)
                                    {
                                        int PreAgencyID = LastInspectionAgencyDoneBy(UnitId);
                                        if (PreAgencyID == 11)
                                        {
                                            tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 5, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                        }
                                        else
                                        {
                                            tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                        }
                                    }
                                    else
                                    {
                                        //TO USE DELEGATE TO CALL THIS FUNCTION
                                        tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 5, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                    }
                                    if (AgencyId == 9)
                                    {
                                        int PreAgencyID = LastInspectionAgencyDoneBy(UnitId);
                                        if (PreAgencyID == 9)
                                        {
                                            tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 391, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                        }
                                        else
                                        {
                                            tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 392, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                        }
                                    }
                                    else
                                    {
                                      //  tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 5, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                    }
                                }
                                else
                                {
                                    //TO USE DELEGATE TO CALL THIS FUNCTION
                                    tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                }

                            }
                            else if (AgencyId == 26 || AgencyId == 22 || AgencyId == 23 || AgencyId == 13)
                            {
                                tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 5, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                            }
                            else
                            {
                                //TO USE DELEGATE TO CALL THIS FUNCTION
                                tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                            }

                            paymentid = tblId.ToString();
                        }
                        else
                        {
                            if (AgencyId == 11 || AgencyId == 12 || AgencyId==9)
                            {
                                DataSet dsf1 = new DataSet();
                                dsf1 = FeeVariationForIA(CompanyId);
                                int InspectionfeeStatus1 = 0;
                                InspectionfeeStatus1 = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);


                                if (InspectionfeeStatus1 == 420)//Nostore or fatory change
                                {
                                    //TO USE DELEGATE TO CALL THIS FUNCTION
                                    // tblId = step6del(UnitId, 0, AgencyId, SlabId, 5, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));

                                    if (AgencyId == 11)
                                    {
                                        int PreAgencyID = LastInspectionAgencyDoneBy(UnitId);
                                        if (PreAgencyID == 11)
                                        {
                                            tblId = step6del(UnitId, 0, AgencyId, SlabId, 5, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                        }
                                        else
                                        {
                                            tblId = step6del(UnitId, 0, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                        }
                                    }
                                    else
                                    {
                                        //TO USE DELEGATE TO CALL THIS FUNCTION
                                        tblId = step6del(UnitId, 0, AgencyId, SlabId, 5, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                    }
                                    if (AgencyId == 9)
                                    {
                                        int PreAgencyID = LastInspectionAgencyDoneBy(UnitId);
                                        if (PreAgencyID == 9)
                                        {
                                            tblId = step6del(UnitId, 0, AgencyId, SlabId, 391, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                        }
                                        else
                                        {
                                            tblId = step6del(UnitId, 0, AgencyId, SlabId, 392, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                        }
                                    }
                                    else
                                    {
                                        tblId = step6del(UnitId, 0, AgencyId, SlabId, 5, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                    }
                                }
                                else
                                {
                                    //TO USE DELEGATE TO CALL THIS FUNCTION
                                    tblId = step6del(UnitId, 0, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                }

                            }
                            else if (AgencyId == 26 || AgencyId == 22 || AgencyId == 23 || AgencyId == 13)
                            {
                                tblId = step6del(UnitId, 0, AgencyId, SlabId, 5, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                            }
                            else
                            {
                                //TO USE DELEGATE TO CALL THIS FUNCTION
                                tblId = step6del(UnitId, 0, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                            }

                            paymentid = paymentid + "," + tblId;
                        }
                    }
                }
                else
                {
                    dsf = new DataSet();
                    dsf = Fetch_ChangesType_Renewal_Amendment_Online(CompanyId);
                    int InspectionfeeStatus1 = 0;
                    InspectionfeeStatus1 = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);

                    // InspectionfeeStatus = 240;
                    if (InspectionfeeStatus1 == 240)
                    {
                        tr_agency.Visible = true;
                        List<int> UnitIdchangedStore = new List<int>();
                        List<int> UnitIdchanged = new List<int>();
                        if (ddIA.Rows.Count > 0)
                        {
                            UnitIdchangedStore = (from r in ddIA.AsEnumerable()
                                                  where
                                                  r.Field<int>("fGPtbl_CUnit_Store_DescriptionStoresProducts") == 0 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_RefOfBISSpecification") == 0 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_ModelBrand") == 0 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") == 0 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_AnnualProductionCapacity") == 0 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_NoOfShifts") == 0 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_DescriptionStoresProducts") == 1 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_RefOfBISSpecification") == 1 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_ModelBrand") == 1 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") == 1 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_AnnualProductionCapacity") == 1 ||
                                                  r.Field<int>("fGPtbl_CUnit_Store_NoOfShifts") == 1
                                                  select r.Field<int>("fGPtbl_CUnit_Store_UnitId")).ToList();
                        }

                        UnitIdchanged.AddRange(UnitIdchangedStore);

                        IEnumerable<int> distinctUnit = UnitIdchanged.Distinct();
                        List<int> distinctUnit1 = new List<int>();
                        foreach (int un in distinctUnit)
                        {
                            distinctUnit1.Add(un);
                        }
                        DataTable dataTable = new DataTable();
                        dataTable = CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(CompanyId, distinctUnit1);

                        foreach (System.Data.DataRow row in dataTable.Rows) // Loop over the rows.
                        {
                            //TO INPUT UNIT AMOUNT ONCE IN PAYMENT DETAIL TABLE
                            cnt++;
                            UnitId = Convert.ToInt32(row[1]);
                            bsRDreg = Request["rdReg"].ToString();
                            FeeId = Convert.ToInt32(bsRDreg);

                            bsRDagency = Request["rd_" + UnitId].ToString();
                            strSplitArr = bsRDagency.Split(separator);
                            AgencyId = Convert.ToInt32(strSplitArr[0].ToString());
                            SlabId = Convert.ToInt32(strSplitArr[1].ToString());
                            if (cnt == 1)
                            {
                                prevUnitID = Convert.ToString(row[1]);
                                //TO USE DELEGATE TO CALL THIS FUNCTION
                                tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                paymentid = tblId.ToString();
                            }
                            else
                            {
                                tblId = step6del(UnitId, 0, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                                paymentid = paymentid + "," + tblId;
                            }
                        }
                    }
                    else
                    {
                        tr_agency.Visible = false;
                        tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                        paymentid = tblId.ToString();
                    }
                }

                dt.Clear();
                dt.Dispose();
            }
            catch
            {
                FeeId = 0;
                AgencyId = 0;
                SlabId = 0;
                UnitId = Convert.ToInt32(prevUnitID);
            }
        }
        else // if 420 is returing from SP
        {
            string bsRDreg = "";
            string bsRDagency = "";
            char[] separator = new char[] { '/' };
            string[] strSplitArr;
            //   string paymentid = "";
            objCompany = new clsCompany();
            clsRenewal objRenewal = new clsRenewal();
            try
            {

                objCompany = new clsCompany();
                objRenewal = new clsRenewal();
                //step6delegate_RA step6del_RA = new step6delegate_RA(CompanyRenewal_Insert_Step6_RA);

                step6delegate step6del = new step6delegate(objCompany.Company_Insert_Step6);
                System.Data.DataTable dt = new System.Data.DataTable();
                // dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);
                // dt = objAmendment.CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(CompanyId);
                dt = CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(CompanyId);
                int cnt = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
                    {
                        //TO INPUT UNIT AMOUNT ONCE IN PAYMENT DETAIL TABLE
                        cnt++;
                        UnitId = Convert.ToInt32(row[1]);
                        bsRDreg = Request["rdReg"].ToString();
                        FeeId = Convert.ToInt32(bsRDreg);
                        // bsRDagency = Request["rd_" + UnitId].ToString();
                        //  strSplitArr = bsRDagency.Split(separator);
                        //  AgencyId = Convert.ToInt32(strSplitArr[0].ToString());
                        // SlabId = Convert.ToInt32(strSplitArr[1].ToString());
                        AgencyId = 0;
                        if (cnt == 1)
                        {
                            //TO USE DELEGATE TO CALL THIS FUNCTION
                            tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                            paymentid = tblId.ToString();
                        }
                        else
                        {
                            tblId = step6del(UnitId, 0, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                            paymentid = paymentid + "," + tblId;
                        }
                    }
                }
                else
                {
                    tr_agency.Visible = false;
                    tblId = step6del(UnitId, FeeId, AgencyId, SlabId, 3, UserId, BranchId, Convert.ToDecimal(HdnFieldAmount.Value));
                    paymentid = tblId.ToString();
                }

                //////int cnt = 0;

                //////foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
                //////{
                //////    //TO INPUT UNIT AMOUNT ONCE IN PAYMENT DETAIL TABLE
                //////    cnt++;

                //////    int a = 0;
                //////    foreach (var item in row.ItemArray) // Loop over the items.
                //////    {

                //////        if (a == 1)
                //////        {

                //////        }
                //////        else if (a == 0)
                //////        {
                //////            UnitId = Convert.ToInt32(item);
                //////            bsRDreg = Request["rdReg"].ToString();
                //////            FeeId = Convert.ToInt32(bsRDreg);


                //////            if (cnt == 1)
                //////            {

                //////                    tblId = step6del_RA(UnitId, FeeId, 3, UserId, Convert.ToDecimal(HdnFieldAmount.Value));

                //////                paymentid = tblId.ToString();
                //////            }
                //////            else
                //////            {
                //////                    tblId = step6del_RA(UnitId, 0, 3, UserId, Convert.ToDecimal(HdnFieldAmount.Value));
                //////                    paymentid = paymentid + "," + tblId;
                //////            }

                //////        }
                //////        a++;
                //////    }

                //////}

                dt.Clear();
                dt.Dispose();


            }
            catch
            {
                FeeId = 0;


            }
        }




        if (tblId != 0)
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());

            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            //string EncryptUnit = objDecEnc.Encrypt("69982");
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            //string EncryptPaymentDetailId = objDecEnc.Encrypt(tblId.ToString());
            string EncryptPaymentDetailId = objDecEnc.Encrypt(paymentid.ToString());
            string EncryptFormtype = objDecEnc.Encrypt("3");
            if (AgencyId == 0)
            {
                Response.Redirect("Online_NewUnitregistration_paymentdetails_step7.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&pd=" + EncryptPaymentDetailId, false);
            }
            else
            {
                Response.Redirect("Online_NewUnitregistration_paymentdetails_step7.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&pd=" + EncryptPaymentDetailId, false);
            }
        }
    }
    public SqlDataReader Company_FetchSlabFee_Step6_Online(decimal turnover, int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchSlabFee_Renewal_Amendment_Online", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@Turnover", SqlDbType.Float).Value = turnover;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            // MySqlComm.Parameters.Add("@Provisional", SqlDbType.Int).Value = _mprovision;
            MyDr = MySqlComm.ExecuteReader();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return MyDr;
    }

    public DataSet Company_Fetch_Datasetrenew_Covid19Discount(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Covid19Discount", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    public DataSet Company_EnterpriseType(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_EnterpriseType", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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
    protected void btnSubmitEdit_Click(object sender, EventArgs e)
    {
        int intEnterpriseType = 0;

        if (Convert.ToDecimal(txtTurnover.Text) > 0)
        {
            objCompany = new clsCompany();
            SqlDataReader Mydr = Company_FetchSlabFee_Step6_Online(Convert.ToDecimal(txtTurnover.Text.Trim()), CompanyId);// objCompany.Company_FetchSlabFee_Step6(Convert.ToDecimal(txtTurnover.Text.Trim()), CompanyId);


            DataSet ds = new DataSet();
            ds = Company_Fetch_Datasetrenew_Covid19Discount(CompanyId);

            //int CovidExistStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyExits"].ToString());

            //if (CovidExistStatus == 240)
            //{
            //    lblMsg_Covid19.Text = "Unit is getting 50% discount on Renewal fee";
            //}
            //else
            //{
            //    lblMsg_Covid19.Text = "";
            //}


            Double Sum;
            Sum = Convert.ToDouble(txtTurnover.Text);


            if (intEnterpriseType == 3)
            {
                if (Sum > 500 || Sum == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('For Micro : Last Year Turnover Should be up to ₹5 crore.');", true);
                    return;
                }
            }

            if (intEnterpriseType == 4)
            {
                if (Sum > 5000 || Sum < 500)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('For Small : Last Year Turnover Should be up to ₹50 crore and It should be more than 5 Crore.');", true);
                    return;
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plant and Machinery: More than 25 lakh rupee but not exceed 5 core');", true);
                //    return;
                //}
            }
            FunfillPaymentSlab(Mydr);
            ModalPopupExtender2.Hide();
            try
            {
                int K = objCompany.Company_insert_turnover_limit(CompanyId, 3, Convert.ToDecimal(txtTurnover.Text.ToString()), UserId);
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
            }  // positive
        }
        else
        {
            lblpopmsg.Text = "Please Enter Positive Turnover value";  // negative value
            ModalPopupExtender2.Show();
            return;
        }
    }

    public void checkButtonDoubleClick(Button button)
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
        sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
        sbValid.Append("this.value = 'Save & Continue >>';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(this.Page.ClientScript.GetPostBackEventReference(button, ""));
        sbValid.Append(";");
        button.Attributes.Add("onclick", sbValid.ToString());
    }
}