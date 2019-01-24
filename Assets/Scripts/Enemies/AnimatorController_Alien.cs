using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Updates Alien Animations
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class AnimatorController_Alien : MonoBehaviour
{
    AlienAnimationStates alienAnimationStates;

    Animator animator;

    Rigidbody2D rb2D;

    Vector3 previousPosition;

    bool isRunning = false;

    void Start()
    {
        alienAnimationStates = new AlienAnimationStates();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        previousPosition = transform.position;
    }

    void Update()
    {
        Running();
    }

    void Running()
    {
        isRunning = (previousPosition != transform.position) ? true : false;
        previousPosition = transform.position;
        animator.SetBool(alienAnimationStates.Running, isRunning);
    }
}
