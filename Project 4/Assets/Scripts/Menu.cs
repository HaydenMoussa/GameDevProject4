using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    MusicPlayer player;
    public void OnPlay()
    {
        SceneManager.LoadScene(0);
        player.SetVolume(.50f);
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
        player = GameObject.FindWithTag("Respawn").GetComponent<MusicPlayer>();   
    }
}
