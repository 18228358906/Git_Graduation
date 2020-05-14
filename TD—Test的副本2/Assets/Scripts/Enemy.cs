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
    //public float magicalDefence=30;
    public bool enemyLive=true;
    public Wave wave;

    public Animator movie;

    [HideInInspector]
    public float unusualTime=0f;//异常状态持续时间
    [HideInInspector]
    public float nowSpeed;//当前速度

    public float magicalDefence=5f;
    public float physicalDefence=5f;
   
    // Start is called before the first frame update
    void Start()
    {
        positions = Waypoints.positions;
        totalHp = hp;
        nowSpeed = Speed;      
        //hpSlider = GetComponentsInChildren<Slider>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (unusualTime > 0)
            unusualTime -= Time.deltaTime;
        else
        {
            unusualTime = 0f;
            nowSpeed = Speed;
        }
        Move();
    }
    void Move()
    {
        
        if (index > positions.Length - 1) return;
        //transform.LookAt(positions[index].position);
        //transform.forward = positions[index].position - transform.position;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * nowSpeed);
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
    public void TakeDamage(float _damage,DamageType _bulletType)
    {
        switch (_bulletType)
        {
            case DamageType.Magic:
                if(_damage>magicalDefence)
                    ReduceHp(_damage - magicalDefence);
                break;
            case DamageType.Physics:
                if(_damage>physicalDefence)
                    ReduceHp(_damage - magicalDefence);
                break;
            case DamageType.Mix:
                if ((_damage > magicalDefence) && (magicalDefence >= physicalDefence))
                    ReduceHp(_damage - magicalDefence);
                else if ((_damage > physicalDefence) && (physicalDefence > magicalDefence))
                    ReduceHp(_damage - magicalDefence);
                else
                    return;
                break;
        }
        
    }
    void ReduceHp(float damage)
    {
        if (hp <= 0) return;
        hp = hp - damage + wave.PhysicDefence;
        hpSlider.value = hp / totalHp;

        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        float f = Random.Range(0, 10);
        if (f >= 8f)
            ItemMakeUse3.isAdd = true;
        print(f);
        enemyLive = false;
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
        
    } 
    //减速效果
    public void SlowSpeed(float f,float durationtime)//减速百分比和持续时间
    {
        this.nowSpeed = this.Speed * f;
        this.unusualTime = durationtime;
    }
        
}
    
