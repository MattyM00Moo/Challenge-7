using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.singleton.AddScore(1);
        FindObjectOfType<BallController>().perfectPass++;
        Debug.Log("Perfect Passes: " + FindObjectOfType<BallController>().perfectPass);
    }
}
