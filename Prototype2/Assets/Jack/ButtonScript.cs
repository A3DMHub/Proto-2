using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject Device;
    public MovingPlatform platMove;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" /*|| collision.gameObject.tag == "Trigger"*/)
        {
            platMove.IsMove = true;
        }
    }
}
