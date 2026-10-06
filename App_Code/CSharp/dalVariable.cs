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

/// <summary>
/// Summary description for dalVariable
/// </summary>
public class dalVariable
{
    private int _PrimaryId;
    private string _Name;
    private string _Desc;
    private int _BranchId;
    private string _BranchName;
    private string _BranchDescription;
    private int _AccessId;
    private int _StatusId;
    private int _CreateUserId;
    private int _EditUserId;
    private int _DeleteUserId;
    private string _CreateUserName;
    private string _EditUserName;
    private string _DeleteUserName;
    private string _CreateDate;
    private string _EditDate;
    private string _DeleteDate;
    private int _Delete;
    private int _Display;
    private int _ApplicationId;
    private string _ApplicationPageName;
    private int _ApplicationLevelId;
    private int _ApplicationMasterId;
    private string _ApplicationPath;
    private byte _ApplicationLink;
    private byte _ApplicationLinkType;
    private int _CityId;
    private string _CityName;
    private string _CityDescription;
    private int _ContentId;
    private string _ContentDescription;
    private int _CoverLetterId;
    private string _CoverLetterName;
    private string _CoverLetterTo;
    private string _CoverLetterCC;
    private string _CoverLetterBCC;
    private string _CoverLetterFrom;
    private string _CoverLetterSubject;
    private string _CoverLetterBody;
    private byte _CoverLetterActive;
    private int _DepartmentId;
    private string _DepartmentName;
    private string _DepartmentDescription;
    private int _FeesId;
    private float _FeesAmount;
    private string _FeesDescription;
    private int _FormTypeId;
    private string _FormTypeName;
    private int _InspDayLimitId;
    private int _InspDayLimitDateIssue;
    private int _InspDayLimitReportStart;
    private int _InspDayLimitReportSubmit;
    private int _InspectionAgencyId;
    private string _InspectionAgencyName;
    private int _InstructionId;
    private string _InstructionContent;
    private int _ListId;
    private string _ListName;
    private string _ListContent;
    private int _MailerId;
    private string _MailerName;
    private string _MailerTo;
    private string _MailerCC;
    private string _MailerBCC;
    private string _MailerFrom;
    private string _MailerSubject;
    private string _MailerBody;
    private string _MailerAttachment;
    private byte _MailerActive;
    private int _RegDayLimitId;
    private int _RegDayLimitDays;
    private int _RoleId;
    private string _RoleName;
    private string _RoleDescription;
    private int _StateId;
    private string _StateName;
    private string _StateDescription;
    private int _UserId;
    private string _UserName;
    private string _UserPassword;
    private int _UserDepartmentId;
    private string _UserFName;
    private string _UserMName;
    private string _UserLName;
    private string _UserDOB;
    private string _UserEmail;
    private string _UserPhone;
    private string _UserMobile;
    private string _UserFax;
    private int _ZoneId;
    private string _ZoneName;
    private string _ZoneDescription;
    private int _SentMailerId;
    private string _SentMailerTo;
    private int _SentMailerToUserId;
    private int _SentMailerToUserTable;
    private string _SentMailerCC;
    private string _SentMailerBCC;
    private string _SentMailerFrom;
    private int _SentMailerFromUserId;
    private int _SentMailerFromUserTable;
    private string _SentMailerSubject;
    private string _SentMailerBody;
    private string _SentMailerAttachment;
    private string _SentMailerLink;
    private int _SentSMSId;
    private string _SentSMSTo;
    private int _SentSMSToUserId;
    private int _SentSMSToUserTable;
    private string _SentSMSFrom;
    private int _SentSMSFromUserId;
    private int _SentSMSFromUserTable;
    private string _SentSMSContent;
    private int _TransInspectionAgencyBranchId;
    private float _TransInspectionAgencyBranchFeeRegistration;
    private float _TransInspectionAgencyBranchFeeRenewal;
    private float _TransInspectionAgencyBranchFeeAmendment;
    private float _TransInspectionAgencyBranchFeeCC;
    private float _TransInspectionAgencyBranchFeeCV;
    private int _TransRoleApplicationId;
    private byte _TransRoleApplicationARView;
    private byte _TransRoleApplicationARInsert;
    private byte _TransRoleApplicationARUpdate;
    private byte _TransRoleApplicationARDelete;
    private byte _TransRoleApplicationARActive;
    private int _TransUserBranchId;
    
