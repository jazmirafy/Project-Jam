using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource levelMusic;
    public bool startPlaying;
    //public BeatScroller beatScroller;
    public HealthManager healthManager;
    public static GameManager instance; 
    private int currentScore;
    public int scorePerNote, scorePerGoodNote = 125, scorePerPerfectNote = 150;
    private float totalNotes, normalHits, goodHits, perfectHits, missedHits;
    public float healAmount, damageAmount;
    public GameObject resultsScreen;
    private Text percentHitText, normalText, goodText, perfectText, missedText, rankText, finalScoreText;
    private int currentMultiplier, multiplierTracker; //tracks your in a row hit streak
    public int[] multiplierThresholds; //the threshold of of in a row hits u need to level up your multiplyer
    public Text scoreText, multiplierText;
    // Start is called before the first frame update
   // public float elapsedMusicTime;//the amount of time that has passed since the start of the music
   // public float musicStartTime; // record of the time since the music start
    public float[] transitionTimes; //when in the song to trigger the switch from attack to defend phase
    /*note to self for late but since the first transition will be us going to the defend phase, in our array all transition times with an 
    even index mean we need to transition to the defend phase and odd indexes are transition phase
    even meaning when you divide by two the remainder is zero
    this note to self is to help for when u decide if your gonna call the method to transition to defend phase or transition to the attack phase
    im thinking we do a transitionToAttack and transitionToDefend method to control which buttons are activates and what direction the 
    beat escroller moves the notes*/
    public float gameEndTime; //the end of the song

    public float currentTransitionTime; //the current time we are looking for a transition at
    public int transitionIndex; //the transition index we are currently at
    public GameObject tapButtons;
    public UIManager UIManager;
    public bool gameEnded = false;
    
    //public float tapButtonY = tapButtons.transform.position.y;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        resultsScreen.SetActive(false);
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        currentTransitionTime = transitionTimes[0]; //set the current transition time to the first transition time in the list at start


        Debug.Log("Is the music playing: " + levelMusic.isPlaying);
        Debug.Log("Is the results screen active in hierarchy: " + resultsScreen.activeInHierarchy);
        Debug.Log("Is the game paused: " + UIManager.isPaused);
    }

    // Update is called once per frame
    void Update()
    {

        

        if ( gameEnded && !resultsScreen.activeInHierarchy && !UIManager.isPaused)
            {
            Debug.Log("Is the results screen active in hierarchy: " + resultsScreen.activeInHierarchy);
            Debug.Log("Is the game paused: " + UIManager.isPaused);

                resultsScreen.SetActive(true);
                normalText.text = normalHits.ToString();
                goodText.text = goodHits.ToString();
                perfectText.text = perfectHits.ToString();
                missedText.text = missedHits.ToString();
                float totalHit = normalHits + goodHits + perfectHits;
                totalNotes = normalHits + goodHits + perfectHits + missedHits;
                float percentHit = totalHit / totalNotes * 100;
                percentHitText.text = percentHit.ToString("F1") + "%"; //f1 is there to only show one decimal place
                string rankValue = "F";
                if (percentHit > 90)
                {
                    rankValue = "A";
                }
                else if (percentHit > 80)
                {
                    rankValue = "B";
                }
                else if (percentHit > 70)
                {
                    rankValue = "C";
                }
                else if (percentHit > 60)
                {
                    rankValue = "D";
                }
                rankText.text = rankValue;
                finalScoreText.text = currentScore.ToString();
            }
        
    }
    //when  u hit a note up the score. score is determined by score per note and the score multiplier if the player has a streak of getting the hits correct
    //we want to reward players for getting hits in a row so we apply a multiplier to their score as a reward for their streak
    //basically this tracks your streak, and once u hit a certain amount in a row, the multiplier levels up
    public void NoteHit()
    {
        healthManager.RobotTakeDamage(damageAmount);
        healthManager.PlayerHeal(healAmount);
        
        Debug.Log("Hit on time yuhhhh");
        if (currentMultiplier - 1 < multiplierThresholds.Length) //making sure we stay in bounds of the array indices/dont go over
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
            // currentScore += scorePerNote * currentMultiplier; 
            scoreText.text = "Score: " + currentScore;
        }
        multiplierText.text = "Multiplier: x" + currentMultiplier;
        //give player more health and take robot health if they hit the good note
    }
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }
    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }
    //if u mess up your multiplier resets
    public void NoteMissed()
    {
        Debug.Log("Nauurrr missed note");
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
        Debug.Log("Multiplier has reset");
        missedHits++;
        healthManager.PlayerTakeDamage(damageAmount);
        healthManager.RobotHeal(healAmount);
    }   
}
