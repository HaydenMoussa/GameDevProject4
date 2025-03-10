using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Timer : MonoBehaviour
{
    public bool active;
    public float time;
    float maxTime;
    GameManager manager;
    public Camera camera;
    public Volume globalVolume;
    VolumeProfile volume;
    Vignette vignette;
    float baseIntensity;
    bool pause;
    bool over;
    public TextMeshProUGUI timeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        over = false;
        pause = false;
        maxTime = time;
        active = false;
        manager = camera.GetComponent<GameManager>();
        volume = globalVolume.profile;
        volume.TryGet<Vignette>(out vignette);
        baseIntensity = (float)vignette.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            Tick();
            timeText.text = ((int)time).ToString();
        }
        ChangeVignette();
    }

    public void Reset() {
        pause = true;
        time = maxTime;
    }

    public void SetActive(bool state) {
        active = state;
        timeText.text = time.ToString();
    }

    void Tick() {
        if (time > 0) {
            if (!pause){
            time -= Time.deltaTime;}
            else {
                pause = false;
            }
        }
        else {
            if (!over){
                over = true;
            manager.GameOver();}

        }
    }

    void ChangeVignette() {
        float offset = 1 - (time/maxTime);
        vignette.intensity.Override(baseIntensity + offset*(1-baseIntensity));
        //Debug.Log("timer is " + time + ", offset is " + offset + ", adding " + offset*(1-baseIntensity) + " to vignette");
    }
}
