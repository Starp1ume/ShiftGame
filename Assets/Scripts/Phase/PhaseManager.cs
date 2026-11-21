using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private BlockPreview blockPreview;


    [SerializeField] private GameObject warmWorld;
    [SerializeField] private GameObject coldWorld;

    private bool isColdWorld = true;

    private void Start()
    {
        ApplyWorldStatus();
    }

    /// <summary>
    /// 尝试切换时世界
    /// </summary>
    /// <param name="player"></param>
    public void TrySwitchWorld(Collider2D player)
    {
        Bounds bounds = player.bounds;
        GameObject targetWorld = isColdWorld ? warmWorld : coldWorld;
        
        targetWorld.SetActive(true);
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            bounds.center,
            (Vector2)bounds.size - new Vector2(0.05f, 0.05f),
            0f
            );
        
        int blockerCount = 0;
        SpriteRenderer blocker = null;
        foreach (Collider2D hit in hits)
        {
            if (!hit.transform.IsChildOf(targetWorld.transform))
            {
                continue;
            }

            blocker = hit.GetComponent<SpriteRenderer>();

            Debug.Log($"重叠：{hit.name}，所在层：{LayerMask.LayerToName(hit.gameObject.layer)}");
            blockerCount++;
        }
        Debug.Log($"{blockerCount}个阻挡物");

        if (blockerCount == 0)
        {
            isColdWorld = !isColdWorld;
            ApplyWorldStatus();
        }
        else
        {
            targetWorld.SetActive(false);
            blockPreview.Show(blocker);
        }
    }

    /// <summary>
    /// 应用世界状态
    /// </summary>
    private void ApplyWorldStatus()
    {
        warmWorld.SetActive(!isColdWorld);
        coldWorld.SetActive(isColdWorld);
    }
}
