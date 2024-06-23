using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 targetPosition; // Vị trí mục tiêu hiện tại
    private Transform playerTransform; // Tham chiếu đến transform của người chơi

    void Start()
    {
        targetPosition = pointA.position; // Ban đầu di chuyển về phía điểm bên phải
    }

    void Update()
    {
        Vector3 previousPosition = transform.position;

        // Di chuyển nền tảng về phía vị trí mục tiêu
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Nếu nền tảng đã tới vị trí mục tiêu
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Đổi vị trí mục tiêu
            if (targetPosition == pointB.position)
            {
                targetPosition = pointA.position;
            }
            else
            {
                targetPosition = pointB.position;
            }
        }

        // Di chuyển người chơi nếu đang đứng trên nền tảng
        if (playerTransform != null)
        {
            Vector3 movement = transform.position - previousPosition;
            playerTransform.position += movement;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu người chơi va chạm với nền tảng
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Kiểm tra nếu người chơi rời khỏi nền tảng
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }

    private void OnDrawGizmos()
    {
        // Vẽ đường giữa các điểm A và B để dễ dàng xem trên Editor
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
