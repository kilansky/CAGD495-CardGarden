using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : SingletonPattern<GameUI>
{
    [Header("Text Components")]
    [SerializeField]
    private Text goldText = null;
    [SerializeField]
    private Text incomeText = null;
    private bool showIncome = false;

    [Header("Pop up")]
    // used for tutorial, new cards, etc
    [SerializeField]
    private GameObject popUpPanel = null;
    [SerializeField]
    private Text puPanelText = null;

    // Temorarily updating every frame, change to less frequently to optimize
    private void Update()
    {
        RefreshPlayerStatsUI();
    }


    private void RefreshPlayerStatsUI()
    {
        goldText.text = PlayerStats.Instance.PlayerGold.ToString();
        IncomeColorCheck();
        incomeText.text = "(+" + PlayerStats.Instance.GoldIncome + ")";
    }

    private void OpenPU(string puText)
    {
        puPanelText.text = puText;
        popUpPanel.SetActive(true);
    }
    public void ClosePU()
    {
        popUpPanel.SetActive(false);
        // stop coroutine that has it open/close automatically
    }

    // Add ienuerator with timer to open a popup and make it say something for a brief period. Need it to be able to close itself.

    // Composite Income Text
    private string CompositeGoldText()
    {
        if (showIncome)
        {
            return PlayerStats.Instance.PlayerGold.ToString() + " (+" + PlayerStats.Instance.GoldIncome + ")";
        }
        else
        {
            return PlayerStats.Instance.PlayerGold.ToString();
        }
    }

    // Change Color of income depending on how good it is
    private void IncomeColorCheck()
    {
        float income = PlayerStats.Instance.GoldIncome;
        if (income == 0)
        {
            showIncome = false;
        }
        else if (income < 0f)
        {
            showIncome = true;
            incomeText.color = Color.red;
        }
        else if (income <= 5f)
        {
            showIncome = true;
            incomeText.color = Color.white;
        }
        else if (income < 10f)
        {
            showIncome = true;
            incomeText.color = Color.yellow;
        }
        else if (income < 25f)
        {
            showIncome = true;
            incomeText.color = Color.green;
        }
        else if (income < 50f)
        {
            showIncome = true;
            incomeText.color = Color.black;
        }
        if (showIncome)
        {
            incomeText.gameObject.SetActive(true);
        }
        else
        {
            incomeText.gameObject.SetActive(false);
        }
    }
}
