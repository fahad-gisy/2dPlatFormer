using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    public void TutorialPanelOnOff(bool setActive)
    {
        tutorialPanel.SetActive(setActive);
    }
}
