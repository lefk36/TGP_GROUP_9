using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerLockOnAdrian : MonoBehaviour
{
    //private Collider[] enemyColliders;
    private List<Collider> allCollideers = new List<Collider>();
    private List<Collider> enemyColliders = new List<Collider>();
    private List<Collider> orderedColliders = new List<Collider>();
    public Camera m_camera;
    private Collider m_lockedOn;
    private bool m_isLockedOn = false;
    private int enemyLookAtIndex = 0;
    private void Update()
    {
        lockOn();

        
    }
    private void lockOn()
    {



        if(Input.GetKeyDown(KeyCode.Tab))
        {
            enemyColliders.Clear();
            allCollideers.Clear();
            allCollideers.AddRange(Physics.OverlapSphere(transform.position, 10f));

            for (int i = 0; i < allCollideers.Count; i++)
            {
                if (allCollideers[i].GetComponent<entityWithHealth>() != null)
                {
                    enemyColliders.Add(allCollideers[i]);
                }
            }
            

            /*for (int i = 1; i < enemyColliders.Count; i++)
            {
                
                Vector3 currentLow = (transform.position - enemyColliders[enemyLookAtIndex].transform.position);
                Vector3 checkPos = (transform.position - enemyColliders[i].transform.position);
                float enemyCombine = checkPos.x + checkPos.y + checkPos.z;

                for(int x = 0; x < orderedColliders.Count; x++)
                {

                    Vector3 orderedCurrent = transform.position - orderedColliders[x].transform.position;
                    float orderCombine = orderedCurrent.x + orderedCurrent.y + orderedCurrent.z;

                    if(enemyCombine > orderCombine)
                    {
                        if(x == orderedColliders.Count - 1)
                        {
                            orderedColliders.Add(enemyColliders[i]);
                            break;
                        }
                    }
                    else if(enemyCombine < orderCombine)
                    {
                        if(x == 0)
                        {
                            orderedColliders.Insert(0, enemyColliders[i]);
                            break;
                        }
                    }

                    
                    
                }


                
            }*/
            enemyLookAtIndex = 0;
            updateLockOn();
            Debug.Log("current lock on is: " + enemyLookAtIndex);

            //m_camera.transform.LookAt()
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (enemyLookAtIndex <= enemyColliders.Count - 1)
            {
                enemyLookAtIndex++;
                Debug.Log("current lock on is: " + enemyLookAtIndex);
                updateLockOn();
            }
            else
            {
                enemyLookAtIndex = 0;
                Debug.Log("current lock on is: " + enemyLookAtIndex);
                updateLockOn();
            }
            
        }



    }

    private void updateLockOn()
    {
        m_camera.transform.LookAt(enemyColliders[enemyLookAtIndex].transform);
    }
}
