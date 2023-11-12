using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject MenuUI;
    [Header("Buttons")]
    public Button startButton;
    public Button retryButton;
    public Button upgradesButton;
    public Button exitButton;
    public GameObject UpgradesMenu;
    public Button backToMainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        MenuUI.SetActive(false);
        UpgradesMenu.SetActive(false);
        startButton.onClick.AddListener(StartGameButton);
        retryButton.onClick.AddListener(RetryButton);
        upgradesButton.onClick.AddListener(UpgradesButton);
        exitButton.onClick.AddListener(ExitButton);
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayMenu();

        if (gameManager.IfLostRound == false)
        {
            retryButton.gameObject.SetActive(false);
        }
        else if (gameManager.IfLostRound == true)
        {
            retryButton.gameObject.SetActive(true);
        }

        
    }

    public void StartGameButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void RetryButton()
    {
        
    }

    public void UpgradesButton()
    {
        UpgradesMenu.SetActive(true);
        MenuUI.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void DisplayMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameManager.canDisplayMenu == true)
        {
            MenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameManager.canDisplayMenu = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameManager.canDisplayMenu == false)
        {
            MenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameManager.canDisplayMenu = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UpgradesMenu.SetActive(false);
        }
    }
    public void BackToMainMenu()
    {
        MenuUI.SetActive(true);
        UpgradesMenu.SetActive(false);
        
    }
}
