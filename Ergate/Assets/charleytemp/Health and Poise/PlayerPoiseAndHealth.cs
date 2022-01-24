using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoiseAndHealth : MonoBehaviour
{
    #region inaccessable stuff
    [HideInInspector]public Rigidbody rb;
    private int minimumPoise = -100;
    private int defaultHealthRegen = 3;
    private int defaultPoiseRegen = 5;
    private int defaultMaxHealth = 100;
    private int defaultMaxPoise = 100;
    private bool hasRegenedHealth;
    private bool hasRegenedPoise;
    [SerializeField] private int currentPlayerHealth;       //
    [SerializeField] private int currentPlayerPoise;        // Variables Serialized for testing purposes
    [SerializeField] private int timeBetweenHealthRegen;    //time in seconds before health regen
    [SerializeField] private int timeBetweenPoiseRegen;     //time in seconds before Poise regen
    [SerializeField] private bool isKnockedDown;            //
    #endregion

    public int maximumPoise = 100;  //temp numbers. might be changed with items 
    public int maximumHealth = 100; //

    [Min(0)]public int currentPlayerPoiseRegen;  //May be changed with items in future
    public int currentPlayerHealthRegen;       //

    
    void Awake()
    {
        maximumPoise = defaultMaxPoise;     //sets the current max to the default max
        maximumHealth = defaultMaxHealth;   //

        currentPlayerHealth = maximumHealth;    //Sets the current health to the current max health
        currentPlayerPoise = maximumPoise;      //

        currentPlayerPoiseRegen = defaultPoiseRegen;
        currentPlayerHealthRegen = defaultHealthRegen;
    }
    private void Start()
    {
        hasRegenedHealth = false;
        hasRegenedPoise = false;
        rb = GetComponent<Rigidbody>();
        //StartCoroutine(RegenHealth()); //restores health every 5 seconds so that we don't have to mess around with floats
    }
    private void Update()
    {
        if (currentPlayerHealth > maximumHealth)
            currentPlayerHealth = maximumHealth;
        if (currentPlayerPoise > maximumPoise)
            currentPlayerPoise = maximumPoise;
    }
    void FixedUpdate()
    {
        #region health stuff
        if (currentPlayerHealth <= 0)
            PlayerDie();

        if (currentPlayerHealth <= maximumHealth && hasRegenedHealth == false)
            StartCoroutine(RegenHealth()); // only regens when health is below full
        #endregion

        #region Poise Stuff
        if (currentPlayerPoise <= maximumPoise && hasRegenedPoise == false)
            StartCoroutine(RegenPoise());
        if (currentPlayerPoise < minimumPoise)
            currentPlayerPoise = minimumPoise;

        if (currentPlayerPoise <= 0)
            isKnockedDown = true;
        else
            isKnockedDown = false;

        if (isKnockedDown == true)
            KnockedDown();
        #endregion
    }
    public void KnockedDown()
    {
        gameObject.GetComponent<PlayerController>().lockMovement = true;        //stuns the player while they're knocked down
        gameObject.GetComponent<PlayerController>().lockAttackDirection = true; //
        //play an animation
    }
    public void TakeDamage(Vector3 attackDirection, int healthDamageAmount, int poiseDamageAmount)
    {
        rb.AddForce(attackDirection, ForceMode.Impulse);
        currentPlayerHealth -= healthDamageAmount;
        currentPlayerPoiseRegen -= poiseDamageAmount;
    }
    void PlayerDie()
    {
        Debug.LogError("you are dead now. RIP");
        gameObject.GetComponent<PlayerController>().lockMovement = true;        //stuns the player while they're DEAD
        gameObject.GetComponent<PlayerController>().lockAttackDirection = true; //
        StopAllCoroutines(); // stops regen
        //do whatever else dead people do
    }
    IEnumerator RegenHealth()
    {
        currentPlayerHealth += currentPlayerHealthRegen;
        hasRegenedHealth = true;
        yield return new WaitForSeconds(timeBetweenHealthRegen);
        hasRegenedHealth = false;
    }
    IEnumerator RegenPoise()
    {
        currentPlayerPoise += currentPlayerPoiseRegen;
        hasRegenedPoise = true;
        yield return new WaitForSeconds(timeBetweenPoiseRegen);
        hasRegenedPoise = false;
    }
}
