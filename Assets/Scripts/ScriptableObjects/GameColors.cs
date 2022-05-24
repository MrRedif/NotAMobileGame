using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GameColors",fileName ="newGameColors")]
public class GameColors : ScriptableObject
{
    //Fields
    [SerializeField] List<GameColor> colors = new List<GameColor>();
    Dictionary<string, Material> nameToMaterial = new Dictionary<string, Material>();

    //Properties
    public List<GameColor> Colors => colors;
    public Dictionary<string, Material> NameToMaterial => nameToMaterial;

    private void OnEnable()
    {
        nameToMaterial.Clear();
        foreach (var item in colors)
        {
            nameToMaterial.Add(item.ColorName, item.MaterialValue);
        }
    }
}

[System.Serializable]
public class GameColor
{
    [SerializeField] string colorName;
    [SerializeField] Material material;
    public Material MaterialValue => material;
    public string ColorName => colorName;

}
