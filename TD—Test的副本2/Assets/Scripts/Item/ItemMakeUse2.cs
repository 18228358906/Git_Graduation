using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakeUse2 : MonoBehaviour
{
    public GameObject obj;//生成的物体
    [HideInInspector]
    public bool isUseItem = false;
    public int num;//道具数量

    public Text numText;

    AudioSource sounEffects;

    void Start()
    {
        isUseItem = false;
        sounEffects = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        numText.text = "数量：" + num;
        if (Input.GetMouseButton(0))
        {
            if (isUseItem && num > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool res = Physics.Raycast(ray, out hit);
                if (res)
                {
                    GameObject item = GameObject.Instantiate(obj, new Vector3(hit.point.x,hit.point.y+10,hit.point.z), Quaternion.identity);
                    item.GetComponent<InpactItem>().SetTarget(hit.point);
                    //sounEffects.Play();
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
