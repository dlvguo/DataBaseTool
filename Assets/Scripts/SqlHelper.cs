using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using System.Data.Sql;
using System.Data.SqlClient;

//Mysql使用这个
//using MySql.Data.MySqlClient; 并且把所有Sql换成MySql
using System;

public class SqlHelper
{
    private static SqlHelper _instance = new SqlHelper();
    //Mysql的连接方法
    //private static string connStr="Database=Lwcc;Data Source = 172.22.224.87; User Id = root; Password=123456;port=3306";
    //SqlServer方法
    private static string connStr = "server=106.15.121.111;database=GameDB;uid=sa;pwd=a2241939438...";
    private string ip;
    private string acc;
    private string psd;
    private string db;

    //用于切割用的Sql分隔符
    private const string _spiltStr = "/-/";
    public string SpiltStr
    {
        get
        {
            return _spiltStr;
        }
    }


    public static SqlHelper Insatance
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>
    /// 获取连接
    /// </summary>
    /// <returns></returns>
    private SqlConnection GetSqlConn()
    {
        SqlConnection sqlConn = null;
        sqlConn = new SqlConnection(connStr);
        return sqlConn;
    }

    /// <summary>
    /// 获取所有表名
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetTables(string sql)
    {
        //string sql = string.Format(@"Select Name FROM SysObjects Where XType='U' orDER BY Name;", db);
        //string sql = @"select table_name from information_schema.tables where table_schema='lwcc' and table_type='base table'";
        List<string> strs = new List<string>();
        var conn = GetSqlConn();
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string str = "";
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (i != 0)
                    str += SpiltStr;
                str += reader[i].ToString();
            }
            strs.Add(str);
        }
        conn.Close();
        return strs;
    }



    /// <summary>
    /// 设置连接Str
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="acc"></param>
    /// <param name="psd"></param>
    /// <param name="db"></param>
    public void SetConnStr(string ip, string acc, string psd, string db)
    {
        this.ip = ip;
        this.acc = acc;
        this.psd = psd;
        this.db = db;
        //server = 106.15.121.111; database = GameDB; uid = sa; pwd = a2241939438...
        //connStr = string.Format("DataBase={0};Data Source= {1};User Id = {2};Password={3};port=3306;", db, ip, acc, psd);
        connStr = string.Format("server={0};database= {1};uid = {2};pwd={3};", ip, db, acc, psd);
    }

    public void ClearTables(IEnumerable<string> tbs)
    {
        string cmdText = "";
        foreach (var item in tbs)
        {
            cmdText += string.Format("truncate table {0};", item);
        }
        var conn = GetSqlConn();
        conn.Open();
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    /// <summary>
    /// 测试连接
    /// </summary>
    /// <returns></returns>
    public string TestConn()
    {
        var conn = GetSqlConn();
        try
        {
            conn.Open();
            conn.Close();
            return "数据库连接成功";
        }

        catch
        {
            throw;
        }
    }


    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="values"></param>
    public void Insert(string tableName, string values, string vars = "")
    {
        string sql = string.Format("INSERT INTO {0} {1} VALUES ({2});", tableName, vars, values);
        var conn = GetSqlConn();
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    //更新
    public void Update(string table, string setstr, string limit)
    {
        //UPDATE Person SET FirstName = 'Fred' WHERE LastName = 'Wilson' 
        string sql = string.Format("UPDATE {0} SET {1} WHERE {2};", table, setstr, limit);
        var conn = GetSqlConn();
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void SetDB(string db)
    {
        SetConnStr(ip, acc, psd, db);
    }
}
