using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakeUse1 : MonoBehaviour
{
    public GameObject obj;//生成的物体
    public GameObject obj2;
    [HideInInspector]
    public bool isUseItem = false;
    public float timer = 1f;//默认1f
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
                    GameObject item = GameObject.Instantiate(obj, hit.point, Quaternion.identity);
                    GameObject item2 = GameObject.Instantiate(obj2, hit.point, Quaternion.identity);
                    //sounEffects.Play();
                    Destroy(item,timer);
                    Destroy(item2, timer);
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
