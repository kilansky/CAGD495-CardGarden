using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMasterToggle : MonoBehaviour
{
    public GameObject UIParent;
    private bool isHidden = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            if (isHidden)
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }
    }

    private void HideUI()
    {
        UIParent.SetActive(false);
        isHidden = true;
    }
    private void ShowUI()
    {
        UIParent.SetActive(true);
        isHidden = false;
    }

}
