using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AOFL.Kabash.V1.Core;

namespace Kabash.UnitTests
{
    [TestClass]
    public class ObservableListTests
    {
        [TestMethod]
        public void DoesInvokeAddedEvent()
        {
            var didPass = false;
            var list = new ObservableList<float>();
            list.ElementAdded += delegate
            {
                didPass = true;
            };

            list.Add(1);

            Assert.AreEqual(true, didPass);
        }

        [TestMethod]
        public void DoesInvokeRemovedEvent()
        {
            var didPass = false;
            var list = new ObservableList<float>();
            list.Add(1);
            list.ElementRemoved += delegate
            {
                didPass = true;
            };

            list.Remove(1);

            Assert.AreEqual(true, didPass);
        }

        [TestMethod]
        public void DoesMirrorValuesAdded()
        {
            var listA = new ObservableList<float>();
            var listB = new ObservableList<float>();

            listA.Mirror(listB);
            listB.Add(1);

            Assert.AreEqual(listB.Count, listA.Count);
        }

        [TestMethod]
        public void DoesMirrorValuesRemoved()
        {
            var listA = new ObservableList<float>();
            var listB = new ObservableList<float>();

            listA.Mirror(listB);
            listB.Add(1);
            listB.Remove(1);

            Assert.AreEqual(listB.Count, listA.Count);
        }

        [TestMethod]
        public void DoesMirrorValuesModified()
        {
            var listA = new ObservableList<float>();
            var listB = new ObservableList<float>();

            listA.Mirror(listB);
            listB.Add(1);
            listB[0] = 2;

            Assert.AreEqual(2, listA[0]);
        }
    }
}
