using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private ProdGeneration prodGeneration;
    private PG_GMTwo pg_gmTwo;
    [SerializeField]GameObject prod_gen_two;
    float accelerationPower = 5f;
    [SerializeField]
    float steeringPower = 3f;
    float steeringAmount, speed, direction;
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
        steeringAmount = -Input.GetAxis("Horizontal");
        speed = Input.GetAxis("Vertical") * accelerationPower;
        direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;

        rb.AddRelativeForce(Vector2.up * speed);

        rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * steeringAmount / 2);
    }
    
}
