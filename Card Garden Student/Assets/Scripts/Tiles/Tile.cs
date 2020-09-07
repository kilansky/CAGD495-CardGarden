using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType { empty, rock, trees, water };
    public TileType tileType;

    public Transform spawnPos;

    public Material empty;
    public Material rock;
    public Material trees;
    public Material water;

    private Card storedCard;

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
