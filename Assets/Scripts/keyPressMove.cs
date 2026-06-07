using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressMove : MonoBehaviour
{
    public textInput searchText;
    public string thisCharacter;
    
    [Header("Hand Reference")]
    public handController handScript; // <-- Added this back!

    [Header("Key Visual Settings")]
    public float maxDepressionDepth = 0.1f;
    public float keyReturnSpeed = 15f;

    private float originalYPosition;
    private bool isPressed = false;
    private Transform activeFinger = null;

    void Start()
    {
        originalYPosition = transform.position.y;
    }

    void Update()
    {
        if (isPressed && activeFinger != null)
        {
            float targetY = Mathf.Clamp(activeFinger.position.y - 0.02f, originalYPosition - maxDepressionDepth, originalYPosition);
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
        else
        {
            Vector3 returnPos = new Vector3(transform.position.x, originalYPosition, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, returnPos, Time.deltaTime * keyReturnSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FingerTip") && !isPressed)
        {
            isPressed = true;
            activeFinger = other.transform;

            // 1. Tell the hand to instantly bounce back up
            if (handScript != null)
            {
                handScript.ReboundHand();
            }

            // 2. Send the character to the search bar
            searchText.ControlSearchBar(thisCharacter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FingerTip") && isPressed)
        {
            isPressed = false;
            activeFinger = null;
        }
    }
}