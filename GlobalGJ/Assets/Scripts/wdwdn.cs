using UnityEngine;
using System.Collections;

public class wdwdn : MonoBehaviour {

	public void Load (string name) {
		GameLogic.Instance().LoadLevel(name);
	}
}
