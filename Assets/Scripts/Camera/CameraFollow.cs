using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.15f;
    [SerializeField] private Vector2 offset = new Vector2(0f, 1f);
    private Vector3 currentVelocity;

    private void Awake()
    {
        if (target == null)
        {
            Debug.LogError("摄像机未绑定玩家！", this);
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 targetPostion = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
            );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPostion,
            ref currentVelocity,
            smoothTime
            );
    }
}
