using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
                         //Store a reference to our BoardManager which will set up the level.
                        //Current level number, expressed in game as "Day 1".

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if(instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if(instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
 
 
    }
    void Start()
    {
        Events.OnPlayerDied.AddListener(NextLevel);
        Events.OnPlayerLevelUp.AddListener(PlayerLeveledUp);
        

    }

    public void NextLevel()
    {
        NextLevel(0);
    }
    public void PlayerLeveledUp()
    {
        NextLevel(1);

    }
    public void NextLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void QuitGame()
    {
        Application.Quit();

    }
}