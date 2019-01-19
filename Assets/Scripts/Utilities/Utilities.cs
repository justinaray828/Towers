using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

    public static Quaternion QuaternionBetweenTwoPoints(Vector3 position, Vector3 targetPosition)
    {
        Vector3 relativePosition = targetPosition - position;
        float AngleRad = Mathf.Atan2(relativePosition.x, relativePosition.y);
        float ang = (180 / Mathf.PI) * AngleRad;
        return Quaternion.AngleAxis(-ang + 90, Vector3.forward);
    }

    public static float AngleBetweenTwoPoints(Vector3 position, Vector3 targetPosition)
    {
        Vector3 relativePosition = targetPosition - position;
        float AngleRad = Mathf.Atan2(relativePosition.x, relativePosition.y);
        return (180 / Mathf.PI) * AngleRad;
    }

}
