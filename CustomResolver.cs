using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Saxon10CustomResolverTest1
{
    sealed class CustomResolver : XmlResolver
    {
        private readonly XmlResolver _innerResolver;
        public CustomResolver(XmlResolver innerResolver) => _innerResolver = innerResolver;
        public override ICredentials Credentials { set { _innerResolver.Credentials = value; } }
        public override Uri ResolveUri(Uri baseUri, string relativeUri) => base.ResolveUri(baseUri, relativeUri);

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            if (absoluteUri.Scheme == "custom-scheme")
            {
                var memoryStream = new MemoryStream();
                var writerSettings = new XmlWriterSettings { OmitXmlDeclaration = true };

                using (var xmlWriter = XmlWriter.Create(memoryStream, writerSettings))
                {
                    new XDocument(new XElement("root", new XElement("data", "custom data here"))).Save(xmlWriter);
                }

                memoryStream.Position = 0;
                return memoryStream;
            }
            return _innerResolver.GetEntity(absoluteUri, role, ofObjectToReturn);
        }
    }
}
