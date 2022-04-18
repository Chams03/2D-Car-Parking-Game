using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProdGeneration : MonoBehaviour
{

    public int width;
    public int height;
    int entry = 0;
    public bool randomSeed;
    public string seed;

    [Range(0, 100)]
    public int density;

    public GameObject player;
    public GameObject roadTile;
    public GameObject buildingTile0;
    public GameObject buildingTile1;
    public GameObject buildingTile2;
    
    public int tempx;
    public int tempy;

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
                if (x == 0 || y == 0 || width-1 == x || height-1 == y) {
                    map[x, y] = 0;
                } else {
                    map[x, y] = (rand.Next(0,100) < density) ? 1:0;
                }                
            }
        }

        if (entry == 0) {
            entry++;
            int side = rand.Next(0, 3);
            if (side == 0) {
                tempx = width / 2 - 1;
                tempy = 0;
                for(int j = 0; j < 4; j++) {
                    map[tempx, tempy] = 1;
                    tempx++;
                }
                tempx = width / 2 + 1;
            } else if (side == 1) {
                tempx = 0;
                tempy = height / 2 - 1;
                for(int j = 0; j < 4; j++) {
                    map[tempx, tempy] = 1;
                    tempy++;
                }
                tempy = height / 2 + 1;
            } else if (side == 2) {
                tempx = width / 2 - 1;
                tempy = height-1;
                for(int j = 0; j < 4; j++) {
                    map[tempx, tempy] = 1;
                    tempx++;
                }
                tempx = width / 2 + 1;
            } else {
                tempx = width-1;
                tempy = height / 2 - 1;
                for(int j = 0; j < 4; j++) {
                    map[tempx, tempy] = 1;
                    tempy++;
                }
                tempy = height / 2 + 1;
            }
        }
    }

    void spawnTile() {
        gmap = new GameObject[width, height];
        Vector2 sPos = this.transform.position;
        if (map != null) {
            for(int x = 0; x < width; x++) {
                for(int y = 0; y < height; y++) {
                    GameObject spawn;
                    if (map[x, y] == 1) {
                        spawn = Instantiate(roadTile) as GameObject;
                    } else if (x != 0 && x != width-1 && y != 0 && y != height-1) {
                        spawn = Instantiate(buildingTile1) as GameObject;
                    } else {
                        spawn = Instantiate(buildingTile0) as GameObject;
                    }

                    spawn.transform.parent = this.transform;
                    spawn.transform.position = new Vector2(x, y);
                    gmap[x, y] = spawn;
                }
            }
        }
    }
}
