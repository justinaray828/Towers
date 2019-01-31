using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class handles movement and control actions
/// ******Contains Update Method******
/// </summary>
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TowerManager))]
public class GameController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = .3f;
    [SerializeField] private float towerSelectDelayTime = .1f;
    [SerializeField] private float towerBuildDelayTIme = .1f;
    [SerializeField] private GameObject clickLocationSprite;
    private GameObject clickLocationSpriteTemp;

    private Animator kidAnimator;

    [Tooltip("Needs the GameObject containing the tower toggles in UI")]
    [SerializeField] private GameObject spellSelect;
    [SerializeField] private Toggle[] towers;

	private Camera cam;
	private Rigidbody2D rb2D;

	private int numberOfTowers = 4; //Currently the game supports 4 towers
	private int currentTower = 0; 

	private Delay fireballDelay;
	private Delay towerSelectDelay;
    private Delay spellCastDelay;

	private CharacterSpriteUpdater characterSpriteUpdater;
    private TowerManager TowerManager;

	//Currently used in shoot to shoot in correct direction and rotate sprite
	private Quaternion rotation; 
    private Vector3 mousePosition;

    private bool axisInUse = false; //Needed so towers don't keep getting built

    private Vector3 clickDownMousePosition;


    /// <summary>
    /// Checks if script is on Player
    /// Sets the main camera
    /// </summary>
    void Start()
    {
		TagCheck ("Player");

        //Get Game Components
        cam = Camera.main;
        rb2D = gameObject.GetComponent<Rigidbody2D> ();
        rb2D.freezeRotation = true;
		characterSpriteUpdater = gameObject.GetComponent<CharacterSpriteUpdater> ();
        //playerManager = gameObject.GetComponent<PlayerManager>();
        TowerManager = gameObject.GetComponent<TowerManager>();
        kidAnimator = GetComponent<Animator>();

		//Initialize the spells
		towers = new Toggle[numberOfTowers]; 
		FillSpellArray ();
		TowerToggle (currentTower);

        //Sets the DelayBool information
        //fireballDelay = new DelayBool(TowerManager.GetDartDelayTime());
        fireballDelay = new Delay(TowerManager.GetDartDelayTime());
		towerSelectDelay = new Delay(towerSelectDelayTime);
        spellCastDelay = new Delay(towerBuildDelayTIme);
	}

	void Update ()
    {
		Movement ();
		Direction ();
		Shoot ();
		TowerSelect ();
        BuildTower();
	}

	/// <summary>
	/// Takes keyboard input to move sprite on screen
	/// </summary>
	void Movement()
    {
        float translationX = Input.GetAxis("Horizontal");// * playerSpeed * Time.deltaTime;
        float translationY = Input.GetAxis("Vertical");// * playerSpeed * Time.deltaTime;
        Vector2 movement = new Vector2(translationX, translationY);
        rb2D.AddForce(movement * playerSpeed);

        //Animation update
        float movementMagnitude = movement.magnitude;
        if (movementMagnitude > 0)
        {
            kidAnimator.SetBool("Running", true);
            kidAnimator.speed = movementMagnitude * 2;
        }
        else
        {
            kidAnimator.SetBool("Running", false);
            kidAnimator.speed = 1; //reset animator speed
        }

        //transform.Translate(translationX, translationY, 0);
    }

	/// <summary>
	/// Uses Mouse position to move the orientation of sprite on screen
	/// </summary>
	void Direction()
    {
		mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); //Gets the mouse position on screen
		Vector2 charPos2D = new Vector2 (transform.position.x, transform.position.y); //Gets character position
		// Get Angle in Radians
		float AngleRad = Mathf.Atan2(charPos2D.y - mousePosition.y, charPos2D.x - mousePosition.x);
		// Get Angle in Degrees
		float ang = (180 / Mathf.PI) * AngleRad;

		rotation = Quaternion.AngleAxis (ang + 180, Vector3.forward);

		if (ang < 0)
			ang = 360 + ang;

        transform.rotation = rotation;
		//characterSpriteUpdater.UpdateDirection (ang);
	}

	/// <summary>
	/// Checks the tag the script is attached to.
	/// </summary>
	/// <param name="tag">Tag.</param>
	private void TagCheck(string tag)
    {
		if (gameObject.tag != tag)
        {
			Debug.LogError ("Script must be attached to GameObject with tag: " + tag);
		}
	}

	/// <summary>
	/// Shoots Dart
	/// </summary>
	private void Shoot()
    {
		if ( Input.GetAxis("Shoot") == 1 && fireballDelay.CallDelay())
        {
            TowerManager.ShootDart(transform, rotation);
		}
	}

    /// <summary>
    /// Build a tower
    /// </summary>
    private void BuildTower()
    {
        if (Input.GetAxis("SpellCast") != 0)
        {
            if (axisInUse == false)
            {
                clickDownMousePosition = mousePosition;
                clickLocationSpriteTemp = Instantiate(clickLocationSprite, new Vector3 (Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y),0),Quaternion.identity);
                axisInUse = true;
            }
        }
        if (Input.GetAxis("SpellCast") == 0 && axisInUse == true && spellCastDelay.CallDelay())
        {
            TowerManager.PlaceTower(currentTower, mousePosition, clickDownMousePosition);
            Destroy(clickLocationSpriteTemp);
            axisInUse = false;
        }
    }



	/// <summary>
	/// Select which tower is toggled
	/// </summary>
	void TowerSelect()
    {
        if (Input.GetAxis("SpellSelect") == 1 && towerSelectDelay.CallDelay())
        {
            if (currentTower != numberOfTowers - 1)
            {
                currentTower++;
            }
            else
            {
                currentTower = 0;
            }

            TowerToggle(currentTower);
        }
        else if (Input.GetAxis("SpellSelect") == -1 && towerSelectDelay.CallDelay())
        {
            if (currentTower != 0)
            {
                currentTower--;
            }
            else
            {
                currentTower = numberOfTowers - 1;
            }

            TowerToggle(currentTower);
        }
        else if (Input.GetAxis("Spell1") == 1 && towerSelectDelay.CallDelay())
        {
            currentTower = 0;
            TowerToggle(currentTower);
        }
        else if (Input.GetAxis("Spell2") == 1 && towerSelectDelay.CallDelay())
        {
            currentTower = 1;
            TowerToggle(currentTower);
        }
        else if (Input.GetAxis("Spell3") == 1 && towerSelectDelay.CallDelay())
        {
            currentTower = 2;
            TowerToggle(currentTower);
        }
        else if (Input.GetAxis("Spell4") == 1 && towerSelectDelay.CallDelay())
        {
            currentTower = 3;
            TowerToggle(currentTower);
        }
	}

	/// <summary>
	/// A method to error check the tower being turned on
	/// </summary>
	/// <param name="tower">tower to toggle on</param>
	void TowerToggle(int tower)
    {
		if (tower > numberOfTowers - 1)
        {
			Debug.LogError ("Input tower is larger than number of towers");
		} 
		else if (tower < 0)
        {
			Debug.LogError ("Cannot input towers below 0");
		} 
		else
        {
			towers [tower].isOn = true;
		}
	}

	/// <summary>
	/// Uses the spellSelect UI GameObject and fills the spells array with
	/// all the children GameObjects
	/// </summary>
	void FillSpellArray()
    {
		int i = 0;

		foreach (Transform child in spellSelect.transform)
        {
			if (i < numberOfTowers)
            {
				towers [i] = child.GetComponent<Toggle>();
				i++;
			} 
			else
            {
				Debug.LogError ("Too many spells under the TowerToggle or numberOfTowers is not high enough");
			}

		}
	}

}
