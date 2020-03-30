namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal interface IExpandingMoveFieldChecker
    {
        bool IsExpandedMoveField(PublicDataType.IntegerVector2 position);
    }
}