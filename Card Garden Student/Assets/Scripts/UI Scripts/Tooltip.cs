using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TMP_Text level;
    public TMP_Text nameText;
    public TMP_Text health;
    public TMP_Text attack;
    public TMP_Text attackSpeed;

    public RectTransform rectTransform;

    public bool followMouse = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (followMouse)
        {
            Vector2 position = Input.mousePosition;
            float pivotX = position.x / Screen.width;
            float pivotY = position.y / Screen.height;
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            transform.position = position;
        }
    }

    public void SetText(string _level, string _name, string _health, string _attack, string _attackspeed)
    {
        level.text = _level;
        nameText.text = _name;
        health.text = _health;
        attack.text = _attack;
        attackSpeed.text = _attackspeed;
    }


}
