namespace Azarashi.Utilities.Database
{
    /// <summary>
    /// 読み取り専用データベース.
    /// </summary>
    /// <typeparam name="IDType"></typeparam>
    /// <typeparam name="ItemType"></typeparam>
    public interface IReadOnlyDatabase<IDType, ItemType>
    {
        ItemType Get(IDType id);
    }
}