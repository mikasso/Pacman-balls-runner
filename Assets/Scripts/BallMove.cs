using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    // Start is called before the first frame update
   
        public Rigidbody2D rb;
        public float yVelocity = 1.0f;
        private float yLimit = -6.5f;
        public static float multiplyVel = 1.0f;
        public static float velAdd = 0.0f;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(0, yVelocity * multiplyVel + velAdd, 0);
        }

        void Update()
        {
             rb.velocity = new Vector3(0, yVelocity * multiplyVel + velAdd, 0);
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
                affectPacman( (PacmanController) collision.gameObject.GetComponent<PacmanController>() );
                Destroy(gameObject);
            }
        }
        
        virtual protected void affectPacman(PacmanController pacman)
        {
            Debug.Log("Pacman has eatean a ball");
        }
}