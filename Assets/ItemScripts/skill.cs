using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
    [Header("Identifiers")]
    [SerializeField]public string skillName;
    [SerializeField]public int skillID;
    [SerializeField]
    [Space]
    [Header("Effects")]
    [SerializeField]public float damageMod;
    [SerializeField]public string damageType;
    [SerializeField]public string debuff;
    [SerializeField]public string debuffTurns;
    [SerializeField]public bool selfTarget;

    public enum damageType {    //damage types can correspond to sprites or simply just for varierty in damage calculation
        phys,
        tech,
        psi,
        energy,
        chem

    }
    public enum debuff{     //don't have to correlate with the in-game represenation, just describes debuff stat effect
        poison,
        stun,
        epBurn,
        weaken
    }
}
