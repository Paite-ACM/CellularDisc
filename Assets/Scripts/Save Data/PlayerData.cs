[System.Serializable]
public class PlayerData
{
    public float score;
    public float highScore;
    // add powerup stuff (and whatever else we're adding) later

    public PlayerData(GameManager gm)
    {
        score = gm.score;
        highScore = gm.highScore;
    }
}
