using UnityEngine;
using UnityEngine.EventSystems;

public class DrawButton : MonoBehaviour, IPointerDownHandler
{
    public AnimationCurve curve;
    public void OnPointerDown(PointerEventData eventData)
    {
        int result = Mathf.RoundToInt(CurveWeightedRandom(curve) * 100);
        if (result >= 0 && result <= 59) Debug.Log("Rare"); //60%
        else if (result >= 60 && result <= 79) Debug.Log("Unique"); //20%
        else if (result >= 80 && result <= 94) Debug.Log("Epic"); //15%
        else Debug.Log("Legend"); //5%
        Debug.Log(result);
    }
    float CurveWeightedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
}
