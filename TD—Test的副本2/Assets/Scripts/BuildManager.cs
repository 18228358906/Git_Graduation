using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public Animator moneyAnimator;
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;
    private TurretData selectedTurretData;//表示当前选择的炮台（要建造的炮台）
    private int money = 1000;
    public Text moneyText;
    public GameObject upgradeCanvas;
    public Button buttonUpgrade;
    private MapCube selectedMapCube;//表示当前选择的炮台（场景中的游戏物体）
    
    void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "$" + money;
    }
    public void GetMoney(int getmoney=0)
    {
        money += getmoney;
        moneyText.text = "$" + money;
    }
   
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                //开发炮台的建造
                Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                //Debug.DrawRay(ray,Vector3.up,Color.red);
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    
                    if (mapCube.turretGo == null&&selectedTurretData!=null)
                    {
                        //可以创建
                        if (money > selectedTurretData.cost)
                        {

                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            moneyAnimator.SetTrigger("Flick");
                            //todo 提示钱不够
                        }
                    }
                    else if(mapCube.turretGo!=null)
                    {
                       
                        ShowUpgradedUI(mapCube.transform.position, mapCube.isUpgraded);
                        //if (mapCube.isUpgraded)
                        //{
                            //ShowUpgradedUI(mapCube.transform.position, true);
                        //}
                        //else
                        //{
                           // ShowUpgradedUI(mapCube.transform.position, false);
                        //}
                        if (mapCube.turretGo == selectedMapCube && upgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());
                        }
                        selectedMapCube=mapCube;
                        //todo 升级处理
                    }
                }
            }
        }
    }
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }
    void ShowUpgradedUI(Vector3 pos, bool isDisableUpgraded=false)
    {
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable = !isDisableUpgraded;
    }

    IEnumerator HideUpgradeUI()
    {
        
        upgradeCanvas.SetActive(false);
        yield return new WaitForSeconds(0f);
    }
    
    public void OnUpgradeButtonDown()
    {
        //todo
        if (money >= selectedMapCube.turretData.costUpgraded)
        {
            ChangeMoney(-selectedMapCube.turretData.costUpgraded);
            selectedMapCube.UpgradeTurret();
        }
        else
        {
            moneyAnimator.SetTrigger("Flick");
        }
        StartCoroutine( HideUpgradeUI());
    }
    public void OnDestroyButtonDown()
    {
        //todo
        selectedMapCube.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
    }
}
