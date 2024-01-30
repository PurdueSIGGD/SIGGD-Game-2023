using UnityEngine;

namespace DefaultNamespace
{
    //[CreateAssetMenu(menuName = "Player Run Data")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
    public class PlayerData : MonoBehaviour
    {
        [Header("Run")]
        [SerializeField] private float maxMoveSpeed; //Target speed
        [SerializeField] private float acceleration; //Time to accelerate from 0 to the runMaxSpeed.
        [SerializeField] private float accelAmount; //The actual force (multiplied with speedDiff) applied to the player.
        [SerializeField] private float deceleration; //Time to decelerate from runMaxSpeed to 0.
        [SerializeField] private float decelAmount; //Actual force (multiplied with speedDiff) applied to the player .
        [SerializeField] private float lerpAmount;
        [SerializeField] private float turnSpeed;


        public float GetMaxSpeed()
        {
            return this.maxMoveSpeed;
        }

        public float GetAcceleration()
        {
            return this.acceleration;
        }

        public float GetDeceleration()
        {
            return this.deceleration;
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
            float runAccelAmount = (acceleration) / maxMoveSpeed;
            float runDecelAmount = (deceleration) / maxMoveSpeed;

            float runAcceleration = Mathf.Clamp(acceleration, 0.01f, maxMoveSpeed);
            float runDeceleration = Mathf.Clamp(deceleration, 0.01f, maxMoveSpeed);
        }
        
    }
}