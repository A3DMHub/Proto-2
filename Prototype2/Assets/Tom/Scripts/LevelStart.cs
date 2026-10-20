using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public GameObject playerRef;

    void Start()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, 0);
        GameObject spawnedPlayer = Instantiate(playerRef, position, Quaternion.identity);
        NewMonoBehaviourScript ghostScript = spawnedPlayer.GetComponent<NewMonoBehaviourScript>();
        ghostScript.startPosition = position;
    }

}
