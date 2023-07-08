using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Health Bars")]
    [SerializeField] private SliderManager fighter1HealthBar;
    [SerializeField] private SliderManager fighter2HealthBar;

    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;


    void Update()
    {
        HandleUpdateTimer();

        HandleUpdateHealthBars();
    }

    void HandleUpdateHealthBars()
    {
        Fighter fighter1 = GameManager.Instance.fighter1;
        Fighter fighter2 = GameManager.Instance.fighter2;

        if (fighter1 == null || fighter2 == null)
            return;

        fighter1HealthBar.mainSlider.value = fighter1.GetHealthPercent();
        fighter2HealthBar.mainSlider.value = fighter2.GetHealthPercent();
    }

    void HandleUpdateTimer()
    {
        timerText.text = GameManager.Instance.timeRemainingSeconds.ToString("0");
    }

}
