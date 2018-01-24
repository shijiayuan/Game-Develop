using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWatch : MonoBehaviour {
    private GameObject target;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
	// Use this for initialization
	void Start () {
        target = GameObject.FindWithTag("Airplane");
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (target == null) return;
        float wantedRotation = target.transform.eulerAngles.y;
        float wantedHeight = target.transform.position.y + height;

        float currentRotation = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotation = Mathf.LerpAngle(currentRotation, wantedRotation, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight,wantedHeight,heightDamping*Time.deltaTime);

        var cur=Quaternion.Euler(0, currentRotation, 0);

        transform.position = target.transform.position;
        transform.position -= cur * Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        transform.LookAt(target.transform);
	}
}
