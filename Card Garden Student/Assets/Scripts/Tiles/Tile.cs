using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { empty, path, rock, trees, water };

public class Tile : MonoBehaviour
{   
    public TileType tileType;

    public Card storedCard;
    public Transform spawnPos;

    public Material empty;
    public Material path;
    public Material rock;
    public Material trees;
    public Material water;  

    // Start is called before the first frame update
    void Start()
    {
        if(tileType == TileType.empty)
        {
            gameObject.GetComponent<Renderer>().material = empty;
        }
        else if (tileType == TileType.rock)
        {
            gameObject.GetComponent<Renderer>().material = rock;
        }
        else if (tileType == TileType.trees)
        {
            gameObject.GetComponent<Renderer>().material = trees;
        }
        else if (tileType == TileType.water)
        {
            gameObject.GetComponent<Renderer>().material = water;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