    public string Desc {
        get { return _Desc; }
        set { _Desc = value; }
    }

    public string Name {
        get { return _Name; }
        set { _Name = value; }
    }

    public int PrimaryId {
        get { return _PrimaryId; }
        set { _PrimaryId = value; }
    }

    #region CommonFields

    public int AccessId
    {
        get { return _AccessId; }
        set { _AccessId = value; }
    }

    public int StatusId
    {
        get { return _StatusId; }
        set { _StatusId = value; }
    }

    public int CreateUserId
    {
        get { return _CreateUserId; }
        set { _CreateUserId = value; }
    }

    public int EditUserId
    {
        get { return _EditUserId; }
        set { _EditUserId = value; }
    }

    public int DeleteUserId
    {
        get { return _DeleteUserId; }
        set { _DeleteUserId = value; }
    }

    public string CreateUserName
    {
        get { return _CreateUserName; }
        set { _CreateUserName = value; }
    }

    public string EditUserName
    {
        get { return _EditUserName; }
        set { _EditUserName = value; }
    }

    public string DeleteUserName
    {
        get { return _DeleteUserName; }
        set { _DeleteUserName = value; }
    }

    public string CreateDate
    {
        get { return _CreateDate; }
        set { _CreateDate = value; }
    }

    public string EditDate
    {
        get { return _EditDate; }
        set { _EditDate = value; }
    }

    public string DeleteDate
    {
        get { return _DeleteDate; }
        set { _DeleteDate = value; }
    }

    public int Delete
    {
        get { return _Delete; }
        set { _Delete = value; }
    }

    public int Display
    {
        get { return _Display; }
        set { _Display = value; }
    }

    #endregion

    #region MasterApplication

    public int ApplicationId
    {
        get { return _ApplicationId; }
        set { _ApplicationId = value; }
    }

    public string ApplicationPageName
    {
        get { return _ApplicationPageName; }
        set { _ApplicationPageName = value; }
    }

    public int ApplicationLevelId
    {
        get { return _ApplicationLevelId; }
        set { _ApplicationLevelId = value; }
    }

    public int ApplicationMasterId
    {
        get { return _ApplicationMasterId; }
        set { _ApplicationMasterId = value; }
    }

    public string ApplicationPath
    {
        get { return _ApplicationPath; }
        set { _ApplicationPath = value; }
    }

    public byte ApplicationLink
    {
        get { return _ApplicationLink; }
        set { _ApplicationLink = value; }
    }
    public byte ApplicationLinkType
    {
        get { return _ApplicationLinkType; }
        set { _ApplicationLinkType = value; }
    }

#endregion

    #region MasterBranch

    public string BranchName
    {
        get { return _BranchName; }
        set { _BranchName = value; }
    }

    public int BranchId
    {
        get { return _BranchId; }
        set { _BranchId = value; }
    }

    public string BranchDescription
    {
        get { return _BranchDescription; }
        set { _BranchDescription = value; }
    }


    #endregion

    #region MasterCity

    public int CityId
    {
        get { return _CityId; }
        set { _CityId = value; }
    }

    public string CityName
    {
        get { return _CityName; }
        set { _CityName = value; }
    }

    public string CityDesc
    {
        get { return _CityDescription; }
        set { _CityDescription = value; }
    }

    #endregion

    #region MasterContent

    public int ContentId
    {
        get { return _ContentId; }
        set { _ContentId = value; }
    }

    public string ContentDescription
    {
        get { return _ContentDescription; }
        set { _ContentDescription = value; }
    }

    #endregion

    #region MasterCoverLetter

    public int CoverLetterId
    {
        get { return _CoverLetterId; }
        set { _CoverLetterId = value; }
    }

    public string CoverLetterName
    {
        get { return _CoverLetterName; }
        set { _CoverLetterName = value; }
    }

