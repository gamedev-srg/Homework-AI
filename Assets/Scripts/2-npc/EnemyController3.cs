using System;
using UnityEngine;

/**
 * This component patrols between given points, chases a given target object when it sees it, and rotates from time to time.
 */
[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Sabotager))]
public class EnemyController3: MonoBehaviour {
    [SerializeField] float radiusToWatch = 5f;
    [SerializeField] float probabilityToRotate = 0.2f;
    [SerializeField] float probabilityToStopRotating = 0.2f;
    //Enum class to control behaviour
    public enum Role {chaser,brave,coward,sabatoger};
    private Chaser chaser;
    private Sabotager sabotager;
    private Patroller patroller;
    private Rotator rotator;
    public Role role;
    
    private void Chase() {
        chaser.enabled = true;
        patroller.enabled = rotator.enabled = sabotager.enabled =false;
    }

    private void Patrol(int mode) { 
        
        patroller.enabled = true;
        patroller.changeMode(mode); //Change patrol mode.
        chaser.enabled = rotator.enabled = sabotager.enabled = false;
    }

    private void Rotate() {
        rotator.enabled = true;
        chaser.enabled = patroller.enabled = sabotager.enabled =false;
    }
    private void Sabotage()
    {
        //run towards the Sabotage target.
        sabotager.enabled = true;
        chaser.enabled = patroller.enabled = rotator.enabled =false;
    }

    private void Start() {
        chaser = GetComponent<Chaser>();
        patroller = GetComponent<Patroller>();
        rotator = GetComponent<Rotator>();
        sabotager = GetComponent<Sabotager>();
    }

    private void Update() {
        //activate role according to the current role enum.
        if (role == Role.chaser)
        {
            Chase();
        }
        else if (role == Role.brave || role == Role.coward) //same script for patrol, we just pass a different argumnt in the function.
        {
            Patrol((int)role);
        }
        else if (role == Role.sabatoger)
        {
            Sabotage();
        }
    }

 
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
 
