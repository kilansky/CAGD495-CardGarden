using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleporter : MonoBehaviour
{
	Collider[] c;
	List<GameObject> temp;
	LayerMask l;
	void Start()
	{
		temp = new List<GameObject>();
		l = 1 << 8;
		StartCoroutine(getEnemies());
	}
	
	IEnumerator getEnemies()
	{
		while(true)
		{
			c = Physics.OverlapBox(new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y+2, transform.GetChild(0).position.z)
			, new Vector3(2, 4, 2), Quaternion.identity, l);
			if(c.Length>0)
			{
				Teleport();
			}
			Array.Clear(c, 0, c.Length);
			yield return null;
		}
	}
	
	void Teleport()
	{
		float height;
		for(int i=0; i<c.Length; i++)
		{
			temp.Add(c[i].gameObject);
			height = c[i].bounds.size.y;
			Vector3 v = c[i].gameObject.GetComponent<NavMeshAgent>().destination;
			c[i].gameObject.GetComponent<NavMeshAgent>().Warp(new Vector3(transform.GetChild(1).position.x, transform.GetChild(1).position.y+(height/2), transform.GetChild(1).position.z));
			c[i].gameObject.GetComponent<NavMeshAgent>().destination = v;
		}
	}
}
