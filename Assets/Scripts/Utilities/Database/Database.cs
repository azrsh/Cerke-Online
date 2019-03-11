namespace Azarashi.Utilities.Database
{
    public class Database<ItemType> : IDatabase<int, ItemType>
        where ItemType : class
    {
        protected ItemType[] itemList;

        public Database(int capacity = 32)
        {
            itemList = new ItemType[capacity];
        }

        public virtual int Add(ItemType item)
        {
            //条件に合えばobjectを追加
            if (CheckRegisterable(item))
            {
                //配列から空きを検索して空きがあれば代入
                for (int i = 0; i < itemList.Length; i++)
                {
                    if (itemList[i] == null)
                    {
                        itemList[i] = item;
                        return i;
                    }
                }

                //ここまで来た場合、配列に空きがなかったということなので配列を増設して代入
                ItemType[] temp = itemList;
                itemList = new ItemType[temp.Length + 16];
                for (int i = 0; i < temp.Length + 1; i++)
                {
                    if (i < temp.Length)
                    {
                        itemList[i] = temp[i];
                    }
                    else
                    {
                        itemList[i] = item;
                        temp = null;
                        return i;
                    }
                }
                temp = null;
            }

            return -1;
        }

        public virtual bool CheckRegisterable(ItemType item) { return true; }
        
        public virtual ItemType Get(int id)
        {
            if (id < 0 || id > itemList.Length || itemList[id] == null)
                return null;

            return itemList[id];
        }

        public virtual void Remove(int id)
        {
            if (id < 0 || id > itemList.Length || itemList[id] == null)
                return;

            itemList[id] = null;
        }
    }
}