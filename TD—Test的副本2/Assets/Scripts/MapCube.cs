using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretGo;//保存当前cube的炮台
    public GameObject buildEffect;
    private Renderer render;
    [HideInInspector]
    public bool isUpgraded = false;
    [HideInInspector]
    public TurretData turretData;
    private void Start()
    {
        render = GetComponent<Renderer>();
    }
    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        turretGo= GameObject.Instantiate(turretData.turretPrefab,transform.position,Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    private void OnMouseEnter()
    {
        if (turretGo == null&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            render.material.color = Color.green;
        }
    }
    private void OnMouseExit()
    {
        render.material.color = Color.white;
    }
    public void UpgradeTurret()
    {
        if (isUpgraded == true) return;
        Destroy(turretGo);
        isUpgraded = true;
        turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;
        turretGo = null;
        turretData = null;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
}
