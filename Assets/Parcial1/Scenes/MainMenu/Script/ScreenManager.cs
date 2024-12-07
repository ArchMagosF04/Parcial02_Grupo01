using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    [SerializeField] private Button puzzleButton;
    [SerializeField] private Button scoreButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button returnButton;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } 
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        puzzleButton.onClick.AddListener(GoToPuzzleScene);
        scoreButton.onClick.AddListener(GoToHighScoreScene);
        shopButton.onClick.AddListener(GoToShopScene);
        returnButton.onClick.AddListener(ReturnButton);
    }

    private void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoToPuzzleScene()
    {
        ChangeScene(1);
    }
    public void GoToHighScoreScene()
    {
        ChangeScene(2);
    }
    public void GoToShopScene()
    {
        ChangeScene(3);
    }

    public void ReturnButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if(currentScene != 0)
        {
            ChangeScene(0);
            Debug.Log("GoToMenu");
        }else
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }

}
