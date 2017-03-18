using UnityEngine;

public class LevelDifficulty : MonoBehaviour
{
    public RectTransform selectionHighlight;

    public enum Difficulties
    {
        Easy,
        Medium,
        Hard
    }


    public void OnSelectedEasy()
    {
        LevelSelect.currentDifficulty = Difficulties.Easy;

        HighlightSelection();
    }

    public void OnSelectedMedium()
    {
        LevelSelect.currentDifficulty = Difficulties.Medium;

        HighlightSelection();
    }

    public void OnSelectedHard()
    {
        LevelSelect.currentDifficulty = Difficulties.Hard;

        HighlightSelection();
    }

    void HighlightSelection()
    {
        if (selectionHighlight)
        {
            selectionHighlight.anchoredPosition = transform.localPosition;
        }
    }
}

