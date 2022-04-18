using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{

    [SerializeField] private float fillDelay = 0.2f;
    private ProdGeneration board;
    private Renderer sprite;
    int count = 0;

    void Start()
    {
        board = this.GetComponent<ProdGeneration>();
    }

    private IEnumerator Flood(int x, int y, Color oldColor, Color newColor) {
        WaitForSeconds wait = new WaitForSeconds(fillDelay);

        if (x >= 0 && x < board.width && y >= 0 && y < board.height) {
            yield return wait;
            if (board.gmap[x, y].GetComponent<SpriteRenderer>().color == oldColor) {
                count++;
                
                board.gmap[x, y].GetComponent<SpriteRenderer>().color = newColor;
                StartCoroutine(Flood(x + 1, y, oldColor, newColor));
                StartCoroutine(Flood(x - 1, y, oldColor, newColor));
                StartCoroutine(Flood(x, y + 1, oldColor, newColor));
                StartCoroutine(Flood(x, y - 1, oldColor, newColor));
                Debug.Log(count);
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            int x = board.tempx;
            int y = board.tempy;

            if (board.gmap[x, y].GetComponent<SpriteRenderer>().color != Color.white) {
                board.gmap[x, y].GetComponent<SpriteRenderer>().color = Color.white;
            }
            StartCoroutine(Flood(x, y, Color.white, Color.red));
        }
    }
}
