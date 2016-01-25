using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public float timeoutInSeconds;
	private float _t;

	// Use this for initialization
	void Start () {
		_t = timeoutInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		_t -= Time.deltaTime;
		if (_t < 0) {
			Debug.Log("Timeout at "+Time.time.ToString());
			GameLogic.Instance().Message(Constants.STATE["TIMEOUT"]);
			return;
		}
	}
}
