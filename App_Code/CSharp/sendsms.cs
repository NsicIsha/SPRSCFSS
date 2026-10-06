using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using NSICGP_VBClassLibrary;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for sendsms
/// </summary>
public static class sendsms
{
    //public sendsms()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

    public static void Send(string msg, string mobileno)
    {
        try
        {
            msg = RemoveHTMLTagsFromString(msg);
            // http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=gaurav@nagsoft.com:nsic@123&senderID=TEST SMS&receipientno=9811170340&dcs=0&msgtxt=This is Test message&state=4 


            // string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=gaurav@nagsoft.com:nsic@123&senderID=TEST SMS&receipientno=" + mobileno + "&msgtxt=" + msg + "&state=4";
            // string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=dmit@nsic.co.in:nsic@5294&senderID=TEST SMS&receipientno=" + mobileno + "&msgtxt=" + msg + "&state=4";
            string strUrl = "http://staticking.org/index.php/smsapi/httpapi/?uname=attendance&password=tisdc&sender=NSICAT&receiver="+mobileno+"&route=TA&msgtype=1&sms="+msg+"";
           WebRequest request = HttpWebRequest.Create(strUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Exception From Sending SMS " + ex.Message);
            
        }
    }

    private static string RemoveHTMLTagsFromString(string Text)
    {
        return Regex.Replace(Text, "<[^>]*>", String.Empty);
    }






}

