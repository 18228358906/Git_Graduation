using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InpactItem : MonoBehaviour
{
    public float damage = 0f;
    private float speed = 30f;
    public Vector3 target;

    DamageType type;
    // Start is called before the first frame update
    void Start()
    {
        type = DamageType.Physics;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != Vector3.zero)
        {
            transform.LookAt(target);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        /*if (this.gameObject.transform.position.y <= 3)
            Destroy(this.gameObject);*/
    }

    public void SetTarget(Vector3 _target)
    {
        this.target = _target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (damage > 0)
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<Enemy>().TakeDamage(damage,type);
            }            
        }
        Destroy(this.gameObject);
    }
}
