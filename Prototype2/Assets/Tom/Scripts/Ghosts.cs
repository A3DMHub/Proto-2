using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ghosts : MonoBehaviour
{
    public int ghostLimit;
    public GameObject ghostRef;
    public GameObject lastCheckpoint;

    [Header("Ghost Sprites")]
    public Sprite ghostStandSprite;
    public Sprite ghostCrouchSprite;

    private List<GameObject> ghosts = new List<GameObject>();

    private void Awake()
    {
        TeleportToStart();
    }

    public void TeleportToStart()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector2.zero;
        transform.position = lastCheckpoint.transform.position;
    }

    public void OnCreateGhost(InputValue value)
    {
        if (value.isPressed && ghosts.Count < ghostLimit)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject spawnedGhost = Instantiate(ghostRef, position, Quaternion.identity);

            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            SpriteRenderer ghostRenderer = spawnedGhost.GetComponentInChildren<SpriteRenderer>();

            if (playerMovement != null && ghostRenderer != null)
            {
                if (playerMovement.IsCrouching)
                {
                    ghostRenderer.sprite = ghostCrouchSprite;
                }
                else
                {
                    ghostRenderer.sprite = ghostStandSprite;
                }
            }

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
