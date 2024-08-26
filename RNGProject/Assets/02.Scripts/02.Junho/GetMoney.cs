using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GetMoney : MonoBehaviour
{
    [SerializeField] private SpeedUpgrade speedUpgrade;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private TextMeshProUGUI gold;

    private float currentValue;

    private void Start()
    {
        currentValue = speedUpgrade.speed - 0.1f;
        speedSlider.maxValue = speedUpgrade.speed;
        speedSlider.value = currentValue;
    }

    private void Update()
    {
        CheckSpeed();
        RotatingGear();
    }

    private void RotatingGear()
    {
        GameObject handle = speedSlider.handleRect.gameObject;

        float minSpeed = 50f;
        float maxSpeed = 150f;
        float normalizedValue = (speedUpgrade.speed - 1f) / 3f;
        float rotationSpeed = Mathf.Lerp(maxSpeed, minSpeed, normalizedValue);

        float rotationAmount = rotationSpeed * Time.deltaTime;

        handle.transform.Rotate(0f, 0f, rotationAmount);
    }




    private void CheckSpeed()
    {
        currentValue -= Time.deltaTime;
        speedSlider.value = currentValue;

        if (currentValue <= 0)
        {
            AddMoney();
            currentValue = speedUpgrade.speed;
            speedSlider.value = currentValue;
        }
    }

    public void UpdateSpeed(float newSpeed)
    {
        speedSlider.maxValue = newSpeed;
        speedSlider.value = newSpeed - 0.1f;
    }

    private void UpdateGold()
    {
        gold.text = Inventory.Instance.gold.ToString("N0") + "G";
    }


    private void AddMoney()
    {
        for (int i = 0; i < Inventory.Instance.currentFactory.Length; i++)
        {
            if (Inventory.Instance.currentFactory[i].IsUnityNull())
                continue;

            if (Inventory.Instance.currentFactory[i].data.IsUnityNull())
                continue;

            Inventory.Instance.gold += Inventory.Instance.currentFactory[i].data.goldGeneration;
        }

        UpdateGold();
    }
}
