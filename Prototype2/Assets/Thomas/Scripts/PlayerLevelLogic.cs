using UnityEngine;

public class PlayerLevelLogic : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ReturnToStart();
    }

    public void OnDeath()
    {
        ReturnToStart();
    }

    private void ReturnToStart()
    {
        Ghosts ghostScript = GetComponent<Ghosts>();
        ghostScript.TeleportToStart();
    }
}
