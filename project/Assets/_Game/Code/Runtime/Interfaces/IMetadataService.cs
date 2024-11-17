using System.Collections.Generic;

namespace PixelWizards.Interfaces.Interfaces
{
    /// <summary>
    /// Generic Interface for Services
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMetadataService<T>
    {
        public void Init();
        public T Get(string id);
        public List<T> GetAll();
    }
}