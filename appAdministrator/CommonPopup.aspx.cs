using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class CommonPopup : System.Web.UI.Page
{

    clsMasterInstruction objInst;
    clsMasterList objList;
    clsMasterContent objContent;
    DataSet Myds;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] != null)
            {

                if (Request.QueryString["name"] == "list")
                {
                    objList = new clsMasterList();
                    Myds = new DataSet();
                    //listAction = Convert.ToInt32(Request.QueryString["id"]);
                    Myds = objList.MasterList_Fetch_DataSet(Convert.ToInt32(Request.QueryString["id"]));
                    ltName.Text = Myds.Tables[0].Rows[0][1].ToString();

                    lblContent.Text = Myds.Tables[0].Rows[0][2].ToString();
                    lblHeader.Text = "Master List";

                }

                else if (Request.QueryString["name"] == "inst")
                {
                    objInst = new clsMasterInstruction();
                    Myds = new DataSet();
                    Myds = objInst.MasterInstruction_Fetch_DataSet(Convert.ToInt32(Request.QueryString["id"]));

                    ltName.Text = Myds.Tables[0].Rows[0][2].ToString();
                    lblContent.Text = Myds.Tables[0].Rows[0][3].ToString();
                    lblHeader.Text = "Master Instruction";
                }

                else if (Request.QueryString["name"] == "cont")
                {
                    objContent = new clsMasterContent();
                    Myds = new DataSet();
                    Myds = objContent.MasterContent_Fetch_DataSet(Convert.ToInt32(Request.QueryString["id"]));

                    ltName.Text = Myds.Tables[0].Rows[0][2].ToString();
                    lblContent.Text = Myds.Tables[0].Rows[0][3].ToString();
                    lblHeader.Text = "Master Content";
                }


            }
            if (lblContent.Text == "")
                lblContent.Text = "[No Content]";


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            
        }
    }
}
