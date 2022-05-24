using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    //Game Colors
    [SerializeField] GameColors gameColors;

    //Fields
    [SerializeField] bool isAbsorbable;
    [SerializeField] int score;
    [SerializeField] GameObject deathParticle;
    float speed;
    bool isAlive;
    string absorbColor;
    Renderer rend;

    //Properties
    public float Speed => speed;
    public bool IsAlive => isAlive;
    public bool IsAbsorbable => isAbsorbable;
    public string AbsorbColor => absorbColor;

    //Actions
    public static event System.Action<int> KillNoteAction;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }
    public void InitNote(float speed,int colorIndex)
    {
        this.speed = speed;
        colorIndex = Mathf.Clamp(colorIndex, 0, gameColors.Colors.Count-1);
        isAlive = true;

        //Change Material
        absorbColor = gameColors.Colors[colorIndex].ColorName;
        rend.material = gameColors.Colors[colorIndex].MaterialValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(1);
            isAbsorbable = false;
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            transform.position += Vector3.back * Time.deltaTime * speed;
        }
    }


    public void KillNote()
    {
        if (!isAlive) return;
        isAlive = false;
        Instantiate(deathParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystemRenderer>().material = rend.material;
        KillNoteAction?.Invoke(score);
        Destroy(gameObject);
    }


}