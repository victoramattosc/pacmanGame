using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }
    private Animator animator;
    private Collider2D col;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.animator = GetComponent<Animator>();
        this.col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        if (keyboard.wKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame)
        {
            this.movement.SetDirection(Vector2.up);
        }
        else if (keyboard.sKey.wasPressedThisFrame || keyboard.downArrowKey.wasPressedThisFrame)
        {
            this.movement.SetDirection(Vector2.down);
        }
        else if (keyboard.aKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame)
        {
            this.movement.SetDirection(Vector2.left);
        }
        else if (keyboard.dKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame)
        {
            this.movement.SetDirection(Vector2.right);
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void Die()
    {
        this.movement.enabled = false;
        this.col.enabled = false;
        this.enabled = false;
        this.transform.rotation = Quaternion.identity;
        this.animator.SetTrigger("Die");
        Invoke(nameof(Hide), 1.5f);
    }

    private void Hide()
{
    this.gameObject.SetActive(false);
}

    public void ResetState()
    {
        this.enabled = true;
        this.col.enabled = true;
        this.gameObject.SetActive(true);
        this.movement.ResetState();
    }
}