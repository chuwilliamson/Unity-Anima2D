using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public Stats playerStats;
    void Start()
    {
        playerStats.Initialize(10, 25, 1, 10, 5);
        Events.OnPlayerDied.AddListener(Dead);
    }
    void Dead()
    {
        Debug.Log("im ded");
    }
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))     
            TakeDamage(5);
        if(Input.GetKeyDown(KeyCode.F))
            LevelUp(1);

    }
    void LevelUp(int increment)
    {
        playerStats.Level += increment;
        Events.OnPlayerLevelUp.Invoke();
    }
    void TakeDamage(int amount)
    {
        playerStats.Health -= amount;
        if(playerStats.Health <= 0)
        {
            Events.OnPlayerDied.Invoke();           
        }
            
    }
}
