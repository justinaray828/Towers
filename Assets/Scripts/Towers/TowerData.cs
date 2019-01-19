using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script must be attached to each spell. It contains the data needed to be pulled
/// from the TowerManager class about spells
/// </summary>
public class TowerData : MonoBehaviour
{

    public int spellCost = 25;

    public int GetSpellCost()
    {
        return spellCost;
    }

    public void SetSpellCost(int newSpellCost)
    {
        spellCost = newSpellCost;
    }
}
