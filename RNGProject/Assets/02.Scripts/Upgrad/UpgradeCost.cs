using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class UpgradeCost : MonoBehaviour
{
    [SerializeField] private ProbabilityUpgrade probability;
    [SerializeField] private SpeedUpgrade speed;

    private Dictionary<int, int> probabilityUpgradeCostTable = new Dictionary<int, int>();
    private Dictionary<int, int> speedUpgradeCostTable = new Dictionary<int, int>();

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI probabilityGold;
    [SerializeField] private TextMeshProUGUI speedGold;
    [SerializeField] private TextMeshProUGUI currentGold;

    private void Start()
    {
        LoadUpgradeCost();
        LoadSpeedUpgradeCost();
        UpdateUI();
    }

    #region Load CSV File
    private void LoadUpgradeCost()
    {
        string path = Path.Combine(Application.dataPath, "05.Data/DataTable/UpgradeCost.csv");

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                int level = int.Parse(values[0]);
                int cost = int.Parse(values[1]);
                probabilityUpgradeCostTable[level] = cost;
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + path);
        }
    }

    private void LoadSpeedUpgradeCost()
    {
        string path = Path.Combine(Application.dataPath, "05.Data/DataTable/SpeedUpgradCost.csv");

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                int level = int.Parse(values[0]);
                int cost = int.Parse(values[1]);
                speedUpgradeCostTable[level] = cost;
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + path);
        }
    }
    #endregion

    #region Level

    public void UpgradeProbabilityLevel()
    {
        int currentLevel = probability.level;

        if (currentLevel >= 200)
        {
            return;
        }

        if (probabilityUpgradeCostTable.ContainsKey(currentLevel))
        {
            int cost = probabilityUpgradeCostTable[currentLevel];
            if (Inventory.Instance.gold >= cost)
            {
                Inventory.Instance.gold -= cost;
                probability.UpgradeProbabilityLevel();
                UpdateUI();
            }
            else
            {
                Debug.Log("need gold");
            }
        }
    }

    public void UpgradeSpeedLevel()
    {
        int currentSpeedLevel = speed.level;
        if (speedUpgradeCostTable.ContainsKey(currentSpeedLevel))
        {
            int cost = speedUpgradeCostTable[currentSpeedLevel];
            if (Inventory.Instance.gold >= cost)
            {
                Inventory.Instance.gold -= cost;
                speed.UpgradeSpeedLevel();
                UpdateUI();
            }
            else
            {
                Debug.Log("need gold");
            }
        }
    }

    #endregion

    private void UpdateUI()
    {
        int currentProbabilityLevel = probability.level;
        int nextProbabilityCost = probabilityUpgradeCostTable.ContainsKey(currentProbabilityLevel) ? probabilityUpgradeCostTable[currentProbabilityLevel] : 0;
        probabilityGold.text = nextProbabilityCost.ToString("N0");

        int currentSpeedLevel = speed.level;
        int nextSpeedCost = speedUpgradeCostTable.ContainsKey(currentSpeedLevel) ? speedUpgradeCostTable[currentSpeedLevel] : 0;
        speedGold.text = nextSpeedCost.ToString("N0");

        currentGold.text = Inventory.Instance.gold.ToString("N0") + "G";
    }
}
