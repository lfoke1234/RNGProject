using UnityEngine;

public class RotatingIcon : MonoBehaviour
{
    [SerializeField] private float speed = 0.03f;

    private void Update()
    {
        this.gameObject.transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
