using System;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;


/// <summary>
/// Summary description for clsMasterUser
/// </summary>
public class clsMasterUser
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;
    

	public clsMasterUser()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //TO FETCH ALL DATA FROM TABLE
    public DataSet MasterUser_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_All", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
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

    //TO FETCH ALL DATA FROM TABLE IN DESCENDING ORDER
    public DataSet MasterUser_FetchDesc_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_All_OBDesc", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
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

    //TO FETCH SELECTED DATA FROM TABLE USING ID
    public DataSet MasterUser_Fetch_DataSet(int MasterUserId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MU_Id", SqlDbType.Int).Value = MasterUserId;

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

    //TO FETCH DATA FROM TABLE USING ROLE ID
    public DataSet MasterUser_FetchByRoleId_DataSet(int RoleId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_ByRoleId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MU_RoleId", SqlDbType.Int).Value = RoleId;

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

    //TO FETCH USER NAME FOR AUTOCOMPLETE
    public DataSet MasterUser_FetchAutocomplete_DataSet(string Username)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_Autocomplete", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = Username;

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



    //TO FETCH DATA FROM TABLE USING ROLE ID IN DESCENDING ORDER
    public DataSet MasterUser_FetchByRoleIdDesc_DataSet(int RoleId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_ByRoleId_OBDesc", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MU_RoleId", SqlDbType.Int).Value = RoleId;

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

    //TO DELETE USING ID
    public int MasterUser_DeleteById(int MasterUserId, int UserId)
    {
        try
        {
            
            MySqlComm = new SqlCommand("GPsp_Delete_MasterUser_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MU_Id", SqlDbType.Int).Value = MasterUserId;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outVal = new SqlParameter("@OutPut", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }

    //TO INSERT OR UPDATE
    
    public SqlDataReader MasterUser_InsertUpdate(int MasterUserId, string fGPtbl_MU_Name, string fGPtbl_MU_Pwd, int fGPtbl_MU_RoleId,
        int fGPtbl_MU_DepartmentId, string fGPtbl_MU_FName, string fGPtbl_MU_MName, string fGPtbl_MU_LName, string fGPtbl_MU_DOB,
        string fGPtbl_MU_Email, string fGPtbl_MU_Phone, string fGPtbl_MU_Mobile, string fGPtbl_MU_Fax, int UserId)
    {
        try
        {
            
            MySqlComm = new SqlCommand("GPsp_Update_MasterUser", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MU_Id", SqlDbType.Int).Value = MasterUserId;
            MySqlComm.Parameters.Add("@fGPtbl_MU_Name", SqlDbType.VarChar,200).Value = fGPtbl_MU_Name;
            MySqlComm.Parameters.Add("@fGPtbl_MU_Pwd", SqlDbType.NVarChar, 2000).Value = fGPtbl_MU_Pwd;
            MySqlComm.Parameters.Add("@fGPtbl_MU_RoleId", SqlDbType.Int).Value = fGPtbl_MU_RoleId;
            MySqlComm.Parameters.Add("@fGPtbl_MU_DepartmentId", SqlDbType.Int).Value = fGPtbl_MU_DepartmentId;
            if (fGPtbl_MU_FName.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MU_FName", SqlDbType.VarChar, 200).Value = fGPtbl_MU_FName;
            if (fGPtbl_MU_MName.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MU_MName", SqlDbType.VarChar, 200).Value = fGPtbl_MU_MName;
            if (fGPtbl_MU_LName.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MU_LName", SqlDbType.VarChar, 200).Value = fGPtbl_MU_LName;
            //if (fGPtbl_MU_DOB.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MU_DOB", SqlDbType.VarChar).Value = fGPtbl_MU_DOB;
            if (fGPtbl_MU_Email.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MU_Email", SqlDbType.VarChar, 200).Value = fGPtbl_MU_Email;
            if (fGPtbl_MU_Phone.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MU_Phone", SqlDbType.VarChar, 15).Value = fGPtbl_MU_Phone;
            if (fGPtbl_MU_Mobile.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MU_Mobile", SqlDbType.VarChar, 15).Value = fGPtbl_MU_Mobile;
            if (fGPtbl_MU_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MU_Fax", SqlDbType.VarChar, 15).Value = fGPtbl_MU_Fax;
            MySqlComm.Parameters.Add("@Output", SqlDbType.Int).Value = 8;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

           MyDr=MySqlComm.ExecuteReader();
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

    public SqlDataReader MasterUser_CheckCredentials(string UserName, string Password)
    {
        try
        {
            //DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptedPwd = Password.ToString().Trim();

            MySqlComm = new SqlCommand("GPsp_Fetch_UserByName", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MU_Name", SqlDbType.VarChar, 200).Value = UserName;
            MySqlComm.Parameters.Add("@fGPtbl_MU_Pwd", SqlDbType.VarChar, 2000).Value = EncryptedPwd;

            MyDr = MySqlComm.ExecuteReader();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        return MyDr;
    }

    public int MasterUser_UpdateLoginNo(int UserId, int LoginNo, string LoginIP)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_LoginNo", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@LoginNo", SqlDbType.Int).Value = LoginNo;
            MySqlComm.Parameters.Add("@LoginIP", SqlDbType.VarChar, 50).Value = LoginIP;

            SqlParameter outVal = new SqlParameter("@OutPut", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return ErrorId;
    }

    //TO CHANGE PASSWORD
    public int MasterUser_ChangePassword(int UserId, string Newpassword, string Oldpassword, int Edituser)
    {
        try
        {

            /*SIMPLE PASSWORD 7/6/2012
             DecryptEncrypt objDecEnc = new DecryptEncrypt();*/
            string EncryptedPwd = Newpassword.ToString().Trim();
            string EncryptedOldPwd = Oldpassword.ToString().Trim();

            MySqlComm = new SqlCommand("GPsp_ChangePassword", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MU_Id", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 2000).Value = EncryptedPwd;
            MySqlComm.Parameters.Add("@fGPtbl_MU_Pwd", SqlDbType.NVarChar, 2000).Value = EncryptedOldPwd;
            MySqlComm.Parameters.Add("@fGPtbl_MU_EUser", SqlDbType.Int).Value = Edituser;

            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }

    //FORGOT PASSWORD
    public ArrayList MasterUser_ForgotPassword(string Username)
    {
        string Encryptpwd=string.Empty;
        ArrayList arr = new ArrayList();
        try
        {
            //DecryptEncrypt objDecEnc = new DecryptEncrypt();
            RandomKeyGenerator objRnd = new RandomKeyGenerator();
            Encryptpwd = objRnd.Generate();
            //Encryptpwd = objDecEnc.Encrypt(Encryptpwd).Trim();

            MySqlComm = new SqlCommand("GPsp_ForgotPassword", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MU_Name", SqlDbType.VarChar, 200).Value = Username;
            MySqlComm.Parameters.Add("@fGPtbl_MU_Pwd", SqlDbType.NVarChar, 2000).Value = Encryptpwd;

            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            SqlParameter outEmail = new SqlParameter("@Email", SqlDbType.VarChar, 200);
            outEmail.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outEmail);

            SqlParameter outPwd = new SqlParameter("@Password", SqlDbType.NVarChar, 2000);
            outPwd.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outPwd);

            MySqlComm.ExecuteNonQuery();
            //Decryptpwd = objDecEnc.Decrypt(outPwd.Value.ToString());
            arr.Add(outVal.Value);
            arr.Add(outEmail.Value);
            arr.Add(Encryptpwd);
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
        return arr;
    }

    public DataSet MasterUser_FetchByRoleUserName_DataSet(int RoleId, string UserName)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_Search", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
            if(UserName.Trim()!="") MyAdp.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = UserName;

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


    public DataSet MasterUser_FetchAllRoleUserName_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_SearchAll", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

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


    public int MasterUser_ActivateById(int MasterUserId, int UserId, string Activate)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterUser_ActivateById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MU_Id", SqlDbType.Int).Value = MasterUserId;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@Activate", SqlDbType.VarChar, 15).Value = Activate;

            SqlParameter outVal = new SqlParameter("@OutPut", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }

    //TO FETCH USER ID USING ROLEID AND ZONEID
    public DataSet MasterUser_FetchUserByRoleZone_DataSet(int RoleId, int ZoneId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_ByRoleIdZoneId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MU_RoleId", SqlDbType.Int).Value = RoleId;
            MyAdp.SelectCommand.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneId;

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

    //TO FETCH USER ID BY ROLEID, ZONEID, BRANCHID
    //public DataSet MasterUser_FetchUserByRoleZoneBranch_DataSet(int RoleId, int ZoneId, int BranchId)
    //{
    //    try
    //    {
    //        MyDs = new DataSet();
    //        MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_ByRoleIdZoneIdBranchId", DBManager.ConnectToDB());
    //        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

    //        MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MU_RoleId", SqlDbType.Int).Value = RoleId;
    //        MyAdp.SelectCommand.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneId;
    //        MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;

    //        MyAdp.Fill(MyDs);
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MyAdp.Dispose();
    //    }
    //    return MyDs;
    //}

    public int MasterUser_FetchUserByRoleZoneBranchId(int RoleId, int ZoneId, int BranchId, string Username)
    {
        try
        {
            MyDs = new DataSet();
            MySqlComm = new SqlCommand("GPsp_Fetch_MasterUser_ByRoleIdZoneIdBranchId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MU_RoleId", SqlDbType.Int).Value = RoleId;
            MySqlComm.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneId;
            MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            MySqlComm.Parameters.Add("@fGPtbl_MU_Name", SqlDbType.VarChar, 200).Value = Username;

            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;

            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();

            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;

    }

    //TO FETCH ZONAL HEAD EMAIL BY USER ID
    public DataSet MasterUser_FetchZonalHeadEmailById_DataSet(int Userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_EmailofZonalHead_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = Userid;

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

    //  TO FETCH All Active Department
    public DataTable Master_FETCH_Department(int ROLEID)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterDepartment", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = ROLEID;
            MyAdp.Fill(dt);
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
        return dt;

    }

    public DataSet MasterUser_FetchUser_ByBranchIdRole(int branchid, int roleid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_ByBranchIdRole", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.SelectCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleid;

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


}
