using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDrawTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeToDraw;
    private float elapsedTime = 0;
    public Image drawTimer;

    void Start()
    {
        timeToDraw = CardManager.Instance.timeToDraw;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        //Check to see if enough time has elapsed in order to draw
        if(elapsedTime > timeToDraw)
        {
            timerText.text = "0";
            
            //Check if hand is less than capacity
            if (CardManager.Instance.hand.Count < CardManager.Instance.maxHandSize)
            {
                CardManager.Instance.DrawCard();
                elapsedTime = 0;

                timerText.text = timeToDraw.ToString("0");
            }
        }
        else
        {
            timerText.text = Mathf.Ceil(timeToDraw - elapsedTime).ToString("0");
        }
        drawTimer.fillAmount = elapsedTime / timeToDraw;
    }
}
