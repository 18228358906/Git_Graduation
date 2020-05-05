using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceItem : MonoBehaviour
{
    public float damage=0f;//伤害
    public float slowDown=0f;//减速百分比
    //public float continueDamage=0f;//持续伤害量
    public float durationTime=0f;//异常状态持续时间
    DamageType type; 
    public List<GameObject> enemys = new List<GameObject>();//敌人组


    void Start()
    {
        type = DamageType.Magic;
    }

    void Update()
    {
        if (enemys.Count > 0)
        {
            if (enemys[0] == null)
            {
                enemys.RemoveAt(0);
            }
            if (damage >= 0 && slowDown >= 0 && durationTime >= 0/*&&continueDamage>=0*/)
            {
                for (int i = 0; i < enemys.Count; i++)
                {
                    enemys[i].GetComponent<Enemy>().SlowSpeed(slowDown, durationTime);
                    enemys[i].GetComponent<Enemy>().TakeDamage(damage,type);
                    //enemys[i].GetComponent<Enemy>().TakeDamage(continueDamage);
                }
            }                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!enemys.Exists(p => p == other.gameObject))
        {
            if (other.tag == "Enemy")
                enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!enemys.Exists(p => p == other.gameObject))
        {
            if (other.tag == "Enemy")
                enemys.Add(other.gameObject);
        }
    }
}
