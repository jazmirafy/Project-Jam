using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource levelMusic;
    public bool startPlaying;
    public BeatScroller beatScroller;
    public static GameManager instance; //so i dont need to drag this script to each and every note thats hella work
    public int currentScore;
    public int scorePerNote;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;
    public float totalNotes, normalHits, goodHits, perfectHits, missedHits;
    public GameObject resultsScreen;
    public Text percentHitText, normalText, goodText, perfectText, missedText, rankText, finalScoreText;
    public int currentMultiplier; 
    public int multiplierTracker; //tracks your in a row hit streak
    public int[] multiplierThresholds; //the threshold of of in a row hits u need to level up your multiplyer
    public Text scoreText;
    public Text multiplierText;
    // Start is called before the first frame update


    void Start()
    {
        resultsScreen.SetActive(false);
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        totalNotes = FindObjectsOfType<NoteObject>().Length; //tally 
    }

    // Update is called once per frame
    void Update()
    {
        /*if the music hasnt started playing and the player clicks any button
        start the music and start the beat scroller and turn the bool variables tracking if music and beat
        scroller have started to tru*/
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.hasStarted = true;
                levelMusic.Play();
            }
        }
        else
        {
            //if the music isnt playing and the results screen isnt on rn
            if (!levelMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);
                normalText.text = normalHits.ToString();
                goodText.text = goodHits.ToString();
                perfectText.text = perfectHits.ToString();
                missedText.text = missedHits.ToString();
                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100;
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
    }
    //when  u hit a note up the score. score is determined by score per note and the score multiplier if the player has a streak of getting the hits correct
    //we want to reward players for getting hits in a row so we apply a multiplier to their score as a reward for their streak
    //basically this tracks your streak, and once u hit a certain amount in a row, the multiplier levels up
    public void NoteHit()
    {
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
    }   
}
