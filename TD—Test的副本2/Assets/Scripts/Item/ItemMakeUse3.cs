using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakeUse3 : MonoBehaviour
{
    public int num;//道具数量
    public Text numText;
    public static bool isAdd;

    AudioSource sounEffects;

    // Start is called before the first frame update
    void Start()
    {
        isAdd = false;
        sounEffects = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        numText.text = "数量：" + num;
        if (isAdd)
        {
            num++;isAdd = false;
        }
    }
    public void ItemUseButton()
    {
        if (num > 0)
        {
            num--;
            BuildManager.getmoney += 200;
            sounEffects.Play();
        }        
    }
}
