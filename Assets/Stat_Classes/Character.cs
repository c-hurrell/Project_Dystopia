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
    public double hitpoints, energypoints, attack, defence, speed; 
    // #Notes for the player these will be calculated but for enemies these will be hand made potentially - or would be decided for bosses only etc.



    // Possible removal unsure currently
    public int xp;

    // PARTS SECTION POST PARTS CLASS CREATION
    [Header("Character Parts")]
    public Part armsPart; 
    public Part chestPart;
    public Part headPart; 
    public Part legsPart;
    public Part powerCore; //<< single slot effects some kind of MP system? >> ~ Input needed from combat design
    //public Part personaCore; //<< determines specialty of enemy specific types based off of Jungian archetypes perhaps determining certain skill sets >> removing for now


    // Add in Unity Event trigger? So when parts on a character are changed an event is raised?
    
    // Calculates the totals for each part
    public void StatTotals()
    {
        PartStatCalc(armsPart); 
        PartStatCalc(chestPart);
        PartStatCalc(headPart);
        PartStatCalc(legsPart);
        PartStatCalc(powerCore);
        //PartStatCalc(personaCore);
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
    public void ChangePart(Part part)
    {
        switch (part.partType)
        {
            case Part.PartType.Head:
                headPart = part;
                StatTotals();
                break;
            case Part.PartType.Arms:
                armsPart = part;
                StatTotals();
                break;
            case Part.PartType.Chest:
                chestPart = part;
                StatTotals();
                break;
            case Part.PartType.PowerCore:
                powerCore = part;
                StatTotals();
                break;
            case Part.PartType.Legs:
                legsPart = part;
                StatTotals();
                break;
            default:
                Debug.Log(" > Error: Part doesn't have a type");
                break;
        }
    }



}
