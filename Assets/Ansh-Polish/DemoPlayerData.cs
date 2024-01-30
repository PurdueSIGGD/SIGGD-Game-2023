using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayerData : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private float targetSpeed; //Target speed
    [SerializeField] private float accelDuration; //Time to accelerate from 0 to the runMaxSpeed.
    [SerializeField] private float accelAmount; //The actual force (multiplied with speedDiff) applied to the player.
    [SerializeField] private float decelDuration; //Time to decelerate from runMaxSpeed to 0.
    [SerializeField] private float decelAmount; //Actual force (multiplied with speedDiff) applied to the player .
    [SerializeField] private float lerpAmount;
    [SerializeField] private float turnSpeed;

    // Getters
            public float GetMaxSpeed()
        {
            return this.targetSpeed;
        }

        public float GetAcceleration()
        {
            return this.accelDuration;
        }

        public float GetDeceleration()
        {
            return this.decelDuration;
        }
        
        public float GetAccelAmount()
        {
            return this.accelAmount;
        }
        public float GetDecelAmount()
        {
            return this.decelAmount;
        }

        public float GetLerpAmount()
        {
            return this.lerpAmount;
        }
        public float GetTurnSpeed()
        {
            return this.turnSpeed;
        }
        private void OnValidate()
        {
            //Calculate are run accelDuration & decelDuration forces using formula: amount = ((1 / Time.fixedDeltaTime) * accelDuration) / runMaxSpeed
            float runAccelAmount = (accelDuration) / targetSpeed;
            float runDecelAmount = (decelDuration) / targetSpeed;

            float runAcceleration = Mathf.Clamp(accelDuration, 0.01f, targetSpeed);
            float runDeceleration = Mathf.Clamp(decelDuration, 0.01f, targetSpeed);
        }
}
