using UnityEngine;

[System.Serializable]
public class Attack
{
    public static string HIT_DICE = "1d20";
    public string name;
    public string diceThrow;
    public DamageType damageType;

    public Attack(string name, string diceThrow, DamageType damageType)
    {
        this.name = name;
        this.diceThrow = diceThrow;
        this.damageType = damageType;
    }
}