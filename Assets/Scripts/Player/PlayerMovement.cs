using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData data;
    private Rigidbody RB;
    private Vector3 input;
    private Vector2 direction;
    void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {        
        RB = GetComponent<Rigidbody>();
        data = GetComponent<PlayerData>();
    }

    private void move()
    {
        input = new Vector3(direction.x, 0, direction.y)* Time.fixedDeltaTime;
        Vector3 targetSpeed = input * data.GetMaxSpeed();
        targetSpeed = Vector3.Lerp(RB.velocity, targetSpeed, data.GetLerpAmount());

        bool isStopping = input == Vector3.zero || Vector3.Dot(input, RB.velocity) < 0;
        float accelRate = (!isStopping && targetSpeed.magnitude > 0.01f) ? data.GetAccelAmount() : data.GetDecelAmount();

        if(RB.velocity.magnitude > targetSpeed.magnitude && Vector3.Dot(RB.velocity, targetSpeed) > 0 && targetSpeed.magnitude > 0.01f )
        {
            accelRate = 0; 
        }

        Vector3 speedDif = targetSpeed - RB.velocity;
        Vector3 movement = speedDif * accelRate;

        RB.AddForce(movement, ForceMode.Force);
    }

    private void look()
    {        
        Vector3 viewInput = new Vector3(direction.x, 0, direction.y) ;

        if (viewInput != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(viewInput, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, data.GetTurnSpeed() * Time.fixedDeltaTime);
        }
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        Debug.Log("new input is getting things");
    }
    private void FixedUpdate()
    {
        look();
        move();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}


