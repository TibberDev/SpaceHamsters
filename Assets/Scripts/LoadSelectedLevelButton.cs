using GameFramework.GameStructure;
using UnityEngine;

public class LoadSelectedLevelButton : MonoBehaviour
{
    public void LoadSelectedLevel()
    {
        GameManager.LoadSceneWithTransitions("Level" + LevelSelect.currentlySelectedLevel);
    }
}
