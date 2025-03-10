using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnItems : MonoBehaviour
{
    public List<GameObject> items;
    private int prevScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.getScore() - prevScore == 5){
            prevScore = GameManager.Instance.getScore();
            Debug.Log("Spawning Items");
            Spawn();
        }
        
    }
    //z max 4 min -60
    //x max 70 min 43
    //y 6
    //Random.Range(0.6f, 1.6f), Random.Range(2.4f, 2.7f)

    void Spawn()
    {
        Debug.Log("Trying to spawn");
        for(int i  = 0; i < 5; i++){ 
        var position = new Vector3(Random.Range(-13.0f, 14.0f), 2, Random.Range(-10.0f, 4.0f)); // source 1
        Instantiate(items[i], position, Quaternion.identity);
        Debug.Log("Spawned " + items[i].name + " at " + position);
       
        }

    }
}
