using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float totalHp;
    public float Speed = 10f;
    public float hp = 150;
    public GameObject explosionEffect;
    public Slider hpSlider;
    private Transform[] positions;
    private int index = 0;
    public float magicalDefence=30;
    public bool enemyLive=true;
    public Wave wave;
    // Start is called before the first frame update
    void Start()
    {
        positions = Waypoints.positions;
        totalHp = hp;
        //hpSlider = GetComponentsInChildren<Slider>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * Speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }
    void ReachDestination()//到达终点
    {
        GameManager.Instance.Faild();
        GameObject.Destroy(this.gameObject);
       
    }
   void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }
    public void TakeDamage(float damage)
    {
        
        if (hp <= 0) return;
        hp = hp-damage+wave.PhysicDefence;
        hpSlider.value = hp / totalHp;
       
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        enemyLive = false;
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
        
    }   
    
}
    
