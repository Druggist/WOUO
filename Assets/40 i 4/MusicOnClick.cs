using UnityEngine;
using System.Collections;

public class MusicOnClick : MonoBehaviour {
	private bool _correct = true, _endSound = false;
	private KeyCode[] _word = new KeyCode[] {
		KeyCode.Alpha4,
		KeyCode.Alpha0,
		KeyCode.I,
		KeyCode.Alpha4
	};
	
	GameObject Particles;
	GameObject[] Sounds;
	private ushort _readMarker = 0;
	
	void Awake () {
		Particles = GameObject.Find("Particles");
		Sounds = new GameObject[]{
			GameObject.Find("Sound_3"),
			GameObject.Find("Sound_1"),
			GameObject.Find("Sound_2"),
			GameObject.Find("Sound_6"),
			GameObject.Find("Sound_4"),
			GameObject.Find("Sound_5"),
		};
	}

	// Update is called once per frame
	void Update() {
		if (Input.anyKeyDown) {

			if (Input.GetKeyDown(_word [_readMarker])) {
				Sounds[_readMarker].GetComponent<AudioSource>().Play();
				Debug.Log("Playing sound "+Sounds[_readMarker].name+" for key "+_readMarker);
			} else {
				//GameLogic.Instance().Message(Constants.STATE ["LOSE"]);
				_correct = false;
				Sounds[4].GetComponent<AudioSource>().Play();
				Debug.Log("Playing sound 4, readMarker="+_readMarker.ToString());
			}
			_readMarker++;

			Particles.GetComponent<ParticleSystem>().Play();
		}

		if (_readMarker >= _word.Length) {
			GameObject _sound = Sounds[( _correct )?3:5];
			if (!_endSound) {
				_sound.GetComponent<AudioSource>().Play();
				_endSound = true;
			}
			if(!_sound.GetComponent<AudioSource>().isPlaying) {
				GameLogic.Instance().Message(Constants.STATE[( _correct )?"WIN":"LOSE"]);
			}
		}
	}
}
