using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor;
using UnityEngine;

public class handController : MonoBehaviour
{

    public GameObject playerCamera;
    public bool movingHand;

    public bool falling;

    public double cameraRotationY;

    public double cameraRotationX;
    
    //public double cameraRotationYLocal;

    //public double cameraRotationXLocal;

    public double handPositionX;

    public double handPositionZ;

    public double handHeight;

    public bool keyHit;

    public bool keyHitBottom;
    // Start is called before the first frame update
    void Start()
    {
        movingHand = true;
        keyHit = false;
        handHeight = 0.577f;
        keyHitBottom = false;
    }

    // Update is called once per frame
    void Update()
    {
        ///*
        if(Input.GetMouseButtonDown(0))
        {
            movingHand = false;
            falling = true;   
            keyHitBottom = false;      
        }
        if(movingHand == false)
        {
            if (keyHitBottom == true || (handHeight <= 0.425f && falling == true && keyHit == false))
            {
                falling = false;
                keyHit = false;
                keyHitBottom = false;
            }   
            else if (falling == true && handHeight > 0.425f)
            {
                handHeight -= 0.002f;
                gameObject.transform.position = new Vector3((float)handPositionX, (float)handHeight, (float)handPositionZ);
            } 
            else if (falling == false && handHeight < 0.577f)
            {
                handHeight += 0.002f;
                gameObject.transform.position = new Vector3((float)handPositionX, (float)handHeight, (float)handPositionZ);
            }         
            else if (falling == false && keyHit == false)
            {
                movingHand = true;
            }            
        }
        if(movingHand == true)
        {
            cameraRotationY = playerCamera.transform.eulerAngles.y;
            cameraRotationX = playerCamera.transform.eulerAngles.x;
            //cameraRotationYLocal = playerCamera.transform.localEulerAngles.y;
            //cameraRotationXLocal = playerCamera.transform.localEulerAngles.x;

            if(cameraRotationY >= 0 && cameraRotationY <= 180)
            {
                handPositionX = 1.1182 + ((1.8104 - 1.1182) * ((cameraRotationY + 22.5) / 45));
            }
            else
            {
                //handPositionX = 1.4643 - ((1.8104 - 1.1182) * ((cameraRotationY - 360) / 45));
                handPositionX = 1.4643 - ((1.8104 - 1.1182) * ((360 - cameraRotationY) / 45));
                //handPositionX = 1.4643 - ((1.8104 - 1.1182) * (45/ (cameraRotationY - 360)));
            }

            if(cameraRotationX < 35)
            {
                handPositionZ = 1.3023;
            }
            else if (cameraRotationX > 45)
            {
                handPositionZ = 1.1077;
            }
            else
            {
                handPositionZ = 1.3023 - ((1.3023 - 1.1077) * ((cameraRotationX - 35)/10));
            }
            
            if(handPositionX > 0)
            {
                gameObject.transform.position = new Vector3 (   
                                                            gameObject.transform.position.x + (((float)handPositionX - gameObject.transform.position.x)/10), 
                                                            0.577f, 
                                                            gameObject.transform.position.z + (((float)handPositionZ - gameObject.transform.position.z)/10)
                                                        );
            }
            
        }
        //camera y rotation between -22.5 (left) and 22.5 (right), x is between 35 (top) and 50 (bottom)
        //hand left and right motion is between x 1.1182 (left) and 1.8104 (right)
        //hand up and down motion is between z 1.3023 (top) and 1.1077 (bottom)
        
        //gameObject.transform.position = new Vector3(x, y, z);
        //print(5);
        //Debug.Log(gameObject.transform.position);
        //*/
    }
}
