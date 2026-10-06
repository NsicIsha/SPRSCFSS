using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using NSICGP_VBClassLibrary;

public partial class HomeNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();

     // string compid=  objDecEnc.Decrypt("ok2kBYVSDrk=");
        //int Ccc = 63416;
        //string sssss = objDecEnc.Encrypt(Ccc.ToString());
        SqlCommand sqlcom = new SqlCommand();  
        try
        {
            //sqlcom.Connection = DBManager.ConnectToDB();
            //sqlcom.CommandText = "select count(fGPtbl_Company_Id) from dbo.GPtbl_Company where fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0 and fGPtbl_Company_Status!=5";
            //string comcount = Convert.ToString(sqlcom.ExecuteScalar());
            //lblUnits.Text = "CURRENT REGISTERED UNITS: " + comcount;
            //sqlcom.Connection.Close();
            //UpdateUnitCount();

        }
        catch { }
        finally
        {
            sqlcom.Dispose();
            DBManager.CloseConnection();
        }

    }

    public void UpdateUnitCount()
    {
        DataSet ds = new DataSet();       
        try
        {
            SqlCommand MySqlComm;
            int CountStatus = 0;

             if (File.Exists((Server.MapPath("/Document/Upload/XMLData/" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml"))))
             {
                XmlDocument xmlDocument = new XmlDocument();
                 //Read the XML File  
                 xmlDocument.Load((Server.MapPath("/Document/Upload/XMLData/" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml")));
                 //Create a XML Node List with XPath Expression  
                 XmlNode xmlNode = xmlDocument.SelectSingleNode("/UnitCount");
                 lblUnits.Text = "CURRENT REGISTERED UNITS: " + xmlNode.InnerText;
             }

             else
            {
                MySqlComm = new SqlCommand("GP_SP_HomePageSPRSCount", DBManager.ConnectToDB());
                MySqlComm.CommandType = CommandType.StoredProcedure;
                SqlParameter outCount = new SqlParameter("@UnitCount", SqlDbType.Int);
                outCount.Direction = ParameterDirection.Output;
                MySqlComm.Parameters.Add(outCount);
                MySqlComm.ExecuteNonQuery();
                CountStatus = Convert.ToInt32(outCount.Value);
                if (CountStatus != 0)
                {
                     XmlTextWriter writer = new XmlTextWriter(Server.MapPath("/Document/Upload/XMLData/" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml"), System.Text.Encoding.UTF8);
                     //Start XM DOcument
                     writer.WriteStartDocument(true);
                     writer.Formatting = Formatting.Indented;
                     writer.Indentation = 2;
                     //ROOT Element
                     writer.WriteStartElement("UnitCount");
                     writer.WriteString(CountStatus.ToString());
                     writer.WriteEndElement();
                     //End XML Document
                     writer.WriteEndDocument();
                     //Close writer
                     writer.Close();
                    lblUnits.Text = "CURRENT REGISTERED UNITS: " + CountStatus.ToString();
                }
                else
                {
                    lblUnits.Text = "CURRENT REGISTERED UNITS: " ;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

    }
}