using System;

[Serializable]
public class PlayerDataModel
{
    public int CurrentShellCount;
    public int TotalScore;
    public int HighScore;

    public int MaxHealth;
    public int CurrentHealth;

    public PlayerDataModel()
    {
        ResetData();
    }

    public void ResetData()
    {
        CurrentShellCount = 0;
        TotalScore = 0;
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
    }

    public void AddShell(int amount, int scorePerShell)
    {
        CurrentShellCount += amount;
        TotalScore += amount * scorePerShell;

        if (TotalScore > HighScore)
        {
            HighScore = TotalScore;
        }
    }
}