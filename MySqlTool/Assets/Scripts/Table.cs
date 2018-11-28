using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour {

    public string TableName { get; set; }
    public bool IsDel { get; set; }

    private Button button;
    

    public void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (IsDel)
        {
            DelTablesMan.Instance.DelTable(TableName);
        }
        else
        {
            DelTablesMan.Instance.AddTable(TableName);
        }
    }

}
