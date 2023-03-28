using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;

public class TurnHandler : MonoBehaviour
{
    public Player_Character combatant1;
    public Enemy_Character combatant2;
    public int turnInt; // 1 for player 2 for enemy
    // Start is called before the first frame update
    void Start()
    {
       if (combatant1.speed > combatant2.speed){
        turnInt = 1;
       }
       else{
        turnInt = 2;
       }
    }

  
}
