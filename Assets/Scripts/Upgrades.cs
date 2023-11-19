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

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        discThrow = GetComponent<DiscThrow>();
        gameManager = GetComponent<GameManager>();
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
    }

    public void UpgradeMovementSpeed()
    {
        if (canUpgradePlayerSpeed == true)
        {
            playerMovement.movementSpeed *= 1.1f;
            upgradePlayerSpeedLimit++;
        }
    }

    public void UpgradeProjSpeed()
    {
        if (canUpgradeBallSpeed == true)
        {
            discThrow.speed *= 1.1f;
            upgradeBallSpeedLimit++;
        }
    }

    public void UpgradePointGain()
    {    
        if (canUpgradePointGain == true)
        { 
            gameManager.baseScoreGive *= 1.1f;
            upgradePointGainLimit++;
        }
    }

    public void UpgradeJumpHeight()
    {
        if (canUpgradePlayerJumpHeight == true)
        {
            playerMovement.jumpForce *= 1.1f;
            upgradePlayerJumpHeightLimit++;
        }
    }
}
