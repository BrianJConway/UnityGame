using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabSpawner : MonoBehaviour {

    private float nextSpawn = 0;
    public Transform prefabToSpawn;
    public AnimationCurve spawnCurve;
    public float curveLengthSeconds = 30;
    private float startTime;
    public float jitter = 0.25f;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if( Time.time > nextSpawn )
        {
            Instantiate( prefabToSpawn, transform.position, Quaternion.identity );

            // Get value between 0 and 1 based on amount of seconds passed within curve length
            float curvePos = (Time.time - startTime) / curveLengthSeconds;

            // Reset where you are in curve if passed curve length
            if( curvePos > 1f )
            {
                curvePos = 1f;
                startTime = Time.time;
            }

            // Set next spawn time based on where you are in the curve
            nextSpawn = Time.time + spawnCurve.Evaluate( curvePos ) + Random.Range( -jitter, jitter );
        }
	}
}
