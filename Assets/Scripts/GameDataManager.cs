using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public int SizeLevel = 1;
    public int SpeedLevel = 1;
    public int TimeLevel = 1;

    public float sizePrice = 50;
    public float speedPrice = 50;
    public float timePrice = 50;

    string sizeLevelKey = "SizeLevel";
    string speedLevelKey = "SpeedLevel";
    string timeLevelKey = "TimeLevel";
    string totalMoneyKey = "TotalMoney";
    public int totalMoney;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadData();
        }
    }
    private void Start()
    {
        
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt(sizeLevelKey, SizeLevel);
        PlayerPrefs.SetInt(speedLevelKey, SpeedLevel);
        PlayerPrefs.SetInt(timeLevelKey, TimeLevel);
        PlayerPrefs.SetInt(totalMoneyKey, totalMoney);
        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        SizeLevel = PlayerPrefs.GetInt(sizeLevelKey, 1);
        SpeedLevel = PlayerPrefs.GetInt(speedLevelKey, 1);
        TimeLevel = PlayerPrefs.GetInt(timeLevelKey, 1);
        totalMoney = PlayerPrefs.GetInt("TotalMoney", 100);
    }
}
