using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public ParticleSystem goalParticles;
    public AudioClip goalSound;
    
    // Default tag for backward compatibility
    public string ballTag = "CanPickUp";
    
    // We'll use this to determine if we need to check for specific ball types
    private bool useSpecificBallType = false;
    private string expectedBallTag = "";
    
    private int score = 0;
    public Timer timer;
    
    public TMPro.TextMeshProUGUI scoreText;
    
    private void Start()
    {
 
        if (gameObject.CompareTag("BaseballGoal"))
        {
            useSpecificBallType = true;
            expectedBallTag = "Baseball";
        }
        else if (gameObject.CompareTag("BeachballGoal"))
        {
            useSpecificBallType = true;
            expectedBallTag = "Beachball";
        }
        else
        {
            useSpecificBallType = false;
            Debug.Log("This is a standard goal. Will accept any object with tag: " + ballTag);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (useSpecificBallType)
        {
            if (other.CompareTag(expectedBallTag))
            {
                ScoreGoal(other);
            }
            else
            {
                Debug.Log("Wrong ball type for this goal!");
            }
        }
        else
        {
            if (other.CompareTag(ballTag))
            {
                ScoreGoal(other);
            }
        }
    }
    
    private void ScoreGoal(Collider ball)
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
            goalParticles.transform.position = ball.transform.position;
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
        Destroy(ball.gameObject);
        
        Debug.Log("Goal! Score is now: " + score);
    }
    
    public int getScore(){
        return score;
    }
}