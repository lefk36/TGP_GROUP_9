using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttack : AttackState
{
    protected override IEnumerator AttackCoroutine()
    {
        completed = false;
        playerScript.lockMovement = true;
        playerScript.lockFalling = true;
        if (animationTrigger != null)
        {
            playerAnimator.SetTrigger(animationTrigger);
        }
        Rigidbody rb = playerScript.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        RaycastHit hit;
        yield return new WaitForSeconds(attackBeginningTime);
        if(Physics.Raycast(playerScript.camera.transform.position, playerScript.camera.transform.forward, out hit, Mathf.Infinity))
        {
            attackInstance = GameObject.Instantiate(attackObject, playerScript.transform.position, Quaternion.Euler(0, 0, 0));
            attackInstance.transform.LookAt(hit.point);
        }
        else
        {
            attackInstance = GameObject.Instantiate(attackObject, playerScript.transform.position, Quaternion.Euler(0, 0, 0));
            attackInstance.transform.LookAt(playerScript.camera.transform.position + (playerScript.camera.transform.forward * 500));

        }
        if (!playerScript.isOnGround)
        {
            rb.AddForce(0, 1 * attackRange, 0, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(attackEndTime);
        playerScript.lockMovement = false;
        playerScript.lockFalling = false;
        completed = true;
    }
}
