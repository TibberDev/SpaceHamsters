using UnityEngine;
using GameFramework.GameStructure.Levels;

public class LethalObstacle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
       LevelManager.Instance.GameOver(false);
    }
}
