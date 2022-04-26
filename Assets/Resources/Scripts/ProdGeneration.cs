using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProdGeneration : MonoBehaviour
{
    //Scripts
    PlayerScript playerScript;
    SettingsScript settings;
    FloodFill floodFill;
    [SerializeField]GameObject pScript;

    public int width;
    public int height;
    int entry = 0;

    public int tempx;
    public int tempy;

    public int[,] map;
    public GameObject[,] gmap;

    void Awake() {
        playerScript = pScript.GetComponent<PlayerScript>();
        settings = GetComponent<SettingsScript>();
        floodFill = GetComponent<FloodFill>();
    }

    void Start()
    {
        width = settings.width;
        height = settings.height;
        playerScript.gameMode = 0;
        floodFill.gameMode = 0;

        GenerateMap();
    }

    void GenerateMap() {
        map = new int[width, height];
        
        FillMap();
        spawnTile();
    }

    void FillMap() {
        if(settings.randomSeed) {
            settings.seed = DateTime.Now.ToString("h:mm:ss");
        }

        System.Random rand = new System.Random(settings.seed.GetHashCode());

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                if (x == 0 || y == 0 || width-1 == x || height-1 == y) {
                    map[x, y] = 0;
                } else {
                    map[x, y] = (rand.Next(0,100) < settings.density) ? 1:0;
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
                        spawn = Instantiate(settings.roadTile) as GameObject;
                        settings.roadTile.name = "Road";
                    } else if (x != 0 && x != width-1 && y != 0 && y != height-1) {
                        System.Random rand = new System.Random();
                        int random = rand.Next(0, 8);
                        if (random == 0) {
                            spawn = Instantiate(settings.obstacleTile) as GameObject;
                        } else if (random == 1) {
                            spawn = Instantiate(settings.obstacleTile1) as GameObject;
                        } else if (random == 2) {
                            spawn = Instantiate(settings.obstacleTile2) as GameObject;
                        } else if (random == 3) {
                            spawn = Instantiate(settings.obstacleTile3) as GameObject;
                        } else if (random == 4) {
                            spawn = Instantiate(settings.obstacleTile4) as GameObject;
                        } else if (random == 5) {
                            spawn = Instantiate(settings.obstacleTile5) as GameObject;
                        } else if (random == 6) {
                            spawn = Instantiate(settings.obstacleTile6) as GameObject;
                        } else if (random == 7) {
                            spawn = Instantiate(settings.obstacleTile7) as GameObject;
                        } else {
                            spawn = Instantiate(settings.obstacleTile8) as GameObject;
                        }
                    } else {
                        System.Random rand = new System.Random();
                        int randomNum = rand.Next(0, 3);
                        if (randomNum == 0) {
                            spawn = Instantiate(settings.wallTile) as GameObject;
                        } else if (randomNum == 1) {
                            spawn = Instantiate(settings.wallTile1) as GameObject;
                        } else {
                            spawn = Instantiate(settings.wallTile2) as GameObject;
                        }
                    }

                    spawn.transform.parent = this.transform;
                    spawn.transform.position = new Vector2(x, y);
                    gmap[x, y] = spawn;
                }
            }
        }
    }
}
