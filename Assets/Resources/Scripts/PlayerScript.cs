using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private ProdGeneration prodGeneration;

    private Vector3 movement;

    void Start()
    {
        prodGeneration = this.GetComponent<ProdGeneration>();
        boxCollider = GetComponent<BoxCollider2D>();

        transform.position = new Vector3(prodGeneration.tempx, prodGeneration.tempy, 0);
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector3(x, y, 0);

        if (movement.y < 0) {
            transform.localScale = Vector3.one;
        } else if (movement.y > 0) {
            transform.localScale = new Vector3(1, -1, 1);
        }

        transform.Translate(movement * Time.deltaTime);
    }
    
}
