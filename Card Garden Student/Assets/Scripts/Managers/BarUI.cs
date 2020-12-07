using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
	Quaternion rot;
	float barSpeed;
	public Slider healthSlider, xpSlider;
	public Text level;
	
	void Start()
	{
		if(barSpeed == 0)
		{
			barSpeed = 100;
		}
		rot = transform.GetChild(0).rotation;
		StartCoroutine(fixedRotation());
	}
	
	public void SetHealth(float health)
	{
		if(gameObject.activeInHierarchy)
		{
			StartCoroutine(healthLerp(healthSlider.value, health));
		}
	}
	
	public void SetMaxHealth(float health)
	{
		healthSlider.maxValue = health;
		healthSlider.value = health;
	}
	
	public void SetXP(int xp)
	{
		StartCoroutine(xpLerp(xpSlider.value, xp));
	}
	
	public void SetNextLevel(int xp)
	{
		xpSlider.maxValue = xp;
	}
	
	public void SetLevel(int Level)
	{
		level.text = Level.ToString();
	}
	
	IEnumerator fixedRotation()
	{
		while(true)
		{
			transform.GetChild(0).rotation = rot;
			yield return null;
		}
	}

	IEnumerator healthLerp(float current, float toChange)
	{
		while(current>toChange)
		{
			current -= Time.deltaTime*barSpeed;
			healthSlider.value = current;
			yield return null;
		}
		healthSlider.value = toChange;
	}
	IEnumerator xpLerp(float current, int toChange)
	{
		if(current>toChange)
		{
			xpSlider.value = current;
		}
		else
		{
			while(current<toChange)
			{
				current+= Time.deltaTime*barSpeed;
				xpSlider.value = current;
				yield return null;
			}
		}
		xpSlider.value = toChange;
	}
}
