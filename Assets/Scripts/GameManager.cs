using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject currentDisc;
    private DiscThrow throwState;
    public List<GameObject> allPanels;
    public DiscThrow throwB;
    public TMP_Text gameTimerText;

    private float changeColourTimer;
    [SerializeField] private float colourChangeTimerMax; // the value the timer reaches when a colour change happens

    [SerializeField] private Color32[] allColours;

    public float score;
    public float combo;
    public float highScore;

    public float baseScoreGive; // the value representing the minimum score given by a correct panel

    public bool IfLostRound;
    public bool canDisplayMenu;

    public delegate void EndsGame();
    public static event EndsGame GameEnded;

    public bool gameHasEnded;

    public Color32 nextBallColour;
    public bool canChangeNextBallColour;

    public float gameTimer = 60f;


    private void Awake()
    {
        // save data management
        if (SaveSystem.DoesPlayerFileExist())
        {
            LoadData();
        }
        else
        {
            SaveSystem.CreatePlayerFile(this);
            SaveData();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        allColours = new Color32[4];
        SetColours();
        throwState = FindObjectOfType<DiscThrow>();
        FindPanels();
        score = 0f;
        combo = 0f;
        IfLostRound = false;
        canDisplayMenu = true;
        gameHasEnded = false;

        nextBallColour = allColours[Random.Range(0, allColours.Length)];
        canChangeNextBallColour = true;
    }

    public void SetColours()
    {
        allColours[0] = new Color32(255, 101, 80, 255);
        allColours[1] = new Color32(0, 180, 80, 255);
        allColours[2] = new Color32(136, 0, 255, 255);
        allColours[3] = new Color32(255, 255, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
        }
        changeColourTimer += Time.deltaTime;

        UpdateTimer();

        GameFinished();

        if (!throwState.ThrowReady)
        {


            if (currentDisc == null)
            {
                currentDisc = throwB.newBall;

                currentDisc.GetComponent<MeshRenderer>().material.color = nextBallColour;
                
            }
        }
        else
        {
            currentDisc = null;

        }

        
        if (canChangeNextBallColour == true)
        {           
            ChangeNextBallColour();
        }

        if (changeColourTimer > colourChangeTimerMax)
        {
            if (currentDisc != null)
            {
                currentDisc.GetComponent<MeshRenderer>().material.color = allColours[Random.Range(0, allColours.Length)];

            }

            // change all platform colours
            for (int i = 0; i < allPanels.Count; i++)
            {
                allPanels[i].GetComponent<MeshRenderer>().material.color = allColours[Random.Range(0, allColours.Length)];
                Color32 panelColours = allPanels[i].GetComponent<MeshRenderer>().material.color;
                panelColours.a = 220;
                allPanels[i].GetComponent<MeshRenderer>().material.color = panelColours;
            }

            changeColourTimer = 0;
        }
    
        if (gameTimer <= 0)
        {
            GameEnded?.Invoke();
        }

        //IncreaseScore();
        
    }

    // will put every panel gameobject into the list
    private void FindPanels()
    {
        var panels = FindObjectsOfType<Panel>();

        foreach (var current in panels)
        {
            allPanels.Add(current.gameObject);
        }
    }
    /// <summary>
    /// Will increase score variable
    /// </summary>
    public void IncreaseCombo()
    {
        combo++;
        Debug.Log(combo);
    }

    public void IncreaseScore()
    {
        // multiplying by zero is weird don't do it
        if (combo > 0)
        {
            score += baseScoreGive * combo;
        }
        else
        {
            score += baseScoreGive;
        }       
    }

    // if statement can be changed to be linked to player health later
    public void GameFinished()
    {
        if (gameHasEnded == true)
        {
            GameEnded?.Invoke();
        }
    }

   // load player save data
   public void LoadData()
   {
        PlayerData data = SaveSystem.LoadPlayer();

        score = data.score;
   }

    // save player data
    public void SaveData()
    {
        SaveSystem.SaveGame(this);
    }

    //Will be used to take away score when upgrades are "brought"
    public void ReducePoints(float reducedPoints)
    {
        score -= reducedPoints;
    }

    //Is used to keep nextBallColour as one colour instead of constantly updating
    public void ChangeNextBallColour()
    {
        nextBallColour = allColours[Random.Range(0, allColours.Length)];

        canChangeNextBallColour = false;
    }

    public void UpdateTimer()
    {
        gameTimerText.text = gameTimer.ToString("F1");
    }
}
