using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridTileTool : EditorWindow
{
	public enum tileEnum
	{
		Building,
		Lane,
		Lane_Spawn,
		Locked
	}
	
	public GameObject test;
	private List<tileEnum> gridList = new List<tileEnum>();
	private List<GameObject> tileList = new List<GameObject>();
	private int rowSize, columnSize, oldRowSize, oldColumnSize;
	private Vector2 scrollPos = new Vector2(0,0);
	private bool assetsLoaded = false;
	
	private GameObject Lane;
	private GameObject Building;
	private GameObject Spawner;
	private GameObject Locked;
	
	[MenuItem("Level Designer Tools/Grid Build Tool")]
	public static void instantiateWindow()
	{
		GetWindow(typeof(GridTileTool));
	}
	
	void OnGUI()
	{
		int gridCheck = rowSize*columnSize;
		GUILayout.Label("grid Size");
		EditorGUI.BeginChangeCheck();
		rowSize = EditorGUILayout.IntField("Row Size", rowSize);
		columnSize = EditorGUILayout.IntField("Column Size", columnSize);
		
		if(EditorGUI.EndChangeCheck())
		{
			if(gridCheck<rowSize*columnSize)
			{
				addToGrid();
			}
			else if(gridCheck>rowSize*columnSize)
			{
				subtractFromGrid();
			}
			else
			{
			}
		}
		if(!assetsLoaded)
		{
			loadAssets();
		}
		if(gridList.Count>0)
		{
			int i = 0;
			int k = 0;
			int j = 0;
			
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
			for(; i<rowSize; i++)
			{
				EditorGUILayout.BeginHorizontal();
				for(; k<columnSize; k++)
				{
					gridList[j] = (tileEnum)EditorGUILayout.EnumPopup
					(
						"Tile "+(i+1)+","+(k+1), gridList[j]
					);
					j++;
				}
				EditorGUILayout.EndHorizontal();
				k = 0;
			}
			EditorGUILayout.EndScrollView();
		}
		
	}
	
	/*
	* @brief instantiates tiles to the scene
	* Called whenever a tile is added to the grid.
	*/
	private void addToGrid()
	{
		int k = 0;
		if(oldRowSize<rowSize)
		{
			for(int i = oldRowSize; i<rowSize; i++)
			{
				for(; k<columnSize; k++)
				{
					gridList.Add(new tileEnum());
					GameObject toSpawn = Instantiate((GameObject)Locked, new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity);//test.GetComponent<Transform>()
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
					tileList.Add(toSpawn);
				}
				k = 0;
			}
		}
		else if(oldColumnSize<columnSize)
		{
			for(int i=oldColumnSize; i<columnSize; i++)
			{
				for(; k<rowSize; k++)
				{
					gridList.Add(new tileEnum());
					GameObject toSpawn = Instantiate((GameObject)Locked, new Vector3(2*(k+1), 1, 2*(i+1)), Quaternion.identity);//test.GetComponent<Transform>()
					toSpawn.name = "Tile ("+(k+1)+","+(i+1)+")";
					tileList.Add(toSpawn);
				}
				k = 0;
			}
		}
		oldRowSize = rowSize;
		oldColumnSize = columnSize;
	}
	
	
	/*
	* @brief deletes part of the grid.
	 * Called whenever the grid size is dimished
	*/
	private void subtractFromGrid()
	{
		if(rowSize<oldRowSize)
		{
			for(int k=oldRowSize; k>rowSize; k--)
			{
				Debug.Log(k);
				for(int i=0; i<tileList.Count; i++)
				{
					if(tileList[i].name.IndexOf(k.ToString()) != -1)
					{
						if(tileList[i].name.IndexOf(k.ToString())<tileList[i].name.IndexOf(','))
						{
							DestroyImmediate(tileList[i]);
							tileList.Remove(tileList[i]);
							gridList.Remove(gridList[i]);
							i--;
						}
					}
				}
			}
		}
		else if(columnSize<oldColumnSize)
		{
			for(int k=oldColumnSize; k>columnSize; k--)
			{
				for(int i=0; i<tileList.Count; i++)
				{
					if(tileList[i].name.IndexOf(k.ToString()) != -1)
					{
						if(tileList[i].name.LastIndexOf(k.ToString())>tileList[i].name.IndexOf(','))
						{
							DestroyImmediate(tileList[i]);
							tileList.Remove(tileList[i]);
							gridList.Remove(gridList[i]);
							i--;
						}
					}
				}
			}
		}
		oldRowSize = rowSize;
		oldColumnSize = columnSize;
	}
	
	private void rebuildGrid()
	{
	}		
	
    void loadAssets()
    {
		AssetBundle.UnloadAllAssetBundles(true);
        var AB = AssetBundle.LoadFromFile("Assets/AssetBundles/tiletypes");
		Lane = (GameObject)AB.LoadAsset("Assets/Prefabs/GridTiles/LaneTile.prefab");
		Building = (GameObject)AB.LoadAsset("Assets/Prefabs/GridTiles/BuildingTile.prefab");
		Spawner = (GameObject)AB.LoadAsset("Assets/Prefabs/GridTiles/LaneTileSpawner.prefab");
		Locked = (GameObject)AB.LoadAsset("Assets/Prefabs/GridTiles/LockedTile.prefab");
        if (AB == null)
        {
            Debug.Log("Failed to load AssetBundle!");
			assetsLoaded = false;
            return;
        }
		assetsLoaded = true;
	}
}
