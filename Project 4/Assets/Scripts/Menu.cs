using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene(0);
    }

    public void OnCredits()
    {
        SceneManager.LoadScene(4);
    }

    public void OnControls()
    {
        SceneManager.LoadScene(3);
    }
}
