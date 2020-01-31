using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

//已经注册账号查询
public class SearchAccount : MonoBehaviour {

    private Button searchButton;
    public DynamicInfinityListRenderer m_Dl;

    // Use this for initialization
    void Start()
    {
        searchButton = this.GetComponent<Button>();
        searchButton.onClick.AddListener(OnClick);
        m_Dl.InitRendererList(OnSelectHandler, null);
    }

    void OnClick()
    {
        Ribbon.Instance.ClearRibbon();
        Ribbon.Instance.ListContainer.SetActive(true);
        SqlHelper.Insatance.SetDB("GameDBArea");
        try
        {
            var strs = SqlHelper.Insatance.GetTables("select UserName,PassWord from account ORDER BY UserID;");
            List<string> strList = new List<string>();
            foreach (var str in strs)
            {
                var vars = Regex.Split(str, SqlHelper.Insatance.SpiltStr);
                vars[1] = StrUnitl.EncodeSHA1(vars[1]);
                strList.Add("账号:" + vars[0]);
            }
            m_Dl.SetDataProvider(strList);
        }
        catch (Exception e)
        {
            Tips.Instance.OnException(e);
        }
       
    }

    void OnSelectHandler(DynamicInfinityItem item)
    {
        Tips.Instance.OnSuccess(item.ToString());
    }
}
