
using UnityEngine;

public abstract class Stats : ScriptableObject
{
    public int Health;
    public int Mana;
    public int Level;
    public int Strength;
    public int Intelligence;
    public abstract void Initialize(int h, int m, int l, int s, int i);
    public StatEvent OnStatsChanged = new StatEvent();

}
