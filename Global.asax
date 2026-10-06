<%@ Application Language="C#" %>
<%@ Import Namespace="System.Timers" %>
<%@ Import Namespace="System.Threading" %>


<script runat="server">

    static int InspDtobeIssue=1, MonLimToSet=1, MonLimToApprove=1, RegRen21=1, RegRen14=1, RegRen7=1, RegRen1=1;

    void Application_Start(object sender, EventArgs e)
    {
        //SchedulerThread();

        /*string[] myCookies = HttpContext.Current.Request.Cookies.AllKeys;
        foreach (string cookie in myCookies)
        {
            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        }*/

    }


    protected void SchedulerThread()
    {
        ThreadStart startTask = new ThreadStart(SchedulerLoop);
        Thread thdSche = new Thread(startTask);
        thdSche.Start();
    }
    internal static void SchedulerLoop()
    {
        clsScheduleMail objScheduler = new clsScheduleMail();
        while (true)
        {
            string Currenttime = System.DateTime.Now.TimeOfDay.ToString();
            string HourPart = Currenttime.Substring(0, 5);
            HourPart = HourPart.Replace(":", ".");
            double HourMinute = Convert.ToDouble(HourPart.ToString());
            if (HourMinute >= 01.00 && HourMinute <= 01.10)
            //if (HourMinute >= 16.10 && HourMinute <= 16.13)
            {
                if (InspDtobeIssue == 1)
                {
                    objScheduler.Mail_InspectionDateTobeIssued();
                    FlagChange(0, 1, 1, 1, 1, 1, 1);
                }
            }
            else if (HourMinute >= 01.15 && HourMinute <= 01.25)
            //else if (HourMinute >= 16.16 && HourMinute <= 16.19)
            {
                if (MonLimToSet == 1)
                {
                    objScheduler.Mail_MonetaryLimitTobeSet();
                    FlagChange(1, 0, 1, 1, 1, 1, 1);
                }
            }
            else if (HourMinute >= 01.30 && HourMinute <= 01.25)
            //else if (HourMinute >= 16.22 && HourMinute <= 16.25)
            {
                if (MonLimToApprove == 1)
                {
                    objScheduler.Mail_MonetaryLimitTobeApproved();
                    FlagChange(1, 1, 0, 1, 1, 1, 1);
                }
            }
            else if (HourMinute >= 01.30 && HourMinute <= 01.45)
            //else if (HourMinute >= 11.13 && HourMinute <= 11.18)
            {
                if (RegRen21 == 1)
                {
                    //1 FOR REGISTERED 4 FOR EXPIRED
                    objScheduler.Mail_RenewalExpiredDateRange(-21, 1);
                    objScheduler.Mail_RenewalExpiredDateRange(-21, 4);
                    objScheduler.Mail_RenewalExpiredDateRange_ForBranch(-21, 1);
                    objScheduler.Mail_RenewalExpiredDateRange_ForBranch(-21, 4);
                    FlagChange(1, 1, 1, 0, 1, 1, 1);
                }
            }
            else if (HourMinute >= 01.50 && HourMinute <= 02.00)
            //else if (HourMinute >= 11.20 && HourMinute <= 11.25)
            {
                if (RegRen14 == 1)
                {
                    objScheduler.Mail_RenewalExpiredDateRange(-14, 1);
                    objScheduler.Mail_RenewalExpiredDateRange(-14, 4);
                    objScheduler.Mail_RenewalExpiredDateRange_ForBranch(-14, 1);
                    objScheduler.Mail_RenewalExpiredDateRange_ForBranch(-14, 4);
                    FlagChange(1, 1, 1, 1, 0, 1, 1);
                }
            }
            else if (HourMinute >= 02.05 && HourMinute <= 02.15)
            //else if (HourMinute >= 12.08 && HourMinute <= 12.12)
            {
                if (RegRen7 == 1)
                {
                    objScheduler.Mail_RenewalExpiredDateRange(-7, 1);
                    objScheduler.Mail_RenewalExpiredDateRange(-7, 4);
                    objScheduler.Mail_RenewalExpiredDateRange_ForBranch(-7, 1);
                    objScheduler.Mail_RenewalExpiredDateRange_ForBranch(-7, 4);
                    FlagChange(1, 1, 1, 1, 1, 0, 1);
                }
            }
            else if (HourMinute >= 02.20 && HourMinute <= 02.30)
            //else if (HourMinute >= 11.34 && HourMinute <= 11.39)
            {
                if (RegRen1 == 1)
                {
                    objScheduler.Mail_RenewalExpiredDateRange(-1, 1);
                    objScheduler.Mail_RenewalExpiredDateRange(-1, 4);
                    objScheduler.Mail_RenewalExpiredDateRange_ForBranch(-1, 1);
                    objScheduler.Mail_RenewalExpiredDateRange_ForBranch(-1, 4);
                    FlagChange(1, 1, 1, 1, 1, 1, 0);
                }
            }


            //objScheduler.Mail_insert();
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(7));
        }//while
    }

    internal static void FlagChange(int InspDtoIssue, int MonTobeSet, int MonToApprove,
        int Reg21, int Reg14, int Reg7, int Reg1)
    {
        try
        {
            InspDtobeIssue=InspDtoIssue;
            MonLimToSet=MonTobeSet;
            MonLimToApprove=MonToApprove;
            RegRen21=Reg21;
            RegRen14=Reg14;
            RegRen7 = Reg7;
            RegRen1 =Reg1;
        }
        catch{}
    }




    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        Exception err = Server.GetLastError();
        // string ssss= Server.GetLastError().ToString();

        // Get the error details
        HttpException lastErrorWrapper =
            Server.GetLastError() as HttpException;

        //    Exception lastError = lastErrorWrapper;
        //if (lastErrorWrapper.InnerException != null)
        //    lastError = lastErrorWrapper.InnerException;

        //string lastErrorTypeName = lastError.GetType().ToString();
        //string lastErrorMessage = lastError.Message;
        //string lastErrorStackTrace = lastError.StackTrace;



    }

    void  Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

        string sessionId = Session.SessionID;
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        //NSICGP_VBClassLibrary.ErrHandler.WriteError("void Session_End(object sender, EventArgs e)");

    }

</script>
