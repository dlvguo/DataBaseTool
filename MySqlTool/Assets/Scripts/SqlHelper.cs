using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;

public class SqlHelper {
    private static SqlHelper _instance = new SqlHelper();
    private static string connStr="Database=Lwcc;Data Source = 172.22.224.87; User Id = root; Password=123456;port=3306";
    private string ip;
    private string acc;
    private string psd;
    private string db;
    public static SqlHelper Insatance { get
        {
            return _instance;
        } }

    /// <summary>
    /// 获取连接
    /// </summary>
    /// <returns></returns>
    private MySqlConnection GetSqlConn()
    {
        MySqlConnection sqlConn = null;
        sqlConn = new MySqlConnection(connStr);
        return sqlConn;
    }
	
    /// <summary>
    /// 获取所有表名
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetTables()
    {
        string sql = string.Format(@"select table_name from information_schema.tables where table_schema='{0}' and table_type='base table'", db);
        //string sql = @"select table_name from information_schema.tables where table_schema='lwcc' and table_type='base table'";
        List<string> strs = new List<string>();
        var conn = GetSqlConn();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            strs.Add(reader[0].ToString());
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
    public void SetConnStr(string ip,string acc,string psd,string db)
    {
        this.ip = ip;
        this.acc = acc;
        this.psd = psd;
        this.db = db;
        connStr = string.Format("DataBase={0};Data Source= {1};User Id = {2};Password={3};port=3306;", db, ip, acc, psd);
    }

    public void ClearTables(IEnumerable<string> tbs)
    {
        string cmdText = "";
        foreach(var item in tbs)
        {
            cmdText += string.Format("truncate table {0};", item);
        }
        var conn = GetSqlConn();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(cmdText,conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

}
