using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class StatEvent : UnityEvent
{

}
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/Player", order = 1)]
public class PlayerStats : Stats
{
    
    public int FireBall = 0;
    
    public override void Initialize(int h, int m, int l, int s, int i)
    {
        this.Health = h;
        this.Mana = m;
        this.Level = l;
        this.Strength = s;
        this.Intelligence = i;
    }
}
