using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    
    public ParticleSystem goalParticles;
    public AudioClip goalSound;
    
    public string ballTag = "CanPickUp";
    
    private int score = 0;

    public Timer timer;
    
    public TMPro.TextMeshProUGUI scoreText;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ballTag))
        {
            score++;
            
            timer.Reset();

            if (!timer.active) {
                timer.SetActive(true);
            }

            // Update score text if assigned
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
            
            // Play particle effect if assigned
            if (goalParticles != null)
            {
                // Play at the ball's position
                goalParticles.transform.position = other.transform.position;
                goalParticles.Play();
            }
            else
            {
                Debug.LogWarning("No particle system assigned to Goal script!");
            }
            
            // Play sound if assigned
            if (goalSound != null && GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().PlayOneShot(goalSound);
            }
            
            // Destroy the ball
            Destroy(other.gameObject);
            
            Debug.Log("Goal! Score is now: " + score);
        }
    }

    public int getScore(){
        return score;
    }
}