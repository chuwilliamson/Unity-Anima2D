
using UnityEngine;
using UnityEngine.UI;

public class Subscriber : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        var button = GetComponentInChildren<Button>();
        button.onClick.AddListener(NextLevel);
	}
    public int lvl = 0;
	void NextLevel()
    {
        GameManager.instance.NextLevel(lvl);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
