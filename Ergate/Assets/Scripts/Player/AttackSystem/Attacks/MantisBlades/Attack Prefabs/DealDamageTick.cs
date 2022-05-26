using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DealDamageTick : MonoBehaviour
{
    public float tickRate;
    float soundTime;
    public float damagePerTick;
    public string soundName;
    public audioController audioControllerScript;
    public Vector3 force;

    Dictionary<GameObject, float> trackedEnemies;
    private void Start()
    {
        soundName = "";
        audioControllerScript = null;
        soundTime = tickRate;
        trackedEnemies = new Dictionary<GameObject, float>();
    }
    private void Update()
    {
        soundTime += Time.deltaTime;
        if(soundTime > tickRate)
        {
            soundTime = 0;
            if(soundName != "" && audioControllerScript != null)
            {
                audioControllerScript.play(soundName);
            }
        }
    }
    public void SetAudio(string p_soundName, audioController p_audioController)
    {
        soundName = p_soundName;
        audioControllerScript = p_audioController;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !trackedEnemies.ContainsKey(other.transform.parent.gameObject))
        {
            trackedEnemies.Add(other.transform.parent.gameObject, tickRate);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy" && trackedEnemies.ContainsKey(other.transform.parent.gameObject))
        {
            trackedEnemies[other.transform.parent.gameObject] += Time.deltaTime;
            if(trackedEnemies[other.transform.parent.gameObject] > tickRate)
            {
                trackedEnemies[other.transform.parent.gameObject] = 0;
                DealDamage(other.transform.parent.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && trackedEnemies.ContainsKey(other.transform.parent.gameObject))
        {
            trackedEnemies[other.transform.parent.gameObject] = tickRate;
        }
    }
    void DealDamage(GameObject enemy)
    {
        BaseEnemy enemyScript = enemy.GetComponent<BaseEnemy>();
        if (!enemyScript.isDead)
        {
            enemyScript.m_Agent.enabled = false;
            enemyScript.rb.velocity = new Vector3(0, 0, 0);
            enemyScript.rb.AddForce(force, ForceMode.Impulse);
            enemyScript.TakeDamage(damagePerTick, true);
        }
    }
}
