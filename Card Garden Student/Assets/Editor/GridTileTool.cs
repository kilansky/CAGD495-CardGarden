using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridTileTool : EditorWindow
{
	public enum tileEnum
	{
		Ground1,
		Ground2,
		Ground3,
		Ground4,
		LaneDown,
		LaneDownT,
		LaneFourWay,
		LaneLeftDownCorner,
		LaneLeftUpCorner,
		LaneLeft,
		LaneLeftT,
		LaneRightDownCorner,
		LaneRightLeftStrait,
		LaneRightUpCorner,
		LaneRight,
		LaneRightT,
		LaneUpDownStrait,
		LaneUp,
		LaneUpT,
		AliveTree1,
		AliveTree2,
		AliveTree3,
		CrumblingWall1,
		CrumblingWall2,
		DeadTree1,
		DeadTree2,
		Flowers1,
		Flowers2,
		Flowers3,
		Flowers4,
		MossyRock1,
		MossyRock2,
		Mushroom1,
		Mushroom2,
		Rock1,
		Rock2,
		Rock3,
		Rock4,
		Rock5,
		Rock6,
		RuinedStairs,
		SwampGround1,
		SwampGround2,
		SwampLaneDown,
		SwampLaneDownT,
		SwampLaneFourWay,
		SwampLaneLeft,
		SwampLaneLeftDownCorner,
		SwampLaneLeftUpCorner,
		SwampLaneLeftT,
		SwampLaneRight,
		SwampLaneRightDownCorner,
		SwampLaneRightLeftStrait,
		SwampLaneRightUpCorner,
		SwampLaneRightT,
		SwampLaneUp,
		SwampLaneUpDownStrait,
		SwampLaneUpT,
		SwampFlower1,
		SwampLilyPad1,
		SwampLilyPad2,
		SwampReeds,
		SwampWater,
		SwampTree1,
		SwampTree2,
		SwampTree3,
		SwampTree4,
	}
	
	private List<tileEnum> gridList = new List<tileEnum>();
	private List<GameObject> tileList = new List<GameObject>();
	private Dictionary<string, Tuple<int, tileEnum>> buttonList = new Dictionary<string, Tuple<int, tileEnum>>();
	private UnityEngine.Object[] assetList;
	private int rowSize, columnSize, oldRowSize, oldColumnSize;
	private Vector2 scrollPos = new Vector2(0,0);
	private bool assetsLoaded = false;
	private bool gridInstantiated = false;

	private GameObject Grid;
	string fileName = "Grid";
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
			if(rowSize<0)
			{
				rowSize = oldRowSize;
				Debug.Log("Can't set grid size to a negative number");
			}
			else if(columnSize<0)
			{
				columnSize = oldColumnSize;
				Debug.Log("Can't set grid size to a negative number");
			}
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
		if(GUILayout.Button("Rebuild Grid"))
		{
			rebuildGrid();
		}
		if(GUILayout.Button("Clear Grid"))
		{
			clearGrid();
		}
		if(GUILayout.Button("Save Grid"))
		{
			saveGrid();
		}
		if(GUILayout.Button("Load Grid"))
		{
			
			loadGrid();
			GUI.changed = true;
		}
		if(!assetsLoaded)
		{
			loadAssets();
		}
		if(!gridInstantiated)
		{
			Grid = new GameObject();
			Grid.name = "Grid";
			gridInstantiated = true;
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
					EditorGUI.BeginChangeCheck();
					gridList[j] = (tileEnum)EditorGUILayout.EnumPopup
					(
						"Tile "+(i+1)+","+(k+1), gridList[j]
					);
					GUI.changed = true;
					if(EditorGUI.EndChangeCheck())
					{
						for(int h=0; h<tileList.Count; h++)
						{
							if(tileList[h].name == "Tile ("+(i+1)+","+(k+1)+")")
							{
								DestroyImmediate(tileList[h]);
								tileList.Remove(tileList[h]);
								break;
							}
						}
						if(gridList[j] == tileEnum.Ground1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[0], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Ground2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[1], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Ground3)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[2], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Ground4)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[3], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneDown)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[4], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneDownT)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[5], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneFourWay)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[6], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneLeftDownCorner)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[7], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneLeftUpCorner)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[8], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneLeft)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[9], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneLeftT)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[10], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneRightDownCorner)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[11], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneRightLeftStrait)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[12], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneRightUpCorner)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[13], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneRight)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[14], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneRightT)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[15], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneUpDownStrait)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[16], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneUp)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[17], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.LaneUpT)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[18], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.AliveTree1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[19], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.AliveTree2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[20], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.AliveTree3)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[21], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.CrumblingWall1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[22], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.CrumblingWall2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[23], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.DeadTree1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[24], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.DeadTree2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[25], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Flowers1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[26], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Flowers2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[27], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Flowers3)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[28], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Flowers4)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[29], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.MossyRock1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[30], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.MossyRock2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[31], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Mushroom1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[32], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Mushroom2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[33], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Rock1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[34], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Rock2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[35], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Rock3)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[36], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Rock4)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[37], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Rock5)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[38], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.Rock6)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[39], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.RuinedStairs)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[40], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampGround1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[41], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampGround2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[42], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneDown)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[43], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneDownT)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[44], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneLeft)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[45], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneLeftDownCorner)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[46], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneLeftUpCorner)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[47], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneLeftT)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[48], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneRight)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[49], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneRightDownCorner)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[50], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneRightLeftStrait)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[51], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneRightUpCorner)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[52], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneRightT)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[53], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneUp)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[54], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneUpDownStrait)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[55], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneUpT)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[56], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLaneFourWay)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[57], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampFlower1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[58], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLilyPad1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[59], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampLilyPad2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[60], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampReeds)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[61], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampWater)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[62], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampTree1)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[63], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampTree2)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[64], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampTree3)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[65], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
						else if(gridList[j] == tileEnum.SwampTree4)
						{
							GameObject toSpawn = Instantiate((GameObject)assetList[66], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
							toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
							tileList.Add(toSpawn);
						}
					}
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
					buttonList.Add((i+1).ToString()+","+(k+1).ToString(), new Tuple<int, tileEnum>(gridList.Count-1, gridList[gridList.Count-1]));
					GameObject toSpawn = Instantiate((GameObject)assetList[0], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
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
					buttonList.Add((k+1).ToString()+","+(i+1).ToString(), new Tuple<int, tileEnum>(gridList.Count-1, gridList[gridList.Count-1]));
					GameObject toSpawn = Instantiate((GameObject)assetList[0], new Vector3(2*(k+1), 1, 2*(i+1)), Quaternion.identity, Grid.GetComponent<Transform>());
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
				for(int i=0; i<tileList.Count; i++)
				{
					if(tileList[i].name.IndexOf(k.ToString()) != -1)
					{
						if(tileList[i].name.IndexOf(k.ToString())<tileList[i].name.IndexOf(','))
						{
							if(buttonList.ContainsKey(tileList[i].name.Substring(tileList[i].name.IndexOf(",")-1, 3)))
							{
								buttonList.Remove(tileList[i].name.Substring(tileList[i].name.IndexOf(",")-1, 3));
							}
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
		int k = 0;
		int j = 0;
		for(int i=0; i<rowSize; i++)
		{
			for(; k<columnSize; k++)
			{
				DestroyImmediate(tileList[j]);
				GameObject toSpawn = Instantiate((GameObject)assetList[0], new Vector3(2*(k+1), 1, 2*(i+1)), Quaternion.identity, Grid.GetComponent<Transform>());
				toSpawn.name = "Tile ("+(k+1)+","+(i+1)+")";
				tileList[j] = toSpawn;
				gridList[j] = tileEnum.Ground1;
				j++;
			}
			k=0;
		}
	}		
	
	void clearGrid()
	{
		rowSize = 0;
		columnSize = 0; 
	}
	
    void loadAssets()
    {
		AssetBundle.UnloadAllAssetBundles(true);
        var level1 = AssetBundle.LoadFromFile("Assets/AssetBundles/level1tiles");
		var level2 = AssetBundle.LoadFromFile("Assets/AssetBundles/level2tiles");
		UnityEngine.Object[] temp1 = level1.LoadAllAssets();
		UnityEngine.Object[] temp2 = level2.LoadAllAssets();
		assetList = new UnityEngine.Object[temp1.Length+temp2.Length];
		Array.Copy(temp1, assetList, temp1.Length);
		Array.Copy(temp2, 0, assetList, temp1.Length, temp2.Length);
		/*for(int i=0; i<assetList.Length; i++)
		{
			Debug.Log(assetList[i].name);
		}*/
        if (level1 == null || level2 == null)
        {
            Debug.Log("Failed to load AssetBundle!");
			assetsLoaded = false;
            return;
        }
		assetsLoaded = true;
	}
	
	private void loadGrid()
	{
		string[] fileLines;
		fileName = EditorUtility.OpenFilePanel("Open Grid", "", "grid");
		using (FileStream fs = File.OpenRead(fileName))
		{
			fileLines = File.ReadAllLines(fileName);
		}
		rowSize = 0;
		columnSize = 0;
		subtractFromGrid();
		rowSize = Int32.Parse(fileLines[0].Substring(0, 1));
		columnSize = Int32.Parse(fileLines[0].Substring(2, 1));
		addToGrid();
		for(int i=1; i<fileLines.Length; i++)
		{
			if(buttonList.ContainsKey(fileLines[i].Substring(0,3)))
			{
				gridList[buttonList[fileLines[i].Substring(0,3)].Item1] = 
				(tileEnum)Enum.Parse(typeof(tileEnum), fileLines[i].Substring(fileLines[i].IndexOf(",")+2, fileLines[i].Length-(fileLines[i].IndexOf(",")+2)));
			}
		}
	}
	
	private void saveGrid()
	{
		fileName = EditorUtility.SaveFilePanel("Save Grid", "", "myGrid", "grid");
		string[] lines = new string[tileList.Count+1];
		lines[0] = rowSize.ToString() + "," + columnSize.ToString();
		if(fileName.Length>0)
		{
			for(int i=0; i<tileList.Count; i++)
			{
				lines[i+1] = tileList[i].name.Substring(tileList[i].name.IndexOf('(')+1, 
				tileList[i].name.Length-(tileList[i].name.IndexOf('('))-2) + " " +
				gridList[i].ToString();
			}
			File.WriteAllLines(fileName, lines);
		}
	}
}
