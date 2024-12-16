using UnityEngine;
using System.Collections.Generic;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;

    // Dictionary to store materials
    private Dictionary<string, Material> materialCache = new Dictionary<string, Material>();

    // Assign materials in the inspector
    [SerializeField] private Material raccoonMaterial;
    [SerializeField] private Material eboueurMaterial;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PreloadMaterials();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PreloadMaterials()
    {
        materialCache["RaccoonMaterial"] = raccoonMaterial;
        materialCache["EboueurMaterial"] = eboueurMaterial;
    }

    public Material GetMaterialByName(string materialName)
    {
        if (materialCache.TryGetValue(materialName, out var material))
        {
            return material;
        }

        Debug.LogError($"Material {materialName} not found in cache!");
        return null;
    }
}