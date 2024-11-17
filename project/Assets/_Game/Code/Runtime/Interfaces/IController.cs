namespace PixelWizards.Interfaces.Interfaces
{
    /// <summary>
    /// Generic interface for object controllers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IController<T>
    {
        public void Init(T entry);
    }
}