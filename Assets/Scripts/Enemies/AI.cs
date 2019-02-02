using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

[RequireComponent(typeof(Bullets))]
public class AI : MonoBehaviour {

    [SerializeField] private bool followPlayer = false;
    [SerializeField] private bool keepDistance = false;
    [SerializeField] private bool followPath = false;

    [SerializeField] private float speedTotal = 10f;
    [SerializeField] private float distanceFromPlayer = 1f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private int playerDamage = 1;

    private bool inFrontOfEnemy = false;

    private float speedCurrent;

    private int totalPathPoints;
    private int currentPathPoint;

    [SerializeField] private GameObject AI_Path;
    private GameObject player;

    public Vector3[] pathPoints;

    private LineRenderer pathLine;

    private Bullets bullets;

    private TowerManager towerManager;

    private Delay canShootDelay;
    private Delay attackDelay;

    private RaycastHit2D hit;

    private GameObject[] towerArray;

    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        towerManager = player.GetComponent<TowerManager>();
        towerArray = towerManager.GetTowerArray();

        if(followPath) GetAIPath();
        speedCurrent = speedTotal;
        attackDelay = new Delay(attackRate);
	}


    private void FixedUpdate()
    {
        RaycastCheck(); //Updates hit with raycast collider
        TowerCheck(); //Uses hit to determin if a tower is in front of GameObject then attack it if so
        PlayerCheck();

        if (!inFrontOfEnemy)
        {
            if (followPlayer)
            {
                FollowPlayer();
            }
            else if (followPath)
            {
                FollowPath(pathPoints);
            }
        }

        if (keepDistance)
            KeepDistanceFromPlayer(distanceFromPlayer);
    }

    private void FollowPath(Vector3[] path)
    {

        Vector3 destination = new Vector3(pathPoints[currentPathPoint].x, pathPoints[currentPathPoint].y,0);

        transform.position = Vector2.MoveTowards(transform.position, pathPoints[currentPathPoint], speedCurrent * Time.deltaTime);

        if ( transform.position == destination )
        {
            if (currentPathPoint == totalPathPoints - 1)
            {
                currentPathPoint = 0;
            }
            else
            {
                currentPathPoint++;
            }
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speedCurrent * Time.deltaTime);
        transform.rotation = Utilities.QuaternionBetweenTwoPoints(transform.position, player.transform.position);
    }

    /// <summary>
    /// Cast raycast to detect for a collider
    /// </summary>
    private RaycastHit2D RaycastCheck()
    {
        //Ignores layer 2 (raycast ignore) and 11 (enemy)
        var layerMask = ~((1 << 2) | (1 << 11)); 

        Vector2 relativePosition = player.transform.position - transform.position;
        hit = Physics2D.Raycast(transform.position, relativePosition, .25f, layerMask);
        Debug.DrawRay(transform.position, relativePosition);
        return hit;
    }

    private void TowerCheck()
    {
        if (hit.collider)
        {
            foreach (GameObject tower in towerArray)
            {
                if (tower.tag == hit.collider.tag)
                {
                    inFrontOfEnemy = true;
                    AttackGameObject(hit.collider.gameObject);
                    return;
                }
            }
        }

        inFrontOfEnemy = false;
    }

    private void PlayerCheck()
    {
        if(hit.collider)
        {
            if (hit.collider.tag == "Player")
            {
                AttackPlayer();
            }
        }
    }

    private void AttackPlayer()
    {
        if (attackDelay.CallDelay())
        {
            player.GetComponent<PlayerManager>().HurtPlayer(playerDamage);
        }
    }

    private void AttackGameObject(GameObject gameObject)
    {
        if(attackDelay.CallDelay())
        {
            gameObject.GetComponent<Health>().HurtGameObject(attackDamage);
        }
    }

    private void KeepDistanceFromPlayer(float distance)
    {
        
    }

    private void GetAIPath()
    {

        if (AI_Path)
        {
            pathLine = AI_Path.GetComponent<LineRenderer>();
            totalPathPoints = pathLine.positionCount;
            pathPoints = new Vector3[totalPathPoints];
            pathLine.GetPositions(pathPoints);
            currentPathPoint = 0;
        }
        else
        {
            Debug.LogError("No AI_Path was added to AI Script");
        }
        
    }

    public float GetGameObjectSpeed()
    {
        return speedCurrent;
    }

    public void SetGameObjectSpeed(float newSpeed)
    {
        speedCurrent = newSpeed;
    }

    public void ResetGameObjectSpeed()
    {
        speedCurrent = speedTotal;
    }

}
