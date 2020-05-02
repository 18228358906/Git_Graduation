using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float bgmValue=1f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = bgmValue;
    }
    public void setBgm(System.Single val)
    {
        bgmValue = val;
    }
}
