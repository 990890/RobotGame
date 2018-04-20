using Complete;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    #region Static
    private static ScoreManager _instance;

    public static ScoreManager Instance {
        // singleton
        get {
            if (_instance == null)
                _instance = FindObjectOfType<ScoreManager>();
            return _instance;
        }
    }

    #endregion

    #region Fields

    // dictionary containing the score table
    private Dictionary<int, int> scores;
    
    // score change callback 
    public event Action<Dictionary<int,int>> OnScoreChange = delegate { };

    #endregion

    #region Methods

    private void Awake()
    {
        // init dictionary
        scores = new Dictionary<int, int>();
    }
    private void Start()
    {
        InitScores();
    }
    private void InitScores()
    {
        // find all the players in scene and order them by player number (crescent)
        TankMovement[] players = FindObjectsOfType<TankMovement>().OrderBy( x => x.m_PlayerNumber ).ToArray();

        // add all the players to the score table
        foreach (var item in players)
        {
            scores.Add(item.m_PlayerNumber, 0);
        }
        OnScoreChange(scores);
    }

    public void AddScore(int playerNumber) { 
        if (scores.ContainsKey(playerNumber) == false)
            return;

        // increment the score of the player
        scores[playerNumber]++;

        // call any functions linked to this event
        OnScoreChange(scores);
    }

    #endregion

}
