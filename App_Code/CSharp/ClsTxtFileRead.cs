using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// Summary description for ClsTxtFileRead
/// </summary>
public class ClsTxtFileRead
{
    public static string MailContentsRead(string strFilename)
    {
        string contents = "";
        strFilename = "~/Mailor/" + strFilename;
        try
        {
            string FILENAME = System.Web.HttpContext.Current.Request.MapPath(strFilename);
            StreamReader objStreamReader = default(StreamReader);
            objStreamReader = File.OpenText((FILENAME));
            contents = objStreamReader.ReadToEnd();
            Console.WriteLine(contents);
            objStreamReader.Close();
        }
        catch (Exception ex)
        {
        }
        return contents;
    }
}