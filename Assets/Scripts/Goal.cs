using UnityEngine;

public class Goal : MonoBehaviour
{
    private int ballsCompleted = 0;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (LevelSelect.currentDifficulty == LevelDifficulty.Difficulties.Easy)
        {
            GameLevelManager.instance.OnWinLevel();
        }
        else if (LevelSelect.currentDifficulty == LevelDifficulty.Difficulties.Medium)
        {
            ballsCompleted++;
            Destroy(coll.gameObject);

            if (ballsCompleted == 2)
                GameLevelManager.instance.OnWinLevel();
        }
        else
        {
            ballsCompleted++;
            Destroy(coll.gameObject);

            if (ballsCompleted == 3)
                GameLevelManager.instance.OnWinLevel();
        }

        

        /*(if(MyLevelManager))
        LevelManager.Instance.Level.StarWon(1, true);*/
    }
}
