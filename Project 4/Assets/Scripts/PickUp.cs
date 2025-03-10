using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //base pick up script https://github.com/JonDevTutorial/PickUpTutorial/blob/main/PickUpScript.cs
    public GameObject player;
    public Transform holdPos;
    public float throwForce = 500f;
    public float pickUpRange = 5f;
    private float rotationSensitivity = 1f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool canDrop = true;
    private int LayerNumber;
    private int originalLayer;
    
    // Add offset to raycast to avoid hitting player
    public float raycastOffset = 0.5f;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("E key pressed. Currently holding: " + (heldObj != null ? heldObj.name : "nothing"));
            
            if (heldObj == null)
            {
                RaycastHit hit;
                // Offset the raycast origin to avoid hitting player
                Vector3 rayOrigin = transform.position + transform.forward * raycastOffset;
                
                Debug.DrawRay(rayOrigin, transform.forward * pickUpRange, Color.red, 1.0f);
                
                if (Physics.Raycast(rayOrigin, transform.forward, out hit, pickUpRange))
                {
                    
                    if (hit.transform.gameObject.tag == "Baseball" || hit.transform.gameObject.tag == "Beachball")
                    {
                        //Debug.Log("Object has CanPickUp tag - attempting pickup");
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if(canDrop == true)
                {
                    //Debug.Log("Dropping object: " + heldObj.name);
                    StopClipping();
                    DropObject();
                }
            }
        }
        
        if (heldObj != null)
        {
            MoveObject();
            RotateObject();
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true)
            {
                //Debug.Log("Throwing object: " + heldObj.name);
                StopClipping();
                ThrowObject();
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        //Debug.Log("Starting PickUpObject for: " + pickUpObj.name);
        
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            //Debug.Log("Object has Rigidbody - proceeding with pickup");
            
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            
            // Store the original layer before changing it
            originalLayer = heldObj.layer;
            //Debug.Log("Stored original layer: " + LayerMask.LayerToName(originalLayer));
            
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;
            heldObj.layer = LayerNumber;
            //Debug.Log("Changed to holdLayer: " + LayerNumber);
            
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            //Debug.Log("Pickup complete for: " + pickUpObj.name);
        }
        else
        {
            //Debug.LogError("Object has no Rigidbody - cannot pick up: " + pickUpObj.name);
        }
    }

    void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        
        //Debug.Log("Restoring object from layer " + heldObj.layer + " to original layer " + originalLayer);
        heldObj.layer = originalLayer;
        
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;
    }

    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        
        //Debug.Log("Restoring object from layer " + heldObj.layer + " to original layer " + originalLayer);
        heldObj.layer = originalLayer;
        
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }

    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))
        {
            canDrop = false;
            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            heldObj.transform.Rotate(Vector3.down, XaxisRotation);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
            canDrop = true;
        }
    }

    void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}