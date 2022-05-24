using System.Collections;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    //GameColor
    [SerializeField] GameColors gameColors;

    //Particles
    [SerializeField] GameObject deathParticle;

    //Rendering
    MaterialPropertyBlock block;
    Renderer rend;
    Color startColor;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.GetColor("_EmissionColor");
        block = new MaterialPropertyBlock();

        PlayerStatusManager.PlayerColorChange += ChangeMaterialColor;
        PlayerStatusManager.PlayerDeathAction += SpawnDeathParticle;
        PlayerStatusManager.PlayerTakeDamageAction += FlashPlayer;
    }

    private void OnDestroy()
    {
        PlayerStatusManager.PlayerColorChange -= ChangeMaterialColor;
        PlayerStatusManager.PlayerDeathAction -= SpawnDeathParticle;
        PlayerStatusManager.PlayerTakeDamageAction -= FlashPlayer;
    }

    void ChangeMaterialColor(string colorName)
    {
        rend.GetPropertyBlock(block);
        if (colorName != "")
        {
            Color color = gameColors.NameToMaterial[colorName].GetColor("_EmissionColor");
            block.SetColor("_EmissionColor", color);
        }
        else
        {
            block.SetColor("_EmissionColor", startColor);
        }
        rend.SetPropertyBlock(block);
    }

    void SpawnDeathParticle()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
    }

    void FlashPlayer()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < 4; i++)
        {
            rend.enabled = false;
            yield return new WaitForSeconds(0.1f);
            rend.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
