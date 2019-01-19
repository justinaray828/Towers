using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] private Slider manaCap; //Max mana will refill to
    [SerializeField] private Slider availableMana; //Mana available to use

    /// <summary>
    /// Initilizes UI Elements
    /// </summary>
    private void Start()
    {
        int manaMinValue = 0;
        int manaMaxValue = 100;
        UpdateManaMaxMinValues(manaMinValue, manaMaxValue);
    }

    /// <summary>
    /// Updates UI Element for the mana cap.
    /// </summary>
    /// <param name="newValue"></param>
    public void UpdateManaCap(int newValue)
    {
        manaCap.value = newValue;
    }

    /// <summary>
    /// Updates UI Element for players current mana.
    /// </summary>
    /// <param name="newValue"></param>
    public void UpdatePlayerMana(int newValue)
    {
        availableMana.value = newValue;
    }

    /// <summary>
    /// Update the minimum and maximum mana values
    /// </summary>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    public void UpdateManaMaxMinValues(int minimum, int maximum)
    {
        manaCap.minValue = minimum;
        manaCap.maxValue = maximum;
        availableMana.minValue = minimum;
        availableMana.maxValue = maximum;
    }
}


