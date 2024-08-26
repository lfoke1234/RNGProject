using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CollectionPopup : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject define;

    [SerializeField] private Button defineBtn;
    [SerializeField] private Button closeBtn;

    private void Start()
    {
        defineBtn.onClick.AddListener(() => ClickDefineButton());
        closeBtn.onClick.AddListener(() => ClickClose());
    }

    private void ClickClose()
    {
        defineBtn.GetComponentInChildren<TextMeshProUGUI>().text = "정의";
        define.SetActive(false);
        popup.SetActive(false);
    }

    private void ClickDefineButton()
    {
        if (define.activeSelf == true)
        {
            defineBtn.GetComponentInChildren<TextMeshProUGUI>().text = "정의";
            define.SetActive(false);
        }
        else
        {
            defineBtn.GetComponentInChildren<TextMeshProUGUI>().text = "뒤로";
            define.SetActive(true);
        }
    }
}
