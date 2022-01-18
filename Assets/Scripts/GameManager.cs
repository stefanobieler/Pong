using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public int ScorePlayer1 { get; private set; }
    public int ScorePlayer2 { get; private set; }
    public static event Action ResetPuck;
    public static event Action<Goal.GoalTeam, int> UpdatedScore;

    private void Awake()
    {
        if (gm != null) { Destroy(this); }

        gm = this;
        DontDestroyOnLoad(gm);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HostGame(string ipAddress){

    }

    public void JoinGame(){

    }


    private void OnEnable()
    {
        Goal.ScoreEvent += OnScore;
    }

    public void Start()
    {
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;
    }

    private void OnScore(Goal.GoalTeam gt)
    {

        switch (gt)
        {
            case Goal.GoalTeam.Team1:
                Debug.Log("Player 2 scored");
                break;
            case Goal.GoalTeam.Team2:
                Debug.Log("Player 1 scored");
                break;
            default:
                break;
        }
        UpdateScoreboard(gt);
        StartCoroutine(SignalResetPuck(1.0f));
    }

    private void UpdateScoreboard(Goal.GoalTeam gt)
    {
        if(gt == Goal.GoalTeam.Team1){
            ScorePlayer2 += 1;
            UpdatedScore?.Invoke(gt, ScorePlayer2);
        }else if(gt == Goal.GoalTeam.Team2){
            ScorePlayer1 += 1;
            UpdatedScore?.Invoke(gt, ScorePlayer1);
        }
    }

    private void OnDisable()
    {
        Goal.ScoreEvent -= OnScore;
    }


    private IEnumerator SignalResetPuck(float delay)
    {

        yield return new WaitForSeconds(delay);
        ResetPuck?.Invoke();
    }




}
