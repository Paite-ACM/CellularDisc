using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI ScoreDisplay;
    public TextMeshProUGUI ComboDisplay;

    public GameManager gameManager;
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
    }

    public void UpdateScoreDisplay()
    {
        ScoreDisplay.text = "Score:" + gameManager.score;
    }

    public void UpdateComboDisplay()
    {
        ComboDisplay.text = "Combo:" + gameManager.combo;
    }
}
