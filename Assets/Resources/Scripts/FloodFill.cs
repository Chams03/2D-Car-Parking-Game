using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{

    [SerializeField] private float fillDelay = 0.2f;
    private SettingsScript settings;
    private ProdGeneration board;
    private PG_GMTwo pg_gmTwo;
    private Renderer sprite;

    GameObject[,] map;
    public int gameMode;
    Queue<int> queue;
    int width;
    int height;
    int tempx;
    int tempy;
    int entrancex;
    int entrancey;
    int exitx;
    int exity;
    int count = 0;
    float timeleft;
    int mapvalidcount = 1;

    void Awake() {
        settings = this.GetComponent<SettingsScript>();
        board = this.GetComponent<ProdGeneration>();
        pg_gmTwo = this.GetComponent<PG_GMTwo>();
    }

    void Start()
    {
        width = settings.width;
        height = settings.height;
        timeleft = 5f;

        if (gameMode == 0) {
            tempx = board.tempx;
            tempy = board.tempy;
            map = board.gmap;

            StartCoroutine(Floodo(tempx, tempy, Color.white));
        } else {
            tempx = pg_gmTwo.tempx;
            tempy = pg_gmTwo.tempy;
            exitx = pg_gmTwo.exitx;
            exity = pg_gmTwo.exity;
            map = pg_gmTwo.goMap;


            StartCoroutine(Floodo(tempx, tempy, Color.white));

        //     if (map[tempx, tempy].GetComponent<SpriteRenderer>().color != Color.white) {
        //         map[tempx, tempy].GetComponent<SpriteRenderer>().color = Color.white;
        //     }
        //     // StartCoroutine(Flood(tempx, tempy, Color.white, Color.red));
        //     flodofill(tempx, tempy, Color.white, Color.red);
        }
    }

    void flodofill(int x, int y, Color oldColor, Color newColor) {
        // queue = new Queue<int>();
        // queue.Enqueue(x,y);
        // while (queue.Count != 0) {
        //     x = queue.Dequeue();
        //     Debug.Log(x);
            // if (x < 0 || x >= width || y < 0 || y >= height || map[x, y].GetComponent<SpriteRenderer>().color == oldColor) {
            //     continue;
            // } else {
            //     map[x, y].GetComponent<SpriteRenderer>().color = newColor;
            //     queue.Enqueue((x+1, y));
            //     queue.Enqueue((x-1, y));
            //     queue.Enqueue((x, y+1));
            //     queue.Enqueue((x, y-1));
            // }
        // }
    }

    private IEnumerator Floodo(int x, int y, Color newObject) {
        WaitForSeconds wait = new WaitForSeconds(fillDelay);

        if (x >= 0 && x < width && y >= 0 && y < height) {
            yield return wait;
            if (map[x, y].GetComponent<SpriteRenderer>().name == "Road(Clone)") {
                count++;
                map[x, y].GetComponent<SpriteRenderer>().name = "Road1";
                StartCoroutine(Floodo(x + 1, y, newObject));
                StartCoroutine(Floodo(x - 1, y, newObject));
                StartCoroutine(Floodo(x, y + 1, newObject));
                StartCoroutine(Floodo(x, y - 1, newObject));
            } 
        }
    }

    private IEnumerator Flood(int x, int y, Color oldColor, Color newColor) {
        WaitForSeconds wait = new WaitForSeconds(fillDelay);

        // old
        if (x >= 0 && x < width && y >= 0 && y < height) {
            yield return wait;
            if (map[x, y].GetComponent<SpriteRenderer>().color == oldColor) {
                count++;
                
                map[x, y].GetComponent<SpriteRenderer>().color = newColor;
                StartCoroutine(Flood(x + 1, y, oldColor, newColor));
                StartCoroutine(Flood(x - 1, y, oldColor, newColor));
                StartCoroutine(Flood(x, y + 1, oldColor, newColor));
                StartCoroutine(Flood(x, y - 1, oldColor, newColor));
            } 
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(Floodo(tempx, tempy, Color.white));
        }

        if (timeleft > 0) {
            timeleft -= Time.deltaTime;
        } else {
            if (gameMode == 0) {
                if (mapvalidcount == 1) {
                    if (count > 75) {
                        Debug.Log("Map Validated. \nMap Playability: Playable");
                    } else {
                        Debug.Log("Map Validated. \nMap Playability: Not Playable");
                    }
                    mapvalidcount--;
                }
            } else {
                if (mapvalidcount == 1) {
                    if (count > 75  && map[exitx, exity].GetComponent<SpriteRenderer>().name == "Road1") {
                        Debug.Log("Map Validated. \nMap Playability: Playable");
                    } else {
                        Debug.Log("Map Validated. \nMap Playability: Not Playable");
                    }
                    mapvalidcount--;
                }
            }

            
        }
    }
}
