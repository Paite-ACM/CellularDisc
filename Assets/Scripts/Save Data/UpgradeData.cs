[System.Serializable]
public class UpgradeData
{
    public int upgradePlayerSpeedLimit;
    public int upgradeBallSpeedLimit;
    public int upgradePointGainLimit;
    public int upgradePlayerJumpHeightLimit;

    public UpgradeData(Upgrades upgrades)
    {
        upgradePlayerSpeedLimit = upgrades.upgradePlayerSpeedLimit;
        upgradeBallSpeedLimit = upgrades.upgradeBallSpeedLimit;
        upgradePointGainLimit = upgrades.upgradePointGainLimit;
        upgradePlayerJumpHeightLimit = upgrades.upgradePlayerJumpHeightLimit;
    }
}
