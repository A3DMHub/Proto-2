using UnityEngine;

public class PlayerLevelLogic : MonoBehaviour
{
    [Header("Level Logic")]
    public GameObject StartMarker;
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
        rb.position = StartMarker.transform.position;
    }
}
