using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStarter : MonoBehaviour
{
    [SerializeField] private GameObject tutorialParent;
    [SerializeField] private TutorialPanel startPanel;


    public void BeginTutorial()
    {
        tutorialParent.SetActive(true);

        startPanel.gameObject.SetActive(true);
    }

    public void EndTutorial()
    {
        foreach (Transform child in tutorialParent.transform)
        {
            child.gameObject.SetActive(false);
        }
        tutorialParent.SetActive(false);
    }

}
