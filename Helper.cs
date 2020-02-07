using System;
using System.Diagnostics;
using System.Reflection;

namespace Flutje.EmptyWeb
{
  public sealed class Helper
  {
    public static Helper Instance = new Helper();

    private Helper()
    {
      var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
      Description = versionInfo.FileDescription;
      ProductVersion = new Version(versionInfo.ProductVersion);
    }

    public string MachineName
    {
      get { return Environment.MachineName; }
    }

    public string Description { get; private set; }
    public Version ProductVersion { get; private set; }

    public string UtcNow
    {
      get { return DateTime.UtcNow.ToString(@"yyyy-MM-dd HH:mm:ss.fff"); }
    }

    public string GetHelloWorld()
    {
      return string.Format("Hello World from '{0}' on '{1}' using '{2} v{3}'...", MachineName, UtcNow, Description, ProductVersion.ToString(4));
    }
  }
}