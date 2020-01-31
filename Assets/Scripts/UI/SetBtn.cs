using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBtn : MonoBehaviour
{
    //设置按钮
    private Button setBtn;
    //Ip 账号 密码 数据库
    private InputField ip;
    private InputField acc;
    private InputField psd;
    private Dropdown dbs;
    private string _ip;
    private string _acc;
    private string _psd;
    private string _db;
    public GameObject Table;
    private Dictionary<string, Table> dicTables;
    // Use this for initialization
    void Start()
    {
        setBtn = this.GetComponent<Button>();
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


        dbs = GameObject.Find("DBs").GetComponent<Dropdown>();
        dbs.onValueChanged.AddListener((value) =>
        {
            OnValueChanged(3, value.ToString());

        });
        setBtn.onClick.AddListener(OnSet);
        _ip = ip.text;
        _acc = acc.text;
        _psd = psd.text;
        _db = "GameDBArea";
        SqlHelper.Insatance.SetConnStr(_ip, _acc, _psd, _db);

    }
    private void OnSet()
    {
        try
        {

            var str = SqlHelper.Insatance.TestConn();
            Tips.Instance.OnSuccess(str);
        }
        catch (Exception e)
        {
            Tips.Instance.OnException(e);
        }                       
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
                int index;
                if(int.TryParse(value,out index))
                {
                    Debug.Log(index);
                    if (index == 0)
                        _db = "GameDBArea";
                    else
                        _db = "GameDB";
                }
                break;
        }
        SqlHelper.Insatance.SetConnStr(_ip, _acc, _psd, _db);
    }

}
