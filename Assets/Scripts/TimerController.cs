using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {


    #region Fields

    [SerializeField]
    private float timer = 120;

    [SerializeField]
    private Text timerText;
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
            Time.timeScale = 0;
        }
        var seconds = timer % 60;
        var minutes = Mathf.FloorToInt(timer / 60);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

    }
    #endregion

}
