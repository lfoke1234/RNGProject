using System.IO;
using TMPro;
using UnityEngine;

public class ProbabilityUpgrade : MonoBehaviour, ISaveManager
{
    private float[] rareProbs = new float[200];
    private float[] epicProbs = new float[200];
    private float[] uniqueProbs = new float[200];
    private float[] legendProbs = new float[200];

    public float rareProb;
    public float epicProb;
    public float uniqueProb;
    public float legendProb;

    public int level = 1;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI level_txt;
    [SerializeField] private TextMeshProUGUI rare_txt;
    [SerializeField] private TextMeshProUGUI unique_txt;
    [SerializeField] private TextMeshProUGUI epic_txt;
    [SerializeField] private TextMeshProUGUI legend_txt;

    private void Start()
    {
        LoadProbabilitiesData();
        UpdateProbabilities(level);
        UpdateUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeProbabilityLevel();
        }
    }
    public void UpgradeProbabilityLevel()
    {
        if (level >= 200)
        {
            return;
        }

        level++;
        UpdateProbabilities(level);
        UpdateUI();
    }

    public void UpdateProbabilities(int level)
    {
        rareProb = rareProbs[level - 1];
        epicProb = epicProbs[level - 1];
        uniqueProb = uniqueProbs[level - 1];
        legendProb = legendProbs[level - 1];
    }

    public void Gacha()
    {
        Inventory inventory = Inventory.Instance;

        int result = Mathf.RoundToInt(Random.Range(0f, 100f));
        if (result < rareProb)
        {
            inventory.RandomRare();
        }
        else if (result < rareProb + uniqueProb)
        {
            inventory.RandomUnique();
        }
        else if (result < rareProb + uniqueProb + epicProb)
        {
            inventory.RandomEpic();
        }
        else
        {
            inventory.RandomLegend();
        }
    }

    private void LoadProbabilitiesData()
    {
        string path = Path.Combine(Application.dataPath, "05.Data/DataTable/UpgradeDataTable.csv");

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                rareProbs[i - 1] = float.Parse(values[0]);
                epicProbs[i - 1] = float.Parse(values[1]);
                uniqueProbs[i - 1] = float.Parse(values[2]);
                legendProbs[i - 1] = float.Parse(values[3]);
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + path);
        }
    }

    private void UpdateUI()
    {
        level_txt.text = "Lv." + level;
        rare_txt.text = rareProb.ToString("F2") + "%";
        unique_txt.text = uniqueProb.ToString("F2") + "%";
        epic_txt.text = epicProb.ToString("F2") + "%";
        legend_txt.text = legendProb.ToString("F2") + "%";
    }

    public void LoadData(GameData _data)
    {
        level = _data.probablityLevel;
        UpdateUI();
    }

    public void SaveData(ref GameData _data)
    {
        _data.probablityLevel = level;
    }
}
