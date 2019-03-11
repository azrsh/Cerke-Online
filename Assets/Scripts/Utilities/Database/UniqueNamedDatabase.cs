namespace Azarashi.Utilities.Database
{
    public class UniqueNamedDatabase<ItemType> : Database<ItemType>, INameAndID<int, ItemType>
        where ItemType : class, INameable
    {
        public override bool CheckRegisterable(ItemType item)
        {   
            return (NameToId(item.Name) < 0);   //同名のブロックが存在するか
        }

        public virtual ItemType Get(string name)
        {
            int id = NameToId(name);
            return Get(id);
        }

        public virtual int NameToId(string name)
        {
            //index == idだからindexを返す
            for (int i = 0; i < itemList.Length; i++)
            {
                if (itemList[i] != null && itemList[i].Name == name)
                {
                    return i;
                }
            }

            return -1;
        }

        public virtual string IdToName(int id)
        {
            if (id >= 0 && itemList.Length > id && itemList[id] != null)
            {
                return itemList[id].Name;
            }

            return null;
        }
    }
}