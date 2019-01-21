using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingTower : MonoBehaviour {

    [SerializeField] private GameObject punchTower; //Parent punch tower for animation update
    [SerializeField] private int damage;
    [SerializeField] private List<GameObject> targetList;
    
    private bool punching = false;
    private Animator punchTowerAnimator;
    private DelayBool punchRate;

	void Start ()
    {
        punchTowerAnimator = punchTower.GetComponent<Animator>();
        punchRate = new DelayBool(GetPunchAnimationTime());
        targetList = new List<GameObject>();
	}
	
	void Update ()
    {
        targetList.RemoveAll(item => item == null); //Removes GameObject from list if it is destroyed before update is called again

        if (punchTowerAnimator.GetBool("Punching") && CallDelay(punchRate))
        { 
            foreach (GameObject target in targetList.ToArray())
            {
                target.GetComponent<Health>().HurtGameObject(damage);
            }
        }

        if (targetList.Count == 0)
        {
            punchTowerAnimator.SetBool("Punching", false);
        }

	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Enemy")
        {
            targetList.Add(coll.gameObject);
            punchTowerAnimator.SetBool("Punching", true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            targetList.Remove(coll.gameObject);
        }
    }

    private float GetPunchAnimationTime()
    {
        float time = 0f;
        RuntimeAnimatorController ac = punchTowerAnimator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "Punch Tower_Punch")
            {
                return time = ac.animationClips[i].length;
            }
        }

        Debug.LogError("Punch Tower_Punch animation not found");
        return time;
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
