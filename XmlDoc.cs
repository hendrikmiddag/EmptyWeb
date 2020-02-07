using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Flutje.EmptyWeb
{
  public class XmlDoc : XmlDocument
  {
    private XmlNode tmpNode;

    public XmlDoc() : base()
    {
    }

    public XmlDoc(string elementName)
      : base()
    {
      tmpNode = base.AppendChild(base.CreateElement(elementName));
    }

    public XmlDoc(XmlDocument baseDoc)
      : base()
    {
      if (baseDoc == null)
        throw new ArgumentNullException("baseDoc");
      base.LoadXml(baseDoc.OuterXml);
    }

    public void Attribute(string name, string value)
    {
      if (tmpNode.Attributes != null)
        tmpNode.Attributes.Append(base.CreateAttribute(name)).Value = value;
    }

    public void Attribute(string name, int value)
    {
      Attribute(name, value.ToString());
    }

    public void Attribute(string name, long value)
    {
      Attribute(name, value.ToString());
    }

    public void Element(string name)
    {
      tmpNode = tmpNode.AppendChild(base.CreateElement(name));
    }

    public void Element(string name, string value)
    {
      tmpNode.AppendChild(base.CreateElement(name)).InnerText = value;
    }

    public void CloseElement()
    {
      tmpNode = tmpNode.ParentNode;
    }

    public byte[] ToByteArr()
    {
      var arr = new byte[0];
      using (var ms = new MemoryStream())
      {
        using (var xtw = new XmlTextWriter(ms, new UTF8Encoding(false)))
        {
          xtw.Formatting = System.Xml.Formatting.None;
          base.PreserveWhitespace = true;
          base.Save(xtw);

          var len = (int) ms.Position;

          arr = new byte[len];
          ms.Position = 0;
          ms.Read(arr, 0, len);
        }
      }

      return arr;
    }

    public static XmlDoc FromByteArr(byte[] arr)
    {
      var outputDoc = new XmlDoc();
      using (var ms = new MemoryStream(arr))
        outputDoc.Load(ms);
      return outputDoc;
    }

    public static XmlDoc FromStream(Stream readStream)
    {
      var memoryStream = new MemoryStream();
      readStream.CopyTo(memoryStream);
      return FromByteArr(memoryStream.ToArray());
    }

    public string IndentXMLString()
    {
      try
      {
        using (var ms = new MemoryStream())
        {
          using (var xtw = new XmlTextWriter(ms, new UTF8Encoding(false)))
          {
            xtw.Formatting = Formatting.Indented;
            base.WriteContentTo(xtw);
            xtw.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            using (var sr = new StreamReader(ms))
              return sr.ReadToEnd();
          }
        }
      }
      catch
      {
        return base.OuterXml;
      }
    }
  }
}