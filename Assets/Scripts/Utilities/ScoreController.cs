using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private float score = 0;

    void Start()
    {
        RefreshScoreUI();
        KeyController.KeyPickedUp += UpdateScore;
    }

    void UpdateScore()
    {
        score += 10;
        RefreshScoreUI();
    }

    void RefreshScoreUI() =>  scoreText.text = "Score: " + score;
}
