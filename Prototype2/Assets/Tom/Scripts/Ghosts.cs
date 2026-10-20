using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int ghostLimit;
    public GameObject ghostRef;
    public Vector3 startPosition;

    private List<GameObject> ghosts = new List<GameObject>();

    private void TeleportToStart()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.position = startPosition;
        rb.linearVelocity = Vector2.zero;
    }

    public void OnCreateGhost(InputValue value)
    {
        if (value.isPressed && ghosts.Count < ghostLimit)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, 0);
            GameObject spawnedGhost = Instantiate(ghostRef, position, Quaternion.identity);
            ghosts.Add(spawnedGhost);
            TeleportToStart();
        }
    }
    public void OnKillNewGhost(InputValue value)
    {
        if (ghosts.Count <= 0)
        {
            return;
        }
        int lastIndex = ghosts.Count - 1;
        Destroy(ghosts[lastIndex]);
        ghosts.RemoveAt(lastIndex);
    }

    public void OnKillOldGhost(InputValue value)
    {
        if (ghosts.Count <= 0)
        {
            return;
        }
        Destroy(ghosts[0]);
        ghosts.RemoveAt(0);
    }
}
