using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvasLocation : MonoBehaviour
{
    [SerializeField] private GameObject parentGameObject;

    private RectTransform rectTransform;
    private Vector3 zeroRotationPosition;
    private Quaternion zeroRotationQuaternion;
    private Vector3 nineteyRotationPosition;
    private Quaternion nineteyRotationQuaternion;
    private Vector3 oneEightyRotationPosition;
    private Quaternion oneEightyRotationQuaternion;
    private Vector3 twoSeventyRotationPosition;
    private Quaternion twoSeventyRotationQuaternion;

    private void Start()
    {
        rectTransform = (RectTransform)gameObject.transform;

        zeroRotationPosition = new Vector3(0f, 0.75f, 0f);
        zeroRotationQuaternion = new Quaternion(0f, 0f, 0f, 0f);

        nineteyRotationPosition = new Vector3(.75f, 0f, 0f);
        nineteyRotationQuaternion = new Quaternion(0f, 0f, -90f, 0f);

        oneEightyRotationPosition = new Vector3(0f, -.75f, 0f);
        oneEightyRotationQuaternion = new Quaternion(0f, 0f, 180f, 0f);

        twoSeventyRotationPosition = new Vector3(-.75f, 0f, 0f);
        twoSeventyRotationQuaternion = new Quaternion(0f, 0f, 90f, 0f);
    }

    void Update()
    {
        Debug.Log(parentGameObject.transform.eulerAngles.z);
        if (parentGameObject.transform.eulerAngles.z == 0)
        {
            rectTransform.rotation = zeroRotationQuaternion;
            rectTransform.position = zeroRotationPosition;
        }
        if (parentGameObject.transform.eulerAngles.z == 90)
        {
            rectTransform.rotation = nineteyRotationQuaternion;
            rectTransform.position = nineteyRotationPosition;
        }
        if (parentGameObject.transform.eulerAngles.z == 180)
        {
            rectTransform.rotation = oneEightyRotationQuaternion;
            rectTransform.position = oneEightyRotationPosition;
        }
        if (parentGameObject.transform.eulerAngles.z == 270)
        {
            rectTransform.rotation = twoSeventyRotationQuaternion;
            rectTransform.position = twoSeventyRotationPosition;
        }
    }

}
