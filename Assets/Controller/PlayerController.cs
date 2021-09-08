using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour {

    public KeyCode buttonUp = KeyCode.W;
    public KeyCode buttonDown = KeyCode.S;
    [SerializeField]
    private float speed = 10.0f;
    public float yBoundary = 10.0f;
    public float xBoundary = 20.0f;
    private ContactPoint2D lastContact;

    private int score;
    private Rigidbody2D body;

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        arenaBoundary();
        movement();
    }

    public ContactPoint2D getLastContact {
        get { return lastContact; }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name.Equals("Ball")) {
            lastContact = collision.GetContact(0);
        }
    }

    private void arenaBoundary() {
        Vector3 position = transform.position;

        if(position.y > yBoundary) {
            position.y = yBoundary;
        } else if(position.y < -yBoundary) {
            position.y = -yBoundary;
        }
        transform.position = position;
    }
    private void movement() {
        Vector2 velocity = body.velocity;

        if(Input.GetKey(buttonUp)) {
            velocity.y = speed;
        } else if(Input.GetKey(buttonDown)) {
            velocity.y = -speed;
        } else {
            velocity.y = 0.0f;
        }
        body.velocity = velocity;
    }

    public void IncrementScore() {
        score++;
    }

    public void ResetScore() {
        score = 0;
    }

    public int Score {
        get { return score; }
    }

}

