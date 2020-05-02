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
    public float attackRateTime =1f;//多少秒攻击一次
    public float timer = 0f;
    public GameObject bulletPrefab;
    public Transform firPositon;
    public Transform head;
    public bool useLaser = false;
    public float damageRate=70;
    public LineRenderer laserRender;
    public GameObject laserEffect;
   
    private void Start()
    {
        timer = attackRateTime;
       
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
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate* Time.deltaTime);
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
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firPositon.position, firPositon.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
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
