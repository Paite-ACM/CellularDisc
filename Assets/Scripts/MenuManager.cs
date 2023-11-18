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
    public GameObject GameOverScreen;
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
        GameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayMenu();

        /* 
         * If the player loses a round, the retry button will be visible and able to be clicked,
         * while if they haven't, the retry button will not be accessible.
        */
        if (gameManager.IfLostRound == false)
        {
            retryButton.gameObject.SetActive(false);
        }
        else if (gameManager.IfLostRound == true)
        {
            retryButton.gameObject.SetActive(true);
        }

        
    }

    // Starts a new round
    public void StartGameButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }


    
    public void RetryButton()
    {
        
    }

    //Display Upgrades menu
    public void UpgradesButton()
    {
        UpgradesMenu.SetActive(true);
        MenuUI.SetActive(false);
    }

    //Exits the game entirely, is supposed to save and then quit, so needs save system 
    public void ExitButton()
    {
        Application.Quit();
    }


    /// <summary>
    /// If the player presses the escape key while 'canDisplayMenu' is true,
    /// the MenuUI activates, time freezes, canDisplayMenu is set to false
    /// and the cursor is visible and movable.
    /// </summary>
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

        /* While canDisplayMenu is false and the escape key is pressed,
         * both menus will be hidden,
         * time will resume         
         */
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

    //The button assigned to this function is on the Upgrades Menu and when clicked, will return the player to the Main Menu
    public void BackToMainMenu()
    {
        MenuUI.SetActive(true);
        UpgradesMenu.SetActive(false);
        
    }

    private void OnEnable()
    {
        GameManager.GameEnded += DisplayGameOver;
    }

    private void OnDisable()
    {
        GameManager.GameEnded -= DisplayGameOver;
    }

    public void DisplayGameOver()
    {
        GameOverScreen.SetActive(true);
    }
}
