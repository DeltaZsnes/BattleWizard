using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // this.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        var playerObject = GameObject.FindWithTag("Player");
        Debug.Log(playerObject);
        if(playerObject != null)
        {
            // move towards the player
            this.GetComponent<Rigidbody2D>().AddForce(0.001f * (playerObject.transform.position - this.transform.position));

            // turn towards the player
            var targetVector = Vector3.Normalize(playerObject.transform.position - this.transform.position);
            var forwardVector = Vector3.Normalize(this.transform.up);
            var deltaAngle = Vector3.SignedAngle(forwardVector, targetVector, new Vector3(0, 0, 1));
            this.GetComponent<Rigidbody2D>().AddTorque(0.001f * Mathf.Clamp(deltaAngle, -1, +1));
        }
    }
}
