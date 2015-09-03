using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SqlServerDataAdapter;
using CustomStatisticalReport.Infrastructure.Configuration;

namespace CustomStatisticalReport.Service.CustomDayConsumptionReport
{
    public class DayConsumptionReportService
    {
        private static readonly ISqlServerDataFactory _dataFactory = new SqlServerDataFactory(ConnectionStringFactory.NXJCConnectionString);
        public DayConsumptionReportService()
        {
        }
        public static DataTable GetDataFromBalanceTable(string myStartDate, string myEndDate, string myOrganizationId)
        {
            string m_Sql = @"select B.OrganizationID + '_' +  B.VariableId as DictionaryKey, B.OrganizationID as OrganizationId, B.VariableId as VariableId, B.TotalPeakValleyFlatB as Value
                                from tz_Balance A, balance_energy B, system_Organization C, system_Organization D
                                where A.OrganizationID in (D.OrganizationID)
                                and A.StaticsCycle = 'day'
                                and A.TimeStamp >=@StartDate
                                and A.TimeStamp<=@EndDate
                                and A.BalanceId = B.KeyID
								and C.OrganizationID = @OrganizationId
								and D.LevelCode like C.LevelCode + '%'
								and D.LevelType = 'Factory'";
            SqlParameter[] parameter = new SqlParameter[]{
                                        new SqlParameter("OrganizationId",myOrganizationId),
                                        new SqlParameter("StartDate",myStartDate),
                                        new SqlParameter("EndDate",myEndDate)};
            DataTable BalanceDataTable = _dataFactory.Query(m_Sql, parameter);
            return BalanceDataTable;
        }
        public static DataTable GetClinkerComprehensiveData(string myStartDate, string myEndDate, string myOrganizationId)
        {
            string m_Sql = @"select B.VariableId as VariableId, sum(B.TotalPeakValleyFlatB) as Value
                                from tz_Balance A, balance_energy B, system_Organization C, system_Organization D
                                where B.OrganizationID in (D.OrganizationID)
                                and A.StaticsCycle = 'day'
                                and A.TimeStamp >=@StartDate
                                and A.TimeStamp<=@EndDate
                                and A.BalanceId = B.KeyID
								and C.OrganizationID = @OrganizationId
								and D.Type = '熟料'
								and D.LevelCode like C.LevelCode + '%'
								group by B.VariableId ";
            SqlParameter[] parameter = new SqlParameter[]{
                                        new SqlParameter("OrganizationId",myOrganizationId),
                                        new SqlParameter("StartDate",myStartDate),
                                        new SqlParameter("EndDate",myEndDate)};
            DataTable ComprehensiveDataTable = _dataFactory.Query(m_Sql, parameter);
            return ComprehensiveDataTable;
        }
        public static DataTable GetCementComprehensiveData(string myStartDate, string myEndDate, string myOrganizationId)
        {
            string m_Sql = @"select B.VariableId as VariableId, sum(B.TotalPeakValleyFlatB) as Value
                                from tz_Balance A, balance_energy B, system_Organization C, system_Organization D
                                where B.OrganizationID in (D.OrganizationID)
                                and A.StaticsCycle = 'day'
                                and A.TimeStamp >=@StartDate
                                and A.TimeStamp<=@EndDate
                                and A.BalanceId = B.KeyID
								and C.OrganizationID = @OrganizationId
								and D.Type = '水泥磨'
								and D.LevelCode like C.LevelCode + '%'
								group by B.VariableId ";
            SqlParameter[] parameter = new SqlParameter[]{
                                        new SqlParameter("OrganizationId",myOrganizationId),
                                        new SqlParameter("StartDate",myStartDate),
                                        new SqlParameter("EndDate",myEndDate)};
            DataTable ComprehensiveDataTable = _dataFactory.Query(m_Sql, parameter);
            return ComprehensiveDataTable;
        }
        public static Standard_GB16780_2012.Parameters_ComprehensiveData SetComprehensiveDataParameters(string organizationId, string datetime)
        {
            Standard_GB16780_2012.Parameters_ComprehensiveData m_Parameters_ComprehensiveData = AutoSetParameters.AutoSetParameters_V1.SetComprehensiveParametersFromSql("day",
                       datetime, datetime, organizationId, _dataFactory);
            return m_Parameters_ComprehensiveData;
        }
        public static void ExportExcelFile(string myFileType, string myFileName, string myData)
        {
            if (myFileType == "xls")
            {
                UpDownLoadFiles.DownloadFile.ExportExcelFile(myFileName, myData);
            }
        }
    }
}


//select A.OrganizationID, B.VariableId, B.LevelCode, B.Denominator from tz_formula A, formula_formulaDetail B, system_Organization C, system_Organization D
//where A.OrganizationID in (D.OrganizationID) 
//and A.Enable = 1
//and A.State =0
//and A.KeyID = B.KeyID
//and len(B.LevelCode) <=7
//and C.OrganizationID = 'zc_nxjc_byc_byf'
//and D.LevelCode like C.LevelCode + '%'
//order by A.OrganizationID, B.LevelCode";