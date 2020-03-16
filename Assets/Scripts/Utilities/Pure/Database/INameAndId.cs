namespace Azarashi.Utilities.Database
{
    /// <summary>
    /// データベース用の名前とIDの相互変換インターフェース.
    /// </summary>
    /// <typeparam name="IDType"></typeparam>
    /// <typeparam name="ItemType"></typeparam>
    public interface INameAndID<IDType, ItemType>
        where ItemType : INameable
    {
        IDType NameToId(string name);
        string IdToName(IDType id);
    }
}