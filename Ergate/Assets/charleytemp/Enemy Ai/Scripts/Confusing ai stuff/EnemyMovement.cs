using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public LayerMask hideableLayers;
    public EnemyLOSCheck lineOfSightChecker;
    public NavMeshAgent Agent;
    [Range(-1, 1)] [Tooltip("Lower number is a better hiding spot")] public float hideSensitivity = 0;

    private Coroutine movementCoroutine;
    private Collider[] Colliders = new Collider[10]; //change dependant on scene complexity. More has more options for the enemy, Less is move performant.

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();

        lineOfSightChecker.onGainSight += HandleGainSight;
        lineOfSightChecker.onLoseSight += HandleLoseSight;
    }

    private void HandleGainSight(Transform Target)
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine = StartCoroutine(Hide(Target));
    }
    private void HandleLoseSight(Transform Target)
    {                                                       //
        if (movementCoroutine != null)                      //Need a more interesting thing here
        {                                                   //for while there is nothing to hide from
            StopCoroutine(movementCoroutine);               //
        }
    }

    private IEnumerator Hide(Transform Target)
    {
        while (true)
        {
            int hits = Physics.OverlapSphereNonAlloc(Agent.transform.position, lineOfSightChecker.Collider.radius, Colliders, hideableLayers);

            for (int i = 0; i < hits; i++)
            {
                if(NavMesh.SamplePosition(Colliders[i].transform.position, out NavMeshHit hit, 2f, Agent.areaMask))
                {
                    if(!NavMesh.FindClosestEdge(hit.position, out hit, Agent.areaMask))
                    {
                        Debug.LogError($"Unable to find edge close to {hit.position}. (This Error means the NavMesh or Layers are messed up somewhere.)");
                    }
                    if(Vector3.Dot(hit.normal, (Target.position - hit.position).normalized) < hideSensitivity)
                    {
                        Agent.SetDestination(hit.position);
                        break;
                    }
                }
                else
                {
                    Debug.LogError($"unable to find a hiding spot. Make the Collider array bigger");
                }
            }
        }
    }
    // I got confused and tired at this point will go back to it later


}
