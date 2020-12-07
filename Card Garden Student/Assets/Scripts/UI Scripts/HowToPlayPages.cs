using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayPages : SingletonPattern<HowToPlayPages>
{
    public List<Sprite> pages = new List<Sprite>();
    public int index;
    public Image imageRef;

    private void OnEnable()
    {
        SetImage();
    }

    private void SetImage()
    {
        imageRef.sprite = pages[index];
    }

    public void NextImage(int nextValue)
    {
        index += nextValue;
        if (index > pages.Count - 1)
        {
            index = 0;
        }
        if (index < 0)
        {
            index = pages.Count - 1;
        }
        SetImage();
    }


}
