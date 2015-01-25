using UnityEngine;
using System.Collections;

public class ShipClick : MonoBehaviour {
	private KeyCode[] _word = new KeyCode[] {
		KeyCode.LeftControl,
		KeyCode.Space,
		KeyCode.S,
		KeyCode.H,
		KeyCode.I,
		KeyCode.P
	};
	private ushort _readMarker = 0;
	
	// Update is called once per frame
	void Update() {
		GameObject Particles;
		if (Input.anyKeyDown) {
			if (!Input.GetKeyDown(_word [_readMarker++])) {
				GameLogic.Instance().Message(Constants.STATE ["LOSE"]);
			}
			if (_readMarker == _word.Length) {
				GameLogic.Instance().Message(Constants.STATE ["WIN"]);
			}
		
			
			Particles = GameObject.Find("Particles");
			Particles.particleSystem.Play();
		}
	}
}
