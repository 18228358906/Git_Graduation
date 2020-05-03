using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakeUse1 : MonoBehaviour
{
    public GameObject obj;//生成的物体
    [HideInInspector]
    public bool isUseItem = false;
    public float timer = 1f;//默认1f
    public int num;//道具数量

    public Text numText;

    void Start()
    {
        isUseItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        numText.text = "数量：" + num;
        if (Input.GetMouseButton(2))
        {
            if (isUseItem && num > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool res = Physics.Raycast(ray, out hit);
                if (res)
                {
                    Debug.Log(hit.point);
                    GameObject item = GameObject.Instantiate(obj, hit.point, Quaternion.identity);                    
                    Destroy(item,timer);
                }
                isUseItem = false;
                num--;
            }
        }
    }

    public void ItemUseButton()
    {
        isUseItem = true;
    }
}
