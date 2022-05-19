using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPoiseAndHealth : MonoBehaviour
{
    #region inaccessable stuff
    [HideInInspector] public Rigidbody rb;
    private int m_minimumPoise = -100;
    private int m_minimumHealth = 0;

    private int m_defaultHealthRegen = 3;
    private int m_defaultPoiseRegen = 10;

    private int m_defaultMaxHealth = 100;
    private int m_defaultMaxPoise = 100;
    private bool m_hasRegenedHealth;
    private bool m_hasRegenedPoise;
    [SerializeField] public int m_currentPlayerHealth;       //
    [SerializeField] public int m_currentPlayerPoise;        // Variables Serialized for testing purposes
    [SerializeField] private bool m_isKnockedDown;            //

    [SerializeField] private float m_timeBetweenHealthRegen = 5f;    //time in seconds before health regen
    [SerializeField] private float m_timeBetweenPoiseRegen = 5f;     //POISE VALUE NEEDS TO BE HIGHER WHEN PLAYING
    #endregion

    public int m_maximumPoise = 100;  //temp numbers. might be changed with items 
    public int m_maximumHealth = 100; //

    [Min(0)] public int m_currentPlayerPoiseRegen;  //May be changed with items in future
    public int m_currentPlayerHealthRegen;//
    private Animator m_PlayerAnimator; //Animator of the player
    //Character and model of the character
    private GameObject m_Character;
    private GameObject m_Model;
    private PlayerController m_PlayerController;
    private playerSpawn m_PlayerSpawn;
    public bool m_IsDead = false;
    private AttackInput m_AttackInput;

    void Awake()
    {
        //subcscribing taking damage
        //EventManager.current.onPlayerDamage += TakeDamage;
        
        /*m_maximumPoise = m_defaultMaxPoise; */    //sets the current max to the default max
        m_maximumHealth = m_defaultMaxHealth;   //

        m_currentPlayerHealth = m_maximumHealth;    //Sets the current health to the current max health
        /*m_currentPlayerPoise = m_maximumPoise;   */   //

        //m_currentPlayerPoiseRegen = m_defaultPoiseRegen;
        m_currentPlayerHealthRegen = m_defaultHealthRegen;
    }

    private void Start()
    {
        m_PlayerSpawn = GetComponent<playerSpawn>();
        m_PlayerController = GetComponent<PlayerController>();
        m_Character = GameObject.Find("Character");
        m_AttackInput = transform.GetChild(0).GetComponent<AttackInput>();
        if (m_Character != null)
        {
            m_Model = GameObject.Find("Model");
            if (m_Model != null)
            {
                m_PlayerAnimator = m_Model.GetComponent<Animator>();
            }
        }
        m_hasRegenedHealth = false;
        //m_hasRegenedPoise = false;
        rb = GetComponent<Rigidbody>();
        //StartCoroutine(RegenHealth()); //restores health every 5 seconds so that we don't have to mess around with floats
    }

    private void Update()
    {
        m_timeBetweenHealthRegen -= Time.deltaTime;
        //m_timeBetweenPoiseRegen -= Time.deltaTime;
        if (m_currentPlayerHealth <= m_minimumHealth)
            m_currentPlayerHealth = m_minimumHealth;
        if (m_currentPlayerHealth > m_maximumHealth)
            m_currentPlayerHealth = m_maximumHealth;
        //if (m_currentPlayerPoise > m_maximumPoise)
        //    m_currentPlayerPoise = m_maximumPoise;

        #region health stuff
        if (m_currentPlayerHealth <= 0 && !m_IsDead)
        {
            PlayerDie();
            m_PlayerSpawn.onDeath();
        }


        if (m_currentPlayerHealth <= m_maximumHealth && m_timeBetweenHealthRegen <= 0)
        {
            m_currentPlayerHealth += m_currentPlayerHealthRegen;
            m_timeBetweenHealthRegen = 5f;
        }
        #endregion

    }
    void FixedUpdate()
    {
        

        #region Poise Stuff
        //if (m_currentPlayerPoise <= m_maximumPoise && m_timeBetweenPoiseRegen <= 0)
        //{
        //    m_currentPlayerPoise += m_currentPlayerPoiseRegen;
        //    m_timeBetweenPoiseRegen = 5f;
        //}
        //if (m_currentPlayerPoise < m_minimumPoise)
        //    m_currentPlayerPoise = m_minimumPoise;

        //if (m_currentPlayerPoise <= 0)
        //    m_isKnockedDown = true;
        //else if (m_currentPlayerPoise > 0)
        //    m_isKnockedDown = false;

        //if (m_isKnockedDown == true)
        //    KnockedDown();
        #endregion
    }
    //public void KnockedDown()
    //{

    //    m_PlayerController.lockMovement = true;        //stuns the player while they're knocked down
    //    m_PlayerController.lockAttackDirection = true; //
    //    m_PlayerController.readyForAction = false; //
    //    //play an animation
    //    //m_PlayerAnimator.SetTrigger("KnockedDown");
    //}
    public void TakeDamage(Vector3 attackDirection, int healthDamageAmount, int poiseDamageAmount)
    {
      
        m_PlayerAnimator.SetTrigger("TakeDamage");
        Debug.Log("okayer damage taken");
        rb.AddForce(attackDirection, ForceMode.Impulse);
        m_currentPlayerHealth -= healthDamageAmount;
        //m_currentPlayerPoise -= poiseDamageAmount;
    }

    void PlayerDie()
    {
        Debug.LogError("you are dead now. RIP");
        m_PlayerController.lockMovement = true;        //stuns the player while they're DEAD
        m_PlayerController.lockAttackDirection = true; //
        m_PlayerController.readyForAction = false; //
        /*StopAllCoroutines(); */// stops regen
        m_AttackInput.CancelAttacks();
        m_PlayerAnimator.SetTrigger("IsDead");
        m_IsDead = true;
        
        
        //do whatever else dead people do
        
    }

    

}
