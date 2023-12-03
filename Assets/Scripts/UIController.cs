using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI ScoreDisplay;
    public TextMeshProUGUI ComboDisplay;

    public GameManager gameManager;

    public Image BallColourIndicator; //Is for showing what the next ball colour will be
    

    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateScoreDisplay();
        UpdateComboDisplay();
        BallColourIndicator.color = gameManager.nextBallColour;
    }


    //Updates and displays the score
    public void UpdateScoreDisplay()
    {
        ScoreDisplay.text = "Score:" + gameManager.score.ToString("F0");
    }

    //Updates and displays the combo
    public void UpdateComboDisplay()
    {
        ComboDisplay.text = "Combo:" + gameManager.combo;
    }

    public void SaveAndQuitButton()
    {
        gameManager.SaveData();

        // me when no main menu scene yet
        //SceneManager.LoadScene("");
    }
}
