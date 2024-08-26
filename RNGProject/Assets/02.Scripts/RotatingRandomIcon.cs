using UnityEngine;

public class RotatingRandomIcon : MonoBehaviour
{
    private float speed;

    private void OnEnable()
    {
        speed = Random.Range(-15f, 15f);
    }

    private void Update()
    {
        this.gameObject.transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
