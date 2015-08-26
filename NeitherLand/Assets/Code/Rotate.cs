using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    private float rotTimeStart, rotTimeLength;
    int rotationLength = 100;
    Vector3 rotStart;
	Vector3 rotFinish;
    
    // Use this for initialization
	void Start () {
        rotTimeStart  = Time.time;
        rotTimeLength = rotationLength * Time.deltaTime;
        rotStart = new Vector3(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
        rotFinish = new Vector3(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(Vector3.Lerp(rotStart, rotFinish, (Time.time - rotTimeStart)/rotTimeLength));
        if (Time.time >= rotTimeStart + rotTimeLength)
        {
            rotStart = rotFinish;
            rotFinish = new Vector3(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
            rotTimeStart = Time.time;
        }
	}
}
