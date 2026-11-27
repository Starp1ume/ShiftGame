using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Current { get; private set; }
    public PhaseManager Phase { get; private set; }

    [SerializeField] private float fallY = -14f;
    [SerializeField] private AudioSource speaker;
    [Header("Sound effects")]
    [SerializeField] private AudioClip jump, land, shift, blocked, death, openDoor, levelClear;

    private Rigidbody2D playerBody;
    private Vector2 spawn;
    private WorldPhase spawnPhase;
    private Checkpoint checkpoint;

    private void Awake()
    {
        Current = this;
        playerBody = FindAnyObjectByType<PlayerController>().GetComponent<Rigidbody2D>();
        Phase = FindAnyObjectByType<PhaseManager>();
        spawn = playerBody.position;
        spawnPhase = Phase.CurrentPhase;
        Phase.OnWorldSwitched += PlayShift;
        Phase.OnSwitchFailed += PlayBlocked;
    }

    private void FixedUpdate()
    {
        if (playerBody.position.y < fallY) Respawn();
    }

    private void OnDestroy()
    {
        if (Phase)
        {
            Phase.OnWorldSwitched -= PlayShift;
            Phase.OnSwitchFailed -= PlayBlocked;
        }
        if (Current == this) Current = null;
    }

    // 同时记录重生落点和当前世界。
    public void SaveCheckpoint(Checkpoint next)
    {
        if (checkpoint) checkpoint.ShowActive(false);
        checkpoint = next;
        checkpoint.ShowActive(true);
        spawn = next.RespawnPosition;
        spawnPhase = Phase.CurrentPhase;
    }

    // 恢复落点和世界，并清除死亡前的速度。
    public void Respawn()
    {
        playerBody.position = spawn;
        playerBody.linearVelocity = Vector2.zero;
        Phase.SetWorld(spawnPhase);
        Play(death);
    }

    private void Play(AudioClip clip)
    {
        if (clip) speaker.PlayOneShot(clip);
    }

    public void PlayJump() => Play(jump);
    public void PlayLand() => Play(land);
    public void PlayDoor() => Play(openDoor);
    private void PlayShift(WorldPhase phase) => Play(shift);
    private void PlayBlocked() => Play(blocked);

    public void PlayLevelClear()
    {
        if (!levelClear) return;
        // 保留音源，让通关音效在切场后播完。
        var tail = new GameObject("Level Clear Sound").AddComponent<AudioSource>();
        DontDestroyOnLoad(tail.gameObject);
        tail.outputAudioMixerGroup = speaker.outputAudioMixerGroup;
        tail.volume = speaker.volume;
        tail.clip = levelClear;
        tail.Play();
        Destroy(tail.gameObject, levelClear.length);
    }
}
