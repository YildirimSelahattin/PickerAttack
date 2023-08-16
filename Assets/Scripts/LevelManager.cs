using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public GameObject levelParent;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()

    {
      Instantiate(GameManager.Instance.levelPrefabs[GameDataManager.Instance.currentLevel-1],levelParent.transform);
    }

}
