using System.Collections;
using UnityEngine;

public class NewItemIconControl : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 360f;
    [SerializeField] private float slowDownDuration = 2f;

    private float currentSpeed;
    [SerializeField] private float speed;
    private float elapsedTime;
    private bool isDone;

    private void OnEnable()
    {
        currentSpeed = initialSpeed;
        elapsedTime = 0f;
        StartCoroutine(RotateAndSlowDown());
    }

    private void Update()
    {
        if (isDone)
            transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        currentSpeed = 0f;
        elapsedTime = 0f;
        isDone = false;

        transform.rotation = Quaternion.identity;
    }

    private IEnumerator RotateAndSlowDown()
    {
        while (elapsedTime < slowDownDuration)
        {
            elapsedTime += Time.deltaTime;

            currentSpeed = Mathf.Lerp(initialSpeed, 0, elapsedTime / slowDownDuration);
            Quaternion rotation = Quaternion.Euler(0, 0, currentSpeed * Time.deltaTime);
            transform.rotation *= rotation;
            yield return null;
        }

        isDone = true;
        currentSpeed = 0f;
    }
}
