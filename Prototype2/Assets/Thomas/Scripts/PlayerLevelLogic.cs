using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLevelLogic : MonoBehaviour
{
    private const float InteractionRadius = 1f;
    private Rigidbody rb;
    private bool isGrabbing = false;
    public GameObject NearbyGrabbable { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ReturnToStart();
    }

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            if (isGrabbing)
            {
                isGrabbing = false;

                NearbyGrabbable.transform.SetParent(null);
                NearbyGrabbable.GetComponent<Rigidbody>().isKinematic = false;

                NearbyGrabbable = null;
            }
            else
            {
                NearbyGrabbable = null;

                foreach (Collider nearbyCollider in Physics.OverlapSphere(transform.position, InteractionRadius))
                {
                    if (nearbyCollider.CompareTag("Grabbable") || nearbyCollider.CompareTag("GrabbableTrigger"))
                    {
                        NearbyGrabbable = nearbyCollider.gameObject;
                        break;
                    }
                }

                if (NearbyGrabbable != null)
                {
                    isGrabbing = true;

                    NearbyGrabbable.transform.SetParent(transform);
                    NearbyGrabbable.transform.localPosition = new Vector3(-0.75f, 0, 0);
                    NearbyGrabbable.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
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
