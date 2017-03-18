using UnityEngine;
using GameFramework.GameStructure;
using GameFramework.GameStructure.Levels;
using GameFramework.Advertising.UnityAds.Components;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;

    public GameObject ball1, ball2, ball3;
    public OnButtonClickWatchAdvertForCoins adButton;

	void Awake()
    {
        instance = this;

		if(LevelSelect.currentDifficulty == LevelDifficulty.Difficulties.Easy)
        {
            Destroy(ball2);
            Destroy(ball3);
        }
        else if(LevelSelect.currentDifficulty == LevelDifficulty.Difficulties.Medium)
        {
            Destroy(ball3);
        }
	}

    public void OnWinLevel()
    {
        LevelManager.Instance.GameOver(true);

        GameManager.Instance.Levels.Selected.StarWon(1, true);

        if (LevelSelect.currentDifficulty == LevelDifficulty.Difficulties.Medium)
        {
            GameManager.Instance.Levels.Selected.StarWon(2, true);
        }
        else if (LevelSelect.currentDifficulty == LevelDifficulty.Difficulties.Hard)
        {
            GameManager.Instance.Levels.Selected.StarWon(2, true);
            GameManager.Instance.Levels.Selected.StarWon(3, true);
        }

        adButton.Coins = GameManager.Instance.Levels.Selected.Coins;
    }
}
