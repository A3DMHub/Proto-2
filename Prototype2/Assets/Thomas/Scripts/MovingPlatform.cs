using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetA, _targetB;

    [SerializeField]
    private GameObject Platform;

    [SerializeField]
    private float _speed = 2f;
    private bool _switching;

    public bool IsMove = false;
    private void Awake()
    {
    }

    void Update()
    {
        if (IsMove)
        {
            Vector3 targetPosition = _switching ? _targetA.position : _targetB.position;
            Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, targetPosition, _speed * Time.deltaTime);

            if (Platform.transform.position == _targetB.position)
                _switching = true;
            else if (Platform.transform.position == _targetA.position)
                _switching = false;
        }
    }

}
