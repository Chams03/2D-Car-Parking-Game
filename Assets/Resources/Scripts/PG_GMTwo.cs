using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PG_GMTwo : MonoBehaviour
{
    private SettingsScript settings;
    PlayerScript playerScript;
    FloodFill floodFill;
    [SerializeField] GameObject pScript;

    //Map
    public int[,] map;
    public GameObject[,] goMap;
    int exit;
    public int width;
    public int height;
    public int tempx;
    public int tempy;
    public int exitx;
    public int exity;

    void Awake() {
        settings = GetComponent<SettingsScript>();
        playerScript = pScript.GetComponent<PlayerScript>();
        floodFill = GetComponent<FloodFill>();
    }

    void Start() {
        //Change gamemode
        playerScript.gameMode = 1;
        floodFill.gameMode = 1;
        width = settings.width;
        height = settings.height;

        GenerateMap();
    }

    void GenerateMap() {
        map = new int[width, height];

        //call the FillMap and spawnTile method
        FillMap();
        spawnTile();

        //Set to middle to ready for flood fill algo validation
        //And to spawn player in the middle of the map.
        tempx = width / 2;
        tempy = height / 2;
    }

    void FillMap() {
        //check if randomSeed is True.
        if (settings.randomSeed) {
            settings.seed = DateTime.Now.ToString("h:mm:ss");
        }


        System.Random rand = new System.Random(settings.seed.GetHashCode());

        //marking the spot for easy spawning.
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                if (x == 0 || y == 0 || width-1 == x || height-1 == y) {
                    map[x, y] = 0;
                } else {
                    map[x, y] = (rand.Next(0,100) < settings.density) ? 1:0;
                }  
            }
        }

        //create a exit if there's none.
        if (exit == 0) {
            //increment the exit data.
            exit++;
            int side = rand.Next(0, 3);
            if (side == 0) {
                exitx = width / 2 - 1;
                exity = 0;
                for(int j = 0; j < 4; j++) {
                    map[exitx, exity] = 1;
                    exitx++;
                }
                exitx = width / 2 + 1;
            } else if (side == 1) {
                exitx = 0;
                exity = height / 2 - 1;
                for(int j = 0; j < 4; j++) {
                    map[exitx, exity] = 1;
                    exity++;
                }
                exity = height / 2 + 1;
            } else if (side == 2) {
                exitx = width / 2 - 1;
                exity = height-1;
                for(int j = 0; j < 4; j++) {
                    map[exitx, exity] = 1;
                    exitx++;
                }
                exitx = width / 2 + 1;
            } else {
                exitx = width-1;
                exity = height / 2 - 1;
                for(int j = 0; j < 4; j++) {
                    map[exitx, exity] = 1;
                    exity++;
                }
                exity = height / 2 + 1;
            }
        }

        //Create a space for player spawn at the middle of the map.
        tempy = height / 2 - 1;
        for(int i = 0; i < 3; i++) {
            tempx = width / 2 - 1;
            for(int j = 0; j < 4; j++) {
                map[tempx, tempy] = 1;
                tempx++;
            }
            tempy++;
        }
    }

    void spawnTile() {
        //make a map game object for spawning.
        goMap = new GameObject[width, height];

        //check if there's a map already generated.
        if (map != null) {
            for(int x = 0; x < width; x++) {
                for(int y = 0; y < height; y++) {
                    //ready for spawning.
                    GameObject spawn;

                    //check map to know what object to spawn.
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

                    //set to current position in x and y for spawn.
                    spawn.transform.parent = this.transform;
                    spawn.transform.position = new Vector2(x, y);
                    goMap[x, y] = spawn;
                }
            }
        }
    }
}
