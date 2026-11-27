using UnityEngine;

public class NoShiftZone : MonoBehaviour
{
    private PhaseManager lockedPhase;

    // 各禁切区独立持锁，重叠时逐个释放。
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (lockedPhase || !other.TryGetComponent<PlayerController>(out _)) return;
        lockedPhase = GameManager.Current.Phase;
        lockedPhase.AddSwitchLock();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out _)) ReleaseLock();
    }

    private void OnDisable() => ReleaseLock();

    private void ReleaseLock()
    {
        if (!lockedPhase) return;
        lockedPhase.RemoveSwitchLock();
        lockedPhase = null;
    }
}
