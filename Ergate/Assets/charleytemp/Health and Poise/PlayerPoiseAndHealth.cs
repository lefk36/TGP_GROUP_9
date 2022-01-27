using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoiseAndHealth : MonoBehaviour
{
    #region inaccessable stuff
    [HideInInspector] public Rigidbody rb;
    private int minimumPoise = -100;
    private int defaultHealthRegen = 3;
    private int defaultPoiseRegen = 5;
    private int defaultMaxHealth = 100;
    private int defaultMaxPoise = 100;
    private bool hasRegenedHealth;
    private bool hasRegenedPoise;
    [SerializeField] private int currentPlayerHealth;       //
    [SerializeField] private int currentPlayerPoise;        // Variables Serialized for testing purposes
    [SerializeField] private bool isKnockedDown;            //

    [SerializeField] private float timeBetweenHealthRegen = 5f;    //time in seconds before health regen
    [SerializeField] private float timeBetweenPoiseRegen = 5f;     //POISE VALUE NEEDS TO BE HIGHER WHEN PLAYING
    #endregion

    public int maximumPoise = 100;  //temp numbers. might be changed with items 
    public int maximumHealth = 100; //

    [Min(0)] public int currentPlayerPoiseRegen;  //May be changed with items in future
    public int currentPlayerHealthRegen;//
    private Animator m_PlayerAnimator; //Animator of the player
    //Character and model of the character
    private GameObject m_Character;
    private GameObject m_Model;

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
        m_Character = GameObject.Find("Character");
        if (m_Character != null)
        {
            m_Model = GameObject.Find("Model");
            if (m_Model != null)
            {
                m_PlayerAnimator = m_Model.GetComponent<Animator>();

            }
        }
        hasRegenedHealth = false;
        hasRegenedPoise = false;
        rb = GetComponent<Rigidbody>();
        //StartCoroutine(RegenHealth()); //restores health every 5 seconds so that we don't have to mess around with floats
    }
    private void Update()
    {
        timeBetweenHealthRegen -= Time.deltaTime;
        timeBetweenPoiseRegen -= Time.deltaTime;

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

        if (currentPlayerHealth <= maximumHealth && timeBetweenHealthRegen <= 0)
        {
            currentPlayerHealth += currentPlayerHealthRegen;
            timeBetweenHealthRegen = 5f;
        }
        //StartCoroutine(RegenHealth()); // only regens when health is below full
        #endregion

        #region Poise Stuff
        if (currentPlayerPoise <= maximumPoise && timeBetweenPoiseRegen <= 0)
        {
            currentPlayerPoise += currentPlayerPoiseRegen;
            timeBetweenPoiseRegen = 5f;
        }
        //  && hasRegenedPoise == false
        //StartCoroutine(RegenPoise());
        if (currentPlayerPoise < minimumPoise)
            currentPlayerPoise = minimumPoise;

        if (currentPlayerPoise <= 0)
            isKnockedDown = true;
        else if (currentPlayerPoise > 0)
            isKnockedDown = false;

        if (isKnockedDown == true)
            KnockedDown();
        #endregion
    }
    public void KnockedDown()
    {
        
        gameObject.GetComponent<PlayerController>().lockMovement = true;        //stuns the player while they're knocked down
        gameObject.GetComponent<PlayerController>().lockAttackDirection = true; //
        gameObject.GetComponent<PlayerController>().readyForAction = false; //
        //play an animation
        m_PlayerAnimator.SetTrigger("KnockedDown");

    }
    public void TakeDamage(Vector3 attackDirection, int healthDamageAmount, int poiseDamageAmount)
    {
        m_PlayerAnimator.SetTrigger("TakeDamage");
        rb.AddForce(attackDirection, ForceMode.Impulse);
        currentPlayerHealth -= healthDamageAmount;
        currentPlayerPoiseRegen -= poiseDamageAmount;
    }
    void PlayerDie()
    {
        Debug.LogError("you are dead now. RIP");
        gameObject.GetComponent<PlayerController>().lockMovement = true;        //stuns the player while they're DEAD
        gameObject.GetComponent<PlayerController>().lockAttackDirection = true; //
        gameObject.GetComponent<PlayerController>().readyForAction = false; //
        StopAllCoroutines(); // stops regen
        m_PlayerAnimator.SetTrigger("KnockedDown");
        //do whatever else dead people do
    }

    
    //IEnumerator RegenHealth()
    //{
    //    currentPlayerHealth += currentPlayerHealthRegen;
    //    yield return new WaitForSeconds(5);
    //}
    //IEnumerator RegenPoise()
    //{
    //    currentPlayerPoise += currentPlayerPoiseRegen;
    //    yield return new WaitForSeconds(5);
    //}
}
