using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMovement : MonoBehaviour
{

    [SerializeField] private float speed;

   

    // Update is called once per frame
    void Update()
    {
        MoveCredit(new Vector2(0, 1));
    }

    private void MoveCredit(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }


}
