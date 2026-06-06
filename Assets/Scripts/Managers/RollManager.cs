using UnityEngine;

public class RollManager : MonoBehaviour
{
    public static RollManager Instance{get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public int RollDice(string diceThrow)
    {
        string[] diceParts = diceThrow.Split('d');

        if(diceParts.Length != 2)
        {
            Debug.LogError("Invalid dice format. Use XdY format, e.g., 2d6.");
            return 0;
        }

        int numberOfDice = int.Parse(diceParts[0]);
        int typeOfDice = int.Parse(diceParts[1]);

        int totalRoll = 0;

        for(int i = 0; i < numberOfDice; i++)
        {
            int roll = Random.Range(1, typeOfDice + 1);
            totalRoll = totalRoll + roll;
        }

        return totalRoll;
    }
}