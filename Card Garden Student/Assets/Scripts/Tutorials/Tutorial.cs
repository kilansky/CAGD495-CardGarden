using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "New Tutorial", menuName = "Tutorial")]
public class Tutorial : ScriptableObject
{
    [Header("Core Components")]
    public string Title;
    [TextArea]
    public string tutorialText;
    public Sprite icon;

    [Header("Unused stats, tbd")]
    public bool hasBeenShown = false;
    public int tutorialIndex = 0;

}
