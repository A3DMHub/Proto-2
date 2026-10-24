using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public GameObject BottomPoint;
    public GameObject TopPoint;
    public GameObject TopRung;
    public GameObject BottomRung;

    private GameObject PlayerToMove;

    public float speed = 2f;
    Vector3 CurrentPOS;
    Vector3 newPOS;
    Vector3 spawnPOS;
    float dist1;
    float dist2;
    float MoveFlag = 1f;
    bool SetSpawn = false;

    private GameObject CollideREF;
    

    public void WhereMoveTo(Vector3 StartPosition, Vector3 OtherPosition, Vector3 SpawnPosition)
    {
        CurrentPOS = StartPosition;
        newPOS = OtherPosition;
        spawnPOS = SpawnPosition;
        SetSpawn = true;
        MoveFlag = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerToMove = collision.gameObject;
            dist1 = Vector3.Distance(collision.transform.position, BottomPoint.transform.position);
            dist2 = Vector3.Distance(collision.transform.position, TopPoint.transform.position);
            //Destroy(collision.gameObject);

            if (dist1 < dist2)
            {
                WhereMoveTo(collision.transform.position, TopRung.transform.position, TopPoint.transform.position);
            }
            else
            {
                WhereMoveTo(collision.transform.position, BottomRung.transform.position, BottomPoint.transform.position);
            }
        }
    }
    public void Update()
    {
        if (MoveFlag < 1f)
        {
            MoveFlag += Time.deltaTime * speed;
            PlayerToMove.transform.position = Vector3.Lerp(CurrentPOS, newPOS, MoveFlag);
        }
        else if (SetSpawn)
        {
            PlayerToMove.transform.position = spawnPOS;
            SetSpawn = false;
        }
    }
}
