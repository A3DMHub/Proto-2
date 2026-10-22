using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [Header("Player")]
    public GameObject playerRef;
    public int GhostLimit = 5;

    void Start()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject spawnedPlayer = Instantiate(playerRef, position, Quaternion.identity);
        Ghosts ghostScript = spawnedPlayer.GetComponent<Ghosts>();
        ghostScript.startPosition = position;
        ghostScript.ghostLimit = GhostLimit;
    }

}
