using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;


public class Character : MonoBehaviour
{
    // Annoying there isnt a quicker way to make this all public
    // if anyone has an idea on how to let me know
    // Character Identifiers 
    Part part;
    public int id;
    public string characterName;

    // Character stats < - Will change in accordance to combat design ~ Waiting on input

    public int hitpoints, energypoints, attack, defence, speed; 
    // #Notes for the player these will be calculated but for enemies these will be hand made potentially - or would be decided for bosses only etc.



    // Possible removal unsure currently
    public int xp;

    // PARTS SECTION POST PARTS CLASS CREATION

    // arm_parts << max 2 slots each arm? >>
    // chest_parts << max 3 slots? >>
    // head_parts << less sure on solid amount could have optics etc? >>
    // leg_parts << 2 slots? >>
    // hand_parts << 2 slots for each hand? >>
    // power_core << single slot effects some kind of MP system? >> ~ Input needed from combat design
    // persona_core << determines specialty of enemy specific types based off of Jungian archetypes perhaps determining certain skill sets >>
                  //#further note have early ideas for the concept e.g. certain party members will only be compatable with certain persona cores with some freedom for player choice
    



}
