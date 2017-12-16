using System.Xml;

namespace Automata.Interface
{
    /// <summary>
    /// Defines an interface for objects that has read and write from and to XML files.
    /// </summary>
    public interface IXmlObject
    {
        /// <summary>
        /// This method is called to attach extra data to the XML element of this entity.
        /// </summary>
        /// <param name="writer">The current XML writer instance.</param>
        void WriteToXmlWriter(XmlWriter writer);

        /// <summary>
        /// This method is called to read extra data from the XML element of this entity.
        /// </summary>
        /// <param name="reader">The current XML reader instance.</param>
        void ReadFromXmlReader(XmlReader reader);
    }
}
