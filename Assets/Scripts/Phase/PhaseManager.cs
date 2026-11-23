using UnityEngine;
using System;
using System.Collections.Generic;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private BlockPreview blockPreview;


    [SerializeField] private GameObject warmWorld;
    [SerializeField] private GameObject coldWorld;

    [SerializeField] private Vector2 checkShrink = new Vector2(0.05f, 0.15f);

    private WorldPhase currentPhase; // 当前世界状态
    private int lockCount = 0; // 有几个地方锁了切换

    /* 对外接口 */
    public WorldPhase CurrentPhase => currentPhase;
    public event Action<WorldPhase> OnWorldSwitched; // 切换成功（返回目标世界状态）
    public event Action OnSwitchFailed; // 切换失败

    private void Start()
    {
        if (warmWorld == null || coldWorld == null)
        {
            Debug.LogError("有世界未正确绑定！", this);
        }
        else
        {
            ApplyWorldPhase();
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
    public bool TrySwitchWorld(Collider2D player)
    {
        if (lockCount > 0)
        {
            OnSwitchFailed?.Invoke();
            return false;
        }

        Bounds bounds = player.bounds;
        WorldPhase targetPhase =
            currentPhase == WorldPhase.Cold ? WorldPhase.Warm : WorldPhase.Cold;
        GameObject targetWorld =
            targetPhase == WorldPhase.Warm ? warmWorld : coldWorld;


        targetWorld.SetActive(true);
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            bounds.center,
            (Vector2)bounds.size - checkShrink,
            0f
            );

        bool canSwitch = true;
        foreach (Collider2D hit in hits)
        {
            if (hit.transform.IsChildOf(targetWorld.transform)
                && !hit.isTrigger)
            {
                //Debug.Log($"重叠：{hit.name}，所在层：{LayerMask.LayerToName(hit.gameObject.layer)}");
                canSwitch = false; break;
            }
        }

        if (canSwitch)
        {
            currentPhase = targetPhase;
            ApplyWorldPhase();
            OnWorldSwitched?.Invoke(currentPhase);
            return true;
        }
        else
        {
            if (blockPreview != null)
            {
                Collider2D[] nearbyHits = Physics2D.OverlapCircleAll(
                    bounds.center,
                    blockPreview.MaskRadius
                    );

                List<SpriteRenderer> previewTargets = new List<SpriteRenderer>();
                foreach (Collider2D hit in nearbyHits)
                {
                    if (hit.transform.IsChildOf(targetWorld.transform))
                    {
                        previewTargets.Add(hit.GetComponent<SpriteRenderer>());
                    }
                }
                blockPreview.Show(previewTargets, bounds.center);
            }

            targetWorld.SetActive(false);
            OnSwitchFailed?.Invoke();
            return false;
        }
    }

    /// <summary>
    /// 设置世界状态并应用
    /// </summary>
    /// <param name="targetPhase">The world phase to set.</param>
    public void SetWorld(WorldPhase targetPhase)
    {
        currentPhase = targetPhase;
        ApplyWorldPhase();
    }

    public void AddSwitchLock()
    {
        lockCount++;
    }

    public void RemoveSwitchLock()
    {
        lockCount = Mathf.Max(0, lockCount - 1);
    }


    /// <summary>
    /// 应用世界状态
    /// </summary>
    private void ApplyWorldPhase()
    {
        warmWorld.SetActive(currentPhase == WorldPhase.Warm);
        coldWorld.SetActive(currentPhase == WorldPhase.Cold);
    }
}

public enum WorldPhase
{
    Cold,
    Warm
}
