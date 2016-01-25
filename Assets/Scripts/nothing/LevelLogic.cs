using UnityEngine;
using System.Collections;

public class LevelLogic : MonoBehaviour {
	private int _objectCount;

	void Awake () {
		_objectCount = GameObject.FindGameObjectsWithTag("pushOut").Length;
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Player2") {
			GameObject[] gos;
        	gos = GameObject.FindGameObjectsWithTag("Player");
			Destroy(gos[0]);
			Destroy(c.gameObject);
			if (_objectCount < 1) {
				GameLogic.Instance().Message(Constants.STATE["WIN"]);
				print("done");
			} else {
				GameLogic.Instance().Message(Constants.STATE["LOSE"]);
			}

		} else {
			Destroy(c.gameObject);
			_objectCount--;
		}
	}

	// Update is called once per frame
	void Update () {
		Debug.Log("Object count: "+_objectCount);
	}
}