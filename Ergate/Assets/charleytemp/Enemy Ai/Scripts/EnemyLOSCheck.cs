using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyLOSCheck : MonoBehaviour
{
    public SphereCollider Collider;
    public float fieldOfView = 90f;
    public LayerMask lineOfSightLayer;

    public delegate void GainSightEvent(Transform Target);
    public GainSightEvent onGainSight;
    public delegate void LoseSightEvent(Transform Target);
    public LoseSightEvent onLoseSight;

    private Coroutine CheckForLineOfSight;

    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!checkLineOfSight(other.transform))
        {
            CheckForLineOfSight = StartCoroutine(checkForLineOfSight(other.transform));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        onLoseSight?.Invoke(other.transform);
        if (CheckForLineOfSight != null)
        {
            StopCoroutine(CheckForLineOfSight);
        }
    }
    private bool checkLineOfSight(Transform Target)
    {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, direction);
        if(dotProduct >= Mathf.Cos(fieldOfView))
        {
            if(Physics.Raycast(transform.position, direction, out RaycastHit hit, Collider.radius, lineOfSightLayer))
            {
                onGainSight?.Invoke(Target);
                return true;
            }
        }
        return false;
    }
    private IEnumerator checkForLineOfSight(Transform Target)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.5f);
        while (!checkLineOfSight(Target))
        {
            yield return Wait;
        }
    }
}
