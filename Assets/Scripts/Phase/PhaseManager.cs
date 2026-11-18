using UnityEngine;
using UnityEngine.InputSystem;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private GameObject warmWorld;
    [SerializeField] private GameObject coldWorld;

    private bool isColdWorld = true;

    private void Start()
    {
        ApplyWorldStatus();
    }

    public void TrySwitchWorld()
    {
        isColdWorld = !isColdWorld;
        ApplyWorldStatus();
    }

    private void ApplyWorldStatus()
    {
        warmWorld.SetActive(!isColdWorld);
        coldWorld.SetActive(isColdWorld);
    }
}
