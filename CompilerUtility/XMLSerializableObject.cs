using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DaveSoftware.Common
{
    /// <summary>
    /// Questa classe può essere estesa da un qualunque oggetto che deve essere
    /// rappresentato (serializzato/deserializzato) con una codifica XML.
    /// La classe template deve essere instanziata sul tipo dell'oggetto che 
    /// la estende in questo modo:
    ///     class XmlObject : XMLSerializableObject<XmlObject>
    /// La classe metterà a disposizione delle classe che estende i metodi per
    /// la serializzazione (oggetto -> xml) e la deserializzaione (xml -> oggetto).
    /// 
    /// Al fine di ottenere XML concisi, vengono omessi i namspace e la dichiarazione
    /// dell'intestazione xml.
    /// </summary>
    /// <typeparam name="T">Il tipo che estende la classe</typeparam>
    [Serializable]
    public abstract class XMLSerializableObject<T>
    {
        #region Configurations
        protected static bool _indentation;
        protected static Encoding _encoding;
        protected static bool _omitXmlDeclaration;
        protected static XmlSerializerNamespaces _xmlnsEmpty;
        #endregion

        #region Private Fields
        private static readonly XmlSerializer _serializer;
        private static readonly Dictionary<string, XmlSerializer> _xmlSerializersCache = null;
        private static readonly object _staticSyncObj = null;
        #endregion

        #region Properties
        public static Encoding CurrentEncoding { get { return _encoding; } }
        #endregion

        /// <summary>
        /// Inizializza le struttre statiche associate alla classe instanziata
        /// con un tipo T specifico.
        /// Setta le impostazioni di default:
        ///     -> Namespace vuoti
        ///     -> Omette il tag xml (<?xml>)
        ///     -> Non inserisce a capi e identazioni
        ///     -> Encoding UTF8
        /// </summary>
        static XMLSerializableObject()
        {
            // Configurazione di default
            _indentation = false;
            _encoding = Encoding.UTF8;
            _omitXmlDeclaration = true;
            _xmlnsEmpty = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            // Crea il serializzatore statico del tipo T
            _serializer = new XmlSerializer(typeof(T));

            // Inizializza la collezione statica e l'oggetto statico per sincornizzarla
            _xmlSerializersCache = new Dictionary<string, XmlSerializer>();
            _staticSyncObj = new object();
        }

        /// <summary>
        /// Implementa una cache di serializzatori per ovviare alla lentezza della
        /// loro creazione. Se il serializzatore esiste già viene restituito,
        /// altrimenti ne viene creato uno nuovo.
        /// </summary>
        /// <param name="type">Il tipo associato al serializzatore</param>
        /// <param name="rootElementName">Il nome della root dell'XML che il
        /// serializzatore deve gestire</param>
        /// <returns>Il serializzatore in cache</returns>
        protected static XmlSerializer CreateSerializer(Type type, string rootElementName)
        {
            lock (_staticSyncObj)
            {
                // Determine univoque type/root ID
                string key = string.Format("{0}:{1}", type, rootElementName);

                // If the required serializer does not exist, then create it
                if (!_xmlSerializersCache.ContainsKey(key))
                {
                    XmlSerializer xs = new XmlSerializer(type, new XmlRootAttribute(rootElementName));
                    _xmlSerializersCache.Add(key, xs);
                }

                // At this point the serialize exists for sure, then send it back to the requester
                return (_xmlSerializersCache[key]);
            }
        }

        /// <summary>
        /// Serializza l'oggetto passato sullo stream indicato secondo
        /// la codifica configurata
        /// </summary>
        /// <param name="obj">Oggetto di tipo T da serializzare</param>
        public static void ToXml(T obj, Stream outStream)
        {
            String str = String.Empty;
            if (obj != null)
            {
                // Configuro il WRITER con le impostazioni interne
                XmlWriterSettings writerSettings = new XmlWriterSettings();
                writerSettings.Encoding = _encoding;
                writerSettings.Indent = _indentation;
                writerSettings.OmitXmlDeclaration = _omitXmlDeclaration;

                
                TextWriter myStreamWriter = new StreamWriter(outStream, _encoding);
                //XmlTextWriter mytw = new XmlTextWriter(myStreamWriter);
                XmlWriter mytw = XmlTextWriter.Create(myStreamWriter, writerSettings);

                _serializer.Serialize(mytw, obj, _xmlnsEmpty);
            }
        }

        /// <summary>
        /// Serializza l'oggetto passato in una stringa XML utilizzando tutte le impostazioni
        /// (configurate alla creazione dell'oggetto)
        /// </summary>
        /// <param name="obj">Oggetto di tipo T da serializzare</param>
        /// <returns>La stringa XML generata</returns>
        public static string ToXml(T obj)
        {
            return ToXml(obj, null, null, null);
        }

        /// <summary>
        /// Serializza l'oggetto passato in una stringa XML utilizzando
        /// le impostazioni attuali e non quelle di default.
        /// I parametri NULL saranno presi dalla configurazioen di default. 
        /// </summary>
        /// <param name="obj">Oggetto di tipo T da serializzare</param>
        /// <param name="indent">Indica se si vuole la stringa indentata secondo XML</param>
        /// <param name="omitXmlDeclaration">Indica se si vuole omettere il tag XML inziale</param>
        /// <param name="encoding">Indica l'encoding da utilizzare</param>
        /// <returns>La stringa XML generata</returns>
        public static string ToXml(T obj, bool? indentation, bool? omitXmlDeclaration, Encoding encoding)
        {
            String str = String.Empty;
            if (obj != null)
            {
                StringBuilder sb = new StringBuilder();

                // Configuro il WRITER con le impostazioni passate o interne
                XmlWriterSettings writerSettings = new XmlWriterSettings();
                
                if (encoding != null)
                    writerSettings.Encoding = encoding;
                else
                    writerSettings.Encoding = _encoding;

                if (indentation != null)
                    writerSettings.Indent = (bool)indentation;
                else
                    writerSettings.Indent = _indentation;

                if (omitXmlDeclaration != null)
                    writerSettings.OmitXmlDeclaration = (bool)omitXmlDeclaration;
                else
                    writerSettings.OmitXmlDeclaration = _omitXmlDeclaration;


                XmlWriter writer = XmlTextWriter.Create(sb, writerSettings);
                _serializer.Serialize(writer, obj, _xmlnsEmpty);

                str = sb.ToString();
            }

            return str;
        }

        /// <summary>
        /// Deserializza una stringa XML in un oggetto di tipo T.
        /// Attenzione: fornire una stringa codificata secondo encoding
        /// definito nell'oggetto stesso
        /// </summary>
        /// <param name="xmlStr">La stringa XML sorgente</param>
        /// <returns>L'oggetto costruito dal processo di deserializzazione</returns>
        public static T FromXml(String xmlStr)
        {
            TextReader reader = new StringReader(xmlStr);
            T obj = (T)_serializer.Deserialize(reader);
            reader.Close();
            return obj;
        }

        /// <summary>
        /// Deserializza uno stream (per esemoio un file) XML in un oggetto di tipo T.
        /// </summary>
        /// <param name="xmlStr">La stringa XML sorgente</param>
        /// <returns>L'oggetto costruito dal processo di deserializzazione</returns>
        public static T FromXml(Stream inStream)
        {
            TextReader reader = new StreamReader(inStream, _encoding);
            T obj = (T)_serializer.Deserialize(reader);
            reader.Close();
            return obj;
        }
    }
}
