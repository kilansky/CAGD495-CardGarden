using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSystem : SingletonPattern<TooltipSystem>
{
    public static float timer;
    public Tooltip tooltip;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                Hide();
            }
        }

    }

    public static void Show(string _level, string _name, string _health, string _attack, string _attackspeed)
    {
        Instance.tooltip.SetText(_level, _name, _health, _attack, _attackspeed);
        Instance.tooltip.gameObject.SetActive(true);
        AddTime(5.0f);
    }
    public static void AddTime(float timeToAdd)
    {
        timer += timeToAdd;
    }
    public static void Hide()
    {
        Instance.tooltip.gameObject.SetActive(false);
    }
}
