using UnityEditor;
using UnityEngine;

public class ExplodingScript: MonoBehaviour
{

    public GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(player, 2f);
            Destroy(this);
        }
        else if (collision.gameObject.tag == "Trigger")
        {
            Destroy(collision.gameObject);
            Destroy(this);
        }
    }
}
