using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handController : MonoBehaviour
{
    [Header("References")]
    public GameObject playerCamera;

    [Header("Click Animation Settings")]
    public float restingHeight = 0.577f;
    public float minimumHeight = 0.540f; // <-- TWEAK THIS in the Inspector to fix the depth!
    public float verticalSpeed = 0.002f;

    [Header("Camera Tracking Limits")]
    public float trackingSmoothing = 10f;
    
    // Hand X Limits (Left / Right)
    public float handXMin = 1.1182f;
    public float handXMax = 1.8104f;
    
    // Hand Z Limits (Forward / Backward)
    public float handZMin = 1.1077f; 
    public float handZMax = 1.3023f; 

    // State Variables
    private bool movingHand = true;
    private bool falling = false;
    private float currentHeight;

    void Start()
    {
        movingHand = true;
        falling = false;
        currentHeight = restingHeight;
    }

    void Update()
    {
        // Start the click animation when the mouse is pressed
        if (Input.GetMouseButtonDown(0) && movingHand)
        {
            movingHand = false;
            falling = true;
        }
    }

    void FixedUpdate()
    {
        if (!movingHand)
        {
            HandleClickAnimation();
        }
        else
        {
            HandleCameraTracking();
        }
    }

    private void HandleClickAnimation()
    {
        if (falling)
        {
            currentHeight -= verticalSpeed;
            // Stop falling once we hit the new minimumHeight limit
            if (currentHeight <= minimumHeight)
            {
                currentHeight = minimumHeight; 
                falling = false; // Trigger the upward return
            }
        }
        else // returning up
        {
            currentHeight += verticalSpeed;
            if (currentHeight >= restingHeight)
            {
                currentHeight = restingHeight;
                movingHand = true; // Done clicking, resume tracking
            }
        }

        // Apply height position to the hand
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
    }

    private void HandleCameraTracking()
    {
        // 1. Get Camera Rotations
        float camRotY = playerCamera.transform.eulerAngles.y;
        float camRotX = playerCamera.transform.eulerAngles.x;

        // Normalize Y rotation to range between -180 and 180 (Easier to do math with)
        if (camRotY > 180f) camRotY -= 360f;

        // 2. Calculate Target X (Left/Right)
        // InverseLerp turns the -22.5 to 22.5 range into a percentage (0.0 to 1.0)
        float percentX = Mathf.InverseLerp(-22.5f, 22.5f, camRotY);
        float targetX = Mathf.Lerp(handXMin, handXMax, percentX);

        // 3. Calculate Target Z (Up/Down)
        float percentZ = Mathf.InverseLerp(35f, 45f, camRotX);
        float targetZ = Mathf.Lerp(handZMax, handZMin, percentZ); // Notice Zmax and Zmin are swapped here to mimic your original inverted logic

        // 4. Smoothly move the hand to the target X/Z position
        Vector3 targetPos = new Vector3(targetX, restingHeight, targetZ);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * trackingSmoothing);
    }
    // Add this to the bottom of handController.cs
    public void ReboundHand()
    {
        // If the hand is currently going down, instantly make it go back up
        if (falling)
        {
            falling = false;
        }
    }
}