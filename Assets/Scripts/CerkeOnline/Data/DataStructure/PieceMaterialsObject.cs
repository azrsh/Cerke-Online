using UnityEngine;

namespace Azarashi.CerkeOnline.Data.DataStructure
{
    [CreateAssetMenu(menuName = "ScriptableObject/PieceMaterialsObject")]
    public class PieceMaterialsObject : ScriptableObject
    {
        [SerializeField] Material redMaterial = default;
        public Material RedMaterial { get { return redMaterial; } }
        [SerializeField] Material blackMaterial = default;
        public Material BlackMaterial { get { return blackMaterial; } }
    }
}