    public string CoverLetterTo
    {
        get { return _CoverLetterTo; }
        set { _CoverLetterTo = value; }
    }

    public string CoverLetterCC
    {
        get { return _CoverLetterCC; }
        set { _CoverLetterCC = value; }
    }

    public string CoverLetterBCC
    {
        get { return _CoverLetterBCC; }
        set { _CoverLetterBCC = value; }
    }

    public string CoverLetterFrom
    {
        get { return _CoverLetterFrom; }
        set { _CoverLetterFrom = value; }
    }

    public string CoverLetterSubject
    {
        get { return _CoverLetterSubject; }
        set { _CoverLetterSubject = value; }
    }

    public string CoverLetterBody
    {
        get { return _CoverLetterBody; }
        set { _CoverLetterBody = value; }
    }

    public byte CoverLetterActive
    {
        get { return _CoverLetterActive; }
        set { _CoverLetterActive = value; }
    }

    #endregion

    #region MasterDepartment

    public int DepartmentId
    {
        get { return _DepartmentId; }
        set { _DepartmentId = value; }
    }

    public string DepartmentName
    {
        get { return _DepartmentName; }
        set { _DepartmentName = value; }
    }

    public string DepartmentDescription
    {
        get { return _DepartmentDescription; }
        set { _DepartmentDescription = value; }
    }

    #endregion

    #region MasterFees

    public int FeesId
    {
        get { return _FeesId; }
        set { _FeesId = value; }
    }

    public float FeesAmount
    {
        get { return _FeesAmount; }
        set { _FeesAmount = value; }
    }

    public string FeesDescription
    {
        get { return _FeesDescription; }
        set { _FeesDescription = value; }
    }

    #endregion

    #region MasterFormType

    public int FormTypeId
    {
        get { return _FormTypeId; }
        set { _FormTypeId = value; }
    }

    public string FormTypeName
    {
        get { return _FormTypeName; }
        set { _FormTypeName = value; }
    }

    #endregion

    #region MasterInspDayLimit

    public int InspDayLimitId
    {
        get { return _InspDayLimitId; }
        set { _InspDayLimitId = value; }
    }

    public int InspDayLimitDateIssue
    {
        get { return _InspDayLimitDateIssue; }
        set { _InspDayLimitDateIssue = value; }
    }

    public int InspDayLimitReportStart
    {
        get { return _InspDayLimitReportStart; }
        set { _InspDayLimitReportStart = value; }
    }

    public int InspDayLimitReportSubmit
    {
        get { return _InspDayLimitReportSubmit; }
        set { _InspDayLimitReportSubmit = value; }
    }

    #endregion

    #region MasterInspectionAgency

    public int InspectionAgencId
    {
        get { return _InspectionAgencyId; }
        set { _InspectionAgencyId = value; }
    }

    public string InspectionAgencyName
    {
        get { return _InspectionAgencyName; }
        set { _InspectionAgencyName = value; }
    }


    #endregion

    #region MasterInstruction

    public int InstructionId
    {
        get { return _InstructionId; }
        set { _InstructionId = value; }
    }

    public string InstructionContent
    {
        get { return _InstructionContent; }
        set { _InstructionContent = value; }
    }

    #endregion

    #region MasterList

    public int ListId
    {
        get { return _ListId; }
        set { _ListId = value; }
    }

    public string ListName
    {
        get { return _ListName; }
        set { _ListName = value; }
    }

    public string ListContent
    {
        get { return _ListContent; }
        set { _ListContent = value; }
    }

#endregion

    #region MasterMailer

    public int MailerId
    {
        get { return _MailerId; }
        set { _MailerId = value; }
    }

    public string MailerName
    {
        get { return _MailerName; }
        set { _MailerName = value; }
    }

    public string MailerTo
    {
        get { return _MailerTo; }
        set { _MailerTo = value; }
    }

    public string MailerCC
    {
        get { return _MailerCC; }
        set { _MailerCC = value; }
    }

    public string MailerBCC
    {
        get { return _MailerBCC; }
        set { _MailerBCC = value; }
    }

    public string MailerFrom
    {
        get { return _MailerFrom; }
        set { _MailerFrom = value; }
    }

