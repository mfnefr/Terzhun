using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Entity Stats", menuName = "Scriptable Objects/Entity Stats")]
public class EntityStats : ScriptableObject
{
    public string enemyName;
    public string maxHealth;
    public int armorClass;
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHA;
    public float range;
    public float combatSpeed;
    public Sprite sprite;
    public List<Attack> attacks = new List<Attack>();
}