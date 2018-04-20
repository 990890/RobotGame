using Complete;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    #region Fields

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Text scoreboard;

    #endregion

    #region Methods

    private void Awake()
    {
        // linking to update timer event
        TimerController.Instance.OnTimerUpdate += UpdateTimer;

        // linking to score change event
        ScoreManager.Instance.OnScoreChange += UpdateScoreboard;

        // resetting texts
        timerText.text = string.Empty;
        scoreboard.text = string.Empty;
    }

    private void UpdateScoreboard(Dictionary<int, int> scores) {
        
        // free the scoreboard
        scoreboard.text = string.Empty;

        // output score table
        foreach (var item in scores)
        {
            scoreboard.text += "Player " + item.Key + ": " + item.Value;
            scoreboard.text += "\n";
        }
    }
    private void UpdateTimer(float timer){

        // converting to a timer notation
        var seconds = timer % 60;
        var minutes = Mathf.FloorToInt(timer / 60);

        // output timer value
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    #endregion

}
