using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount = 0.1f; // How much the camera shakes
    public float shakeSpeed = 10f;   // How fast the camera shakes

    private Vector3 originalPosition; // Stores the camera's original position
    private bool isShaking = false;

    void Start()
    {
        // Store the camera's original position
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Simulate shaking when walking (e.g., when pressing the movement keys)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isShaking = true;
        }
        else
        {
            isShaking = false;
            transform.localPosition = originalPosition; // Reset to original position when not shaking
        }

        if (isShaking)
        {
            // Apply random offset to the camera's position
            Vector3 shakeOffset = new Vector3(0, Mathf.Cos(Time.time * shakeSpeed) * shakeAmount, 0);
            transform.localPosition = originalPosition + shakeOffset;
        }
    }
}