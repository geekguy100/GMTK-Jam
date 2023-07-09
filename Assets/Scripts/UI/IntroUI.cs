using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IntroUI : MonoBehaviour
{
    Animator introAnimator => GetComponent<Animator>();

    public void OnIntroStart()
    {
        // TODO
        Debug.Log("Intro Started");
    }

    public void OnIntroFinished()
    {
        // Start the game after the Intro
        GameManager.Instance.StartGame();

        // Disable the intro animator 
        introAnimator.gameObject.SetActive(false);
    }

}
