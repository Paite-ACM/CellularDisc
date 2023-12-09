using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button startGameButton;
    public TextMeshProUGUI startGameButtonText;
    public TextMeshProUGUI exitGameButtonText;
    public Button exitGameButton;
    // Start is called before the first frame update
    void Start()
    {
        startGameButton.onClick.AddListener(() => StartGame("cd_overdrive"));
        startGameButtonText.text = "Start Game";
        exitGameButtonText.text = "Exit Game";
        exitGameButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
