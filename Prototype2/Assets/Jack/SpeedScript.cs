using System.Diagnostics.Contracts;
using UnityEngine;

public class SlowScript: MonoBehaviour
{
    public bool speedUp = false;

    public GameObject player;
    public ControllerScript control;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (speedUp)
            {
                control.speed++;
            }
            else
            { 
                control.speed--;
            }
        }
    }
}
