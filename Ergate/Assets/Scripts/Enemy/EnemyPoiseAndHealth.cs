using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoiseAndHealth : MonoBehaviour
{
    #region inaccessable stuff
    [HideInInspector] public Rigidbody rb;
    private int minimumEnemyPoise = -100;
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
        if (currentEnemyHealth >= maximumEnemyHealth)
            currentEnemyHealth = maximumEnemyHealth;
        if (currentEnemyPoise <= minimumEnemyPoise)
            currentEnemyPoise = minimumEnemyPoise;
        if (currentEnemyPoise >= maximumEnemyPoise)
            currentEnemyPoise = maximumEnemyPoise;
        if (currentEnemyHealth <= 0)
            EnemyDie();
    }
    void EnemyDie()
    {
        //death animation
        Destroy(gameObject);
    }
    public void TakeDamage(Vector3 attackDirection, int healthDamageAmount, int poiseDamageAmount)
    {
        Debug.Log("damage taken");
        rb.AddForce(attackDirection, ForceMode.Impulse); // can be used to stagger the player.
        currentEnemyHealth -= healthDamageAmount;        // I cannot figure out why this doesn't work
        currentEnemyPoise -= poiseDamageAmount;     // but if I use =- it works correctly
    }
    public void Stunned()
    {
        //Put knocked down animation or something in here.
    }
}
