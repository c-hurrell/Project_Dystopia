using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;


public class Character : MonoBehaviour
{
    // Annoying there isnt a quicker way to make this all public
    // if anyone has an idea on how to let me know
    // Character Identifiers 
    [Header("Character Identifiers")]
    public int id;
    public string characterName;

    // Character stats < - Will change in accordance to combat design ~ Waiting on input
    [Header("Character Stats")]
    public int hitpoints, energypoints, attack, defence, speed; 
    // #Notes for the player these will be calculated but for enemies these will be hand made potentially - or would be decided for bosses only etc.



    // Possible removal unsure currently
    public int xp;

    // PARTS SECTION POST PARTS CLASS CREATION
    [Header("Character Parts")]
    public Part armsPart; //<< max 2 slots each arm? >>
    public Part chestPart; //<< max 3 slots? >>
    public Part headPart; //<< less sure on solid amount could have optics etc? >>
    public Part legsPart;
    public Part powerCore; //<< single slot effects some kind of MP system? >> ~ Input needed from combat design
    public Part personaCore; //<< determines specialty of enemy specific types based off of Jungian archetypes perhaps determining certain skill sets >>
    //#further note have early ideas for the concept e.g. certain party members will only be compatable with certain persona cores with some freedom for player choice

    // Add in Unity Event trigger? So when parts on a character are changed an event is raised?
    
    // Calculates the totals for each part
    public void StatTotals()
    {
        PartStatCalc(armsPart); 
        PartStatCalc(chestPart);
        PartStatCalc(headPart);
        PartStatCalc(legsPart);
        PartStatCalc(powerCore);
        PartStatCalc(personaCore);
    }
    // Used in to calculate the stat addition of a part
    private void PartStatCalc(Part part)
    {
        switch (part._statType)
        {
            case Part.StatType.Attack:
                attack += part._statVal;
                break;
            case Part.StatType.Defence:
                defence += part._statVal;
                break;
            case Part.StatType.Ep:
                energypoints += part._statVal;
                break;
            case Part.StatType.Hp:
                hitpoints += part._statVal;
                break;
            case Part.StatType.Speed:
                speed += part._statVal;
                break;
            default:
                Debug.Log(" > Error: Part must have a stat type");
                break;
        }
    }



}
