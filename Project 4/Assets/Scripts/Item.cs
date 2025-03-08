using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemName;
    private Rigidbody itemBody;
    public GameObject goal;

    public bool isHeld = false;

    private bool isIn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemBody = GetComponent<Rigidbody>();
        itemName = GetComponent<GameObject>().ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(itemBody.position.x - goal.transform.position.x) <= 1 && Mathf.Abs(itemBody.position.y - goal.transform.position.y) <= 1 && Mathf.Abs(itemBody.position.z - goal.transform.position.z) <= 1){
            isIn = true;
        }
       
    }



//not sure how to structure this part but it's not my job lol
    private void Throw(){
        if(isHeld && !isIn){
            itemBody.AddForce(new Vector3(5,0,0), ForceMode.Force);
            isHeld = false;
        }
    }

    
    
}
