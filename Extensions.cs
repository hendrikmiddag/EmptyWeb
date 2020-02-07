using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Flutje.EmptyWeb
{
  public static class Extensions
  {
    public static XmlDocument ToXmlDocument(this byte[] xml)
    {
      var xmlDocument = new XmlDocument();
      using (var ms = new MemoryStream(xml))
        xmlDocument.Load(ms);
      return xmlDocument;
    }

    public static byte[] ToBytes(this XmlDocument xmlDocument)
    {
      using (var memoryStream = new MemoryStream())
      {
        xmlDocument.Save(memoryStream);
        return memoryStream.ToArray();
      }
    }

    public static T DeserializeFromXml<T>(this byte[] xml)
    {
      using (var xmlStream = new MemoryStream(xml))
      {
        var serializer = new XmlSerializer(typeof(T));
        using (var streamReader = new StreamReader(xmlStream, Encoding.UTF8))
          return (T) serializer.Deserialize(streamReader);
      }
    }

    public static byte[] SerializeToXml<T>(this T obj)
    {
      using (var xmlStream = new MemoryStream())
      {
        var serializer = new XmlSerializer(typeof(T));
        using (var streamWiter = new StreamWriter(xmlStream, Encoding.UTF8))
          serializer.Serialize(streamWiter, obj);
        return xmlStream.ToArray();
      }
    }

    public static T DeserializeFromJson<T>(this byte[] json)
    {
      using (var jsonStream = new MemoryStream(json))
      {
        var serializer = new DataContractJsonSerializer(typeof(T));
        return (T) serializer.ReadObject(jsonStream);
      }
    }

    public static byte[] SerializeToJson<T>(this T obj)
    {
      using (var jsonStream = new MemoryStream())
      {
        var serializer = new DataContractJsonSerializer(typeof(T));
        serializer.WriteObject(jsonStream, obj);
        return jsonStream.ToArray();
      }
    }

    public static T ExecuteXmlRequest<T>(this T obj, string requestUriString, string authorization)
    {
      var request = (HttpWebRequest) WebRequest.Create(requestUriString);
      request.Method = "POST";
      request.ContentType = "text/xml";
      request.Headers.Add("Authorization", string.Format("Basic {0}", authorization));

      var requestData = obj.SerializeToXml();
      request.ContentLength = requestData.Length;
      using (var stream = request.GetRequestStream())
        stream.Write(requestData, 0, requestData.Length);

      var response = (HttpWebResponse) request.GetResponse();
      request.Timeout = TimeSpan.FromSeconds(5.0).Milliseconds;

      var responseData = new byte[response.ContentLength];
      using (var responseStream = response.GetResponseStream())
        if (responseStream != null)
          responseStream.Read(responseData, 0, responseData.Length);

      var ctiResponse = responseData.DeserializeFromXml<T>();
      return ctiResponse;
    }

    public static T ExecuteJsonPostRequest<T>(this T obj, string requestUriString, string authorization)
    {
      var request = (HttpWebRequest) WebRequest.Create(requestUriString);
      request.Method = "POST";
      request.ContentType = "text/json";
      request.Headers.Add("Authorization", string.Format("Basic {0}", authorization));

      var requestData = obj.SerializeToJson();
      request.ContentLength = requestData.Length;
      using (var stream = request.GetRequestStream())
        stream.Write(requestData, 0, requestData.Length);

      var response = (HttpWebResponse) request.GetResponse();
      request.Timeout = TimeSpan.FromSeconds(5.0).Milliseconds;

      var responseData = new byte[response.ContentLength];
      using (var responseStream = response.GetResponseStream())
        if (responseStream != null)
          responseStream.Read(responseData, 0, responseData.Length);

      var ctiResponse = responseData.DeserializeFromJson<T>();
      return ctiResponse;
    }

    public static T ExecuteJsonGetRequest<T>(string requestUriString, string authorization)
    {
      var request = (HttpWebRequest) WebRequest.Create(requestUriString);
      request.Method = "GET";
      request.ContentType = "text/json";
      request.Headers.Add("Authorization", string.Format("Basic {0}", authorization));

      var response = (HttpWebResponse) request.GetResponse();
      request.Timeout = TimeSpan.FromSeconds(5.0).Milliseconds;

      var responseData = new byte[response.ContentLength];
      using (var responseStream = response.GetResponseStream())
        if (responseStream != null)
          responseStream.Read(responseData, 0, responseData.Length);

      var ctiResponse = responseData.DeserializeFromJson<T>();
      return ctiResponse;
    }
  }
}