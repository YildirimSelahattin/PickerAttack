using System.Drawing;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public int SizeLevel = 1;
    public int SpeedLevel = 1;
    public int TimeLevel = 1;

    public int currentLevel = 1;

    public float sizePrice = 50;
    public float speedPrice = 50;
    public float timePrice = 50;

    public float speed;
    public Vector3 size = new Vector3(1, 1, 1);
    public float maxTimer;

    string sizeLevelKey = "SizeLevel";
    string speedLevelKey = "SpeedLevel";
    string speedPriceKey = "SpeedPrice";
    string sizePriceKey = "SizePrice";
    string timePriceKey = "TimePrice";
    string sizeKey = "Size";
    string timerKey = "Timer";
    string speedKey = "Speed";
    string timeLevelKey = "TimeLevel";
    string totalMoneyKey = "TotalMoney";
    public string CurrentLevelKey = "CurrentLevel";
    public string cameraLensKey = "CameraLens";

    public float cameraLens;
    public int totalMoney;
    public AudioClip collectSound;

    public AudioClip bossJumpSound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadData();
        }
        else
        {
            DontDestroyOnLoad(gameObject);

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
        PlayerPrefs.SetInt(CurrentLevelKey, currentLevel);

        PlayerPrefs.SetFloat(timerKey, maxTimer);
        PlayerPrefs.SetFloat(speedKey, speed);
        PlayerPrefs.SetFloat(sizeKey, size.y);
        PlayerPrefs.SetFloat(sizePriceKey, sizePrice);
        PlayerPrefs.SetFloat(speedPriceKey, speedPrice);
        PlayerPrefs.SetFloat(timePriceKey, timePrice);

        PlayerPrefs.SetFloat(cameraLensKey, cameraLens);

        PlayerPrefs.Save();
        LoadData();
    }
    public void LoadData()
    {
        SizeLevel = PlayerPrefs.GetInt(sizeLevelKey, 1);
        SpeedLevel = PlayerPrefs.GetInt(speedLevelKey, 1);
        TimeLevel = PlayerPrefs.GetInt(timeLevelKey, 1);
        totalMoney = PlayerPrefs.GetInt(totalMoneyKey, 1000);
        currentLevel = PlayerPrefs.GetInt(CurrentLevelKey, 1);

        maxTimer = PlayerPrefs.GetFloat(timerKey, 15);
        speed = PlayerPrefs.GetFloat(speedKey, 3);
        size = PlayerPrefs.GetFloat(sizeKey, 1f) * Vector3.one;

        cameraLens = PlayerPrefs.GetFloat(cameraLensKey, 30);

        sizePrice = PlayerPrefs.GetFloat(sizePriceKey, 50);
        speedPrice = PlayerPrefs.GetFloat(speedPriceKey, 50);
        timePrice = PlayerPrefs.GetFloat(timePriceKey, 50);

    }
}
