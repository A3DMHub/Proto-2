using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject Device;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Trigger")
        {

        }
    }
}
