using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

//注册按钮
public class RegisterBtn : MonoBehaviour
{

    private Button register;

    private InputField account;
    private InputField psd;
    private Button confirmRegister;
    void Start()
    {
        register = this.GetComponent<Button>();
        register.onClick.AddListener(OnClick);
    }

    //点击事件
    void OnClick()
    {
        Ribbon r = Ribbon.Instance;
        r.ClearRibbon();
        //确定添加注册按钮
        GameObject confirmGameobject = Instantiate(r.ButtonPrefab) as GameObject;
        confirmGameobject.transform.parent = r.GetComponent<RectTransform>();
        confirmGameobject.transform.localPosition = new Vector3(0, 0, 0);
        confirmRegister = confirmGameobject.GetComponent<Button>();
        confirmRegister.onClick.AddListener(OnRegister);

        //添加输入账号框
        GameObject inputAcc = Instantiate(r.InputField) as GameObject;
        inputAcc.transform.parent = r.GetComponent<RectTransform>();
        inputAcc.transform.localPosition = new Vector3(0, 150, 0);
        account = inputAcc.GetComponent<InputField>();
        account.placeholder.GetComponent<Text>().text = "输入账号";

        //输入密码账号框 
        GameObject inputPsd = Instantiate(r.InputField) as GameObject;
        inputPsd.transform.parent = r.GetComponent<RectTransform>();
        inputPsd.transform.localPosition = new Vector3(0, 75, 0);
        psd = inputPsd.GetComponent<InputField>();
        psd.placeholder.GetComponent<Text>().text = "输入密码";
    }

    //注册
    void OnRegister()
    {
        try
        {
            var sha1 = new SHA1CryptoServiceProvider();
            byte[] str01 = Encoding.Default.GetBytes(psd.textComponent.text);
            byte[] str02 = sha1.ComputeHash(str01);
            var pass = BitConverter.ToString(str02).Replace("-", "");
            string values = string.Format("'{0}','{1}','{1}'", account.textComponent.text, pass);
            SqlHelper.Insatance.SetDB("GameDBArea");
            SqlHelper.Insatance.Insert("account", values, "(UserName,PassWord,SecPsw)");
            Tips.Instance.OnSuccess("注册成功");
        }
        catch (Exception e)
        {
            Tips.Instance.OnException(e);
        }
    }
}
