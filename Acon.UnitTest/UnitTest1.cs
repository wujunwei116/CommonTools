using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Acon.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SequentialGuidGenerator generatror = SequentialGuidGenerator.Instance;
            int i=0;
            Random rand = new Random(new Guid().GetHashCode());
                
            while (i < 10)
            {
                var r=rand.Next(3);
                Console.WriteLine(r);
                Console.WriteLine(generatror.NewSequentialGuid());
                Thread.Sleep(r);
                i++;
            }
        }
    }
}
