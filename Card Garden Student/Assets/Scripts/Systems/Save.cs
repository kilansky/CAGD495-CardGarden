using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class Save : MonoBehaviour
{
	List<GameObject> list = new List<GameObject>();
	[HideInInspector]
	public List<Tile> tileList = new List<Tile>();
	string[] lines;
	int num = 0;
	[SerializeField] private UnityEngine.Object loadButton;
	private UnityEngine.Object[] buildings;
	
	private void Start()
	{
		loadAssets();
		loadScene();
	}
	
	public void quickSave()
	{
		DirectoryInfo di = Directory.CreateDirectory(Application.dataPath + "/Saves");
		findAssets();
		lines = new string[list.Count+tileList.Count
		+GameObject.Find("CardManager").GetComponent<CardManager>().hand.Count
		+GameObject.Find("CardManager").GetComponent<CardManager>().deck.Count
		+GameObject.Find("CardManager").GetComponent<CardManager>().discard.Count
		+3];
		lines[0] = SceneManager.GetActiveScene().buildIndex.ToString();//Scene
		lines[1] = gameObject.GetComponent<AdvancedSpawner>().currentEncounter.ToString();//Encounter number
		lines[2] = GameObject.FindWithTag("Lair").GetComponent<Damageable>().Health.ToString();//Barrier Health
		for(int i=0; i<list.Count; i++)
		{
			if(list[i].name.IndexOf('(') == -1)
			{
				lines[i+3] = list[i].name
				+ "," + list[i].GetComponent<LevelUp>().exp.ToString()
				+ "," + list[i].GetComponent<LevelUp>().totalLevel.ToString()
				+ "," + list[i].transform.position;
			}
			else
			{
				lines[i+3] = list[i].name.Substring(0, list[i].name.IndexOf('('))
				+ "," + list[i].GetComponent<LevelUp>().exp.ToString()
				+ "," + list[i].GetComponent<LevelUp>().totalLevel.ToString()
				+ "," + list[i].transform.position;
			}
		}
		CardManager cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
		for(int i=0; i<cardManager.hand.Count; i++)
		{
			lines[i+list.Count+3] = cardManager.hand[i].name + 
			cardManager.hand[i].name +" hand";
		}
		for(int i=0; i<GameObject.Find("CardManager").GetComponent<CardManager>().deck.Count; i++)
		{
			lines[i+list.Count+GameObject.Find("CardManager").GetComponent<CardManager>().hand.Count+3] 
			= GameObject.Find("CardManager").GetComponent<CardManager>().deck[i].name + " deck";
		}
		for(int i=0; i<GameObject.Find("CardManager").GetComponent<CardManager>().discard.Count; i++)
		{
			lines[i+list.Count+GameObject.Find("CardManager").GetComponent<CardManager>().hand.Count
			+GameObject.Find("CardManager").GetComponent<CardManager>().deck.Count+3]
			= GameObject.Find("CardManager").GetComponent<CardManager>().discard[i].name + " discard";
		}
		for(int i=0; i<tileList.Count; i++)
		{
			lines[i+list.Count+3] = Vector3.SqrMagnitude(tileList[i].transform.position).ToString() +
			", " + tileList[i].isOccupied;
		}
		File.WriteAllLines(Application.dataPath+"/Saves/Quicksave" + num.ToString() + ".save", lines);
		list.Clear();
		num++;
		if(num>5) num=0;
	}
	
	public void playerSave()
	{
		DirectoryInfo di = Directory.CreateDirectory(Application.dataPath + "/Saves");
		transform.parent.Find("GameUIManager").Find("GameplayCanvas").Find("PauseMenu").GetChild(2).gameObject.SetActive(true);
	}
	
	public void nameSave()
	{
		findAssets();
		lines = new string[list.Count+tileList.Count
		+GameObject.Find("CardManager").GetComponent<CardManager>().hand.Count
		+GameObject.Find("CardManager").GetComponent<CardManager>().deck.Count
		+GameObject.Find("CardManager").GetComponent<CardManager>().discard.Count
		+3];
		lines[0] = SceneManager.GetActiveScene().buildIndex.ToString();//Scene
		lines[1] = gameObject.GetComponent<AdvancedSpawner>().currentEncounter.ToString();//Encounter number
		lines[2] = GameObject.FindWithTag("Lair").GetComponent<Damageable>().Health.ToString();//Barrier Health
		for(int i=0; i<list.Count; i++)
		{
			if(list[i].name.IndexOf('(') == -1)
			{
				lines[i+3] = list[i].name
				+ "," + list[i].GetComponent<LevelUp>().exp.ToString()
				+ "," + list[i].GetComponent<LevelUp>().totalLevel.ToString()
				+ "," + list[i].transform.position;
			}
			else
			{
				lines[i+3] = list[i].name.Substring(0, list[i].name.IndexOf('('))
				+ "," + list[i].GetComponent<LevelUp>().exp.ToString()
				+ "," + list[i].GetComponent<LevelUp>().totalLevel.ToString()
				+ "," + list[i].transform.position;
			}
		}
		for(int i=0; i<GameObject.Find("CardManager").GetComponent<CardManager>().hand.Count; i++)
		{
			lines[i+list.Count+3] = GameObject.Find("CardManager").GetComponent<CardManager>().hand[i].name + " hand";
		}
		print(GameObject.Find("CardManager").GetComponent<CardManager>().deck.Count);
		for(int i=0; i<GameObject.Find("CardManager").GetComponent<CardManager>().deck.Count; i++)
		{
			lines[i+list.Count+GameObject.Find("CardManager").GetComponent<CardManager>().hand.Count+3] 
			= GameObject.Find("CardManager").GetComponent<CardManager>().deck[i].name + " deck";
		}
		print(GameObject.Find("CardManager").GetComponent<CardManager>().discard.Count);
		for(int i=0; i<GameObject.Find("CardManager").GetComponent<CardManager>().discard.Count; i++)
		{
			lines[i+list.Count+GameObject.Find("CardManager").GetComponent<CardManager>().hand.Count
			+GameObject.Find("CardManager").GetComponent<CardManager>().deck.Count+3]
			= GameObject.Find("CardManager").GetComponent<CardManager>().discard[i].name + " discard";
		}
		for(int i=0; i<tileList.Count; i++)
		{
			lines[i+list.Count+GameObject.Find("CardManager").GetComponent<CardManager>().hand.Count
			+GameObject.Find("CardManager").GetComponent<CardManager>().deck.Count
			+GameObject.Find("CardManager").GetComponent<CardManager>().discard.Count+3] 
			= Vector3.SqrMagnitude(tileList[i].transform.position).ToString() +
			", " + tileList[i].isOccupied;
		}
		File.WriteAllLines(Application.dataPath+"/Saves/" + 
		transform.parent.Find("GameUIManager").Find("GameplayCanvas").Find("PauseMenu").GetChild(2).gameObject.GetComponent<InputField>().text +
		".save", lines);
		transform.parent.Find("GameUIManager").Find("GameplayCanvas").Find("PauseMenu").GetChild(2).gameObject.SetActive(false);
		list.Clear();
	}
	
	public void ContinueSave()
	{
		DirectoryInfo di = new DirectoryInfo(Application.dataPath+"/Saves/");
		System.IO.FileInfo[] f = di.GetFiles("*.save");
		int newestFileIndex = 0;
		DateTime dateTime = f[0].LastWriteTime;
		for(int i=0; i<f.Length; i++)
		{
			if(f[i].LastWriteTime<dateTime)
			{
				dateTime = f[i].LastWriteTime;
				newestFileIndex = i;
			}
		}
		string[] fileLines;
		using (FileStream fs = File.OpenRead(Application.dataPath+"/Saves/"+f[newestFileIndex].Name))
		{
			fileLines = File.ReadAllLines(Application.dataPath+"/Saves/"+f[newestFileIndex].Name);
		}
		lines = new string[1];
		lines[0] = f[newestFileIndex].Name;
		File.WriteAllLines(Application.dataPath+"/Saves/temp", lines);
		SceneManager.LoadScene(Int32.Parse(fileLines[0]));
	}
	
	public void loadScreen()
	{
		int transformHeight = 250;
		DirectoryInfo di = new DirectoryInfo(Application.dataPath+"/Saves/");
		System.IO.FileInfo[] f = di.GetFiles("*.save");
		for(int i=0; i<f.Length; i++)
		{
			GameObject temp = (GameObject)Instantiate(loadButton, transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0));
			temp.GetComponent<RectTransform>().localPosition = 
			new Vector3(temp.GetComponent<RectTransform>().localPosition.x, transformHeight, temp.GetComponent<RectTransform>().localPosition.z);
			transformHeight -= 80;
			temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = f[i].Name;
		}
	}
	
	public void load(GameObject toLoad)
	{
		string fileName = toLoad.transform.GetChild(0).gameObject.GetComponent<Text>().text;
		string[] fileLines;
		using (FileStream fs = File.OpenRead(Application.dataPath+"/Saves/"+fileName))
		{
			fileLines = File.ReadAllLines(Application.dataPath+"/Saves/"+fileName);
		}
		lines = new string[1];
		lines[0] = fileName;
		File.WriteAllLines(Application.dataPath+"/Saves/temp", lines);
		SceneManager.LoadScene(Int32.Parse(fileLines[0]));
	}
	
	public void loadScene()
	{
		string[] fileLines;
		string fileName;
		try
		{
			using (FileStream fs = File.OpenRead(Application.dataPath+"/Saves/temp"))
			{
				fileLines = File.ReadAllLines(Application.dataPath+"/Saves/temp");
			}
			fileName = fileLines[0];
			using (FileStream fs = File.OpenRead(Application.dataPath+"/Saves/"+fileName))
			{
				fileLines = File.ReadAllLines(Application.dataPath+"/Saves/"+fileName);
			}
			gameObject.GetComponent<AdvancedSpawner>().currentEncounter = Int32.Parse(fileLines[1]);
			gameObject.GetComponent<AdvancedSpawner>().ReadEncounter();//Set Encounter
			float damageToTake = GameObject.FindWithTag("Lair").GetComponent<Damageable>().startingHealth-Int32.Parse(fileLines[2]);
			float takeDamage = GameObject.FindWithTag("Lair").GetComponent<Damageable>().startingHealth-damageToTake;
			GameObject.FindWithTag("Lair").GetComponent<Damageable>().TakeDamage(damageToTake);//Set Barrier Health
			for(int i=3; i<fileLines.Length; i++)
			{
				if(Char.IsNumber(fileLines[i][0]))
				{
					for(int j=0; j<tileList.Count; j++)
					{
						if(Vector3.SqrMagnitude(tileList[j].transform.position).ToString() == fileLines[i].Substring(0, fileLines[i].Length-(fileLines[i].Length-fileLines[i].IndexOf(','))))
						{
							tileList[j].isOccupied = bool.Parse(fileLines[i].Substring(fileLines[i].IndexOf(',')+1, fileLines[i].Length-fileLines[i].IndexOf(',')-1));
							tileList.Remove(tileList[j]);
							break;
						}
					}
				}
				else
				{
					if(fileLines[i].Contains("Apiary"))
					{
						GameObject g = Instantiate((GameObject)buildings[0],
						new Vector3
						(
						float.Parse(fileLines[i].Substring(fileLines[i].IndexOf('(')+1, findNthOccur(fileLines[i], ',', 4)-fileLines[i].IndexOf('(')-1)),//X
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 4)+1, findNthOccur(fileLines[i], ',', 5)-findNthOccur(fileLines[i], ',', 4)-1)),//Y
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 5)+1, fileLines[i].Length-findNthOccur(fileLines[i], ',', 5)-2))//Z
						), Quaternion.identity);
						for(int j=0; j<float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 2)+1, findNthOccur(fileLines[i], ',', 3)-findNthOccur(fileLines[i], ',', 2)-1)); j++)
						{
							g.GetComponent<LevelUp>().IncreaseBaseLevel();
						}
						g.GetComponent<LevelUp>().AddExp(Int32.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 1)+1, findNthOccur(fileLines[i], ',', 2)-findNthOccur(fileLines[i], ',', 1)-1)));
					}
					else if(fileLines[i].Contains("ArrowTower"))
					{
						GameObject g = Instantiate((GameObject)buildings[1],
						new Vector3
						(
						float.Parse(fileLines[i].Substring(fileLines[i].IndexOf('(')+1, findNthOccur(fileLines[i], ',', 4)-fileLines[i].IndexOf('(')-1)),//X
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 4)+1, findNthOccur(fileLines[i], ',', 5)-findNthOccur(fileLines[i], ',', 4)-1)),//Y
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 5)+1, fileLines[i].Length-findNthOccur(fileLines[i], ',', 5)-2))//Z
						), Quaternion.identity);
						for(int j=0; j<float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 2)+1, findNthOccur(fileLines[i], ',', 3)-findNthOccur(fileLines[i], ',', 2)-1)); j++)
						{
							g.GetComponent<LevelUp>().IncreaseBaseLevel();
						}
						g.GetComponent<LevelUp>().AddExp(Int32.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 1)+1, findNthOccur(fileLines[i], ',', 2)-findNthOccur(fileLines[i], ',', 1)-1)));
					}
					else if(fileLines[i].Contains("Blacksmith"))
					{
						GameObject g = Instantiate((GameObject)buildings[2],
						new Vector3
						(
						float.Parse(fileLines[i].Substring(fileLines[i].IndexOf('(')+1, findNthOccur(fileLines[i], ',', 4)-fileLines[i].IndexOf('(')-1)),//X
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 4)+1, findNthOccur(fileLines[i], ',', 5)-findNthOccur(fileLines[i], ',', 4)-1)),//Y
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 5)+1, fileLines[i].Length-findNthOccur(fileLines[i], ',', 5)-2))//Z
						), Quaternion.identity);
						for(int j=0; j<float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 2)+1, findNthOccur(fileLines[i], ',', 3)-findNthOccur(fileLines[i], ',', 2)-1)); j++)
						{
							g.GetComponent<LevelUp>().IncreaseBaseLevel();
						}
						g.GetComponent<LevelUp>().AddExp(Int32.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 1)+1, findNthOccur(fileLines[i], ',', 2)-findNthOccur(fileLines[i], ',', 1)-1)));
					}
					else if(fileLines[i].Contains("HoneyPot"))
					{
						GameObject g = Instantiate((GameObject)buildings[3],
						new Vector3
						(
						float.Parse(fileLines[i].Substring(fileLines[i].IndexOf('(')+1, findNthOccur(fileLines[i], ',', 4)-fileLines[i].IndexOf('(')-1)),//X
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 4)+1, findNthOccur(fileLines[i], ',', 5)-findNthOccur(fileLines[i], ',', 4)-1)),//Y
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 5)+1, fileLines[i].Length-findNthOccur(fileLines[i], ',', 5)-2))//Z
						), Quaternion.identity);
						for(int j=0; j<float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 2)+1, findNthOccur(fileLines[i], ',', 3)-findNthOccur(fileLines[i], ',', 2)-1)); j++)
						{
							g.GetComponent<LevelUp>().IncreaseBaseLevel();
						}
						g.GetComponent<LevelUp>().AddExp(Int32.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 1)+1, findNthOccur(fileLines[i], ',', 2)-findNthOccur(fileLines[i], ',', 1)-1)));
					}
					else if(fileLines[i].Contains("Obeya"))
					{
						GameObject g = Instantiate((GameObject)buildings[4],
						new Vector3
						(
						float.Parse(fileLines[i].Substring(fileLines[i].IndexOf('(')+1, findNthOccur(fileLines[i], ',', 4)-fileLines[i].IndexOf('(')-1)),//X
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 4)+1, findNthOccur(fileLines[i], ',', 5)-findNthOccur(fileLines[i], ',', 4)-1)),//Y
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 5)+1, fileLines[i].Length-findNthOccur(fileLines[i], ',', 5)-2))//Z
						), Quaternion.identity);
						for(int j=0; j<float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 2)+1, findNthOccur(fileLines[i], ',', 3)-findNthOccur(fileLines[i], ',', 2)-1)); j++)
						{
							g.GetComponent<LevelUp>().IncreaseBaseLevel();
						}
						g.GetComponent<LevelUp>().AddExp(Int32.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 1)+1, findNthOccur(fileLines[i], ',', 2)-findNthOccur(fileLines[i], ',', 1)-1)));
					}
					else if(fileLines[i].Contains("StoneTower"))
					{
						GameObject g = Instantiate((GameObject)buildings[5],
						new Vector3
						(
						float.Parse(fileLines[i].Substring(fileLines[i].IndexOf('(')+1, findNthOccur(fileLines[i], ',', 4)-fileLines[i].IndexOf('(')-1)),//X
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 4)+1, findNthOccur(fileLines[i], ',', 5)-findNthOccur(fileLines[i], ',', 4)-1)),//Y
						float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 5)+1, fileLines[i].Length-findNthOccur(fileLines[i], ',', 5)-2))//Z
						), Quaternion.identity);
						for(int j=0; j<float.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 2)+1, findNthOccur(fileLines[i], ',', 3)-findNthOccur(fileLines[i], ',', 2)-1)); j++)
						{
							g.GetComponent<LevelUp>().IncreaseBaseLevel();
						}
						g.GetComponent<LevelUp>().AddExp(Int32.Parse(fileLines[i].Substring(findNthOccur(fileLines[i], ',', 1)+1, findNthOccur(fileLines[i], ',', 2)-findNthOccur(fileLines[i], ',', 1)-1)));
					}
					else if(fileLines[i].Contains("hand"))
					{
						//GameObject.GetComponent
					}
				}
			}
			File.Delete(Application.dataPath+"/Saves/temp");
		}
		catch
		{
			print("No file to load");
		}
	}
	
	static int findNthOccur(String str, char ch, int N) 
	{
		int occur = 0; 
	  
		// Loop to find the Nth 
		// occurence of the character 
		for (int i = 0; i < str.Length; i++)  
		{ 
			if (str[i] == ch) 
			{ 
				occur += 1; 
			} 
			if (occur == N) 
				return i; 
		} 
		return -1; 
	}
	
	private void findAssets()
	{
		GameObject[] temp;
		temp = UnityEngine.GameObject.FindGameObjectsWithTag("Building");
		for(int i=0; i<temp.Length; i++)
		{
			list.Add(temp[i]);
		}
		temp = UnityEngine.GameObject.FindGameObjectsWithTag("Minion");
		for(int i=0; i<temp.Length; i++)
		{
			list.Add(temp[i]);
		}
	}
	
	void loadAssets()
    {
		AssetBundle.UnloadAllAssetBundles(true);
        var AB = AssetBundle.LoadFromFile("Assets/AssetBundles/savebutton");
		loadButton = AB.LoadAsset<GameObject>("SaveSelectButton");
		var abBuildings = AssetBundle.LoadFromFile("Assets/AssetBundles/buildings");
		buildings = abBuildings.LoadAllAssets();
        if (AB == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
	}
}