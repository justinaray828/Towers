using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteUpdater : MonoBehaviour {


	enum Direction{North, East, West, South, NorthWest, NorthEast, SouthWest, SouthEast};

    [Header("GameObject Sprites to be Updated With Direction")]
	public Sprite northSprite;
	public Sprite eastSprite;
	public Sprite southSprite;
	public Sprite northEastSprite;
	public Sprite southEastSprite;

	private SpriteRenderer sr;
	private Direction playerDirection;

	// Use this for initialization
	void Start ()
    {
		//TagCheck ("Player"); //Script needs to be attached to player
		sr = gameObject.GetComponent<SpriteRenderer> ();
	}
	
    /// <summary>
    /// Updates the players direction ENUM based on input angle 
    /// </summary>
    /// <param name="characterAngle"></param>
	public void UpdateDirection(float characterAngle)
    {
		if (337.5f < characterAngle || characterAngle <= 22.5f)
        {
			playerDirection = Direction.West;
		}
        else if (22.5f < characterAngle && characterAngle <= 67.5f)
        {
			playerDirection = Direction.SouthWest;
		}
        else if (67.5f < characterAngle && characterAngle <= 112.5f)
        {
			playerDirection = Direction.South;
		}
        else if (112.5f < characterAngle && characterAngle <= 157.5f)
        {
			playerDirection = Direction.SouthEast;
		}
        else if (157.5f < characterAngle && characterAngle <= 202.5f)
        {
			playerDirection = Direction.East;
		}
        else if (202.5f < characterAngle && characterAngle <= 247.5f)
        {
			playerDirection = Direction.NorthEast;
		}
        else if (247.5f < characterAngle && characterAngle <= 292.5f)
        {
			playerDirection = Direction.North;
		}
        else if (292.5f < characterAngle && characterAngle <= 337.5f)
        {
			playerDirection = Direction.NorthWest;
		}
        else
        {
			Debug.LogError ("Character Angle is wrong: " + characterAngle);
		}

		UpdateSprite ();
	}

    /// <summary>
    /// Updates the sprite using direction ENUM
    /// </summary>
	public void UpdateSprite()
    {
		if (playerDirection == Direction.North)
        {
			sr.sprite = northSprite;
			sr.flipX = false;
		}
        else if (playerDirection == Direction.East)
        {
			sr.sprite = eastSprite;
			sr.flipX = false;
		}
        else if (playerDirection == Direction.South)
        {
			sr.sprite = southSprite;
			sr.flipX = false;
		}
        else if (playerDirection == Direction.West)
        {
			sr.sprite = eastSprite;
			sr.flipX = true;
		}
        else if (playerDirection == Direction.NorthEast)
        {
			sr.sprite = northEastSprite;
			sr.flipX = false;
		}
        else if (playerDirection == Direction.SouthEast)
        {
			sr.sprite = southEastSprite;
			sr.flipX = false;
		}
        else if (playerDirection == Direction.NorthWest)
        {
			sr.sprite = northEastSprite;
			sr.flipX = true;
		}
        else if (playerDirection == Direction.SouthWest)
        {
			sr.sprite = southEastSprite;
			sr.flipX = true;
		}
	}

	/// <summary>
	/// Checks the tag the script is attached to.
	/// </summary>
	/// <param name="tag">Tag.</param>
	void TagCheck(string tag)
    {
		if (gameObject.tag != tag)
        {
			Debug.LogError ("Script must be attached to GameObject with tag: " + tag);
		}
	}
}
