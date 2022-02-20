using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("MissClick")]
    [SerializeField] int maxMissClick;

    [Header("Reference")]
    [SerializeField] NoteSpawner noteSpawner;

    [Header("Scoring")]
    [SerializeField] int score;
    [SerializeField] int currentMultiplier;
    [SerializeField] int multiplierTracker;

    [Header("Total Scoring")]
    [SerializeField] int currentMissClick;
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
        GameUI.Instance.ToggleIntroUI(true);

        currentMultiplier = 1;
        currentMissClick = maxMissClick;

        GameUI.Instance.SetScoreUI(score.ToString());
        GameUI.Instance.ScoreMultiplierUI(currentMultiplier.ToString());
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                GameUI.Instance.ToggleIntroUI(false);

                startPlaying = true;
                noteSpawner.ToggleStart(true);

                music.Play();
            }
        }
    }

    public void NoteHit(Hit hit)
    {
        ScreenShake();
        AddNote();
        EvaluiateScore(hit);

        if (currentMissClick < maxMissClick)
        {
            currentMissClick++;
            GameUI.Instance.SetMissClickBar((float)currentMissClick / (float)maxMissClick);
        }
    }

    public void NoteMissed()
    {
        ScreenShake();
        AddNote();
        missedHits++;
        ResetMultiplier();
        MissClick();
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

        GameUI.Instance.ScoreMultiplierUI(currentMultiplier.ToString());
    }


    public void ResetMultiplier()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;

        GameUI.Instance.ScoreMultiplierUI(currentMultiplier.ToString());
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

    public void MissClick()
    {
        currentMissClick--;
        GameUI.Instance.SetMissClickBar((float)currentMissClick / (float)maxMissClick);
        if (currentMissClick <= 0) GameOver();
    }

    public void ScreenShake()
    {
        CinemachineManager.Instance.ShakeCamera(5, .2f);
    }

    public void GameOver()
    {
        gameOver = true;
        noteSpawner.ToggleStart(false);
        DisplayResultUI();
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public float FallSpeed() => fallSpeed;
    public bool IsGameOver() => gameOver;
}
