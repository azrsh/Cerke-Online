namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public interface IMapProvider<T>
    {
        T[,] GetMap();
    }
}