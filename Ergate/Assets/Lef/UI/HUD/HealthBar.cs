using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public static HealthBar instance;

    [SerializeField] GameObject cubeContainer;

    [SerializeField] List<GameObject> cubeContainers;

   
    private int totalCubes;
    private float currentCubes;

    [SerializeField] private float damageAmount;

    private helathCubeContainer currentContainer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cubeContainers = new List<GameObject>();

    
    }

    public void SetupCubes(int cubesIn)
    {
        cubeContainers.Clear();
        for(int i = transform.childCount -1;i >=0 ; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        totalCubes = cubesIn;
        currentCubes = (float)totalCubes;

        for(int i = 0; i< totalCubes; i++)
        {
            GameObject current = Instantiate(cubeContainer, transform);
            cubeContainers.Add(current);
            if(currentContainer != null)
            {
                currentContainer.next = current.GetComponent<helathCubeContainer>();
            }
            currentContainer = current.GetComponent<helathCubeContainer>();
        }
        currentContainer = cubeContainers[0].GetComponent<helathCubeContainer>();
    }

    public void SetCurrentHealth(float health)
    {
        currentCubes = (health/10);
        currentContainer.SetCube(currentCubes);
    }

    public void AddHealth(float healthUp)
    {
        currentCubes += (healthUp/10);
        if(currentCubes > totalCubes)
        {
            currentCubes = (float)totalCubes;
        }
        currentContainer.SetCube(currentCubes);
    }

    public void RemoveHealth(float healthDown)
    {
        currentCubes -= (healthDown/10);
        if(currentCubes < 0)
        {
            currentCubes = 0f;
        }
        currentContainer.SetCube(currentCubes);
    }
    
    public void AddContainer()
    {
        GameObject newContainer = Instantiate(cubeContainer, transform);
        currentContainer = cubeContainers[cubeContainers.Count - 1].GetComponent<helathCubeContainer>();
        cubeContainers.Add(newContainer);

        if(currentContainer != null)
        {
            currentContainer.next = newContainer.GetComponent<helathCubeContainer>();
        }

        currentContainer = cubeContainers[0].GetComponent<helathCubeContainer>();

        totalCubes++;
        currentCubes = totalCubes;
        SetCurrentHealth(currentCubes);
    }

  
}
