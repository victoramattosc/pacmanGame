using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 postion = other.transform.position;
        postion.x = this.connection.position.x;
        postion.y = this.connection.position.y;

        other.transform.position = postion;
    }
}
