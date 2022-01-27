using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoiseAndHealth : MonoBehaviour
{
    #region inaccessable stuff
    [HideInInspector] public Rigidbody rb;
    private int m_minimumPoise = -100;

    private int m_defaultMaxHealth = 100;
    private int m_defaultMaxPoise = 100;
    private bool m_isKnockedDown;                               //
    [SerializeField] public int m_currentPlayerHealth;         //
    [SerializeField] public int m_currentPlayerPoise;          // Variables Serialized for testing purposes

    private int m_defaultHealthRegenPoise = 3;
    private int m_defaultPoiseRegen = 5;
    [SerializeField] private float m_timeBetweenHealthRegen = 5f;    //time in seconds before health regen
    [SerializeField] private float m_timeBetweenPoiseRegen = 1f;     //POISE VALUE NEEDS TO BE HIGHER WHEN PLAYING
    #endregion

    public int m_maximumPoise = 100;  //temp numbers. might be changed with items 
    public int m_maximumHealth = 100; //

    [Min(0)] public int m_currentPlayerPoiseRegen;  //May be changed with items in future
    public int m_currentPlayerHealthRegen;       //


    void Awake()
    {
        m_maximumPoise = m_defaultMaxPoise;     //sets the current max to the default max
        m_maximumHealth = m_defaultMaxHealth;   //

        m_currentPlayerHealth = m_maximumHealth;    //Sets the current health to the current max health
        m_currentPlayerPoise = m_maximumPoise;      //

        m_currentPlayerPoiseRegen = m_defaultPoiseRegen;
        m_currentPlayerHealthRegen = m_defaultHealthRegenPoise;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //StartCoroutine(RegenHealth()); //restores health every 5 seconds so that we don't have to mess around with floats
    }
    private void Update()
    {
        m_timeBetweenHealthRegen -= Time.deltaTime;
        m_timeBetweenPoiseRegen -= Time.deltaTime;

        if (m_currentPlayerHealth > m_maximumHealth)
            m_currentPlayerHealth = m_maximumHealth;
        if (m_currentPlayerPoise > m_maximumPoise)
            m_currentPlayerPoise = m_maximumPoise;
    }
    void FixedUpdate()
    {
        #region health stuff
        if (m_currentPlayerHealth <= 0)
            PlayerDie();

        if (m_currentPlayerHealth <= m_maximumHealth && m_timeBetweenHealthRegen <= 0)
        {
            m_currentPlayerHealth += m_currentPlayerHealthRegen;
            m_timeBetweenHealthRegen = 5f;
        }
        //StartCoroutine(RegenHealth()); // only regens when health is below full
        #endregion

        #region Poise Stuff
        if (m_currentPlayerPoise <= m_maximumPoise && m_timeBetweenPoiseRegen <= 0)
        {
            m_currentPlayerPoise += m_currentPlayerPoiseRegen;
            m_timeBetweenPoiseRegen = 5f;
        }
        //  && hasRegenedPoise == false
        //StartCoroutine(RegenPoise());
        if (m_currentPlayerPoise < m_minimumPoise)
            m_currentPlayerPoise = m_minimumPoise;

        if (m_currentPlayerPoise <= 0)
            m_isKnockedDown = true;
        else if (m_currentPlayerPoise > 0)
            m_isKnockedDown = false;

        if (m_isKnockedDown == true)
            KnockedDown();
        #endregion
    }
    public void KnockedDown()
    {
        gameObject.GetComponent<PlayerController>().lockMovement = true;        //stuns the player while they're knocked down
        gameObject.GetComponent<PlayerController>().lockAttackDirection = true; //
        gameObject.GetComponent<PlayerController>().readyForAction = false; //
        //play an animation
    }
    public void TakeDamage(Vector3 attackDirection, int healthDamageAmount, int poiseDamageAmount)
    {
        Debug.Log("damage taken");
        //rb.AddForce(attackDirection, ForceMode.Impulse);
        m_currentPlayerHealth -= healthDamageAmount;        // I cannot figure out why this doesn't work
        m_currentPlayerPoiseRegen -= poiseDamageAmount;     // but if I use =- it works correctly
    }
    void PlayerDie()
    {
        Debug.Log($"you are dead now. RIP");

        //--------------UNBLOCK THESE WHEN ON THE REAL PLAYER SCRIPT-------------------------------

        //gameObject.GetComponent<PlayerController>().lockMovement = true;        //stuns the player while they're DEAD
        //gameObject.GetComponent<PlayerController>().lockAttackDirection = true; //
        //gameObject.GetComponent<PlayerController>().readyForAction = false; //
        //do whatever dead people do
    }
    //IEnumerator RegenHealth()
    //{
    //    m_currentPlayerHealth += m_currentPlayerHealthRegen;
    //    yield return new WaitForSeconds(5);
    //}
    //IEnumerator RegenPoise()
    //{
    //    m_currentPlayerPoise += m_currentPlayerPoiseRegen;
    //    yield return new WaitForSeconds(5);
    //}
}
