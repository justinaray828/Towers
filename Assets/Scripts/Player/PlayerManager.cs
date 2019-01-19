using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    //Health Variables
    [SerializeField] private int maxPlayerHealth;
    [SerializeField] private int currentPlayerHealth;
    [SerializeField] private Transform healthParent;
    [SerializeField] private Transform[] playersHearts;

    //Mana Variables
    [SerializeField] private int maxPlayerMana;
    [SerializeField] private int currentPlayerMana;
    [SerializeField] private int currentPlayerManaCap;
    [SerializeField] private float manaRegenRate;
    [SerializeField] private int manaRegenAmount;
    [SerializeField] private bool canRegenMana = true;
    private DelayBool manaDelay;

    //UI Control
    [SerializeField] private UIController uiController;

    // Use this for initialization
    void Start ()
    {
        ErrorCheck();
        playersHearts = new Transform[maxPlayerHealth];
        InitilizePlayerHealth();
        InitilizePlayerMana();
        manaDelay = new DelayBool(manaRegenRate);
    }

    private void Update()
    {
        ManaRegen();
    }

    /// <summary>
    ///     Public method to be called to Damage the player
    /// </summary>
    /// <param name="damage">
    ///     How much damage was done to the player
    /// </param>
    /// <returns>
    ///     True: The player is still Alive
    ///     False: The player is dead
    /// </returns>
    public bool HurtPlayer(int damage)
    {
        int healthTest = currentPlayerHealth - damage;

        currentPlayerHealth = healthTest;
        UpdateHealth();

        if (healthTest > 0)
        {
            return true;
        }
        else
        {
            gameObject.SetActive(false);
            return false;
        }
    }

    /// <summary>
    ///     Public method to be called to use the characters Mana
    /// </summary>
    /// <param name="manaUsed">
    ///     How much Mana was used
    /// </param>
    /// <returns>
    ///     True: Enough mana was left to use
    ///     False: Not enough mana left
    /// </returns>
    public bool UseMana(int manaUsed)
    {
        int manaTest = currentPlayerMana - manaUsed;

        if (manaTest >= 0)
        {
            currentPlayerMana = manaTest;
            uiController.UpdatePlayerMana(currentPlayerMana);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///     Public method to be called to update total mana left. 
    ///     Used for tower placements and buffs that require constant mana use.
    /// </summary>
    /// <param name="manaUsed">
    ///     How much Mana was used
    /// </param>
    /// <returns>
    ///     True: Enough mana was left to use
    ///     False: Not enough mana left
    /// </returns>
    public bool UseManaCap(int manaUsed)
    {
        int manaTest = currentPlayerMana - manaUsed;

        if (manaTest >= 0)
        {
            currentPlayerMana = manaTest;
            currentPlayerManaCap = currentPlayerManaCap - manaUsed;
            uiController.UpdatePlayerMana(currentPlayerMana);
            uiController.UpdateManaCap(currentPlayerManaCap);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Releases specified amount on manaCap
    /// </summary>
    /// <param name="manaUsed">
    ///     Amount to be regained on mana cap
    /// </param>
    public void ReleaseManaCap(int manaUsed)
    {
        if( (manaUsed + currentPlayerManaCap) > maxPlayerMana )
        {
            Debug.LogError("Mana cap cannot exceed maxPlayerMana");
        }
        else
        {
            currentPlayerManaCap += manaUsed;
            uiController.UpdateManaCap(currentPlayerManaCap);
        }
    }

    /// <summary>
    ///     Public method to be called to turn on or off mana regen
    /// </summary>
    /// <param name="boolean">
    ///     True: Mana will regenerate
    ///     False: Mana will not regenerate
    /// </param>
    public void CanManaRegen(bool boolean)
    {
        canRegenMana = boolean;
    }

    /// <summary>
    /// Called by HurtPlayer()
    /// to update the players health. 
    /// </summary>
    private void UpdateHealth()
    {
        for(int i = 0; i < maxPlayerHealth; i++)
        {
            if (i < currentPlayerHealth)
            {
                playersHearts[i].gameObject.SetActive(true);
            }
            else
            {
                playersHearts[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Called by UPDATE method to have the mana regen
    /// </summary>
    private void ManaRegen()
    {
        if(currentPlayerManaCap > currentPlayerMana && CallDelay(manaDelay))
        {
            currentPlayerMana += manaRegenAmount;
            uiController.UpdatePlayerMana(currentPlayerMana);
        }
    }

    /// <summary>
    /// Finds all the heart Transforms under the Health GameObject in the inspector
    /// and adds them to playersHearts array
    /// </summary>
    private void InitilizePlayerHealth()
    {
        int i = 0;

        foreach(Transform playerHeart in healthParent)
        {
            playersHearts[i] = playerHeart;

            if(i >= currentPlayerHealth)
            {
                playersHearts[i].gameObject.SetActive(false);
            }

            i++;
        }
    }

    /// <summary>
    /// Initilize Players Manabars 
    /// </summary>
    private void InitilizePlayerMana()
    {
        currentPlayerManaCap = maxPlayerMana;
        currentPlayerMana = maxPlayerMana;
        uiController.UpdateManaMaxMinValues(0, maxPlayerMana);
    }

    /// <summary>
    /// Check for errors that may arise 
    /// </summary>
    private void ErrorCheck()
    {
        if(healthParent == null)
        {
            Debug.LogError("No healthParent Transform was added to PlayerManager");
        }
        if(currentPlayerHealth > maxPlayerHealth)
        {
            Debug.LogError("currentPlayerHealth cannot exceed maxPlayerHealth");
        }
    }

    //DelayBool Timer
    //--------------------------------------------------------
    private IEnumerator Delay(DelayBool delayBool)
    {
        delayBool.delayBoolState = false;
        yield return new WaitForSeconds(delayBool.delayTime);
        delayBool.delayBoolState = true;
    }

    /// <summary>
    /// Starts Delay and returns true if the delay starts and false if the delay is in progress
    /// </summary>
    /// <param name="delayBool"></param>
    /// <returns></returns>
    private bool CallDelay(DelayBool delayBool)
    {
        if (delayBool.delayBoolState == true)
        {
            StartCoroutine(Delay(delayBool));
            return true;
        }
        else
        {
            return false;
        }
    }
    //--------------------------------------------------------
    //
}
