using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private ProdGeneration prodGeneration;
    private PG_GMTwo pg_gmTwo;
    [SerializeField]GameObject prod_gen_two;

    public int gameMode;

    private Vector3 movement;

    void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
        prodGeneration = prod_gen_two.GetComponent<ProdGeneration>();
        pg_gmTwo = prod_gen_two.GetComponent<PG_GMTwo>();
    }

    void Start()
    {
        if (gameMode == 0) {
            transform.position = new Vector3(prodGeneration.tempx, prodGeneration.tempy, 1);
        } else {
            transform.position = new Vector3(pg_gmTwo.tempx, pg_gmTwo.tempy, 1);
        }
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector3(x, y, 0);

        if (movement.y < 0) {
            // transform.localScale = Vector3.one;
        } else if (movement.y > 0) {
            // transform.localScale = new Vector3(1, 1, 1);
        } else if (movement.x < 0) {
            
        }

        transform.Translate(movement * Time.deltaTime);
    }
    
}
