using UnityEngine;

public class frameRateLimit : MonoBehaviour
{
    public int frameCap = 60; // Default frame rate cap
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Application.targetFrameRate = frameCap;
    }

    
}
