using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SpeedUpgrade : MonoBehaviour, ISaveManager
{
    public int level = 1;
    public float speed;
    private Dictionary<int, float> speedData = new Dictionary<int, float>();

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI speed_txt;
    [SerializeField] private TextMeshProUGUI level_txt;

    [SerializeField] private GetMoney getMoneyScript; // GetMoney 스크립트 참조

    private void Start()
    {
        LoadSpeedData();
        SetSpeed(level);
    }

    private void LoadSpeedData()
    {
        string path = Path.Combine(Application.dataPath, "05.Data/DataTable/SpeedDataTable.csv");

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                int level = int.Parse(values[0]);
                float cost = float.Parse(values[1]);
                speedData[level] = cost;
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + path);
        }
    }

    public float GetSpeedValue(int level)
    {
        if (speedData.ContainsKey(level))
        {
            return speedData[level];
        }
        else
        {
            return 0;
        }
    }

    private void SetSpeed(int level)
    {
        if (speedData.ContainsKey(level))
        {
            speed = speedData[level];
            getMoneyScript.UpdateSpeed(speed);
        }
        else
        {
            return;
        }

        UpdateUI();
    }

    public void UpgradeSpeedLevel()
    {
        if (level >= 100)
        {
            return;
        }

        int currentLevel = level;
        if (speedData.ContainsKey(currentLevel))
        {
            level++;
            SetSpeed(level);
        }
        else
        {
            return;
        }
    }

    private void UpdateUI()
    {
        speed_txt.text = "현재 속도: " + speed.ToString("F2") + "s";
        level_txt.text = "Lv." + level.ToString();
    }

    public void LoadData(GameData _data)
    {
        level = _data.speedLevel;
        UpdateUI();
    }

    public void SaveData(ref GameData _data)
    {
        _data.speedLevel = level;
    }
}
