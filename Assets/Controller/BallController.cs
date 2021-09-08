using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class BallController: MonoBehaviour {

    private Rigidbody2D body;
    public float initForceX;
    public float initForceY;
    private Vector2 trajectoryOrigin;
    public bool constantSpeed = true;
    public bool randomSpeedInSet = false;

    void Start() {
        trajectoryOrigin = transform.position;
        body = GetComponent<Rigidbody2D>();
        restart();
    }

    public Vector2 getTrajectoryOrigin {
        get { return trajectoryOrigin; }
    }


    private void OnCollisionExit2D(Collision2D collision) {
        trajectoryOrigin = transform.position;
    }

    private void pushBall() {

        float yRandomForce = (!constantSpeed && randomSpeedInSet) ? Random.Range(-initForceY, initForceY) : initForceY;
        float randomDirection = Random.Range(0, 2);
        if(randomDirection < 1) {
            body.AddForce(new Vector2(-initForceX, yRandomForce));
        } else {
            body.AddForce(new Vector2(initForceX, yRandomForce));
        }
    }

    private void restart() {
        resetBall();
        Invoke("pushBall", 2);
    }

    private void resetBall() {
        transform.position = Vector2.zero;
        body.velocity = Vector2.zero;
    }

    void LateUpdate() {
        if(constantSpeed && !randomSpeedInSet) body.velocity = 20 * (body.velocity.normalized);
    }
}
