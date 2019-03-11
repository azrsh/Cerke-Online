namespace Azarashi.Utilities.Database
{
    /// <summary>
    /// 読み取り及び追加専用データベース.
    /// </summary>
    /// <typeparam name="IDType"></typeparam>
    /// <typeparam name="ItemType"></typeparam>
    public interface IAddOnlyDatabase<IDType, ItemType> : IReadOnlyDatabase<IDType, ItemType>
    {
        IDType Add(ItemType item);
    }
}