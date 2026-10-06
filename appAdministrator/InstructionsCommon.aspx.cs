using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_InstructionsCommon : System.Web.UI.Page
{
    int req = 0;
    clsMasterInstruction obj;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FunRequest();

            if (req == 1)
                lblHeader.Text = "Registration";
            else if (req == 2)
                lblHeader.Text = "Renewal";
            else if (req == 3)
                lblHeader.Text = "Amendment";
            else if (req == 4)
                lblHeader.Text = "Competency Certificate";
            else if (req == 5)
                lblHeader.Text = "Capacity Verification";

            lblHeader.Text = lblHeader.Text.ToUpper();

            obj = new clsMasterInstruction();
            System.Data.DataSet ds = new System.Data.DataSet();
            ds = obj.MasterInstruction_FetchByFormId_DataSet(req);
            lblInstruction.Text = ds.Tables[0].Rows[0]["fGPtbl_MI_Content"].ToString();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
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
