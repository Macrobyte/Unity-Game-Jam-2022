using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Hit { Normal, Good, Perfect}

    public static GameManager Instance { get; private set; }

    [Header("Gameplay")]
    [SerializeField] AudioSource music;
    [SerializeField] bool startPlaying;
    [SerializeField] float fallSpeed;

    [Header("Scores")]
    [SerializeField] int scorePerNote;
    [SerializeField] int scorePerGoodNote;
    [SerializeField] int scorePerPerfectNote;

    [Header("Score Multiplier")]
    [SerializeField] int[] multiplierThresholds;

    [Header("Pools")]
    public ObjectPool hitEffectPooler, GoodEffectBool, PefectEffectPool, MissEffectPool;

    [Header("Reference")]
    [SerializeField] NoteSpawner noteSpawner;

    [Header("Scoring")]
    [SerializeField] int score;
    [SerializeField] int currentMultiplier;
    [SerializeField] int multiplierTracker;

    [Header("Total Scoring")]
    [SerializeField] int totalNotes;
    [SerializeField] int normalHits;
    [SerializeField] int goodHits;
    [SerializeField] int perfectHits;
    [SerializeField] int missedHits;

    [SerializeField] bool gameOver;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentMultiplier = 1;

        GameUI.Instance.SetScoreUI(score.ToString());
        GameUI.Instance.ScoreMultiplierUI(multiplierTracker.ToString());
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                noteSpawner.ToggleStart(true);

                music.Play();
            }
        }
        else
        {
            if(!music.isPlaying && !gameOver)
            {
                gameOver = true;
                noteSpawner.ToggleStart(false);
                DisplayResultUI();
            }
        }
    }

    public void NoteHit(Hit hit)
    {
        EvaluiateScore(hit);
    }

    public void NoteMissed()
    {
        missedHits++;
        ResetMultiplier();
        Debug.Log("Missed");
    }

    public void EvaluiateScore(Hit hit)
    {
        switch (hit)
        {
            case Hit.Normal:
                NormalHit();
                break;
            case Hit.Good:
                GoodHit();
                break;
            case Hit.Perfect:
                PerfectHit();
                break;
            default:
                break;
        }

        GameUI.Instance.SetScoreUI(score.ToString());

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        GameUI.Instance.ScoreMultiplierUI(multiplierTracker.ToString());
    }


    public void ResetMultiplier()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;

        GameUI.Instance.ScoreMultiplierUI(multiplierTracker.ToString());
    }

    public void NormalHit()
    {
        score += scorePerNote * currentMultiplier;
        normalHits++;
    }

    public void GoodHit()
    {
        score += scorePerGoodNote * currentMultiplier;
        goodHits++;
    }

    public void PerfectHit()
    {
        score += scorePerPerfectNote * currentMultiplier;
        perfectHits++;
    }


    public void DisplayResultUI()
    {
        int totalHit = normalHits + goodHits + perfectHits;
        float percentHit = (float)totalHit / (float)totalNotes * 100f;

        GameUI.Instance.DisplayResult(normalHits,goodHits,perfectHits,missedHits,percentHit, CalculateGrade(percentHit), score);
    }


    public string CalculateGrade(float percentHit)
    {
        string rankValue = "F";

        if (percentHit > 40)
        {
            rankValue = "D";
            if (percentHit > 55)
            {
                rankValue = "C";
                if (percentHit > 70)
                {
                    rankValue = "B";
                    if (percentHit > 85)
                    {
                        rankValue = "A";
                        if (percentHit > 95)
                        {
                            rankValue = "S";
                        }
                    }
                }
            }
        }

        return rankValue;
    }

    public void AddNote()
    {
        totalNotes ++;
    }

    public float FallSpeed() => fallSpeed;
    public bool GameOver() => gameOver;
}
