using UnityEngine;

public class BlockPreview : MonoBehaviour
{
    [SerializeField] private Color displayColor = new Color(1f, 0.3f, 0.3f, 0.5f);
    [SerializeField] private float showDuration = 0.3f;
    private SpriteRenderer previewRenderer;

    private float leftTime;

    private void Awake()
    {
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
    public void Show(SpriteRenderer source)
    {
        if (source == null)
        {
            return;
        }

        leftTime = showDuration;

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
