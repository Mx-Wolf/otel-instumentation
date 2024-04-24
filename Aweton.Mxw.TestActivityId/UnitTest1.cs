using System.Diagnostics;

namespace Aweton.Mxw.TestActivityId
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestMethod1()
    {
      ActivitySource.AddActivityListener(new ActivityListener()
      {
        ShouldListenTo = s=>true,
        SampleUsingParentId = ((ref ActivityCreationOptions<string> options) => ActivitySamplingResult.AllData),
        Sample = ((ref ActivityCreationOptions<ActivityContext> options) => ActivitySamplingResult.AllData),
      });
      var x = new ActivitySource("test");
      
      using var t = x.StartActivity();
      t.TraceStateString = "aa=bb";
      var id = t.Id;
      Assert.IsTrue(id?.Cast<char>().Count(e =>e == '-') == 3);
    }
  }
}