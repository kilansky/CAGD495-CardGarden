using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialUI : SingletonPattern<TutorialUI>
{
    [Header("UI components")]
    public GameObject tutPanel;
    public TMP_Text tutName;
    public TMP_Text tutText;
    public Image tutIcon;

    [Header("Tutorials")]
    public Tutorial currentTutorial;
    public List<Tutorial> tutorials;
    public int tutorialIndex = 0;
    public bool dontShowAgain = false;

    private void Start()
    {
        currentTutorial = tutorials[0];
    }

    public void LoadTutorial()
    {
        tutName.text = currentTutorial.name;
        tutText.text = currentTutorial.tutorialText;
        tutIcon.sprite = currentTutorial.icon;
        // ShowPopUp();
    }
    public void LoadTutorial(Tutorial newTut)
    {
        tutName.text = newTut.name;
        tutText.text = newTut.tutorialText;
        tutIcon.sprite = newTut.icon;
        ShowPopUp();
    }
    public void LoadTutorial(int index)
    {
        tutName.text = tutorials[index].name;
        tutText.text = tutorials[index].tutorialText;
        tutIcon.sprite = tutorials[index].icon;
        ShowPopUp();
    }

    public void NextTutorial(int nextIndex)
    {
        tutorialIndex += nextIndex;
        if (tutorialIndex < 0)
        {
            tutorialIndex = tutorials.Count - 1;
        }
        if (tutorialIndex >= tutorials.Count)
        {
            tutorialIndex = 0;
        }
        LoadTutorial(tutorialIndex);
    }

    public void ShowPopUp()
    {
        tutPanel.SetActive(true);
    }
    public void HidePopUp()
    {
        tutPanel.SetActive(false);
        if (dontShowAgain)
        {
            tutorials.RemoveAt(tutorialIndex);
            dontShowAgain = false;
        }
    }

    public void SetDontShow(bool value)
    {
        dontShowAgain = value;
    }
}
