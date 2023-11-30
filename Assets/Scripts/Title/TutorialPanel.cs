using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private TutorialPanel previousPanel;
    [SerializeField] private TutorialPanel nextPanel;


    public void ShowPreviousPanel()
    {
        if (previousPanel != null)
        {
            previousPanel.gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    public void ShowNextPanel()
    {
        if (nextPanel != null)
        {
            nextPanel.gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }

}