    public string MailerSubject
    {
        get { return _MailerSubject; }
        set { _MailerSubject = value; }
    }

    public string MailerBody
    {
        get { return _MailerBody; }
        set { _MailerBody = value; }
    }

    public string MailerAttachment
    {
        get { return _MailerAttachment; }
        set { _MailerAttachment = value; }
    }

    public byte MailerActive
    {
        get { return _MailerActive; }
        set { _MailerActive = value; }
    }

    #endregion

    #region MasterRegDayLimit

    public int RegDayLimitId
    {
        get { return _RegDayLimitId; }
        set { _RegDayLimitId = value; }
    }

    public int RegDayLimitDays
    {
        get { return _RegDayLimitDays; }
        set { _RegDayLimitDays = value; }
    }

    #endregion

    #region MasterRole

    public int RoleId
    {
        get { return _RoleId; }
        set { _RoleId = value; }
    }

    public string RoleName
    {
        get { return _RoleName; }
        set { _RoleName = value; }
    }

    public string RoleDescription
    {
        get { return _RoleDescription; }
        set { _RoleDescription = value; }
    }

    #endregion

    #region MasterState

    public int StateId
    {
        get { return _StateId; }
        set { _StateId = value; }
    }

    public string StateName
    {
        get { return _StateName; }
        set { _StateName = value; }
    }

    public string StateDescription
    {
        get { return _StateDescription; }
        set { _StateDescription = value; }
    }

    #endregion

    #region MasterUser

