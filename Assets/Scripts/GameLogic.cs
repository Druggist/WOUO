using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class Constants {	
	public static Dictionary<string, ushort> STATE = new Dictionary<string, ushort>{
		{ "WIN", 1 },
		{ "LOSE", 2 },
		{ "TIMEOUT", 3 }
	};
}

public class GameLogic : MonoBehaviour {
	public string[] levels;
	private Dictionary<string, ushort> levelStates = new Dictionary<string, ushort>();
	private static GameLogic _instance;
	private bool _completed;

	public static GameLogic Instance() {
		if (_instance == null) {
			GameLogic logic = FindObjectOfType(typeof(GameLogic)) as GameLogic;
			_instance = logic;
		}
		return _instance;
	}

	void Awake () {
		GameObject.DontDestroyOnLoad(this.gameObject);
		if (levels.Length == 0) {
			levels[0] = Application.loadedLevelName;
		}
		Instance().levels = this.levels;
	}

	public void Message(ushort state) {
		string levelName = Application.loadedLevelName;
		if ( _completed ) {
            SceneManager.LoadScene("win");
		}
		levelStates [levelName] = state;
		Debug.Log("Current level name: "+levelName+" State: "+levelStates[levelName]);
		List<string> availableLevels = new List<string>();
		foreach (string name in levels) {
			ushort levelState = 0;
			if (!levelStates.TryGetValue(name, out levelState) ||
				levelState != Constants.STATE ["WIN"]) {
				availableLevels.Add(name);
			}
		}
		if (availableLevels.Count == 0) {
			print("You won the game!");
			_completed = true;
			Application.Quit();
			return;
		}
		int randomIndex = Random.Range(0, availableLevels.Count);
		string levelNameToLoad = availableLevels [randomIndex];
		print("Loading level: " + levelNameToLoad);
        if(levelNameToLoad == "") SceneManager.LoadScene("win");
        else SceneManager.LoadScene(levelNameToLoad);
    }

	public void LoadLevel(string name){
        SceneManager.LoadScene(name);
	}

	public void Exit () {
		Debug.Log("Exiting!");
		Application.Quit();
	}

}