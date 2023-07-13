using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public int SizeLevel = 1;
    public int SpeedLevel = 1;
    public int TimeLevel = 1;

    public float sizePrice =50;
    public float speedPrice =50;
    public float timePrice =50;

    public string sizeLevelKey = "SizeLevel";
    public string speedLevelKey = "SpeedLevel";
    public string timeLevelKey = "TimeLevel";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(sizeLevelKey, SizeLevel);
        PlayerPrefs.SetInt(speedLevelKey, SpeedLevel);
        PlayerPrefs.SetInt(timeLevelKey, TimeLevel);

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(sizeLevelKey))
        {
            SizeLevel = PlayerPrefs.GetInt(sizeLevelKey);
        }
        else
        {
            PlayerPrefs.SetInt(sizeLevelKey, 1);
        }

        if (PlayerPrefs.HasKey(speedLevelKey))
        {
            SpeedLevel = PlayerPrefs.GetInt(speedLevelKey);
        }
        else
        {
            PlayerPrefs.SetInt(speedLevelKey, 1);

        }

        if (PlayerPrefs.HasKey(timeLevelKey))
        {
            TimeLevel = PlayerPrefs.GetInt(timeLevelKey);
        }
        else
        {
            PlayerPrefs.SetInt(timeLevelKey, 1);

        }
    }

    // Call this method to update the values and save the data
    public void UpdateData(int newSizeLevel, int newSpeedLevel, int newTimeLevel)
    {
        SizeLevel = newSizeLevel;
        SpeedLevel = newSpeedLevel;
        TimeLevel = newTimeLevel;

        SaveData();
    }
}
