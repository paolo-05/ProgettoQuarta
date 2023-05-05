using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
