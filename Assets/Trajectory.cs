using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Trajectory: MonoBehaviour {

    public BallController ball;
    CircleCollider2D ballCollider;
    Rigidbody2D ballRigidBody;
    public GameObject ballAtCollision;
    bool drawBallAtCollision = false;
    Vector2 offsetHitPoint = new Vector2();


    private void Start() {

        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    private void Update() {
        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigidBody.position, ballCollider.radius, ballRigidBody.velocity.normalized);
        foreach(RaycastHit2D castHit in circleCastHit2DArray) {
            if(castHit.collider != null && castHit.collider.GetComponent<BallController>() == null) {
                Vector2 hitPoint = castHit.point;
                Vector2 hitNormal = castHit.normal;
                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;

                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                if(castHit.collider.GetComponent<SideWall>() == null) {
                    // Hitung vektor datang
                    Vector2 inVector = (offsetHitPoint - ball.getTrajectoryOrigin).normalized;

                    // Hitung vektor keluar
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    // Hitung dot product dari outVector dan hitNormal. Digunakan supaya garis lintasan ketika 
                    // terjadi tumbukan tidak digambar.
                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if(outDot > -1.0f && outDot < 1.0) {
                        // Gambar lintasan pantulannya
                        DottedLine.DottedLine.Instance.DrawDottedLine(
                            offsetHitPoint,
                            offsetHitPoint + outVector * 10.0f);

                        // Untuk menggambar bola "bayangan" di prediksi titik tumbukan
                        drawBallAtCollision = true;
                    }
                }
                break;
            }
            if(drawBallAtCollision) {
                // Gambar bola "bayangan" di prediksi titik tumbukan
                ballAtCollision.transform.position = offsetHitPoint;
                ballAtCollision.SetActive(true);
            } else {
                // Sembunyikan bola "bayangan"
                ballAtCollision.SetActive(false);
            }
        }
    }

}
