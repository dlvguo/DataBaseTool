using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class SearchIP : MonoBehaviour
{
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
        SqlHelper.Insatance.SetDB("GameDB");
        try
        {
            var strs = SqlHelper.Insatance.GetTables("select ActorName,IP from Actor ORDER BY IP;");
            List<string> strList = new List<string>();
            foreach (var str in strs)
            {
                var vars = Regex.Split(str, SqlHelper.Insatance.SpiltStr);
                strList.Add(vars[1] + " " + vars[0]);
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
