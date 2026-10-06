using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_GetRefNumber :  System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsGenerateNo objGenerate = new clsGenerateNo();
        int BranchId = 11;

        string ReferenceNo = objGenerate.Company_ReferenceNo(BranchId);
        //"NSIC/GP/NOI/2023/99930"
        //NSIC/GP/NOI/2023/99931
        lblReFnUMBER.Text= ReferenceNo;
    }
}