using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoonBtn : MonoBehaviour
{
    private Button conBtn;
    private InputField ip;
    private InputField acc;
    private InputField psd;
    private InputField db;
    private Transform tablesGrid;
    private string _ip;
    private string _acc;
    private string _psd;
    private string _db;
    public GameObject Table;
    private Dictionary<string, Table> dicTables;
    // Use this for initialization
    void Start()
    {
        conBtn = this.GetComponent<Button>();
        ip = GameObject.Find("Ip").GetComponent<InputField>();
        ip.onEndEdit.AddListener((value) =>
        {
            OnValueChanged(0, value);
        });
        acc = GameObject.Find("Acc").GetComponent<InputField>();
        acc.onEndEdit.AddListener((value) =>
        {
            OnValueChanged(1, value);
        });
        psd = GameObject.Find("Psd").GetComponent<InputField>();
        psd.onEndEdit.AddListener((value) =>
        {
            OnValueChanged(2, value);
        });
        db = GameObject.Find("Db").GetComponent<InputField>();
        db.onEndEdit.AddListener((value) =>
        {
            OnValueChanged(3, value);
        });
        tablesGrid = GameObject.Find("TablesGrid").GetComponent<Transform>();
        dicTables = new Dictionary<string, Table>();
        conBtn.onClick.AddListener(OnConn);
        _ip = ip.text;
        _acc = acc.text;
        _psd = psd.text;
        _db = db.text;
        SqlHelper.Insatance.SetConnStr(ip.text, acc.text, psd.text, db.text);

    }
    private void OnConn()
    {
        try
        {
            if (dicTables.Count > 0)
                ClearTb();
            ClearTb();
            var strs = SqlHelper.Insatance.GetTables();
            CreateTables(strs);
        }
        catch (Exception e)
        {
            Tips.Instance.OnException(e);
        }
    }

    private void CreateTables(IEnumerable<string> strs)
    {
        foreach (var item in strs)
        {
            GameObject table = Instantiate(Table, tablesGrid) as GameObject;
            Text text = table.GetComponentInChildren<Text>();
            Table tb = table.GetComponent<Table>();
            dicTables.Add(item, tb);
            tb.TableName = item;
            tb.IsDel = false;
            text.text = item;
        }
        DelTablesMan.Instance.tbs = strs;
    }

    private void ClearTb()
    {
        foreach (var item in dicTables.Values)
        {
            Destroy(item.gameObject);
        }
        dicTables.Clear();
    }

    private void OnValueChanged(int type, string value)
    {
        switch (type)
        {
            case 0:
                _ip = value;
                break;
            case 1:
                _acc = value;
                break;
            case 2:
                _psd = value;
                break;
            default:
                _db = value;
                break;
        }
        SqlHelper.Insatance.SetConnStr(_ip, _acc, _psd, _db);
    }

}
