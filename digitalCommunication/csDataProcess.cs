using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

    public class csDataProcess
    {
        public SqlConnection getConnection()
        {
            string connStr = @"data source=192.168.1.251;database=DBL_Group;uid=Prod;password=DbLPr0dDB";
            //string connStr = @"Trusted_Connection=yes; server=(local);database=DBL_Group;";
            //Conn = "PROVIDER=MSDASQL;driver={SQL Server};server=HRDS;database=DBL_GROUP_NEW;"
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }
        public OracleConnection getOrclConnection()
        {
           // string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.20.2.13)(PORT=1541)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=PROD)));User Id=appsro;Password=roapps321;";
            string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.150.351)(PORT=1531)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=EBS_PROD)));User Id=appsro;Password=roapps321;";
            //string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.20.1.11)(PORT=1531)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=PROD)));User Id=appsro;Password=roapps321;";
            // string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.20.47)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=hris;Password=hris;";
            // string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.20.1.11)(PORT=1531)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=PROD)(INSTANCE_NAME=PRODCDB)));User Id=appsro;Password=roapps321;";
            OracleConnection connOrcl = new OracleConnection(connString);
            return connOrcl;
        }
        public int executeSQL(string strSQl)
        {
            SqlConnection dbCon = null;
            int rcdCnt = 0;
            try
            {
                dbCon = getConnection();
                dbCon.Open();
                SqlCommand sqlCmd = new SqlCommand(strSQl, dbCon);
                rcdCnt = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCon.Close();
            }
            return rcdCnt;
        }

        public decimal executeMaxSQL(string strSQl)
        {
            SqlConnection dbCon = null;
            decimal rcdCnt = 0;
            try
            {
                dbCon = getConnection();
                dbCon.Open();
                SqlCommand sqlCmd = new SqlCommand(strSQl, dbCon);
                rcdCnt = Convert.ToDecimal(sqlCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCon.Close();
            }
            return rcdCnt;
        }

        public DataTable getDataTable(string str)
        {
            SqlConnection dbCon = null;
            DataTable dt = new DataTable();
            try
            {
                dbCon = getConnection();
                dbCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(str, dbCon);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCon.Close();
            }
            return dt;
        }
        public DataTable getDataTableOrcl(string str)
        {
            OracleConnection dbCon = null;
            DataTable dt = new DataTable();
            try
            {
                dbCon = getOrclConnection();
                dbCon.Open();
                OracleDataAdapter da = new OracleDataAdapter(str, dbCon);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCon.Close();
            }
            return dt;
        }

    }

