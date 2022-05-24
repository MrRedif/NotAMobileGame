using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Inputs
    readonly BufferedInput leftInput = new BufferedInput();
    readonly BufferedInput rightInput = new BufferedInput();
    readonly BufferedInput upInput = new BufferedInput();
    readonly BufferedInput downInput = new BufferedInput(0.2f);

    //Properties
    public bool LeftInput => leftInput.InputStatus;
    public bool RightInput => rightInput.InputStatus;
    public bool DownInput => downInput.InputStatus;
    public bool UpInput => upInput.InputStatus;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftInput.StartInput();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightInput.StartInput();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downInput.StartInput();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upInput.StartInput();
        }
        BufferedInput.Step(Time.deltaTime);
    }
}

/// <summary>
/// An Input with a buffer. When started will be active for the given buffer time.
/// </summary>
class BufferedInput {
    public const float BUFFER_TIME = 0.1f;
    float bufferTimer;
    float bufferTime;
    public bool InputStatus => bufferTimer > 0;
    public static System.Action<float> Step;

    public BufferedInput()
    {
        Step += StepIndividual;
        bufferTime = BUFFER_TIME;
    }

    public BufferedInput(float bufferTime)
    {
        Step += StepIndividual;
        this.bufferTime = bufferTime;
    }

    ~BufferedInput()
    {
        Step -= StepIndividual;
    }
    void StepIndividual(float time)
    {
        if (InputStatus)
        {
            bufferTimer -= time;
        }
    }
    /// <summary>
    /// Activates input.
    /// </summary>
    public void StartInput()
    {
        bufferTimer = bufferTime;
    }
}