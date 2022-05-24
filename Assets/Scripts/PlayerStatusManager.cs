using UnityEngine;
using System;

public class PlayerStatusManager : MonoBehaviour,IDamageable
{
    //Fields
    [SerializeField] int health;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float invisTime;
    float invisTimer;
    bool isDying;
    bool isInvincible;
    string currentColor = "";

    //Properties
    public int Health => health;
    public bool IsDying => isDying;
    public bool IsInvincible => isInvincible;

    //Actions
    public static event Action<int> PlayerHealthChangeAction;
    public static event Action PlayerTakeDamageAction;
    public static event Action PlayerDeathAction;
    public static event Action<string> PlayerColorChange;

    //Classes
    PlayerInput input;
    PlayerMovement movement;
    NoteBehaviour currentNote;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        PlayerHealthChangeAction?.Invoke(health);
    }

    private void Update()
    {
        if (!movement.IsMoving && input.UpInput && currentColor != "")
        {
            FireBullet();
        }

        if (invisTimer > 0)
        {
            invisTimer -= Time.deltaTime;
            if (invisTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }
    public void TakeDamage(int dmg)
    {
        if (isDying || isInvincible) return;

        health -= dmg;
        isInvincible = true;
        invisTimer = invisTime;
        if (health <= 0)
        {
            KillPlayer();
        }
        PlayerTakeDamageAction?.Invoke();
        PlayerHealthChangeAction?.Invoke(health);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isDying && input.DownInput && currentNote != null && currentColor == "")
        {
            if (currentNote.IsAbsorbable && currentNote.IsAlive)
            {
                ChangePlayerColor(currentNote.AbsorbColor);
                currentNote.KillNote();
                currentNote = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NotePress"))
        {
            if (other.transform.parent.TryGetComponent(out NoteBehaviour note))
            {
                currentNote = note;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NotePress"))
        {
            if (other.transform.parent.TryGetComponent(out NoteBehaviour note))
            {
                if (currentNote == note)
                {
                    currentNote = null;
                }
            }
        }
    }

    void ChangePlayerColor(string color)
    {
        currentColor = color;
        PlayerColorChange?.Invoke(color);
    }

    void FireBullet()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletBehaviour>().InitBullet(currentColor);
        ChangePlayerColor("");
    }

    public void KillPlayer()
    {
        isDying = true;
        PlayerDeathAction?.Invoke();
        Destroy(gameObject);
    }

}
