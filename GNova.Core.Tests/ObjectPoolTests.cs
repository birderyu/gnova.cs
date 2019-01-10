using Microsoft.VisualStudio.TestTools.UnitTesting;
using GNova.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Core.Tests
{
    [TestClass()]
    public class ObjectPoolTests
    {
        [TestMethod()]
        public void ObjectPoolTest()
        {

        }

        [TestMethod()]
        public void BorrowObjectTest()
        {
            
        }

        [TestMethod()]
        public void ReturnObjectTest()
        {
            using (IObjectPool<string> pool = new ObjectPool<string>())
            {
                string s = "this is a string";
                for (int i = 0; i < pool.Capacity; i++)
                {
                    pool.ReturnObject(s);
                }

                try
                {
                    pool.ReturnObject(s);
                    Assert.Fail();
                }
                catch
                {
                    // succeed!
                }
            }
        }

        [TestMethod()]
        public void DisposeTest()
        {

        }
    }
}