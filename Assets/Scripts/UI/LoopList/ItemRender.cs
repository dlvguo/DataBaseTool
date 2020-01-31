using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemRender : DynamicInfinityItem
{
    public Text m_TxtName;

	// Use this for initialization
	void Start () {
		
	}

    protected override void OnRenderer()
    {
        base.OnRenderer();
        m_TxtName.text = mData.ToString();
    }
}
