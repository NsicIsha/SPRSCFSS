using System;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;


/// <summary>
/// Summary description for clsRenewal
/// </summary>
public class clsRenewal
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    //FormInputReplace objInputReplace;
    int outVariable = 0;

	public clsRenewal()
	{
		//
		// TODO: Add constructor logic here
		//
      }

    #region UTTAM KUMAR

      public DataSet CompanyRenewal_Fetch_Step1_ByCompanyId(int companyid)
      {
          try
          {
              MyDs = new DataSet();
              MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_FetchByCompanyId_Step1", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
              MyAdp.SelectCommand.CommandTimeout = 60000;
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

      public DataSet CompanyRenewal_Fetch_Step2_ByCompanyId(int companyid)
      {
          try
          {
              MyDs = new DataSet();
              MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_FetchByCompanyId_Step2_RA", DBManager.ConnectToDB());
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

      public SqlDataReader CompanyRenewal_Fetch_Step3_ByCompanyId(int companyid)
      {
          try
          {

              MySqlComm = new SqlCommand("GPsp_CompanyRenewal_FetchByCompanyId_Step3", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;

              MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = companyid;
              MyDr = MySqlComm.ExecuteReader();

          }
          catch (Exception ex)
          {
              ErrHandler.WriteError(ex.Message);
          }
          finally
          {
              MySqlComm.Dispose();
              DBManager.CloseConnection();
          }
          return MyDr;
      }

      public DataSet CompanyRenewal_Fetch_Step4_ByCompanyId(int companyid)
      {
          try
          {
              MyDs = new DataSet();
              MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_FetchByCompanyId_Step4_RA", DBManager.ConnectToDB());
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

      public SqlDataReader CompanyRenewal_FetchBranchPayment_ByCompanyId_Step6(int CompanyId)
      {
          try
          {
              MySqlComm = new SqlCommand("GPsp_CompanyRenewal_FetchBranchSlab_Step6", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;

              MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

      public int CompanyRenewal_WaitingCompanies_Accept(int companyid, int userid)
      {
          try
          {
              MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Waiting_Update_AcceptCompany_ById", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;

              MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
              MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;

              SqlParameter outputValue = new SqlParameter("@Output", SqlDbType.Int);
              outputValue.Direction = ParameterDirection.Output;
              MySqlComm.Parameters.Add(outputValue);

              MySqlComm.ExecuteNonQuery();

              outVariable = Convert.ToInt32(outputValue.Value);
          }
          catch (Exception ex)
          {
              ErrHandler.WriteError(ex.Message);
          }
          finally
          {
              MySqlComm.Dispose();
              DBManager.CloseConnection();
          }

          return outVariable;
      }

      public DataTable CompanyRenewal_Fetch_Rejected_Search(int branchid, int ddlcriteria, string searchcriteria)
      {
          MyDt = new DataTable();
          try
          {
              MyAdp = new SqlDataAdapter("GPsp_Company_Rejected_Search", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
              MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlcriteria;
              MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;

              MyAdp.Fill(MyDt);
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
          return MyDt;
      }

      public SqlDataReader CompanyRenewal_FetchPaymentSlabFee_ByCompanyId_Step6(int CompanyId, double amount)
      {
          try
          {
              MySqlComm = new SqlCommand("GPsp_CompanyRenewal_FetchSlabFee_Step6", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;
              MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
              MySqlComm.Parameters.Add("@amount", SqlDbType.Decimal).Value = amount;
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


      public int CompanyRenewal_InsertMissingValueByAdmin(string companystr, string factorystr, string directorstr, int companyid, int UnitId, int DirectorId)
      {
          try
          {
              MySqlComm = new SqlCommand("GPsp_Update_MissingInformation", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;
              if (companystr.ToString() != "") MySqlComm.Parameters.Add("@companystr", SqlDbType.Text).Value = companystr;
              if (factorystr.ToString() != "") MySqlComm.Parameters.Add("@unitstr ", SqlDbType.Text).Value = factorystr;
              if(directorstr.ToString()!="") MySqlComm.Parameters.Add("@directorstr ", SqlDbType.Text).Value = directorstr;
              MySqlComm.Parameters.Add("@companyid ", SqlDbType.Int).Value = companyid;
              if (UnitId != 0) MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = UnitId;
              if (DirectorId != 0) MySqlComm.Parameters.Add("@directorid", SqlDbType.Int).Value = DirectorId;

              SqlParameter outputValue = new SqlParameter("@output", SqlDbType.Int);
              outputValue.Direction = ParameterDirection.Output;
              MySqlComm.Parameters.Add(outputValue);

              MySqlComm.ExecuteNonQuery();

              outVariable = Convert.ToInt32(outputValue.Value);
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
          return outVariable;
      }

      public int CompanyRenewal_CheckPaymentSlabFee_ByCompanyId_Step6(int companyid)
      {
          try
          {
              MySqlComm = new SqlCommand("GPsp_CompanyRenewal_CheckForSlabFee_Step6", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;
              MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;

              SqlParameter outParameter = new SqlParameter("@unitamount", SqlDbType.Int);
              outParameter.Direction = ParameterDirection.Output;

              MySqlComm.Parameters.Add(outParameter);

              MySqlComm.ExecuteNonQuery();

              outVariable = Convert.ToInt32(outParameter.Value);
          }
          catch (Exception ex)
          {
              ErrHandler.WriteError(ex.Message);
          }
          finally
          {
              MySqlComm.Dispose();
              DBManager.CloseConnection();
          }
          return outVariable;
      }

      public int CompanyRenewal_WatingCom_Branch_Head_Acccept(int comapnyid, int userid)
      {
          try
          {
              MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Waiting_Br_head_Update_AcceptCompany_ById", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;

              MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = comapnyid;
              MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;

              SqlParameter outputValue = new SqlParameter("@Output", SqlDbType.Int);
              outputValue.Direction = ParameterDirection.Output;
              MySqlComm.Parameters.Add(outputValue);

              MySqlComm.ExecuteNonQuery();

              outVariable = Convert.ToInt32(outputValue.Value);
          }
          catch (Exception ex)
          {
              ErrHandler.WriteError(ex.Message);
          }
          finally
          {
              MySqlComm.Dispose();
              DBManager.CloseConnection();
          }

          return outVariable;
      }

      //TO FETCH UNIT DETAILS BY COMPANY ID
      public DataSet CompanyRenewal_UnitDetails_FetchForBranchHead_ByCompanyId(int companyid)
      {
          try
          {
              MyDs = new DataSet();
              MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_Waiting_Fetch_CUnitForBranchHead_ByCompanyid", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

              MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = companyid;

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

    #endregion

      #region RAVISH PANDEY

      public DataTable Company_Fetch_CompanyStaus(int CompanyID)
      {
          try
          {
              MyDt = new DataTable();
              MyAdp = new SqlDataAdapter("GPsp_Company_FetchCompanyStatus", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyID;
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

      public int CompanyRenewal_Insert_Step6(int UnitId, int FeeId, int FormTypeId, int UserId, decimal amount)
      {
          int tblId = 0;
          try
          {
              MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step6", DBManager.ConnectToDB());
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

      public string CompanyRenewal_Insert_Step7(int Companyid, int Unitid, int fGPtbl_PD_Id, int fGPtbl_PD_PaymentType,
          double @fGPtbl_PD_PaidAmount, string fGPtbl_PD_PaybalAt, string fGPtbl_PD_BankBranch, string fGPtbl_PD_BankName,
          string fGPtbl_PD_ChequeDDNo, string fGPtbl_PD_ChequeDDate, string fGPtbl_PD_Filename, string fGPtbl_PD_Filepath, string fGPtbl_PD_CashReceiptNo,
          int fGPtbl_PD_BranchConfirmation, int FormTypeID, int UserId)
      {
          string Returnvalue = "";
          try
          {
              MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step7", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;

              MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = fGPtbl_PD_Id;
              MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = fGPtbl_PD_PaymentType;
              MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
              MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = Unitid;

              if (fGPtbl_PD_ChequeDDNo != "")
              {
                  MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_ChequeDDNo;
              }

              if (fGPtbl_PD_BankName != "")
              {
                  MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankName;
              }

              if (fGPtbl_PD_BankBranch != "")
              {
                  MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankBranch;
              }

              if (fGPtbl_PD_PaybalAt != "")
              {
                  MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar, 500).Value = fGPtbl_PD_PaybalAt;
              }

              if (fGPtbl_PD_Filename != "")
              {
                  MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar, 200).Value = fGPtbl_PD_Filename;
              }

              if (fGPtbl_PD_Filepath != "")
              {
                  MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar, 500).Value = fGPtbl_PD_Filepath;
              }

              if (fGPtbl_PD_CashReceiptNo != "")
              {
                  MySqlComm.Parameters.Add("@fGPtbl_PD_CashReceiptNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_CashReceiptNo;
              }
              if (fGPtbl_PD_ChequeDDate != "")
              {
                  MySqlComm.Parameters.Add("@fGPtbl_PD_Date", SqlDbType.DateTime).Value = Convert.ToDateTime(fGPtbl_PD_ChequeDDate);
              }

              MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation", SqlDbType.Int).Value = fGPtbl_PD_BranchConfirmation;


              MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Float).Value = fGPtbl_PD_PaidAmount;
              MySqlComm.Parameters.Add("@FormTypeID", SqlDbType.Int).Value = FormTypeID;

              MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

              SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.VarChar, 500);
              outError.Direction = ParameterDirection.Output;
              MySqlComm.Parameters.Add(outError);

              MySqlComm.ExecuteNonQuery();

              Returnvalue = outError.Value.ToString();
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
          return Returnvalue;
      }

      public DataTable Company_Fetch_RejectedListcount(int branchid)
      {
          try
          {
              MyDt = new DataTable();
              //MyAdp = new SqlDataAdapter("GPsp_Company_Rejected_fetch", DBManager.ConnectToDB());
              MyAdp = new SqlDataAdapter("GPsp_Company_Rejected_fetch_count", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
              MyAdp.Fill(MyDt);

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
          return MyDt;
      }
      public DataTable Company_Fetch_RejectedList(int branchid)
      {
          try
          {
              MyDt = new DataTable();
              MyAdp = new SqlDataAdapter("GPsp_Company_Rejected_fetch", DBManager.ConnectToDB());
              //MyAdp = new SqlDataAdapter("GPsp_Company_Rejected_fetch_count", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;

              MyAdp.Fill(MyDt);

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
          return MyDt;
      }


      public SqlDataReader Company_Fetch_UnconfirmedTransactionRenewal_ByPaymentid(int Paymentid)
      {
          try
          {
              MySqlComm = new SqlCommand("GPsp_Company_UnconfirmedPaymentRenewal_FetchByPaymentid", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;
              MySqlComm.Parameters.Add("@Paymentid", SqlDbType.Int).Value = Paymentid;
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



      #endregion
      public SqlDataReader Company_FetchSlabFee_Renewal_Step6(decimal turnover, int CompanyId)
      {
          try
          {
              MySqlComm = new SqlCommand("GPsp_Company_FetchSlabFee_Renewal", DBManager.ConnectToDB());
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

      public DataSet Company_MonetaryLimit_Fetch_Datasetrenew(int UserId)
      {
          try
          {
              MyDs = new DataSet();
              MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Renewal_Fetch_All", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = UserId;
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

      public DataSet Company_MonetaryLimit_FetchZonalHeadRenew_Dataset(int UserId, int roleid, int deptid)
      {
          try
          {
              MyDs = new DataSet();
              MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_ZonalHead_Renewal_FetchFor_BHZHO", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              //MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = UserId;
              MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = UserId;
              MyAdp.SelectCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleid;
              MyAdp.SelectCommand.Parameters.Add("@DeptId", SqlDbType.Int).Value = deptid;
              //  MyAdp.SelectCommand.Parameters.Add("@Formtype", SqlDbType.Int).Value = FormtypeId;
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

      public DataSet Company_MonetaryLimit_FetchByCompId_Dataset(int Compid)
      {
          try
          {
              MyDs = new DataSet();
              MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Renewal_Fetch_ByCompanyId", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Compid;
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

      public string Company_InsertUpdate_Zone(int companyid, string monetarylimit, int userid)
      {
          string passwd = "";
          try
          {
              RandomKeyGenerator objkey = new RandomKeyGenerator();
              passwd = objkey.Generate();
              MySqlComm = new SqlCommand("GPsp_Company_Monetary_Renewal_Update_ById_zone", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;
              MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = companyid;
              MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
              if (monetarylimit != "a") MySqlComm.Parameters.Add("@MonetaryLimit", SqlDbType.VarChar, 1000).Value = monetarylimit;
              MySqlComm.Parameters.Add("@fGPtbl_Company_Pwd", SqlDbType.NVarChar, 4000).Value = passwd;
              //MySqlComm.Parameters.Add("@fGPtbl_Company_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
              //MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
              MySqlComm.ExecuteNonQuery();
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
          return passwd;
      }

      public DataSet Company_MonetaryLimit_FetchByCompId_Datasethistory(int Compid)
      {
          try
          {
              MyDs = new DataSet();
              MyAdp = new SqlDataAdapter("gp_sp_monetryhistory", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Compid;
              MyAdp.SelectCommand.Parameters.Add("@processid", SqlDbType.Int).Value = 1;
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


      public DataSet Company_Upload_DocumentView(int Compid)
      {
          try
          {
              MyDs = new DataSet();
              MyAdp = new SqlDataAdapter("GP_SP_FethlabeltovieDocument", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Compid;
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

      public int f_Request_changevaliditydate(int companyId, string validitydate)
      {
          int result = 0;
          try
          {
              MySqlComm = new SqlCommand("GP_sp_Create_Request", DBManager.ConnectToDB());
              MySqlComm.CommandType = CommandType.StoredProcedure;
              MySqlComm.Parameters.Add("@comapnyId", SqlDbType.Int).Value = companyId;
              MySqlComm.Parameters.Add("@validitydate", SqlDbType.VarChar, 500).Value = validitydate;
              SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
              outError.Direction = ParameterDirection.Output;
              MySqlComm.Parameters.Add(outError);
              MySqlComm.ExecuteNonQuery();
              result = Convert.ToInt32(outError.Value);
          }
          catch (Exception ex)
          {
              ErrHandler.WriteError(ex.Message);
          }
          finally
          {

          }
          return result;
      }
}
