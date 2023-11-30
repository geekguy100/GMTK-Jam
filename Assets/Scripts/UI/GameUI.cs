using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        bool fighterOneNull = fighter1 == null;
        bool fighterTwoNull = fighter2 == null;
        bool anyFighterDead = fighterOneNull || fighterTwoNull;

        if (fighterOneNull)
        {
            fighter1HealthBar.mainSlider.value = 0;
        }

        if (fighterTwoNull)
        {
            fighter2HealthBar.mainSlider.value = 0;
        }

        if (anyFighterDead)
        {
            return;
        }

        fighter1HealthBar.mainSlider.value = fighter1.GetHealthPercent();
        fighter2HealthBar.mainSlider.value = fighter2.GetHealthPercent();
    }

    void HandleUpdateTimer()
    {
        timerText.text = GameManager.Instance.timeRemainingSeconds.ToString("0");
    }

}
