using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] bool firstInteraction = true;

    public bool caffeinated = false;
    public int repeatStartPosition;

    public Inventory inv;

    public string npcName;
    public DialogueAsset dialogueAsset;

    [HideInInspector]
    public int StartPosition {
        get
        {
            if (firstInteraction)
            {
                firstInteraction = false;
                return 0;
            }
            else
            {
                return repeatStartPosition;
            }
        }
    }
    public void ChangeDialogue(DialogueAsset d){
        dialogueAsset = d;
        caffeinated = true;
    }

    public bool checkCaffeinated(){
        return caffeinated;
    }

}