using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
public partial class MsmeDBVisit : System.Web.UI.Page
{
    clsCompany objCompany;

    DataTable dt;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        //SqlCommand sqlcom = new SqlCommand();
        //try
        //{
        //    sqlcom.Connection = DBManager.ConnectToDB();
        //    sqlcom.CommandText = "select count(fGPtbl_Company_Id) from dbo.GPtbl_Company where fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0 and fGPtbl_Company_Status!=5";
        //    string comcount = Convert.ToString(sqlcom.ExecuteScalar());
        //    lblCount.Text = "Current Registered Units: " + comcount;
        //    sqlcom.Connection.Close();
        //}
        //catch { }
        //finally
        //{
        //    sqlcom.Dispose();
        //    DBManager.CloseConnection();
        //}


        
        Funfill();



    }

    private void Funfill()
    {
        SqlDataReader Mydr = NsicIntranet_FetchRecord();

        TableRow trow;
        TableCell tcell;
        int Cnt = 0;
        //System.Data.SqlClient.SqlDataReader MyDr;
        //objCompany = new clsCompany();
        //MyDr = objCompany.Company_FetchBranchPayment_ByFormtypeId_Step6(1);
        while (MyDr.Read())
        {
            Table tbl_1 = new Table();
            tbl_1.CellPadding = 0;
            tbl_1.CellSpacing = 0;
            tbl_1.Width = Unit.Percentage(100);
            if (Cnt == 0)
            {
                trow = new TableRow();
                trow.CssClass = "dynRowHeader";

                tcell = new TableCell();
                tcell.Text = "S.No.";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                //tcell = new TableCell();
                //tcell.Text = "";
                //tcell.CssClass = "clstblCellHead";
                //trow.Cells.Add(tcell);

                //tcell = new TableCell();
                //tcell.Text = "Payment Slab";
                //tcell.CssClass = "clstblCellHead";
                //trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Name";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Degree";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Exam passed";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Year of Passing";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Degree";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);


                tcell = new TableCell();
                tcell.Text = "Year of Passing";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);


                tcell = new TableCell();
                tcell.Text = "Regular/Part Time/Distance";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Name of Employer";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Position Held";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Salary";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "From Dt";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "To Dt";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);



                tbl_1.Rows.Add(trow);
            }
            Cnt += 1;

            trow = new TableRow();
            trow.Style.Add("padding-top", "10px");

            tcell = new TableCell();
            tcell.Text = Cnt.ToString() + ".";
            tcell.Width = Unit.Percentage(15);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            //tcell = new TableCell();
            //tcell.Text = "<input id='rdReg' name='rdReg' value='" + MyDr["fGPtbl_MFee_Id"].ToString() + "' type='radio' checked='checked' />";
            //tcell.Width = Unit.Percentage(2);
            //tcell.CssClass = "clstblCell";
            //trow.Cells.Add(tcell);

            //tcell = new TableCell();
            //tcell.Text = "<b>Payment</b>";
            //tcell.Width = Unit.Percentage(28);
            //tcell.CssClass = "clstblCell";
            //trow.Cells.Add(tcell);

            tcell = new TableCell();
            tcell.Text = "<b>" + MyDr["fInFTrack_Name"].ToString() + "</b>" ;            
            tcell.Width = Unit.Percentage(10);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tcell = new TableCell();
            tcell.Text = MyDr["DOB"].ToString();
            tcell.Width = Unit.Percentage(55);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);


            TableRow trowE;
            TableCell tcellE;

            SqlDataReader MydrEdu = NsicIntranet_FetchRecord_Education(Convert.ToInt32(MyDr["fInFTrack_1GeneratedNo"]));
            Table tbl_1E = new Table();
            while (MydrEdu.Read())
            {
               

                trowE = new TableRow();
                trowE.Style.Add("padding-top", "10px");

                tcellE = new TableCell();
                tcellE.Text = MyDr["fInFTrack_Ed_EducationType"].ToString();
                tcellE.Width = Unit.Percentage(55);
                tcellE.CssClass = "clstblCell";
                trowE.Cells.Add(tcell);


                tcellE = new TableCell();
                tcellE.Text = MyDr["fInFTrack_Ed_AttendanceType"].ToString();
                tcellE.Width = Unit.Percentage(55);
                tcellE.CssClass = "clstblCell";
                trowE.Cells.Add(tcell);


                tcellE = new TableCell();
                tcellE.Text = MyDr["fInFTrack_Ed_ExamName"].ToString();
                tcellE.Width = Unit.Percentage(55);
                tcellE.CssClass = "clstblCell";
                trowE.Cells.Add(tcell);

                tcellE = new TableCell();
                tcellE.Text = MyDr["fInFTrack_Ed_ExamSubjects"].ToString();
                tcellE.Width = Unit.Percentage(55);
                tcellE.CssClass = "clstblCell";
                trowE.Cells.Add(tcell);

                tcellE = new TableCell();
                tcellE.Text = MyDr["fInFTrack_Ed_YearPassing"].ToString();
                tcellE.Width = Unit.Percentage(55);
                tcellE.CssClass = "clstblCell";
                trowE.Cells.Add(tcell);

                tcellE = new TableCell();
                tcellE.Text = MyDr["fInFTrack_Ed_UniversityName"].ToString();
                tcellE.Width = Unit.Percentage(55);
                tcellE.CssClass = "clstblCell";
                trowE.Cells.Add(tcell);

                tcellE = new TableCell();
                tcellE.Text = MyDr["fInFTrack_Ed_Percentage"].ToString();
                tcellE.Width = Unit.Percentage(55);
                tcellE.CssClass = "clstblCell";
                trowE.Cells.Add(tcell);

                tbl_1E.Rows.Add(trowE);

            }



            tcell = new TableCell();
            tcell.Text = tbl_1E.ToString();
            tcell.Width = Unit.Percentage(55);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);





            tbl_1.Rows.Add(trow);

            TableRow trowMain = new TableRow();
            TableCell tcellMain = new TableCell();
            tcellMain.Controls.Add(tbl_1);
            trowMain.Controls.Add(tcellMain);
            Table2.Controls.Add(trowMain);
        }
        MyDr.Close();
    }

     
    public SqlDataReader NsicIntranet_FetchRecord()
    {
        //try
        //{

        string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionStringIntranet"].ToString();
        SqlConnection con = new SqlConnection(connectionstr);


       // con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionStringIntranet"].ConnectionString);

            //con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString_SPRS"].ConnectionString);

            MySqlComm = new SqlCommand("Nsic_Intranet_Record_inner", con);
        // MySqlComm.CommandType = CommandType;
        /*if (con.State == ConnectionState.Open) con.Close();*/

        if (MySqlComm.Connection.State == ConnectionState.Closed)
        {
            MySqlComm.Connection.Open();
        }

       // con.Open();


            //MySqlComm.Parameters.Add("@Turnover", SqlDbType.Float).Value = turnover;
            //MySqlComm.Parameters.Add("@categoryunit", SqlDbType.Int).Value = CategoryUnitSize;
            // MySqlComm.Parameters.Add("@Provisional", SqlDbType.Int).Value = _mprovision;
            MyDr = MySqlComm.ExecuteReader();
        //}
        //catch (Exception ex)
        //{
        //    //ErrHandler.WriteError(ex.Message);
        //}
        //finally`
        //{
        // DBManager.CloseConnection();
        //con.Close();
       MySqlComm.Dispose();
      //  }
        return MyDr;
    }


    public SqlDataReader NsicIntranet_FetchRecord_Education(int GeneratedNo)
    {
        //try
        //{
        //SqlConnection con;
        //con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionStringIntranet"].ConnectionString);

        string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionStringIntranet"].ToString();
        SqlConnection con = new SqlConnection(connectionstr);


        //if (con.State == ConnectionState.Open) con.Close();
        con.Open();


            MySqlComm = new SqlCommand("Nsic_Intranet_Record_inner_Degree", con);
            MySqlComm.CommandType = CommandType.StoredProcedure;

            //MySqlComm.Parameters.Add("@Turnover", SqlDbType.Float).Value = turnover;
             MySqlComm.Parameters.Add("@fInFTrack_1GeneratedNo", SqlDbType.Int).Value = GeneratedNo;
            // MySqlComm.Parameters.Add("@Provisional", SqlDbType.Int).Value = _mprovision;
            MyDr = MySqlComm.ExecuteReader();
        //}
        //catch (Exception ex)
        //{
        //   // ErrHandler.WriteError(ex.Message);
        //}
        //finally
        //{
        //DBManager.CloseConnection();
        //con.Close();
        MySqlComm.Dispose();
        //}
        return MyDr;
    }
}
