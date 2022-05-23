using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMovement : MonoBehaviour
{

    [SerializeField] public float speed;

    // Update is called once per frame
    void Update()
    {
        CreditsMove(new Vector2(0, 0.5f));
    }

    void CreditsMove(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

}
