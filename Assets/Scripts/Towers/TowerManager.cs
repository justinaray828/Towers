using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTags { WallTower, ShootingTower, PunchTower, GlueTower }

public class TowerManager : MonoBehaviour {

    [Header("Dart Variables")]
    [SerializeField] private int dartManaCost = 5;
    [SerializeField] private float dartDelayTime = .1f;
    [SerializeField] private int dartDamage = 5;
    [SerializeField] private float dartSpeed = 20f;

    [SerializeField] private int wallTowerManaCost = 25;
    [SerializeField] private int punchTowerManaCost = 20;
    [SerializeField] private int shootingTowerManaCost = 10;
    [SerializeField] private int glueTowerManaCost = 10;

    private PlayerManager playerManager;

    public int[,] towerFieldStatusArray;
    public GameObject[,] towerGameObjectArray;

    private PooledShooting pooledShooting;

    [SerializeField] private GameObject[] towerArray;

    private Health health;

	// Use this for initialization
	void Start ()
    {
        towerFieldStatusArray = new int[16, 16];
        towerGameObjectArray = new GameObject[16, 16];
        playerManager = GetComponent<PlayerManager>();
        pooledShooting = new PooledShooting();
        health = GetComponent<Health>();
        //UpdateTowers();
    }

    /// <summary>
    /// Gets dart from pooledobjects and sets it active
    /// </summary>
    public void ShootDart(Transform playerTransform, Quaternion dartRotation)
    {
        playerManager.UseMana(dartManaCost);
        pooledShooting.ShootProjectile(pooledShooting.dart, dartDamage, dartSpeed, playerTransform.position, dartRotation);
    }

    /// <summary>
    /// Returns true if successfully places tower
    /// </summary>
    /// <param name="towerSelected"></param>
    /// <param name="clickUpLocation"></param>
    /// <returns></returns>
    public bool PlaceTower(int towerSelected, Vector3 clickUpLocation, Vector3 clickDownLocation)
    {
        int xLocation = Mathf.RoundToInt(clickDownLocation.x);
        int yLocation = Mathf.RoundToInt(clickDownLocation.y);
        float towerAngle = GetTowerAngle(clickUpLocation, clickDownLocation);

        if (TowerPlacementCheck(towerSelected, xLocation, yLocation))
        {
            if (playerManager.UseManaCap(GetTowerResourceCost(towerSelected)))
            {
                GameObject spellSelectedGameObject = towerArray[towerSelected];
                Vector3 towerLocation = new Vector3(xLocation, yLocation, 0);

                towerGameObjectArray[xLocation, yLocation] = Instantiate(spellSelectedGameObject, towerLocation, Quaternion.identity);
                towerGameObjectArray[xLocation, yLocation].transform.Rotate(0, 0, towerAngle);
                towerFieldStatusArray[xLocation, yLocation] = towerSelected;
                return true;
            }
        }
        return false;
    }

    public float GetTowerAngle(Vector3 targetPosition, Vector3 position)
    {
        float ang = Utilities.AngleBetweenTwoPoints(position, targetPosition);
        ang = Mathf.RoundToInt(ang/90) * 90 * -1; // makes ang 0, 90, -180 or -90
        return ang;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// True: Tower CAN be placed here
    /// False: Tower CANNOT be placed here
    /// </returns>
    private bool TowerPlacementCheck(int spellSelected, int xLocation, int yLocation)
    {
        if(xLocation < 0 || xLocation > 15 || yLocation < 0 || yLocation > 15) //Check if tower is in bounds
        {
            return false;
        }
        else if(!towerGameObjectArray[xLocation, yLocation]) //Checks if location is null
        {
            return true;
        }
        else
        {
            DestroyTower(xLocation, yLocation);
            return false;
        }
    }

    /// <summary>
    /// Destroys built tower on map at given location
    /// </summary>
    /// <param name="xLocation"></param>
    /// <param name="yLocation"></param>
    public void DestroyTower(int xLocation, int yLocation)
    {
        playerManager.ReleaseManaCap(GetTowerResourceCost(towerFieldStatusArray[xLocation, yLocation]));
        Destroy(towerGameObjectArray[xLocation, yLocation]);
    }

    /// <summary>
    /// Pass transform position of tower being destroyed
    /// </summary>
    /// <param name="towerLocation"></param>
    public void DestroyTower(Vector3 towerLocation)
    {
        playerManager.ReleaseManaCap(GetTowerResourceCost(towerFieldStatusArray[Mathf.RoundToInt(towerLocation.x), Mathf.RoundToInt(towerLocation.y)]));
        Destroy(towerGameObjectArray[Mathf.RoundToInt(towerLocation.x), Mathf.RoundToInt(towerLocation.y)]);
    }

    /// <summary>
    /// Returns the mana cost of a fireball
    /// </summary>
    /// <returns></returns>
    public int GetDartManaCost()
    {
        return dartManaCost;
    }

    /// <summary>
    /// The fireball delay is set in this component since this component handles
    /// all spells. This will be used if the player gets a buff or levels up their
    /// casting then it would be changed here.
    /// </summary>
    /// <returns></returns>
    public float GetDartDelayTime()
    {
        return dartDelayTime;
    }

    public GameObject[] GetTowerArray()
    {
        return towerArray;
    }

    public void HurtTower(int damage, Vector3 towerTransform)
    {

    }

    /// <summary>
    /// TODO: Need to add TowerManager script to towers to read manaCost off of
    /// </summary>
    /// <param name="spellSelected"></param>
    /// <returns></returns>
    public int GetTowerResourceCost(int spellSelected)
    {
        switch (spellSelected)
        {
            case 0:
                return wallTowerManaCost;
            case 1:
                return punchTowerManaCost;
            case 2:
                return shootingTowerManaCost;
            case 3:
                return glueTowerManaCost;
            default:
                Debug.LogError("GetSpellManaCost in TowerManager recieved a spell out of range");
                return 1;
        }
    }

    [System.Obsolete("This is an obsolete method")]
    public void UpdateTowers()
    {
        try
        {
            towerArray[0].GetComponent<TowerData>().SetSpellCost(punchTowerManaCost);
            towerArray[1].GetComponent<TowerData>().SetSpellCost(wallTowerManaCost);
            towerArray[2].GetComponent<TowerData>().SetSpellCost(shootingTowerManaCost);
            towerArray[3].GetComponent<TowerData>().SetSpellCost(glueTowerManaCost);
        }
        catch(System.IndexOutOfRangeException E)
        {
            Debug.LogError("Add towers to player GameObject TowerManager component in inspector");
        }
    }
}
