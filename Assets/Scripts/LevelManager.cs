using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject menuCanvas;
    public bool isOpen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        menuCanvas.SetActive(false);
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        if (!isOpen)
        {
            menuCanvas.SetActive(true);
            isOpen = true;
        }
        else
        {
            menuCanvas.SetActive(false);
            isOpen = false;
        }
    }

    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        ToggleMenu();
    }
}
