using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testhealth : MonoBehaviour
{
    int total;
    float amountUp;
    float amountDown;

   public void TotalHealth(string valueInput)
    {
        total = int.Parse(valueInput);
    }

    public void SubmitSetUp()
    {
        HealthBar.instance.SetupCubes(total);
    }

    public void UpAmount(string valueInput)
    {
        //amountUp = float.Parse(valueInput);
    }

    public void SubmitUp()
    {
       
        HealthBar.instance.AddHealth(amountUp);
    }

    public void DownAmount(string valueInput)
    {
        amountDown = float.Parse(valueInput);
    }

    public void SubmitSown()
    {
        HealthBar.instance.RemoveHealth(amountDown);
    }

    public void AddContainer()
    {
        HealthBar.instance.AddContainer();
    }

}
