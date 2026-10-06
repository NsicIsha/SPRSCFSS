using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Text;
using System.Web.Mail;
using System.IO;
 
using System.Net.NetworkInformation;
using System.Security.Cryptography;

public class ABMiscFunction
{
    #region(IP AND MACHINE NAME)
    public static string[] GetIpAndMachine()
    {
        string[] ip = new string[2];
        ip[0] = System.Net.Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress)[0].ToString();
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        ip[1] = nics[0].GetPhysicalAddress().ToString();
        return ip;
    }
    #endregion

    #region(GET LOGIN DATE TIME)
    public static string GetIpAddressAndmachineName()
    {
        string ipAndMachine = string.Empty;
        try
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            ipAndMachine = System.Net.Dns.GetHostAddresses(ipAddress)[0].ToString() + ";" + System.Net.Dns.Resolve(HttpContext.Current.Request.ServerVariables["remote_addr"]).HostName;
        }
        catch (Exception ex)
        {
            //ABApplicationError.LogErrors(ex);
        }
        return ipAndMachine;
    }
    #endregion

    #region (SEND MAIL FROM WEBSITE)
    public static void SendMail(string mailSubject, string mailBody, string mailTo, string mailFrom)
    {
        //try
        //{ 
        MailMessage oMessage = new MailMessage();
        oMessage.BodyFormat = MailFormat.Text;
        oMessage.To = mailTo;
        oMessage.From = mailFrom;
        oMessage.Subject = mailSubject;
        oMessage.Body = mailBody;
        //ENTER MAIL SERVER ADDRESS FOR SENDING MAIL FROM MAIL SERVER
        SmtpMail.SmtpServer = "smtp.gmail.com";
        SmtpMail.Send(oMessage);
        //}
        //catch (Exception ex)
        //{
        //    ApplicationError.LogErrors(ex);
        //}
    }

    #endregion

    #region(GET LOGIN DATE TIME)
    public static string GetLoginTime()
    {
        string sLoginTime = string.Empty;
        try
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity formsId = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket formsTicket = formsId.Ticket;
                        sLoginTime = (formsTicket.UserData.Split(';'))[2];
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //ABApplicationError.LogErrors(ex);
        }
        return sLoginTime;
    }
    #endregion

    #region(GET USER INSTITUE)
    public static string GetUserInstitue()
    {
        //string sRoleName = string.Empty;
        string InstName = string.Empty;
        try
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity formsId = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket formsTicket = formsId.Ticket;
                        //sRoleName = (formsTicket.UserData.Split(';'))[0];
                        InstName = (formsTicket.UserData.Split(';'))[2];
                        //sRoleName = sRoleName +", Tool Room, "+ InstName;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //ABApplicationError.LogErrors(ex);
        }
        return InstName;
    }
    #endregion

    #region(GET USER ROLE)
    public static string GetUserRole()
    {
        string sRoleName = string.Empty;
        try
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity formsId = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket formsTicket = formsId.Ticket;
                        sRoleName = (formsTicket.UserData.Split(';'))[0];
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ////ABApplicationError.LogErrors(ex);
        }
        return sRoleName;
    }
    #endregion

    #region(GET USER ID)
    public static int GetUserID()
    {
        int iUserId = 0;
        try
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity formsId = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket formsTicket = formsId.Ticket;
                        iUserId = Convert.ToInt32((formsTicket.UserData.Split(';'))[1]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
           // //ABApplicationError.LogErrors(ex);
        }
        return iUserId;
    }
    #endregion

    #region(GET Login NAME)
    public static string GetLoginName()
    {
        string sUserName = string.Empty;
        try
        {
            //TO CHECK CURRENT LOGIN USER
            if (HttpContext.Current.User != null)
            {
                //TO GET USER NAME
                sUserName = HttpContext.Current.User.Identity.Name;
            }
            else
            {
                //LOG OUT AND REDIRECT TO LOGIN PAGE
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                HttpContext.Current.Response.End();

            }
        }
        catch (Exception ex)
        {
            //ABApplicationError.LogErrors(ex);
        }
        return sUserName;
    }
    #endregion

    #region(GET USER NAME)
    public static string GetUserName()
    {
        string sUserName = string.Empty;
        try
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity formsId = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket formsTicket = formsId.Ticket;
                        sUserName = (formsTicket.UserData.Split(';'))[3];
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //ABApplicationError.LogErrors(ex);
        }
        return sUserName;
    }
    #endregion

    #region(PASSWORD ENCRYPT/DECRYPT)
    //public static string EncryptPassword(string password)
    //{
    //    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
    //    byte[] hashedDataBytes;
    //    UTF8Encoding encoder = new UTF8Encoding();
    //    hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(password));
    //    System.Text.StringBuilder strHex = new System.Text.StringBuilder();
    //    foreach (byte b in hashedDataBytes)
    //    {
    //        strHex.Append(b.ToString("x2").ToLower());
    //    }

    //    string str = strHex.ToString();
    //    return str;
    //}
    #endregion


    #region(PASSWORD ENCRYPT/DECRYPT)
    public static string EncryptPasswordSha(string password)
    {
        SHA256CryptoServiceProvider shaHasher = new SHA256CryptoServiceProvider();
        byte[] hashedDataBytes;
        UTF8Encoding encoder = new UTF8Encoding();
        hashedDataBytes = shaHasher.ComputeHash(encoder.GetBytes(password));
        System.Text.StringBuilder strHex = new System.Text.StringBuilder();
        foreach (byte b in hashedDataBytes)
        {
            strHex.Append(b.ToString("x2").ToLower());
        }
        string str = strHex.ToString();
        return str;
    }
    #endregion



    #region(PASSWORD ENCRYPT/DECRYPT SHA512)
    public static string GenerateSHA512String(string inputString)
    {
        SHA512CryptoServiceProvider shaHasher = new SHA512CryptoServiceProvider();
        byte[] hashedDataBytes;
        UTF8Encoding encoder = new UTF8Encoding();
        hashedDataBytes = shaHasher.ComputeHash(encoder.GetBytes(inputString));
        System.Text.StringBuilder strHex = new System.Text.StringBuilder();
        foreach (byte b in hashedDataBytes)
        {
            strHex.Append(b.ToString("x2").ToLower());
        }
        string str = strHex.ToString();
        return str;
        //SHA512 sha512 = SHA512Managed.Create();
        //byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        //byte[] hash = sha512.ComputeHash(bytes);
        //return GetStringFromHash(hash);
    }
    #endregion


    #region (POP UP ALERT MESSAGE)
    public static void PopupMessage(string strMessage, Page objPage)
    {
        StringBuilder sbMessage = new StringBuilder();
        sbMessage.Append("<script>");
        sbMessage.Append("window.alert('" + strMessage + "')");
        sbMessage.Append("</script>");
        objPage.RegisterStartupScript("Popup", sbMessage.ToString());
    }

    #endregion

    #region (POP UP WINDOW MESSAGE FOR UPDATE PANEL)
    public static void PopupMessage(string strMessage, Type type, Control ctrlUpdatePanel)
    {
        //try
        //{ 
        ScriptManager.RegisterStartupScript(ctrlUpdatePanel, type, "Message", "window.alert('" + strMessage + "');", true);
        //ScriptManager.RegisterClientScriptBlock(ctrlUpdatePanel, type, "Message", "window.alert('" + strMessage + "');", true);
        //}
        //catch (Exception ex)
        //{
        //    ApplicationError.LogErrors(ex);
        //}
    }
    #endregion

    #region (CHECK FILE TYPE)
    public static bool CheckFileType(FileUpload fileUploadResume)
    {
        string firstline;
        string lastline;
        System.IO.Stream fs;
        fs = fileUploadResume.PostedFile.InputStream;
        System.IO.StreamReader rs;
        rs = new System.IO.StreamReader(fs, true);
        bool chk = false;
        try
        {
            if ((fileUploadResume.PostedFile.ContentType.ToString() == "image/jpg") || (fileUploadResume.PostedFile.ContentType.ToString() == "image/JPG") || (fileUploadResume.PostedFile.ContentType.ToString() == "image/pjpeg") || (fileUploadResume.PostedFile.ContentType.ToString() == "image/jpeg") || (fileUploadResume.PostedFile.ContentType.ToString() == "application/pdf"))
            {
                firstline = rs.ReadLine();
                lastline = rs.ReadToEnd();

                if ((firstline.IndexOf("%PDF") > -1) || (firstline.IndexOf("JFIF") > -1) || (firstline.IndexOf("Exif") > -1))
                {
                    chk = true;
                }
            }
        }
        catch (Exception e) { }
        return chk;
    }

    public static bool CheckResumeFileType(string fileName)
    {
        string extension = Path.GetExtension(fileName);
        switch (extension.ToLower())
        {
            //DOC FILES
            case ".pdf":
                return true;
            case ".jpeg":
                return true;
            case ".jpg":
                return true;
            default:
                return false;
        }
    }
    #endregion

    #region (CHECK USER SESSION)
    public static bool CheckUserSession()
    {
        bool chk = true;

        //if (HttpContext.Current.Session["AuthToken"] != null && HttpContext.Current.Request.Cookies["AuthToken"] != null)
        //{
        //    if (HttpContext.Current.Session["AuthToken"].ToString().Equals(HttpContext.Current.Request.Cookies["AuthToken"].Value))
        //    {
        //        string authToken = HttpContext.Current.Session["AuthToken"].ToString();
        //        string cAuthToken = HttpContext.Current.Request.Cookies["AuthToken"].Value;

        //        string guid = Guid.NewGuid().ToString();
        //        HttpContext.Current.Session["AuthToken"] = guid;
        //        // now create a new cookie with this guid value
        //        HttpCookie cookie = HttpContext.Current.Request.Cookies["AuthToken"];

        //        if (cookie == null)
        //        {
        //            cookie = new HttpCookie("AuthToken");
        //        }
        //        cookie.Expires = DateTime.Now.AddMinutes(20);
        //        cookie.Value = guid;
        //        HttpContext.Current.Response.Cookies.Set(cookie);
        //    }
        //    else
        //        chk = false;
        //}
        //else
        //    chk = false;
        //return chk;
        //-------------------New--------------------------
        if (HttpContext.Current.Session["AuthToken"] != null)
        {
            string guid = Guid.NewGuid().ToString();
            HttpContext.Current.Session["AuthToken"] = guid;
            // now create a new cookie with this guid value
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AuthToken"];
            if (cookie == null)
            {
                cookie = new HttpCookie("AuthToken");
            }
            cookie.Expires = DateTime.Now.AddMinutes(20);
            cookie.Value = guid;
            cookie.Path = "/ab";
            cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
        else
        {
            chk = false;
        }
        return chk;
    }
    #endregion

    #region (DEFAULT CONSTRUCTOR)
    public ABMiscFunction()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region(BIND CURRENT FINANCIAL YEAR)
    public static string CfyearC()
    {
        string Fyear = string.Empty;
        DateTime curDate = DateTime.Parse(DateTime.Now.ToShortDateString());
        if (curDate.Month <= 3)
        {
            int startYr = curDate.Year - 1;
            Fyear = startYr + "-" + curDate.Year.ToString().Substring(2);
            return Fyear;
        }
        else
        {
            int endYr = curDate.Year + 1;
            Fyear = curDate.Year + "-" + endYr.ToString().Substring(2);
            return Fyear;
        }
    }
    #endregion

    #region(GET UniqueID)
    public static int GetUniqueID()
    {
        int LoginUniqueID = 0;
        try
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity formsId = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket formsTicket = formsId.Ticket;
                        LoginUniqueID = Convert.ToInt32((formsTicket.UserData.Split(';'))[4]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //ABApplicationError.LogErrors(ex);
        }
        return LoginUniqueID;
    }
    #endregion
}