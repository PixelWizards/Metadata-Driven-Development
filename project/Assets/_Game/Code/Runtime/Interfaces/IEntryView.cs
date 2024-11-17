namespace PixelWizards.Interfaces.Interfaces
{
    /// <summary>
    /// Generic interface for UI Views
    /// </summary>
    /// <typeparam name="T">the Metadata Model for this metadata type</typeparam>
    public interface IEntryView<T>
    {
        public void Init(T entry);
    }
}