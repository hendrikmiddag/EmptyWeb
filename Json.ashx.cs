using System.Collections.Generic;
using System.Web;

namespace Flutje.EmptyWeb
{
  //[DataContract]
  public class Ticket
  {
    public Ticket()
    {
      Dimensions = new List<Dimension>();
      Metrics = new List<Metric>();
    }

    //[DataMember]
    public List<Dimension> Dimensions { get; set; }

    //[DataMember]
    public List<Metric> Metrics { get; set; }
  }

  //[DataContract]
  public class Dimension
  {
    //[DataMember]
    public string Name { get; set; }

    //[DataMember]
    public string Value { get; set; }
  }

  //[DataContract]
  public class Metric
  {
    //[DataMember]
    public string Name { get; set; }

    //[DataMember]
    public int Value { get; set; }
  }

  /// <summary>
  /// Summary description for JSON
  /// </summary>
  public class Json : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      var ticketsOut = new List<Ticket>();

      var ticketOut1 = new Ticket();
      ticketOut1.Dimensions.Add(new Dimension {Name = "vessel", Value = "Amundsen Spirit"});
      ticketOut1.Dimensions.Add(new Dimension {Name = "status", Value = "new"});
      ticketOut1.Metrics.Add(new Metric {Name = "ticket", Value = 10});
      ticketsOut.Add(ticketOut1);

      var ticketOut2 = new Ticket();
      ticketOut2.Dimensions.Add(new Dimension {Name = "vessel", Value = "Amundsen Spirit"});
      ticketOut2.Dimensions.Add(new Dimension {Name = "status", Value = "closed"});
      ticketOut2.Metrics.Add(new Metric {Name = "ticket", Value = 1});
      ticketsOut.Add(ticketOut2);


      var ticketOut3 = new Ticket();
      ticketOut3.Dimensions.Add(new Dimension {Name = "vessel", Value = "Lamdada Spirit"});
      ticketOut3.Dimensions.Add(new Dimension {Name = "status", Value = "new"});
      ticketOut3.Metrics.Add(new Metric {Name = "ticket", Value = 2});
      ticketsOut.Add(ticketOut3);

      var ticketOut4 = new Ticket();
      ticketOut4.Dimensions.Add(new Dimension {Name = "vessel", Value = "Lamdada Spirit"});
      ticketOut4.Dimensions.Add(new Dimension {Name = "status", Value = "closed"});
      ticketOut4.Metrics.Add(new Metric {Name = "ticket", Value = 8});
      ticketsOut.Add(ticketOut4);


      var ticketOut5 = new Ticket();
      ticketOut5.Dimensions.Add(new Dimension {Name = "vessel", Value = "Nansen Spirit"});
      ticketOut5.Dimensions.Add(new Dimension {Name = "status", Value = "new"});
      ticketOut5.Metrics.Add(new Metric {Name = "ticket", Value = 2});
      ticketsOut.Add(ticketOut5);

      var ticketOut6 = new Ticket();
      ticketOut6.Dimensions.Add(new Dimension {Name = "vessel", Value = "Nansen Spirit"});
      ticketOut6.Dimensions.Add(new Dimension {Name = "status", Value = "closed"});
      ticketOut6.Metrics.Add(new Metric {Name = "ticket", Value = 12});
      ticketsOut.Add(ticketOut6);


      var ticketOut7 = new Ticket();
      ticketOut7.Dimensions.Add(new Dimension {Name = "vessel", Value = "Peary Spirit"});
      ticketOut7.Dimensions.Add(new Dimension {Name = "status", Value = "new"});
      ticketOut7.Metrics.Add(new Metric {Name = "ticket", Value = 1});
      ticketsOut.Add(ticketOut7);

      var ticketOut8 = new Ticket();
      ticketOut8.Dimensions.Add(new Dimension {Name = "vessel", Value = "Peary Spirit"});
      ticketOut8.Dimensions.Add(new Dimension {Name = "status", Value = "closed"});
      ticketOut8.Metrics.Add(new Metric {Name = "ticket", Value = 3});
      ticketsOut.Add(ticketOut8);


      var ticketOut9 = new Ticket();
      ticketOut9.Dimensions.Add(new Dimension {Name = "vessel", Value = "Samba Spirit"});
      ticketOut9.Dimensions.Add(new Dimension {Name = "status", Value = "new"});
      ticketOut9.Metrics.Add(new Metric {Name = "ticket", Value = 5});
      ticketsOut.Add(ticketOut9);

      var ticketOut10 = new Ticket();
      ticketOut10.Dimensions.Add(new Dimension {Name = "vessel", Value = "Samba Spirit"});
      ticketOut10.Dimensions.Add(new Dimension {Name = "status", Value = "closed"});
      ticketOut10.Metrics.Add(new Metric {Name = "ticket", Value = 7});
      ticketsOut.Add(ticketOut10);


      var ticketOut11 = new Ticket();
      ticketOut11.Dimensions.Add(new Dimension {Name = "vessel", Value = "Sertanejo Spirit"});
      ticketOut11.Dimensions.Add(new Dimension {Name = "status", Value = "new"});
      ticketOut11.Metrics.Add(new Metric {Name = "ticket", Value = 4});
      ticketsOut.Add(ticketOut11);

      var ticketOut12 = new Ticket();
      ticketOut12.Dimensions.Add(new Dimension {Name = "vessel", Value = "Sertanejo Spirit"});
      ticketOut12.Dimensions.Add(new Dimension {Name = "status", Value = "closed"});
      ticketOut12.Metrics.Add(new Metric {Name = "ticket", Value = 4});
      ticketsOut.Add(ticketOut12);


      var ticketsBytesOut = ticketsOut.SerializeToJson();

      context.Response.ContentType = "application/json";
      context.Response.OutputStream.Write(ticketsBytesOut, 0, ticketsBytesOut.Length);
    }

    public bool IsReusable
    {
      get { return false; }
    }
  }
}