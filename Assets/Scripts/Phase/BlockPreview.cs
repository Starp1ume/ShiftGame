using UnityEngine;
using System.Collections.Generic;

public class BlockPreview : MonoBehaviour
{
    [SerializeField] private Transform maskTransform;
    [SerializeField] private float maskRadius = 1.5f;


    [SerializeField] private Color displayColor = new Color(1f, 0.3f, 0.3f, 0.5f);
    [SerializeField] private float showDuration = 0.3f;
    [SerializeField] private int sortingOrder = 10;
    private List<SpriteRenderer> previewRenderers = new List<SpriteRenderer>();

    private float leftTime;

    private void Awake()
    {
        if (maskTransform == null)
        {
            Debug.LogError("BlockPreview 未绑定 maskTransform！", this);
        }
    }

    private void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
        }
        else
        {
            foreach (SpriteRenderer previewRenderer in previewRenderers)
            {
                previewRenderer.enabled = false;
            }
        }
    }

    /// <summary>
    /// 显示阻碍物轮廓
    /// </summary>
    /// <param name="source"></param>
    public void Show(List<SpriteRenderer> sources, Vector2 maskCenter)
    {
        if (sources == null || maskTransform == null)
        {
            return;
        }

        leftTime = showDuration;

        maskTransform.position = maskCenter;
        maskTransform.localScale = new Vector3(maskRadius * 2f, maskRadius * 2f, 1f);

        while (previewRenderers.Count < sources.Count)
        {
            GameObject newObject = new GameObject("PreviewItem");
            SpriteRenderer newRenderer = newObject.AddComponent<SpriteRenderer>();

            newObject.transform.SetParent(transform);
            newRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            newRenderer.sortingOrder = sortingOrder;
            previewRenderers.Add(newRenderer);
        }

        for (int i = 0; i < sources.Count; i++)
        {
            if (sources[i] == null)
            {
                continue;
            }

            SpriteRenderer source = sources[i];
            SpriteRenderer preview = previewRenderers[i];

            preview.sprite = source.sprite;
            preview.transform.position = source.transform.position;
            preview.transform.rotation = source.transform.rotation;
            preview.transform.localScale = source.transform.lossyScale;
            preview.flipX = source.flipX;
            preview.flipY = source.flipY;
            preview.color = displayColor;
            preview.enabled = true;
        }
    }
}
