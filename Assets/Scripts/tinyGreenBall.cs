using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tinyGreenBall : MonoBehaviour
{
    // Start is called before the first frame update
    public float minForce = 3.0f;
    public float maxForce = 6.0f;
    public int points = 2;
    const float yLimit = -6.0f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2( Random.Range(-minForce/2,maxForce/2), Random.Range(minForce, maxForce)), ForceMode2D.Impulse);
    }

    void Update()
    {
        
        if (transform.position.y < yLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Do something
        if (collision.gameObject.name == "pacman")
        {
            ((PacmanController)collision.gameObject.GetComponent<PacmanController>()).increaseScore(points);
            Destroy(gameObject);
        }
    }

}
