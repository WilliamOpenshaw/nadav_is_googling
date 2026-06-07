using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using System;

public class resultsController : MonoBehaviour
{
    public TextMeshPro bartext;
    public int gridSizeX;
    public int gridSizeY;
    public int numberOfResults;
    public int currentResultNumber;
    public List<GameObject> resultsList = new List<GameObject>();

    public Vector3 originalScale;

    public float xPos = 0;
    public float yPos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = new Vector3(    gameObject.transform.GetChild(0).transform.localScale.x,
                                        gameObject.transform.GetChild(0).transform.localScale.y,
                                        gameObject.transform.GetChild(0).transform.localScale.z);
    }

    public void updateResults()
    {
        currentResultNumber = 0;

        resultsList = new List<GameObject>();

        foreach (Transform child in gameObject.transform)
        {            
            if (child.CompareTag("Video") && child.name.Contains(bartext.text))
            {
                child.gameObject.SetActive(true);
                resultsList.Add(child.gameObject);
                child.gameObject.transform.localScale = originalScale;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        
        numberOfResults = resultsList.Count;
        Debug.Log("numberOfResults : " + numberOfResults);
        
        gridSizeX = Mathf.CeilToInt((float)Math.Sqrt(numberOfResults));

        Debug.Log("gridSizeX : " + gridSizeX);

        gridSizeY = Mathf.CeilToInt((float)numberOfResults/(float)gridSizeX);        
        
        Debug.Log("gridSizeY : " + gridSizeY);      

        for (int y = 1; y <= gridSizeY; y++) 
        {
            for (int x = 1; x <= gridSizeX; x++)  
            {
                //Console.WriteLine(x);
                //Console.WriteLine(y);
                
                if(currentResultNumber < resultsList.Count)
                {                    
                    xPos = ((float)x/(float)(gridSizeX + 1)) -0.5f;                    
                    
                    yPos = 0.5f - ((float)y/(float)(gridSizeY + 1));
                    
                    resultsList[currentResultNumber].transform.localPosition = new Vector3(xPos, yPos, -0.004f);

                    resultsList[currentResultNumber].transform.localScale = originalScale / gridSizeX;

                    currentResultNumber += 1;
                }
                else
                {
                    continue;
                }
            }            
        }
        //videoResult.transform.position = new        
    }
}
