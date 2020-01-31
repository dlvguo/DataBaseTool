using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ModifyType
{
    CAMP = 0,
    SEX,
    NAME
};

//修改数据按钮
public class ModifyBtn : MonoBehaviour
{

    [SerializeField]
    private GameObject Datas;
    private Button modifyBtn;
    private Dropdown selectType;
    //数据类型
    private ModifyType _type;
    private Button confirmBtn;
    //输入数据
    private InputField data;
    private InputField userId;
    // Use this for initialization
    void Start()
    {
        modifyBtn = this.GetComponent<Button>();
        modifyBtn.onClick.AddListener(OnClick);
    }


    void OnClick()
    {

        Ribbon r = Ribbon.Instance;
        r.ClearRibbon();
        GameObject datas = Instantiate(Datas) as GameObject;
        datas.transform.parent = r.GetComponent<RectTransform>();
        datas.transform.localPosition = new Vector3(0, 150, 0);
        selectType = datas.GetComponent<Dropdown>();
        selectType.onValueChanged.AddListener(OnTypeChange);

        //确定添加确认按钮
        GameObject confirmGameobject = Instantiate(r.ButtonPrefab) as GameObject;
        confirmGameobject.transform.parent = r.GetComponent<RectTransform>();
        confirmGameobject.transform.localPosition = new Vector3(0, 0, 0);
        confirmBtn = confirmGameobject.GetComponent<Button>();
        //confirmBtn.GetComponent<Text>().text = "确认";
        confirmBtn.transform.GetChild(0).GetComponent<Text>().text = "确认";
        confirmBtn.onClick.AddListener(OnConfirm);

        //输入数据 
        GameObject inputID = Instantiate(r.InputField) as GameObject;
        inputID.transform.parent = r.GetComponent<RectTransform>();
        inputID.transform.localPosition = new Vector3(0, 100, 0);
        userId = inputID.GetComponent<InputField>();
        userId.placeholder.GetComponent<Text>().text = "输入用户ID";

        //添加输入账号框
        GameObject inputAcc = Instantiate(r.InputField) as GameObject;
        inputAcc.transform.parent = r.GetComponent<RectTransform>();
        inputAcc.transform.localPosition = new Vector3(0, 50, 0);
        data = inputAcc.GetComponent<InputField>();
        data.placeholder.GetComponent<Text>().text = "输入修改数据";
        Tips.Instance.OnSuccess("角色需要下线才能更改成功 阵营0为帝国 1为联邦 性别1为男性 2为女性 否则报错");
    }

    void OnConfirm()
    {
        if (userId.text.Length < 2)
            return;
        try
        {
            string setstr = "";
            if (_type == ModifyType.CAMP)
            {
                setstr = "CampID";
            }
            else if (_type == ModifyType.NAME)
            {
                setstr = "ActorName";
            }
            else
            {
                setstr = "sex";
            }
            setstr = string.Format("{0} = '{1}'", setstr, data.text);
            SqlHelper.Insatance.SetDB("GameDB");
            SqlHelper.Insatance.Update("Actor", setstr, string.Format("ActorID = '{0}'", userId.text));
            Tips.Instance.OnSuccess("修改成功！");
        }
        catch (System.Exception e)
        {
            Tips.Instance.OnException(e);
        }

        //UPDATE Person SET FirstName = 'Fred' WHERE LastName = 'Wilson' 

    }

    void OnTypeChange(int value)
    {
        _type = (ModifyType)value;
        data.text = "";
    }

}
