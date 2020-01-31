using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum PrefabType
{
    Input,
    Button
};


//功能区域

public class Ribbon : MonoBehaviour
{

    //单例
    private static Ribbon _Instance = new Ribbon();

    [SerializeField]
    private GameObject buttonPrefab;

    public GameObject ButtonPrefab
    {
        get
        {
            return buttonPrefab;
        }
    }
    [SerializeField]
    private GameObject inputField;
    public GameObject InputField
    {
        get
        {
            return inputField;
        }
    }


    public static Ribbon Instance
    {
        get
        {
            return _Instance;
        }
    }


    // Use this for initialization
    void Start()
    {
        _Instance = this;
    }


    //清空
    public void ClearRibbon()
    {

        int childCount = this.GetComponent<RectTransform>().childCount;
        for (int i = 1; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

    }

    public void CreatPrefab(PrefabType type, Vector3 pos, string Info)
    {

    }
}
