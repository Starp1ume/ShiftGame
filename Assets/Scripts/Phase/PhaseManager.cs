using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private BlockPreview blockPreview;


    [SerializeField] private GameObject warmWorld;
    [SerializeField] private GameObject coldWorld;

    [SerializeField] private Vector2 checkShrink = new Vector2(0.05f, 0.15f);

    private bool isColdWorld = true;

    private void Start()
    {
        if (warmWorld == null || coldWorld == null)
        {
            Debug.LogError("有世界未正确绑定！", this);
        }
        else
        {
            ApplyWorldStatus();
        }

        if (blockPreview == null)
        {
            Debug.LogError("blockPreview 未绑定！", this);
        }     
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
            (Vector2)bounds.size - checkShrink,
            0f
            );
        
        int blockerCount = 0;
        SpriteRenderer blocker = null;
        foreach (Collider2D hit in hits)
        {
            if (!hit.transform.IsChildOf(targetWorld.transform)
                || hit.isTrigger)
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
            blockPreview.Show(blocker,bounds.center);
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
