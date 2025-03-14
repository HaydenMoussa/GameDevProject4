using System.Collections;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [Header("Goal Tracking")]
    [SerializeField] private bool autoFindGoals = true;
    [SerializeField] private Goal[] manualGoalReferences;
    [SerializeField] private TextMeshProUGUI totalScoreText;

    private List<Goal> activeGoals = new List<Goal>();
    private int cachedTotalScore = 0;


    void Awake() {
        if(Instance != null)
            Destroy(Instance);
        else
            Instance = this;
        DontDestroyOnLoad(this);
    }
    private Inventory inventory;
    
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find goals in the newly loaded scene
        if (autoFindGoals)
        {
            FindAllGoals();
        }
    }

public void StartDialogue(string[] dialogue, int startPosition, string name)
    {
        nameText.text = name + "...";
        dialoguePanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue, startPosition));
    }

    IEnumerator RunDialogue(string[] dialogue, int startPosition)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();

        for(int i = startPosition; i < dialogue.Length; i++)
        {
            //dialogueText.text = dialogue[i];
            dialogueText.text = null;
            StartCoroutine(TypeTextUncapped(dialogue[i]));

            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialoguePanel.SetActive(false);
        dialogueText.text = null;
        nameText.text = null;
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        StartCoroutine(TypeTextUncapped(dialogue));
        dialoguePanel.SetActive(true);
    }

    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        dialoguePanel.SetActive(false);
    }

float charactersPerSecond = 90;

IEnumerator TypeTextUncapped(string line)
{
    float timer = 0;
    float interval = 1 / charactersPerSecond;
    string textBuffer = null;
    char[] chars = line.ToCharArray();
    int i = 0;

    while (i < chars.Length)
    {
        if (timer < Time.deltaTime)
        {
            textBuffer += chars[i];
            dialogueText.text = textBuffer;
            timer += interval;
            i++;
        }
        else
        {
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}

    public void GameOver() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        
        
        PlayerCam[] playerCams = FindObjectsByType<PlayerCam>(FindObjectsSortMode.None);
        foreach (PlayerCam cam in playerCams) {
            cam.enabled = false;
        }
        
        Initiate.Fade("GameOver", Color.black, 2f);
    }
    void Start()
    {
        dialoguePanel.SetActive(false);

        // Initialize goals list
        if (autoFindGoals)
        {
            FindAllGoals();
        }
        else if (manualGoalReferences.Length > 0)
        {
            activeGoals.AddRange(manualGoalReferences);
        }
        
        StartCoroutine(TrackScoresRoutine());


    }

    private void FindAllGoals()
    {
        activeGoals.Clear();
        Goal[] foundGoals = FindObjectsByType<Goal>(FindObjectsSortMode.None);
        activeGoals.AddRange(foundGoals);
        Debug.Log($"Found {foundGoals.Length} goal objects in the scene");
    }
    
    private IEnumerator TrackScoresRoutine()
    {
        while (true)
        {
            // Check if total score has changed
            int newTotalScore = CalculateTotalScore();
            
            if (newTotalScore != cachedTotalScore)
            {
                cachedTotalScore = newTotalScore;
                
                // Update UI if assigned
                if (totalScoreText != null)
                {
                    totalScoreText.text = "Total Score: " + cachedTotalScore;
                }
                
                //Debug.Log("Total score across all goals: " + cachedTotalScore);
            }
            
            yield return new WaitForSeconds(0.5f); 
        }
    }
    
    private int CalculateTotalScore()
    {
        int total = 0;
        
        // Remove any null references (destroyed goals)
        activeGoals.RemoveAll(goal => goal == null);
        
        foreach (Goal goal in activeGoals)
        {
            total += goal.getScore();
        }
        
        return total;
    }
    
    // Public method for other scripts to get the total score
    public int GetTotalScore()
    {
        return cachedTotalScore;
    }
    
    // Public method to manually add a goal to tracking
    public void RegisterGoal(Goal goal)
    {
        if (!activeGoals.Contains(goal))
        {
            activeGoals.Add(goal);
        }
    }

    void Update()
    {
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}