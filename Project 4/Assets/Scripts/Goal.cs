using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Particle system to play when ball enters goal
    public ParticleSystem goalParticles;
    
    // Optional sound effect
    public AudioClip goalSound;
    
    // Optional reference to score UI text
    public TMPro.TextMeshProUGUI scoreText;
    
    // Score tracking
    private int score = 0;
    
    // Ball type variables
    private const string BASEBALL_TAG = "Baseball";
    private const string BEACHBALL_TAG = "Beachball";
    private const string BASEBALL_GOAL_TAG = "BaseballGoal";
    private const string BEACHBALL_GOAL_TAG = "BeachballGoal";
    
    // The type of ball this goal accepts
    private string acceptedBallTag;
    
    private void Start()
    {

        string goalTag = gameObject.tag;
        
        if (goalTag == BASEBALL_GOAL_TAG)
        {
            acceptedBallTag = BASEBALL_TAG;

        }
        else if (goalTag == BEACHBALL_GOAL_TAG)
        {
            acceptedBallTag = BEACHBALL_TAG;
        }
        else
        {
            acceptedBallTag = ""; 
        }
        
        // Initialize score display if available
        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrEmpty(acceptedBallTag))
            return;
            
        // Check if the entering object has the correct ball tag
        if (other.CompareTag(acceptedBallTag))
        {
            ProcessGoal(other.gameObject);
        }
        else if (other.CompareTag(BASEBALL_TAG) || other.CompareTag(BEACHBALL_TAG))
        {
            Debug.Log("Wrong ball type for this goal! This goal accepts: " + acceptedBallTag);
            
            // PlayWrongBallEffect();
        }
    }

    public int getScore(){
        return score;
    }
}