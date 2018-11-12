using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartRate : MonoBehaviour {

	private float time = 0.0f;
	private TextMesh heartRate;

	// Use this for initialization
	void Start () {
		heartRate = this.GetComponent<TextMesh>();
		int value = Random.Range (100, 110);
		heartRate.text = "Heart Rate: " + value.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time > 5) {
			time = 0.0f; 
			int value = Random.Range (100, 110);
			heartRate.text = value.ToString ();
		}
	}
}
