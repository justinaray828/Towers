using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueTower : MonoBehaviour {

    [SerializeField] float slowTargetByThisMuch;
    [SerializeField] float slowestSpeed;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            AI targetAI = coll.GetComponent<AI>();
            float targetSpeed = targetAI.GetGameObjectSpeed();
            float newTargetSpeed = targetAI.GetGameObjectSpeed() - slowTargetByThisMuch;

            if (newTargetSpeed > slowestSpeed)
            {
                targetAI.SetGameObjectSpeed(newTargetSpeed);
            }
            else
            {
                targetAI.SetGameObjectSpeed(slowestSpeed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            coll.GetComponent<AI>().ResetGameObjectSpeed();
        }
    }
}
