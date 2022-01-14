using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public enum GoalTeam {
        Team1,
        Team2
    }

    [SerializeField] private GoalTeam team = GoalTeam.Team1;


    public static event Action<GoalTeam> ScoreEvent;

    private void OnTriggerEnter2D(Collider2D col){

        if(col.gameObject.layer == LayerMask.NameToLayer("Puck")){
            ScoreEvent?.Invoke(team);
        }

    }
}
