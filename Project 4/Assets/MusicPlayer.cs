using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    GameObject self;
    AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    self = gameObject;
    DontDestroyOnLoad(self);
    audio = self.GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume) {
        audio.volume = volume;
    }

}
