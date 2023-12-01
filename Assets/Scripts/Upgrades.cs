using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public DiscThrow discThrow;
    public GameManager gameManager;

    [Header("Upgrade Buttons")]
    public Button upgradePlayerSpeed;
    public Button upgradeBallSpeed;
    public Button upgradePointGain;
    public Button upgradePlayerJumpHeight;

    public bool canUpgradePlayerSpeed, canUpgradeBallSpeed, canUpgradePointGain, canUpgradePlayerJumpHeight;
    public int upgradePlayerSpeedLimit;
    public int upgradeBallSpeedLimit;
    public int upgradePointGainLimit;
    public int upgradePlayerJumpHeightLimit;

    [Header("UpgradeText")]
    public TextMeshProUGUI PlayerSpeedText;
    public TextMeshProUGUI PointGainText;
    public TextMeshProUGUI BallSpeedText;
    public TextMeshProUGUI JumpHeightText;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        discThrow = GameObject.Find("Player").GetComponent<DiscThrow>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // upgrade save data stuff
        if (SaveSystem.DoesUpgradeFileExist())
        {
            LoadUpgradeData();
            UpgradeCorrection();
        }
        else
        {
            SaveSystem.CreateUpgradeFile(this);
            SaveData();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        upgradePlayerSpeed.onClick.AddListener(UpgradeMovementSpeed);
        upgradeBallSpeed.onClick.AddListener(UpgradeProjSpeed);
        upgradePointGain.onClick.AddListener(UpgradePointGain);
        upgradePlayerJumpHeight.onClick.AddListener(UpgradeJumpHeight);
        upgradeBallSpeedLimit = 0;
        upgradePlayerJumpHeightLimit = 0;
        upgradePlayerSpeedLimit = 0;
        upgradePointGainLimit = 0;
        canUpgradeBallSpeed = true;
        canUpgradePlayerJumpHeight = true;
        canUpgradePlayerSpeed = true;
        canUpgradePointGain = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradeBallSpeedLimit >= 5)
        {
            canUpgradeBallSpeed = false;
        }

        if (upgradePlayerJumpHeightLimit >= 5)
        {
            canUpgradePlayerJumpHeight = false;
        }

        if (upgradePlayerSpeedLimit >= 5)
        {
            canUpgradePlayerSpeed = false;
        }

        if (upgradePointGainLimit >= 5)
        {
            canUpgradePointGain = false;
        }

        DisplayUpgradeVariableText();
    }

    public void UpgradeMovementSpeed()
    {
        if (canUpgradePlayerSpeed == true && (gameManager.score >= 100f))
        {
            playerMovement.movementSpeed *= 1.05f;
            upgradePlayerSpeedLimit++;
            gameManager.ReducePoints(100f);
            
        }
    }

    public void UpgradeProjSpeed()
    {
        if (canUpgradeBallSpeed == true && (gameManager.score >= 100f))
        {
            discThrow.speed *= 1.05f;
            upgradeBallSpeedLimit++;
            gameManager.ReducePoints(100f);
        }
    }

    public void UpgradePointGain()
    {    
        if (canUpgradePointGain == true && (gameManager.score >= 100f))
        { 
            gameManager.baseScoreGive *= 1.05f;
            upgradePointGainLimit++;
            gameManager.ReducePoints(100f);
        }
    }

    public void UpgradeJumpHeight()
    {
        if (canUpgradePlayerJumpHeight == true && (gameManager.score >= 100f))
        {
            playerMovement.jumpForce *= 1.03f;
            upgradePlayerJumpHeightLimit++;
            gameManager.ReducePoints(100f);
        }
    }

    //Displays the variables in this script in a TextMesh pro text
    public void DisplayUpgradeVariableText()
    {
        PlayerSpeedText.text = "Player Speed:" + playerMovement.movementSpeed.ToString("F2");
        JumpHeightText.text = "Jump Height:" + playerMovement.jumpForce.ToString("F2");
        BallSpeedText.text = "Ball Speed:" + discThrow.speed.ToString("F2");
        PointGainText.text = "Base Score Gain:" + gameManager.baseScoreGive.ToString("F2");
    }

    public void LoadUpgradeData()
    {
        UpgradeData data = SaveSystem.LoadUpgrades();

        upgradePlayerSpeedLimit = data.upgradePlayerSpeedLimit;
        upgradeBallSpeedLimit = data.upgradeBallSpeedLimit;
        upgradePointGainLimit = data.upgradePointGainLimit;
        upgradePlayerJumpHeightLimit = data.upgradePlayerJumpHeightLimit;

    }

    public void SaveData()
    {
        SaveSystem.SaveUpgrades(this);
    }

    // called on start upon loading data in order to correct upgraded values
    private void UpgradeCorrection()
    {
        // whole lotta for loops
        for (int i = 0; i < upgradePlayerSpeedLimit; i++)
        {
            playerMovement.movementSpeed *= 1.05f;
        }

        for (int i = 0; i < upgradeBallSpeedLimit; i++)
        {
            discThrow.speed *= 1.05f;
        }

        for (int i = 0; i < upgradePointGainLimit; i++)
        {
            gameManager.baseScoreGive *= 1.05f;
        }

        for (int i = 0; i < upgradePlayerJumpHeightLimit; i++)
        {
            playerMovement.jumpForce *= 1.03f;
        }
    }
}
