using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
     List<Item> inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(Item itemName){
        inventory.Add(itemName);
        Debug.Log("adding: " + itemName);

    }

    public void RemoveItem(Item itemName){
        inventory.Remove(itemName);

    }

    public bool HasItem(Item item){
        if(inventory.Contains(item)){
            return true;
            }
        return false;
    }
}
