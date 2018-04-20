using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TimerController : MonoBehaviour {

    #region Static

    private static TimerController _instance;
    public static TimerController Instance
    {
        // singleton
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<TimerController>();
            return _instance;
        }
    }
    #endregion

    #region Fields

    // actual game timer
    [SerializeField]
    private float timer = 120;

    // timer update callback 
    public event Action<float> OnTimerUpdate = delegate { };
    #endregion

    #region Methods

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer() {

        timer -= Time.deltaTime;

        if (timer <= 0) {
            timer = 0;

            // reload current scene
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        
        // execute any functions linked to this callback
        OnTimerUpdate(timer);
    }
    #endregion

}
