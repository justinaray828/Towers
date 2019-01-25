using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvasLocation : MonoBehaviour
{
    [SerializeField] private GameObject healthCanvas;
    
    Vector3 healthPosition;

    private void Start()
    {
        healthPosition = new Vector3(transform.position.x, transform.position.y + 1, 0);
    }

    void Update()
    {

    }

}
