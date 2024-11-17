using System.Collections.Generic;

namespace PixelWizards.Interfaces.Interfaces
{
    /// <summary>
    /// Generic Interface for Services that parse / load metadata from BG Database
    /// Ironically the MetadataService doesn't inherit from this
    /// </summary>
    /// <typeparam name="T">The Model class that this metadata service references</typeparam>
    public interface IMetadataService<T>
    {
        /// <summary>
        /// Init the service, parse the metadata
        /// </summary>
        public void Init();
        
        /// <summary>
        /// Get a single instance of this metadata type
        /// </summary>
        /// <param name="id">the 'name' / id of the item we want to retrieve</param>
        /// <returns>a single instance of 'T' (the metadata Model)</returns>
        public T Get(string id);
        
        /// <summary>
        /// Retrieve all of the metadata items for this type.
        /// </summary>
        /// <returns>a List of type T, where T is the metadata model.</returns>
        public List<T> GetAll();
    }
}