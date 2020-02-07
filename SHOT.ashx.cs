using System;
using System.Diagnostics;
using System.Threading;
using System.Web;

namespace Flutje.EmptyWeb
{
  public class Shot : IHttpHandler
  {
    static int staticCounter;
    static int threadNumbers;

    [ThreadStatic]
    static int threadNumber;

    [ThreadStatic]
    static int threadStaticCounter;

    static int instances;
    bool instanceUpgraded;

    static object lockObject = new object();

    public void ProcessRequest(HttpContext context)
    {
      lock (lockObject)
      {
        if (threadNumber == 0)
          threadNumber = ++threadNumbers;

        staticCounter++;
        threadStaticCounter++;

        if (!instanceUpgraded)
        {
          instances++;
          instanceUpgraded = true;
        }
      }

      string returnString = string.Format("{0} {1}/{2} {3} {4} {5} {6} {7}",
        staticCounter,
        threadNumber,
        threadNumbers,
        threadStaticCounter,
        instances,
        Process.GetCurrentProcess().Id,
        Thread.CurrentThread.ManagedThreadId,
        Helper.Instance.ProductVersion.ToString(2));

      context.Response.ContentType = "text/plain";
      context.Response.Write(returnString);
    }

    public bool IsReusable
    {
      get { return true; }
    }
  }
}