    public int UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }
    
    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }

    public string UserPassword
    {
        get { return _UserPassword; }
        set { _UserPassword = value; }
    }
    
    public int UserDepartmentId
    {
        get { return _UserDepartmentId; }
        set { _UserDepartmentId = value; }
    }

    public string UserFName
    {
        get { return _UserFName; }
        set { _UserFName = value; }
    }

    public string UserMName
    {
        get { return _UserMName; }
        set { _UserMName = value; }
    }

    public string UserLName
    {
        get { return _UserLName; }
        set { _UserLName = value; }
    }

    public string UserDateOfBirth
    {
        get { return _UserDOB; }
        set { _UserDOB = value; }
    }

    public string UserEmail
    {
        get { return _UserEmail; }
        set { _UserEmail = value; }
    }

    public string UserPhone
    {
        get { return _UserPhone; }
        set { _UserPhone = value; }
    }

    public string UserMobile
    {
        get { return _UserMobile; }
        set { _UserMobile = value; }
    }

    public string UserFax
    {
        get { return _UserMobile; }
        set { _UserMobile = value; }
    }

    #endregion

    #region MasterZone

    public int ZoneId
    {
        get { return _ZoneId; }
        set { _ZoneId = value; }
    }

    public string ZoneName
    {
        get { return _ZoneName; }
        set { _ZoneName = value; }
    }

    public string ZoneDescription
    {
        get { return _ZoneDescription; }
        set { _ZoneDescription = value; }
    }

    #endregion

    #region SentMailer

    public int SentMailerId
    {
        get { return _SentMailerId; }
        set { _SentMailerId = value; }
    }

    public string SentMailerTo
    {
        get { return _SentMailerTo; }
        set { _SentMailerTo = value; }
    }

    public int SentMailerToUserId
    {
        get { return _SentMailerToUserId; }
        set { _SentMailerToUserId = value; }
    }

    public int SentMailerTouserTable
    {
        get { return _SentMailerToUserTable; }
        set { _SentMailerToUserTable = value; }
    }

    public string SentMailerCC
    {
        get { return _SentMailerCC; }
        set { _SentMailerCC = value; }
    }

    public string SentMailerBCC
    {
        get { return _SentMailerBCC; }
        set { _SentMailerBCC = value; }
    }

    public string SentMailerFrom
    {
        get { return _SentMailerFrom; }
        set { _SentMailerFrom = value; }
    }

    public int SentMailerFromUserId
    {
        get { return _SentMailerFromUserId; }
        set { _SentMailerFromUserId = value; }
    }

    public int SentMailerFromUserTable
    {
        get { return _SentMailerFromUserTable; }
        set { _SentMailerFromUserTable = value; }
    }

    public string SentMailerSubject
    {
        get { return _SentMailerSubject; }
        set { _SentMailerSubject = value; }
    }

    public string SentMailerBody
    {
        get { return _SentMailerBody; }
        set { _SentMailerBody = value; }
    }

    public string SentMailerAttachment
    {
        get { return _SentMailerAttachment; }
        set { _SentMailerAttachment = value; }
    }

    public string SentMailerLink
    {
        get { return _SentMailerLink; }
        set { _SentMailerLink = value; }
    }

    #endregion

    #region SentSMS

    public int SentSMSId
    {
        get { return _SentSMSId; }
        set { _SentSMSId = value; }
    }

    public string SentSMSTo
    {
        get { return _SentSMSTo; }
        set { _SentSMSTo = value; }
    }

    public int SentSMSToUserId
    {
        get { return _SentSMSToUserId; }
        set { _SentSMSToUserId = value; }
    }

    public int SentSMSToUserTable
    {
        get { return _SentSMSToUserTable; }
        set { _SentSMSToUserTable = value; }
    }

    public string SentSMSFrom
    {
        get { return _SentSMSFrom; }
        set { _SentSMSFrom = value; }
    }

    public int SentSMSFromUserId
    {
        get { return _SentSMSFromUserId; }
        set { _SentSMSFromUserId = value; }
    }

    public int SentSMSFromUserTable
    {
        get { return _SentSMSFromUserTable; }
        set { _SentSMSFromUserTable = value; }
    }

    public string SentSMSContent
    {
        get { return _SentSMSContent; }
        set { _SentSMSContent = value; }
    }

    #endregion

    #region TransInspectionAgencyBranch

    public int TransInspectionAgencyBranchId
    {
        get { return _TransInspectionAgencyBranchId; }
        set { _TransInspectionAgencyBranchId = value; }
    }

    public float TransInspectionAgencyBranchFeeRegistration
    {
        get { return _TransInspectionAgencyBranchFeeRegistration; }
        set { _TransInspectionAgencyBranchFeeRegistration = value; }
    }

    public float TransInspectionAgencyBranchFeeRenewal
    {
        get { return _TransInspectionAgencyBranchFeeRenewal; }
        set { _TransInspectionAgencyBranchFeeRenewal = value; }
    }

    public float TransInspectionAgencyBranchFeeAmendment
    {
        get { return _TransInspectionAgencyBranchFeeAmendment; }
        set { _TransInspectionAgencyBranchFeeAmendment = value; }
    }

    public float TransInspectionAgencyBranchFeeCC
    {
        get { return _TransInspectionAgencyBranchFeeCC; }
        set { _TransInspectionAgencyBranchFeeCC = value; }
    }

    public float TransInspectionAgencyBranchFeeCV
    {
        get { return _TransInspectionAgencyBranchFeeCV; }
        set { _TransInspectionAgencyBranchFeeCV = value; }
    }

    #endregion

    #region TransRoleApplication

    public int TransRoleApplicationId
    {
        get { return _TransRoleApplicationId; }
        set { _TransRoleApplicationId = value; }
    }

    public byte TransRoleApplicationARView
    {
        get { return _TransRoleApplicationARView; }
        set { _TransRoleApplicationARView = value; }
    }

    public byte TransRoleApplicationARIsert
    {
        get { return _TransRoleApplicationARInsert; }
        set { _TransRoleApplicationARInsert = value; }
    }

    public byte TransRoleApplicationARUpdate
    {
        get { return _TransRoleApplicationARUpdate; }
        set { _TransRoleApplicationARUpdate = value; }
    }

    public byte TransRoleApplicationARDelete
    {
        get { return _TransRoleApplicationARDelete; }
        set { _TransRoleApplicationARDelete = value; }
    }

    public byte TransRoleApplicationARActive
    {
        get { return _TransRoleApplicationARActive; }
        set { _TransRoleApplicationARActive = value; }
    }

    #endregion

    public int TransUserBranchId
    {
        get { return _TransUserBranchId; }
        set { _TransUserBranchId = value; }
    }


    public dalVariable()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
