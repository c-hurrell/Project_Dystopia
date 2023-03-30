using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;


public class Character : MonoBehaviour
{
    // Annoying there isnt a quicker way to make this all public
    // if anyone has an idea on how to let me know
    [Header("GameManager")] [SerializeField]
    public GameObject gameManager;
    [Space]
    // Character Identifiers 
    [Header("Character Identifiers")]
    public int id;
    public string characterName;

    // Character stats < - Will change in accordance to combat design ~ Waiting on input
    [Header("Character Stats")]
    public double hitpoints, energypoints, attack, defence, speed; 
    // #Notes for the player these will be calculated but for enemies these will be hand made potentially - or would be decided for bosses only etc.

    public List<skill> currentSkills;

    // Possible removal unsure currently
    public int xp;

    // PARTS SECTION POST PARTS CLASS CREATION
    [Header("Character Parts")]
    public Part armsPart; 
    public Part chestPart;
    public Part headPart; 
    public Part legsPart;

    public GameObject arms;
    public GameObject chest;
    public GameObject head;
    public GameObject legs;
    


    // Add in Unity Event trigger? So when parts on a character are changed an event is raised?

    void Start()
    {
        armsPart = arms.GetComponent<Part>();
        chestPart = chest.GetComponent<Part>();
        headPart = head.GetComponent<Part>();
        legsPart = legs.GetComponent<Part>();
        
        StatTotals();
    }
    
    // Calculates the totals for each part
    public void StatTotals()
    {
        PartStatCalc(armsPart); 
        PartStatCalc(chestPart);
        PartStatCalc(headPart);
        PartStatCalc(legsPart);
    }
    // Used in to calculate the stat addition of a part
    public void PartStatCalc(Part part)
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
    public void PartSkillAdd()
    {
        currentSkills.Add(headPart._partSkill);
        currentSkills.Add(armsPart._partSkill);
        currentSkills.Add(chestPart._partSkill);
        currentSkills.Add(legsPart._partSkill);
    }
    
    
    



}
