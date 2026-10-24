using UnityEngine;

public class FanScript : MonoBehaviour
{
    public float speed = 2.0f;
    private GameObject MoveThing;
    private int InDraft = 0;
    public ButtonScript buttonREF;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InDraft += 1;
            MoveThing = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            InDraft -= 1;
        }
    }

    private void Update()
    {
        if (InDraft > 0 && buttonREF.FanOn > 0)
        {
            Vector3 pos = MoveThing.transform.position;
            pos.y += speed * Time.deltaTime;
            MoveThing.transform.position = pos;
        }
    }


}
