using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public int levelIndex = 1;
    public Transform startBar;

    public static int currentlySelectedLevel = 1;
    public static LevelDifficulty.Difficulties currentDifficulty = LevelDifficulty.Difficulties.Easy;

	public void SelectLevel()
    {
        currentlySelectedLevel = levelIndex;
        currentDifficulty = LevelDifficulty.Difficulties.Easy;

        SetStartBar();
	}


    void SetStartBar()
    {
        startBar.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 260f, transform.localPosition.z);
    }
}