using UnityEngine;

public class FanScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Trigger")
        {

        }
    }
}
