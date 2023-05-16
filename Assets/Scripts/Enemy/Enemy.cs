using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; // Speed at which the enemy moves
    public float distance = 5f; // Distance the enemy covers while walking

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(distance, 0f, 0f);
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);

            if (transform.position == endPos)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);

            if (transform.position == startPos)
            {
                movingRight = true;
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                Destroy(gameObject); // Destroy the enemy game object
            }
        }
    }
}


