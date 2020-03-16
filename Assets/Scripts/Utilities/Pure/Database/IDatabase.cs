namespace Azarashi.Utilities.Database
{
    /// <summary>
    /// ResisterationManagerから置き換えていきたい所存.
    /// </summary>
    /// <typeparam name="IDType">管理IDの型. Database内では一意. 基本intを想定.</typeparam>
    /// <typeparam name="ItemType">管理対象の型.</typeparam>
    public interface IDatabase<IDType, ItemType> : IAddOnlyDatabase<IDType,ItemType>
        where IDType : struct
    {
        void Remove(IDType id);
    }
}