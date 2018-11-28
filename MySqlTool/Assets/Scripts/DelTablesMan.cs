using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelTablesMan : MonoBehaviour {

    public static DelTablesMan Instance { get; private set; }
    public GameObject table;
    public IEnumerable<string> tbs;
    private Dictionary<string,Table> delDict;
    private Transform delTables;
    private Button clearAll;
    private Button clearPart;

    // Use this for initialization
    void Start () {
        Instance = this;
        delDict =new Dictionary<string, Table>();
        delTables = transform.Find("DelTablesGrid");
        clearAll = transform.Find("ClearAll").GetComponent<Button>();
        clearAll.onClick.AddListener(ClearAll);
        clearPart = transform.Find("ClearPart").GetComponent<Button>();
        clearPart.onClick.AddListener(ClearPart);
	}
	
    public void AddTable(string name)
    {
        if (delDict.ContainsKey(name))
            return;
        GameObject gameOb = Instantiate(table, delTables) as GameObject;
        Table tb = gameOb.GetComponent<Table>();
        Text text = gameOb.GetComponentInChildren<Text>();
        text.text = name;
        tb.IsDel = true;
        tb.TableName = name;
        delDict.Add(name, tb);
    }

    public void DelTable(string name)
    {
        if (!delDict.ContainsKey(name))
            return;
        Table table;
        delDict.TryGetValue(name, out  table);
        Destroy(table.gameObject);
        delDict.Remove(name);
    }

    private void ClearAll()
    {
        try
        {
            if (tbs == null)
                return;
            SqlHelper.Insatance.ClearTables(tbs);
            Tips.Instance.OnSuccess("Delete Ok");
        }
        catch (System.Exception e)
        {
            Tips.Instance.OnException(e);
        }
    }

    private void ClearPart()
    {
        try
        {
            if (delDict.Count < 1)
                return;
            SqlHelper.Insatance.ClearTables(delDict.Keys);
            Tips.Instance.OnSuccess("Delete Ok");
        }
        catch (System.Exception e)
        {
            Tips.Instance.OnException(e);
        }
    }
}
