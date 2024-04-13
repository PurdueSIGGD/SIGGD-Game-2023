using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;

public class MobNav : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform this_enemy;
    private Transform player;
    private NavMeshPath path;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    private Vector3 boxSize;
    [SerializeField] private float rayDist;
    [SerializeField] private float flankDist;
    private LayerMask detectEnemies;
    [SerializeField] private bool flanksEnemies;
    [SerializeField] private bool flanksPlayer;
    [SerializeField] private bool fleeing;
    [SerializeField] private bool attacksTurrets;
    [SerializeField] private float turretDetectionRad;
    private LayerMask detectTurrets;

    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        path = new NavMeshPath();
        boxSize = GetComponent<BoxCollider>().size;
        detectEnemies = LayerMask.NameToLayer("Enemy");
        detectEnemies = ~detectEnemies;
        detectTurrets = LayerMask.NameToLayer("Unit");
    }
    void FixedUpdate()
    {
        Vector3 targetLoc = Vector3.zero;
        if (attacksTurrets) {
            float theminDisnas = turretDetectionRad + 1;
            Collider[] potentialTargets = Physics.OverlapSphere(this_enemy.position, turretDetectionRad, detectTurrets);
            foreach (Collider c in potentialTargets) {
                float tempDsisf = Vector3.Distance(this_enemy.position, c.gameObject.transform.position);
                if (tempDsisf < theminDisnas) {
                    targetLoc = c.gameObject.transform.position;
                    theminDisnas = tempDsisf;
                }
            }
        }
        if (targetLoc == Vector3.zero) {
            targetLoc = player.position;
        }
        NavMesh.CalculatePath(this_enemy.position, targetLoc, NavMesh.AllAreas, path);
        if (path.corners.Length <= 1)
        {
            return;
        }
        Vector3 targetDir = path.corners[1] - this_enemy.position;
        targetDir = targetDir.normalized;
        if (fleeing) {
            targetDir = targetDir * -1;
            targetDir.y = targetDir.y * -1;
        }
        Vector3 newDir = Vector3.RotateTowards(this_enemy.forward, targetDir, turnSpeed * Time.fixedDeltaTime, 0.0f);


        Vector3 move_offset = Vector3.zero;

        this_enemy.rotation = Quaternion.LookRotation(newDir);
        move_offset = this_enemy.forward;

        if (flanksEnemies) {
            RaycastHit leftHit;
            RaycastHit rightHit;
            bool leftHitBool = Physics.Raycast(this_enemy.position + (this_enemy.right * -1 * boxSize.x * 0.51f), (this_enemy.right * -1), out leftHit, rayDist, detectEnemies);
            bool rightHitBool = Physics.Raycast(this_enemy.position + (this_enemy.right * boxSize.x * 0.51f), this_enemy.right, out rightHit, rayDist, detectEnemies);
            if (leftHitBool && !rightHitBool) {
                move_offset += this_enemy.right * 1;
            }
            else if (!leftHitBool && rightHitBool) {
                move_offset += this_enemy.right * -1;
            }
            else if (leftHitBool && rightHitBool) {
                move_offset += this_enemy.right * (rightHit.distance- leftHit.distance) * -1;
            } 
            else if (!leftHitBool && !rightHitBool)
            {
            }
            RaycastHit frontHit;


            //flank if behind other enemy
            if (Physics.Raycast((this_enemy.position + (this_enemy.forward * boxSize.z * 0.51f)), this_enemy.forward, out frontHit, rayDist, detectEnemies)) {
                Vector3 targetPos = frontHit.collider.gameObject.transform.position;
                float angle = Vector3.Angle(this_enemy.right, this_enemy.position - targetPos);
                Vector3 flank_offset = this_enemy.right;
                if (angle > 90) {
                    flank_offset *= -1;
                }
                move_offset += flank_offset;
            }
        }

        
        if (flanksPlayer) {
            //flank if near player
            if (Vector3.Distance(this_enemy.position, player.position) < flankDist) {
                Vector3 targetPos = player.position;
                float angle = Vector3.Angle(this_enemy.right, this_enemy.position - targetPos);
                Vector3 flank_offset = this_enemy.right;
                if (angle > 90) {
                    flank_offset *= -1;
                }
                move_offset += flank_offset;
                move_offset += 2 * this_enemy.forward;
            }
        }

        
        move_offset = move_offset.normalized;

        agent.Move(move_offset * speed * Time.fixedDeltaTime);

        
    }
}
