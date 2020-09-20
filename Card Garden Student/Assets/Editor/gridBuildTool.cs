using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class gridBuildTool : EditorWindow
{
	/*public enum tileEnum
	{
		Locked,
		Lane,
		Building
	}
	
	
	bool assetsLoaded = false;
	
	private class Grid
	{
		public List<tileEnum> gridList = new List<tileEnum>();
		public List<GameObject> tileList = new List<GameObject>();
		public Object[] assetList;
	}
	
	Transform spawnPos;
	
	private Grid grid = new Grid();
	
	
	int rowSize, columnSize = 0;
	
	Vector2 scrollPos = new Vector2(0,0);
	
	[MenuItem("Level Designer Tools/Grid Build Tool")]
	public static void instantiateWindow()
	{
		GetWindow(typeof(gridBuildTool));
	}
	
	void OnGUI()
	{
		int gridCheck = rowSize*columnSize;
		GUILayout.Label("Grid Size");
		EditorGUI.BeginChangeCheck();
		rowSize = EditorGUILayout.IntField("Row Size", rowSize);
		columnSize = EditorGUILayout.IntField("Column Size", columnSize);
		if(!assetsLoaded)
		{
			loadAssets();
			//GameObject newObject = Instantiate((GameObject)grid.assetList[0]);
		}
		if (EditorGUI.EndChangeCheck())
		{
			if(gridCheck < rowSize*columnSize)
			{
				buildGrid();
			}
			else
			{
				rebuildGrid();
			}
		}
		if(GUILayout.Button("Build Grid"))
		{
			buildGrid();
		}
		if(GUILayout.Button("Rebuild Grid"))
		{
			rebuildGrid();
		}
		if(grid.gridList.Count>0)
		{
			int i = 0;
			int k = 0;
			int j = 0;
			
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
			for(; i<rowSize; i++)
			{
				//grid.rowEnum[i] = (tileEnum)EditorGUILayout.EnumFlagsField("Tile "+i+","+k, grid.rowEnum[i]);
				EditorGUILayout.BeginHorizontal();
				for(; k<columnSize; k++)
				{
					//GameObject newOjbect = Instantiate((GameObject)grid.assetList[0]);
					//newObject.name = newObject.name + i;
					grid.gridList[j] = (tileEnum)EditorGUILayout.EnumFlagsField("Tile "+(i+1)+","+(k+1), grid.gridList[j]);
					j++;
				}
				EditorGUILayout.EndHorizontal();
				k = 0;
			}
			EditorGUILayout.EndScrollView();
		}
	}
	
	private void buildGrid()
	{
		int j = 0;
		int k = 0;
		for(int i=grid.gridList.Count; i<rowSize; i++)
		{
			for(; k<columnSize; k++)
			{
				grid.gridList.Add(new  tileEnum());
				grid.tileList.Add(new GameObject());
				grid.tileList[j] = Instantiate((GameObject)grid.assetList[0], new Vector3(2*(i+1), 1, 2*(k+1)), Quaternion.identity);
				grid.tileList[j].name = "Tile ("+(i+1)+","+(k+1)+")";
				j++;
			}
			k = 0;
		}
	}
	
	private void rebuildGrid()
	{
		grid.gridList.Clear();
		grid.tileList.Clear();
		for(int i=0; i<rowSize*columnSize; i++)
		{
			grid.gridList.Add(new tileEnum());
		}
		assetsLoaded = false;
	}
    void loadAssets()
    {
		assetsLoaded = true;
		AssetBundle.UnloadAllAssetBundles(true);
        var myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/tiletypes");
		grid.assetList = myLoadedAssetBundle.LoadAllAssets();
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
			assetsLoaded = false;
            return;
        }
	}*/
}
