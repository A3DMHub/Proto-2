using UnityEngine;

public class DamageScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerLevelLogic LevelLogic = collision.gameObject.GetComponent<PlayerLevelLogic>();
            LevelLogic.OnDeath();
        }
    }
}
