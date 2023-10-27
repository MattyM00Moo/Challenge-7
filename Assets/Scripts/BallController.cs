using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody ballRB;
    public float impulseForce = 5f;
    private Vector3 startPos;
    public int perfectPass = 0;
    public bool isSuperSpeedActive;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        if (isSuperSpeedActive)
        {
            if(!collision.transform.GetComponent<Goal>())
            {
                Destroy(collision.transform.parent);
                Debug.Log("Destroying " + collision.gameObject.name);
            }
        }
        else
        {
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
                deathPart.HitDeathPart();
        }

        //Debug.Log("Ball touched things");

        ballRB.velocity = Vector3.zero;
        ballRB.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update()
    {
        if (perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive=true;
            ballRB.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetBall()
    {
        transform.position = startPos;
    }
}
