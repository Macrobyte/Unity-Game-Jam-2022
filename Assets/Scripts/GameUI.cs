using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    [Header("Gameplay")]
    [SerializeField] GameObject introUI;
    [SerializeField] TMP_Text scoreDisplay;
    [SerializeField] TMP_Text multiplierDisplay;
    [SerializeField] Image missClickBar;

    [Header("Result")]
    [SerializeField] GameObject resultUI;
    [SerializeField] TMP_Text normalHitsDisplay;
    [SerializeField] TMP_Text goodHitsDisplay;
    [SerializeField] TMP_Text perfectHitsDisplay;
    [SerializeField] TMP_Text missedHitsDisplay;
    [SerializeField] TMP_Text percentageHitsDisplay;
    [SerializeField] TMP_Text rankedValueDisplay;
    [SerializeField] TMP_Text finalScoreDisplay;

    private void Awake() => Instance = this;

    public void ToggleIntroUI(bool newstate)
    {
        introUI.SetActive(newstate);
    }

    public void SetScoreUI(string value)
    {
        scoreDisplay.text = "Score: " + value;
    }

    public void ScoreMultiplierUI(string value)
    {
        multiplierDisplay.text = "Multiplier: " + value;
    }

    public void SetMissClickBar(float newValue)
    {
        if (newValue >= 0.7f && newValue <= 1f)
        {
            missClickBar.color = Color.green;
        }
        else if (newValue > 0.3f && newValue < 0.7f)
        {
            missClickBar.color = Color.yellow;
        }
        else if (newValue >= 0 && newValue < 0.3f)
        {
            missClickBar.color = Color.red;
        }

        missClickBar.fillAmount = newValue;
    }

    public void DisplayResult(int normalHit, int goodHit, int perfectHit, int missedHit, float percentageHit, string rankedValueHit, int finalScoreHit)
    {
        normalHitsDisplay.text = normalHit.ToString();
        goodHitsDisplay.text = goodHit.ToString();
        perfectHitsDisplay.text = perfectHit.ToString();
        missedHitsDisplay.text = missedHit.ToString();
        percentageHitsDisplay.text = percentageHit.ToString("F1") + "%";
        rankedValueDisplay.text = rankedValueHit;
        finalScoreDisplay.text = finalScoreHit.ToString();

        resultUI.SetActive(true);
    }
}
