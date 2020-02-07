using System.Web;

namespace Flutje.EmptyWeb
{
  public class EchoXml : IHttpHandler
  {
    public void ProcessRequest(HttpContext context)
    {
      var request = context.Request;
      var xmlDoc = XmlDoc.FromStream(request.InputStream);

      xmlDoc.InsertBefore(xmlDoc.CreateProcessingInstruction("By", "EchoXml"), xmlDoc.DocumentElement);
      xmlDoc.InsertBefore(xmlDoc.CreateProcessingInstruction("Host", Helper.Instance.MachineName), xmlDoc.DocumentElement);
      xmlDoc.InsertBefore(xmlDoc.CreateProcessingInstruction("Version", Helper.Instance.ProductVersion.ToString(2)), xmlDoc.DocumentElement);

      var response = context.Response;
      var bufferOut = xmlDoc.ToByteArr();
      response.ContentType = "text/xml";
      response.OutputStream.Write(bufferOut, 0, bufferOut.Length);
    }

    public bool IsReusable
    {
      get { return true; }
    }
  }
}