using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPressMove : MonoBehaviour
{
    public GameObject fingerTip;

    public handController handScript;
    public textInput searchText;

    public bool thisKeyHit;
    public float originalYPosition;

    public bool keyReturning;

    public double triggerDistance;

    public float fingerTipHeight;

    public float thisKeyHeight;

    public float distanceBetween;

    public string thisCharacter;

    // Start is called before the first frame update
    void Start()
    {
        triggerDistance = 0.03;
        thisKeyHit = false;
        originalYPosition = gameObject.transform.position.y;
        keyReturning = false;
        distanceBetween = Vector3.Distance(fingerTip.transform.position, gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceBetween = Vector3.Distance(fingerTip.transform.position, gameObject.transform.position);
        fingerTipHeight = fingerTip.transform.position.y;
        thisKeyHeight = gameObject.transform.position.y;
        if (gameObject.transform.position.y <= originalYPosition - 0.02 && handScript.falling == true)
        {
            handScript.keyHitBottom = true;
            keyReturning = true;
            Debug.Log(1);
        }
        else if  (
                Vector3.Distance(fingerTip.transform.position, gameObject.transform.position) < triggerDistance 
                && 
                handScript.falling == true 
                && 
                thisKeyHit == false
            )
        {
            thisKeyHit = true;
            handScript.keyHit = true;
            Debug.Log(2);
            searchText.ControlSearchBar(thisCharacter);
        }
        else if (   
                    handScript.falling == true 
                    && 
                    thisKeyHit == true
                    &&
                    (fingerTip.transform.position.y - gameObject.transform.position.y) < 0.007f
                )
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, fingerTip.transform.position.y - 0.0062103f, gameObject.transform.position.z);
            Debug.Log(3);
        }
        else if(handScript.falling == false && keyReturning == true && gameObject.transform.position.y >= originalYPosition && thisKeyHit == true)
        {
            keyReturning = false;
            thisKeyHit = false;
            Debug.Log(4);
        }
        else if(handScript.falling == false && keyReturning == true && gameObject.transform.position.y < originalYPosition && thisKeyHit == true)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.001f, gameObject.transform.position.z);
            Debug.Log(5);
        }
    }
}
