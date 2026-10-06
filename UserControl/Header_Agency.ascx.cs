using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Header_Agency : System.Web.UI.UserControl
{
    string USERFULLNAME, BRANCHNAME;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            BRANCHNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["BRANCHNAME"]).ToString();
        }
        catch (Exception ex)
        {
            USERFULLNAME = "";
            BRANCHNAME = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //lblUser.Text = "Welcome AGENCY (" + BRANCHNAME.ToUpper() + ") :- <b><i>" + USERFULLNAME.ToUpper() + "</i></b>";
        lblUser.Text = "Welcome AGENCY :- <b><i>" + USERFULLNAME.ToUpper() + "</i></b>";

    }
}
