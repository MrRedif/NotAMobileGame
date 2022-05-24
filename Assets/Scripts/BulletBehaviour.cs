using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //Game Color
    [SerializeField] GameColors gameColors;

    //Fields
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] ParticleSystemRenderer psRenderer;
    bool isAlive;
    string bulletColor;
    
    //Properties
    public float Speed => speed;
    public bool IsAlive => isAlive;

    //Render
    MaterialPropertyBlock block;
    Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
    }


    public void InitBullet(string colorName)
    {
        isAlive = true;

        //Change Material
        bulletColor = colorName;
        rend.GetPropertyBlock(block);

        if (colorName != "")
        {
            Color color = gameColors.NameToMaterial[colorName].GetColor("_EmissionColor");
            block.SetColor("_EmissionColor", color);
        }
        else
        {
            Debug.LogWarning("Initializing without a color. Are you sure you want to do this?");
        }
        rend.SetPropertyBlock(block);

        psRenderer.material = rend.material;
        psRenderer.SetPropertyBlock(block);
    }

    private void Update()
    {
        if (isAlive)
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                KillBullet();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            if (other.TryGetComponent(out NoteBehaviour note))
            {
                if(note.AbsorbColor == bulletColor)
                {
                    note.KillNote();
                }
            }
            KillBullet();
        }
    }

    void KillBullet()
    {
        if(psRenderer != null) psRenderer.transform.SetParent(null);
        Destroy(gameObject);
    }
}
