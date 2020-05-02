using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject CourseUI;
    public GameObject AudioUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }
    public  void OnCourse()
    {
        CourseUI.SetActive(true);
    }
    public void OnAudioUI()
    {
        AudioUI.SetActive(true);
    }
    public void OnAudioUIReturn()
    {
        AudioUI.SetActive(false);
    }
    public void OnCouseRuturn()
    {
        CourseUI.SetActive(false);
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
