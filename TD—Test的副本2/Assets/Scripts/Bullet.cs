using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage ;
    float speed;
    private Transform target;//目标
    public GameObject explosionEffectPrefab;//特效
     DamageType type;
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage,type);
            GameObject effect= GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Destroy(effect, 1);
            
        }
    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }

    public DamageType SetGetBulletType
    {
        get
        {
            return this.type;
        }
        set
        {
            this.type = value;
        }
    }

    public void SetDamage(float _damage)
    {
        this.damage = _damage;
    }
    public void SetSpeed(float _speed)
    {
        this.speed = _speed;
    }
}
