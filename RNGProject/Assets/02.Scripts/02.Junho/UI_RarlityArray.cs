using UnityEngine;

public class UI_RarlityArray : MonoBehaviour
{
    [SerializeField] private GameObject[] uiArray;
    public int currentUI = 0;

    private void Start()
    {
        currentUI = 0;
    }

    public void ChangeUI(int value)
    {
        currentUI = value;

        for (int i = 0; i < uiArray.Length; i++)
        {
            uiArray[i].SetActive(false);
        }

        uiArray[currentUI].SetActive(true);
    }

    public void NextUI()
    {
        if (currentUI == uiArray.Length - 1)
            currentUI = 0;
        else
            currentUI++;

        for (int i = 0; i < uiArray.Length; i++)
        {
            uiArray[i].SetActive(false);
        }

        uiArray[currentUI].SetActive(true);
    }

    public void PreviousUI()
    {
        if (currentUI <= 0)
            currentUI = 3;
        else
            currentUI--;

        for (int i = 0; i < uiArray.Length; i++)
        {
            uiArray[i].SetActive(false);
        }

        uiArray[currentUI].SetActive(true);
    }
}
