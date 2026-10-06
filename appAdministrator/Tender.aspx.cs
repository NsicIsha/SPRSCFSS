using NSICGP_VBClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

public partial class Tender : System.Web.UI.Page
{
    dalVariable objDal;
    DataSet Myds;
    clsMasterUser objUser;
    clsMasterMailer objMailer;
    int ErrorId = 0;

    string MailTo, MailFrom;

    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;

    int MessageFlage = 0;
    string userid_chek;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // SqlCommand sqlcom = new SqlCommand();
            try
            {
               // sqlcom.Connection = DBManager.ConnectToDB();
               // sqlcom.CommandText = "select count(fGPtbl_Company_Id) from dbo.GPtbl_Company where fGPtbl_Company_Display=1 and //fGPtbl_Company_Delete=0 and fGPtbl_Company_Status!=5";
               // string comcount = Convert.ToString(sqlcom.ExecuteScalar());
                //Label lblUnits = Master.FindControl("lblUnits") as Label;
                //lblUnits.Text = "CURRENT REGISTERED UNITS: " + comcount;
                //sqlcom.Connection.Close();
				UpdateUnitCount();
                FillTenderGridview();
            }
            catch { }
            finally
            {
               // sqlcom.Dispose();
               // DBManager.CloseConnection();
            }
          
        }

    }
public void UpdateUnitCount()
    {
        DataSet ds = new DataSet();       
        try
        {
            SqlCommand MySqlComm;
            int CountStatus = 0;
			Label lblUnits = Master.FindControl("lblUnits") as Label;
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

    public void FillTenderGridview()
    {
        string query = "select TenderTitle as 'Title',TenderID as 'TenderID',TenderDescription as 'Description',fGPtbl_MB_Name as 'Location', convert(varchar, PublishDate, 103) as 'PublishDate',PublishTime as 'PublishTime',convert(varchar, PrebidDate, 103) as 'PreBidDate',PrebidTime as 'PrebidTime' " +
                   ", convert(varchar, BidStartDate, 103) as 'StartDate',BidStartTime as 'StartTime',convert(varchar, BidEndDate, 103) as 'EndDate',BidEndTime as 'EndTime',convert(varchar, BidOpeningDate, 103) as 'OpenDate', BidOpeningTime as 'OpenTime',Upload as 'Upload' from fGPtbl_Tenders inner join GPtbl_MasterBranch on fGPtbl_MB_Id=Location order by BidEndDate Desc ";

        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ds = new DataSet();
                da.Fill(ds);

                grdTenders.DataSource = ds;
                grdTenders.DataBind();
                grdTenders.HeaderRow.TableSection = TableRowSection.TableHeader;
                
                con.Close();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            //string filePath = (sender as LinkButton).CommandArgument;
            //string path = "~/Document/Tenders/" + filePath;
            //Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path));
            //Response.WriteFile(path);
            //Response.End();
            
            string conn = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("GetFiles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = (sender as LinkButton).CommandArgument;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
           
            if (string.IsNullOrEmpty(dt.Rows[0]["FileContent"].ToString()))
            {
                string filePath = dt.Rows[0]["Upload"].ToString();
                string path = "~/Document/Tenders/" + filePath;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path));
                Response.WriteFile(path);
                Response.End();
            }
            else
            {
                string Name = dt.Rows[0]["FileName"].ToString() + "." + dt.Rows[0]["FileType"].ToString();
                byte[] documentBytes = (byte[])dt.Rows[0]["FileContent"];
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", string.Format("attachment;FileName={0}", Name));
                Response.AddHeader("Content-Length", documentBytes.Length.ToString());
                Response.BinaryWrite(documentBytes);
                Response.Flush();
                Response.Close();
            }

        }
        catch(Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}

