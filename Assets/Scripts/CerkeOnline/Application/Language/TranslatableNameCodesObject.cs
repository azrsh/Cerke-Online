using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TranslatableNameCodesObject")]
public partial class TranslatableNameCodesObject : ScriptableObject
{
    [SerializeField] List<string> translatableNameCodes = default;
    public IReadOnlyList<string> TranslatableNameCodes => translatableNameCodes;
}

public partial class TranslatableNameCodesObject : ScriptableObject
{
    public static TranslatableNameCodesObject Create()
    {
        return ScriptableObject.CreateInstance<TranslatableNameCodesObject>();
    }

    //拡張性を阻害している場合はObject.Instantiateを直接利用してください
    public static TranslatableNameCodesObject Instantiate(TranslatableNameCodesObject original)
    {
        return Object.Instantiate<TranslatableNameCodesObject>(original);
    }
}
