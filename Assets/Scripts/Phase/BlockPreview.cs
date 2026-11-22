using UnityEngine;

public class BlockPreview : MonoBehaviour
{
    [SerializeField] private Transform maskTransform;
    [SerializeField] private float maskRadius = 1.5f;


    [SerializeField] private Color displayColor = new Color(1f, 0.3f, 0.3f, 0.5f);
    [SerializeField] private float showDuration = 0.3f;
    private SpriteRenderer previewRenderer;

    private float leftTime;

    private void Awake()
    {
        if (maskTransform == null)
        {
            Debug.LogError("BlockPreview 未绑定 maskTransform！", this);
        }

        previewRenderer = GetComponent<SpriteRenderer>();
        previewRenderer.enabled = false;
    }

    private void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
        }
        else
        {
            previewRenderer.enabled = false;
        }
    }

    /// <summary>
    /// 显示阻碍物轮廓
    /// </summary>
    /// <param name="source"></param>
    public void Show(SpriteRenderer source, Vector3 maskCenter)
    {
        if (source == null || maskTransform == null)
        {
            return;
        }

        leftTime = showDuration;

        maskTransform.position = maskCenter;
        maskTransform.localScale = new Vector3(maskRadius * 2f, maskRadius * 2f, 1f);

        previewRenderer.sprite = source.sprite;
        previewRenderer.flipX = source.flipX;
        previewRenderer.flipY = source.flipY;
        previewRenderer.color = displayColor;

        transform.position = source.transform.position;
        transform.rotation = source.transform.rotation;
        transform.localScale = source.transform.lossyScale;

        previewRenderer.enabled = true;
    }
}
