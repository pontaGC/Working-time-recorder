using System.Xml.Serialization;
using SharedLibraries.Extensions;
using SharedLibraries.Retry;

namespace SharedLibraries.System.Xml
{
    /// <summary>
    /// Responsible for serializing or deserializing the object related to XML document.
    /// </summary>
    public static class XmlSerializerHelper
    {
        #region Public Methods

        /// <summary>
        /// Serializes the source object to a target stream.
        /// </summary>
        /// <typeparam name="T">The type of a source object.</typeparam>
        /// <param name="source">The object to serialize. If <c>source</c> is <c>null</c>, this method does nothing.</param>
        /// <param name="targetStream">The target stream to serialize.</param>
        /// <param name="namespaces">The namespaces for then generated XML document.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">An error occurred during serialization. The original exception is available using the <c>InnerException</c> property.</exception>
        public static void Serialze<T>(T source, Stream targetStream, XmlSerializerNamespaces? namespaces = null)
        {
            ArgumentNullException.ThrowIfNull(source);

            var serializer = new XmlSerializer(typeof(T));
            RetryHelper.InvokeWithRetry(() => serializer.Serialize(targetStream, source, namespaces));
        }

        /// <summary>
        /// Deserializes the specified XML document to create the target object.
        /// </summary>
        /// <typeparam name="T">The type of a target object deserialized.</typeparam>
        /// <param name="xmlStream">The XML document stream to deserialize.</param>
        /// <returns>The deserialized object which type is <c>T</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="xmlStream"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">An error occurred during serialization. The original exception is available using the <c>InnerException</c> property.</exception>
        public static T Deserialize<T>(Stream xmlStream)
        {
            ArgumentNullException.ThrowIfNull(xmlStream);

            var result = default(T);
            var serializer = new XmlSerializer(typeof(T));
            result = (T)RetryHelper.InvokeWithRetry(() => serializer.Deserialize(xmlStream));
            return result;
        }

        /// <summary>
        /// Try serializing the source object to a target stream.
        /// </summary>
        /// <typeparam name="T">The type of a source object.</typeparam>
        /// <param name="source">The object to serialize. If <c>source</c> is <c>null</c>, this method does nothing.</param>
        /// <param name="targetStream">The target stream to serialize.</param>
        /// <param name="namespaces">The namespaces for then generated XML document.</param>
        /// <returns><c>true</c>, if the serialization is success, Otherwise; <c>false</c>.</returns>
        public static bool TrySerialize<T>(T source, Stream targetStream, XmlSerializerNamespaces? namespaces = null)
        {
            if (source is null)
            {
                return false;
            }

            var serializer = new XmlSerializer(typeof(T));
            try
            {
                RetryHelper.InvokeWithRetry(() => Serialze(source, targetStream, namespaces));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deserializes the specified XML document to create the target object.
        /// </summary>
        /// <typeparam name="T">The type of a target object deserialized.</typeparam>
        /// <param name="xmlStream">The XML document stream to deserialize.</param>
        /// <param name="deserializedObject">The deserialized object which type is <c>T</c>.</param>
        /// <returns><c>true</c>, if the deserialization is success, Otherwise; <c>false</c>.</returns>
        public static bool TryDeserialize<T>(Stream xmlStream, out T deserializedObject)
        {
            deserializedObject = default;

            if (xmlStream.IsNullOrEmpty())
            {
                return false;
            }

            var serializer = new XmlSerializer(typeof(T));
            try
            {
                deserializedObject = Deserialize<T>(xmlStream);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
