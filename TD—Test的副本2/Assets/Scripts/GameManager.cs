using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject GameWinUI;
    public GameObject SettingPanel;
   
    public static GameManager Instance;
    public Text endMassage;
    public Text winMassage;
    private EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Win()
    {
        GameWinUI.SetActive(true);
        winMassage.text = "胜 利";
    }
    public void Faild()
    {
        enemySpawner.Stop();
        GameOverUI.SetActive(true);
        endMassage.text = "失 败";
    }
    public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OnButtonNext2()
    {
        SceneManager.LoadScene(2);
    }
    public void OnButtonNext3()
    {
        SceneManager.LoadScene(3);
    }
    public void OnButtonNext4()
    {
        SceneManager.LoadScene(4);
    }
    public void OnSetting()
    {
        SettingPanel.SetActive(true);
    }
    public void OnSettingReturn()
    {
        SettingPanel.SetActive(false);
    }
    public void OnstartGame()
    {
        Time.timeScale = 1;
    }
    public void OnPauseGame()
    {
        Time.timeScale = 0;
    }
    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
