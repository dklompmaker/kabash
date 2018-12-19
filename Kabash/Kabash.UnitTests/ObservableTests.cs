using AOFL.Kabash.V1.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kabash.UnitTests
{
    [TestClass]
    public class ObservableTests
    {
        [TestMethod]
        public void Observable_DoesTriggerValueChanged()
        {
            var didPass = false;
            var observable = new Observable<float>();
            observable.ValueChanged += delegate
            {
                didPass = true;
            };
            observable.Value = 10;

            Assert.AreEqual(true, didPass);
        }

        [TestMethod]
        public void Observable_DoesTriggerValueChangedOnlyOnChange()
        {
            var didPass = 0;
            var observable = new Observable<float>();
            observable.ValueChanged += delegate
            {
                didPass++;
            };

            observable.Value = 10;
            observable.Value = 10;

            Assert.AreEqual(1, didPass);
        }

        [TestMethod]
        public void Observable_DoesImplicitConversion()
        {
            var observable = (Observable<string>)"Hello World";

            Assert.AreEqual("Hello World", observable.Value);
        }

        [TestMethod]
        public void Observable_DoesMirrorObservable()
        {
            var observable = new Observable<float>();
            observable.Value = 10;

            var observable2 = new Observable<float>();
            observable2.Mirror(observable);
            observable.Value = 20;

            Assert.AreEqual(20, observable2.Value);
        }

        [TestMethod]
        public void Observable_DoesMirrorAndUnMirrorObservable()
        {
            var observable = new Observable<float>();
            observable.Value = 10;

            var observable2 = new Observable<float>();
            observable2.Mirror(observable);
            observable2.UnMirror(observable);
            observable.Value = 20;

            Assert.AreEqual(10, observable2.Value);
        }

        [TestMethod]
        public void Observable_WhenFloatQuerySucceeds()
        {
            var didPass = false;
            var observable = new Observable<float>();
            observable.When(x => x == 10, delegate
            {
                didPass = true;
            });

            observable.Value = 10;

            Assert.AreEqual(true, didPass);
        }

        [TestMethod]
        public void Observable_WhenFloatQuerySucceedsImmediate()
        {
            var didPass = false;
            var observable = new Observable<float>();
            observable.Value = 10;
            observable.When(x => x == 10, delegate
            {
                didPass = true;
            });

            Assert.AreEqual(true, didPass);
        }

        [TestMethod]
        public void Observable_DoesRemoveQuery()
        {
            var didPass = true;
            var observable = new Observable<float>();
            Func<float, bool> query = x => x == 10;
            observable.When(query, delegate
            {
                didPass = false;
            });
            observable.RemoveQuery(query);
            observable.Value = 10;

            Assert.AreEqual(true, didPass);
        }

        [TestMethod]
        public void Observable_TwoWayBindingDoesNotInfiniteLoop()
        {
            var a = new Observable<float>();
            var b = new Observable<float>();

            a.Mirror(b);
            b.Mirror(a);

            a.Value = 10;
            b.Value = 10;

            Assert.AreEqual(a.Value, b.Value);
        }

        [TestMethod]
        public void Observable_ThreeWayBindingDoesNotInfiniteLoop()
        {
            var a = new Observable<float>();
            var b = new Observable<float>();
            var c = new Observable<float>();

            a.Mirror(b);
            a.Mirror(c);

            b.Mirror(a);
            b.Mirror(c);

            c.Mirror(a);
            c.Mirror(b);

            a.Value = 10;
            b.Value = 10;
            c.Value = 10;

            Assert.AreEqual(a.Value, b.Value);
            Assert.AreEqual(b.Value, c.Value);
        }
    }
}
