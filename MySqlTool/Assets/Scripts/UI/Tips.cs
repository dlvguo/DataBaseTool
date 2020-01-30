using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//提示消息
public class Tips : MonoBehaviour {

    public static Tips Instance { get; private set; }
    private Text info;
    private Button confirm;
    void Start () {
        Instance = this;
        info = transform.Find("Info").GetComponent<Text>();
        confirm = transform.Find("confirm").GetComponent<Button>();
        confirm.onClick.AddListener(() => { gameObject.SetActive(false); });
        gameObject.SetActive(false);
	}
	
    public void OnException(Exception e)
    {
        gameObject.SetActive(true);
        info.text = e.Message + e.InnerException;
    }

    public void OnSuccess(string str)
    {
        gameObject.SetActive(true);
        info.text = str;
    }
}
