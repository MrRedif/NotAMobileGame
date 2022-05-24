using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The line that currently the player is on
    /// </summary>
    [SerializeField] Transform currentLine;

    [Header("Movement Fields")]
    [SerializeField] float speed;
    [SerializeField] LeanTweenType leanType;
    bool isMoving;

    //Properties
    public bool IsMoving => isMoving;

    //Classes
    PlayerInput input;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        if (!isMoving)
        {
            if (input.LeftInput)
            {
                Move(-1);
            }
            else if (input.RightInput)
            {
                Move(1);
            }
        }
    }

    void Move(int dir)
    {
        Transform target = null;

        if (dir == 1) //Right
        {
            target = LineManager.instance.GetRightLine(currentLine);

        }
        else if(dir == -1) //Left
        {
            target = LineManager.instance.GetLeftLine(currentLine);
        }

        if (target != null)
        {
            MoveToTarget(target);
        }
    }

    void MoveToTarget(Transform target)
    {
        isMoving = true;
        Vector3 movePosition = target.position;
        movePosition.y = transform.position.y;
        LeanTween.move(gameObject, movePosition, speed).setOnComplete(SetNewLine).setEase(leanType);
        currentLine = target;
    }

    void SetNewLine()
    {
        isMoving = false;
    }
}
