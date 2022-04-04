using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProdGeneration : MonoBehaviour
{

    public int width;
    public int height;
    public bool randomSeed;
    public string seed;

    [Range(0, 100)]
    public int density;

    public GameObject roadTile;
    public GameObject buildingTile;

    public int[,] map;
    public GameObject[,] gmap;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap() {
        map = new int[width, height];
        
        FillMap();
        spawnTile();
    }

    void FillMap() {
        if(randomSeed) {
            seed = DateTime.Now.ToString("h:mm:ss");
        }

        System.Random rand = new System.Random(seed.GetHashCode());

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                map[x, y] = (rand.Next(0,100) < density) ? 1:0;
            }
        }
    }

    void spawnTile() {
        gmap = new GameObject[width, height];
        Vector2 sPos = this.transform.position;
        if (map != null) {
            for(int x = 0; x < width; x++) {
                for(int y = 0; y < height; y++) {
                    GameObject spawn = (map[x, y] == 1) ? Instantiate(roadTile) as GameObject : Instantiate(buildingTile) as GameObject;
                    spawn.transform.parent = this.transform;
                    spawn.transform.position = new Vector2(x, y);
                    gmap[x, y] = spawn;
                }
            }
        }
    }
}
