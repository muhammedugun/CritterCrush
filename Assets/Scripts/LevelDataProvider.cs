using Match3;
using UnityEngine;

public class LevelDataProvider : MonoBehaviour
{
    public LevelList levelList;
    [HideInInspector] public LevelData levelData;

    private void Awake()
    {
        levelData = FindObjectOfType<LevelData>();
    }
}
