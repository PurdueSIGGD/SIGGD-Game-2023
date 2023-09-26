using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private GameObject currentWeapon;
    public GameObject sword;
    private delegate void Attack();
    Attack CurrentAttack;
    private Rigidbody currentRB;

    // Attack Method Variables
    private bool swingingSword;
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
        currentWeapon = sword;
        CurrentAttack = SwordAttack;
        currentRB = sword.GetComponent<Rigidbody>();
        swingingSword = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // currentWeapon.transform.position = Vector3.Lerp()
    }

    void SwordAttack() {
        if (!swingingSword) {
            swingingSword = true;
            timePassed = 0;
        }
    }

    void OnAttack(InputAction.CallbackContext cxt) {
        if (cxt.started) {
            CurrentAttack();
        }
    }
}
