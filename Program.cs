using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Saxon.Api;

namespace Saxon10CustomResolverTest1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Processor processor = new Processor();

            XsltCompiler xsltCompiler = processor.NewXsltCompiler();

            XsltExecutable xsltExecutable = xsltCompiler.Compile(new Uri(Path.Combine(Environment.CurrentDirectory, "xslt-custom-uri-scheme-test1.xsl")));

            XsltTransformer xsltTransformer = xsltExecutable.Load();

            xsltTransformer.InputXmlResolver = new CustomResolver(new XmlUrlResolver());

            xsltTransformer.InitialContextNode = processor.NewDocumentBuilder().Build(new Uri(Path.Combine(Environment.CurrentDirectory, "dummy-input1.xml")));

            xsltTransformer.Run(processor.NewSerializer(Console.Out));

            Console.ReadLine();
        }
    }

 
}
