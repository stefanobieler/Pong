using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private TMP_Text textScorePlayer1;
    [SerializeField] private TMP_Text textScorePlayer2;


    private void Start(){
        textScorePlayer1.text = $"Player 1: 0";
        textScorePlayer2.text = $"Player 2: 0";
    }
    private void OnEnable()
    {
        GameManager.UpdatedScore += OnUpdatedScore;
    }

    private void OnUpdatedScore(Goal.GoalTeam gt, int newScore)
    {
        if(gt == Goal.GoalTeam.Team1){
            textScorePlayer2.text = $"Player 2: {newScore}";
        }else if(gt == Goal.GoalTeam.Team2){
            textScorePlayer1.text = $"Player 1: {newScore}";   
        }
    }

    private void OnDisable()
    {

        GameManager.UpdatedScore -= OnUpdatedScore;
    }

}
