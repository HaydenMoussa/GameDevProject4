using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    MusicPlayer player;
    public void OnPlay()
    {
        SceneManager.LoadScene(1);
        //player.SetVolume(.50f);
    }

    public void OnCredits()
    {
        SceneManager.LoadScene(4);
    }

    public void OnControls()
    {
        SceneManager.LoadScene(3);
    }

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Respawn");
        if (playerObject != null)
        {
            MusicPlayer player = playerObject.GetComponent<MusicPlayer>();
            // You can now use the player variable safely
        }
        else
        {
            // Handle the case where no object with the "Respawn" tag was found
            Debug.Log("No object with Respawn tag found!");
        }
         
    }
    public void MenuBack()
    {
        Debug.Log("Click back");
        SceneManager.LoadScene(0);
    }
}
