using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;

            Ghosts ghostScript = player.GetComponent<Ghosts>();
            ghostScript.lastCheckpoint = gameObject;
        }
    }
}
