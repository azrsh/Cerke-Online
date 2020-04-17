using Azarashi.CerkeOnline.Domain.UseCase;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Data.Repository
{
    public class NoteRepository : INoteRepository
    {
        readonly INoteDataStore dataStore;

        public NoteRepository(INoteDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public NoteData GetNoteData()
        {
            return dataStore.Load();
        }

        public void SetNoteData(NoteData noteData)
        {
            dataStore.Save(noteData);
        }
    }

    public interface INoteDataStore
    {
        NoteData Load();
        void Save(NoteData noteData);
    }
}