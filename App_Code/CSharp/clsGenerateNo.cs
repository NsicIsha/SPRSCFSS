using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsGenerateNo
/// </summary>
public class clsGenerateNo
{
    clsCompany objCompany;
	public clsGenerateNo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string Company_ReferenceNo(int BranchId)
    {
        string ReferenceNo = "";

        objCompany=new clsCompany();
        int MaxId = objCompany.Company_Fetch_MaxId();
        string strMaxId = MaxId.ToString();
        if (strMaxId.Length == 1)
            strMaxId = "0000" + strMaxId.ToString();
        else if (strMaxId.Length == 2)
            strMaxId = "000" + strMaxId.ToString();
        else if (strMaxId.Length == 3)
            strMaxId = "00" + strMaxId.ToString();
        else if (strMaxId.Length == 4)
            strMaxId = "0" + strMaxId.ToString();

        objCompany=new clsCompany();
        string BranchCode = objCompany.MasterBranch_Fetch_Code(BranchId);

        int Year = DateTime.Now.Year;
        int Month = DateTime.Now.Month;
        //TO PICK YEAR AS FINANCIAL YEAR
        if (Month < 4)
        {
            Year = Year - 1;
        }
        string strYear = Year.ToString();

        //strYear = strYear.Substring(2);

        ReferenceNo = "NSIC/GP/" + BranchCode.ToString() + "/" + strYear.ToString() + "/" + strMaxId.ToString();
        return ReferenceNo;
    }

    
}
