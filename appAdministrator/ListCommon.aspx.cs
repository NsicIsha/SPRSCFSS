using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_ListCommon : System.Web.UI.Page
{
    int req = 0;
    clsMasterList obj = new clsMasterList();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FunRequest();

            if (req == 1)
                lblHeader.Text = "SSI List of reserved items which comes under SSI";
            else if (req == 2)
                lblHeader.Text = "Registrar of Firm with contact details";
            else if (req == 3)
                lblHeader.Text = "Registrar of Company with contact details";
            else if (req == 4)
                lblHeader.Text = "DIC Offices with contact details";
            else if (req == 5)
                lblHeader.Text = "MSME DIC Offices with contact details";

            obj = new clsMasterList();
            System.Data.DataSet ds = new System.Data.DataSet();
            ds = obj.MasterList_Fetch_DataSet(req);
            lblInstruction.Text = ds.Tables[0].Rows[0]["fGPtbl_ML_Content"].ToString();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    private void FunRequest()
    {
        try
        {
            req = Convert.ToInt32(Request["i"].ToString());
        }
        catch (Exception ex)
        {
            req = 0;
            ErrHandler.WriteError(ex.Message);
        }
    }
}
