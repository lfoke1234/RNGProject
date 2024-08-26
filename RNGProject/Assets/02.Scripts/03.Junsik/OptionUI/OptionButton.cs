using UnityEngine;
using UnityEngine.EventSystems;

public class OptionButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject UI;
    bool IsUIon = false;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsUIon)
        {
            UI.SetActive(true);
            IsUIon = true;
        }
        else
        {
            UI.SetActive(false);
            IsUIon = false;
        }

    }

    private void Awake()
    {
        IsUIon = false;
        UI.SetActive(false);
    }

}
