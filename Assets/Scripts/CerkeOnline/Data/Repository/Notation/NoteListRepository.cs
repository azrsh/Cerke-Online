using System.Collections.Generic;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Data.Repository
{
    public class NoteListRepository : INoteListRepository
    {
        readonly INoteListDataStore dataStore;

        public NoteListRepository(INoteListDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public IEnumerable<string> GetNoteList()
        {
            return dataStore.Load();
        }
    }

    public interface INoteListDataStore
    {
        IEnumerable<string> Load(); 
    }
}