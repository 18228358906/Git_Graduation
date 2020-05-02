using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Control : MonoBehaviour
{
    public float Speed = 25f;
    public float mouseSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h*Speed, mouse*mouseSpeed, v*Speed) * Time.deltaTime ,Space.World);
    }
}
