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
    // Start is called before the first frame update
    void Start()
    {
        startGameButton.onClick.AddListener(() => StartGame("cd_overdrive"));
        startGameButtonText.text = "Start Game";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
