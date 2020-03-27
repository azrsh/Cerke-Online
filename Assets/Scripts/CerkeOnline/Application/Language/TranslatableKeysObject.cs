using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TranslatableKeysObject")]
public partial class TranslatableKeysObject : ScriptableObject
{
    [SerializeField] List<string> translatableKeys = default;
    public IReadOnlyList<string> TranslatableKeys => translatableKeys;
}

public partial class TranslatableKeysObject : ScriptableObject
{
    public static TranslatableKeysObject Create()
    {
        return ScriptableObject.CreateInstance<TranslatableKeysObject>();
    }

    //拡張性を阻害している場合はObject.Instantiateを直接利用してください
    public static TranslatableKeysObject Instantiate(TranslatableKeysObject original)
    {
        return Object.Instantiate<TranslatableKeysObject>(original);
    }
}
