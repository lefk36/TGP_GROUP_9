using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoiseAndHealth : MonoBehaviour
{
    #region inaccessable stuff
    [HideInInspector] public Rigidbody rb;
    private int minimumPoise = -100;
    //private int defaultHealthRegen = 3;
    private int defaultPoiseRegen = 5;
    private int defaultMaxHealth = 100;
    private int defaultMaxPoise = 100;
    private bool hasRegened;
    [SerializeField] private int currentEnemyHealth;
    [SerializeField] private int currentEnemyPoise;
    [SerializeField] private int timeBetweenRegen; //time in seconds before health regen
    [SerializeField] private bool isKnockedDown;
    #endregion

    public int maximumEnemyHealth;
    public int maximumEnemyPoise;
    public int currentEnemyPoiseRegen;
    private void Awake()
    {
        maximumEnemyPoise = defaultMaxPoise;
        maximumEnemyHealth = defaultMaxHealth;

        currentEnemyHealth = maximumEnemyHealth;
        currentEnemyPoise = maximumEnemyPoise;

        currentEnemyPoiseRegen = defaultPoiseRegen;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
