using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridLoader : MonoBehaviour
{
	public UnityEngine.Object GridFile;
	private GameObject Grid;
    private UnityEngine.Object[] assetList;
	private string fileName;
	int rowSize, columnSize;
	
    void Start()
    {
		Grid = new GameObject();
		Grid.name = "Grid";
		FileInfo f = new FileInfo(GridFile.name);
		fileName = Application.streamingAssetsPath+"/Grid/"+GridFile.name+".grid";
        loadAssets();
		loadGrid();
    }
	
	private void loadGrid()
	{
		string[] fileLines;
		using (FileStream fs = File.OpenRead(fileName))
		{
			fileLines = File.ReadAllLines(fileName);
		}
		rowSize = Int32.Parse(fileLines[0].Substring(0, 1));
		columnSize = Int32.Parse(fileLines[0].Substring(2, 1));
		int k=0;
		int j=1;
		for(int i=0; i<rowSize; i++)
		{
			for(; k<columnSize; k++)
			{
				if(fileLines[j].Contains("Ground1"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[0], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Ground2"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[1], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Ground3"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[2], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Ground4"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[3], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneDown"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[4], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneDownT"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[5], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneFourWay"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[6], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneLeftDownCorner"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[7], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneLeftUpCorner"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[8], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneLeft"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[9], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneLeftT"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[10], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneRightDownCorner"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[11], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneRightLeftStrait"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[12], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneRightUpCorner"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[13], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneRight"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[14], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneRightT"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[15], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneUpDownStrait"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[16], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneUp"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[17], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("LaneUpT"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[18], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("AliveTree1"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[19], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("AliveTree2"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[20], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("AliveTree3"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[21], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("CrumblingWall1"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[22], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("CrumblingWall2"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[23], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("DeadTree1"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[24], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("DeadTree2"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[25], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Flowers1"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[26], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Flowers2"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[27], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Flowers3"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[28], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Flowers4"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[29], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("MossyRock1"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[30], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("MossyRock2"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[31], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Mushroom1"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[32], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Mushroom2"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[33], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Rock1"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[34], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Rock2"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[35], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Rock3"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[36], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Rock4"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[37], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Rock5"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[38], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("Rock6"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[39], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				else if(fileLines[j].Contains("RuinedStairs"))
				{
					GameObject toSpawn = Instantiate((GameObject)assetList[40], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity, Grid.GetComponent<Transform>());
					toSpawn.name = "Tile ("+(i+1)+","+(k+1)+")";
				}
				j++;
			}
			k=0;
		}
	}

    void loadAssets()
    {
		AssetBundle.UnloadAllAssetBundles(true);
        var AB = AssetBundle.LoadFromFile("Assets/AssetBundles/level1tiles");
		assetList = AB.LoadAllAssets();
        if (AB == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
	}
}
