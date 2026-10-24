using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject Device;
    public MovingPlatform platMove;
    public bool IsFan = false;
    public int FanOn = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsFan)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Trigger" || collision.gameObject.tag == "GrabbableTrigger")
            {
                platMove.IsMove = true;
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                FanOn += 1;
            }

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!IsFan)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Trigger" || collision.gameObject.tag == "GrabbableTrigger")
            {
                platMove.IsMove = false;
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                FanOn -= 1;
            }

        }
    }
}
