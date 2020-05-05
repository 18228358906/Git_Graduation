using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }
    public float attackRateTime =1f;//攻击间隔
    public float timer = 0f;
    public GameObject bulletPrefab;
    public Transform firPositon;
    public Transform head;
    public bool useLaser = false;
    public float damageRate=70;//持续伤害-魔法塔用
    public LineRenderer laserRender;
    public GameObject laserEffect;

    AudioSource soundEffects;//射击音效
    public DamageType damageType;//伤害类型
    public float bulletDamage;//炮塔伤害
    public float bulletSpeed;//炮弹飞行速度
   
    private void Start()
    {
        timer = attackRateTime;
        soundEffects = GetComponent<AudioSource>();
       
    }
    private void Update()
    {
        if (useLaser == false)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                timer = 0f;
                Attack();
            }
        }
        else if(enemys.Count>0)
        {
            if (laserRender.enabled == false) laserRender.enabled = true;
            laserEffect.SetActive(true);
            if (enemys[0] == null)
            {
                UpdateEnemys();
            }
            if (enemys .Count> 0)
            {
                laserRender.SetPositions(new Vector3[] { firPositon.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate* Time.deltaTime,damageType);
                laserEffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserRender.enabled = false;
        }
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPositon = enemys[0].transform.position;
            targetPositon.y = head.position.y;
            head.LookAt(targetPositon);
            //head.forward = new Vector3(targetPositon.x - head.position.x,0,targetPositon.z-head.position.z);
        }
    }
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firPositon.position, Quaternion.identity); //firPositon.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
            bullet.GetComponent<Bullet>().SetDamage(bulletDamage);
            bullet.GetComponent<Bullet>().SetSpeed(bulletSpeed);
            bullet.GetComponent<Bullet>().SetGetBulletType = this.damageType;
            soundEffects.Play();
        }
        else
        {
            timer = attackRateTime;
        }
    }
    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for(int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for(int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i]+i);
        }
    }
}
