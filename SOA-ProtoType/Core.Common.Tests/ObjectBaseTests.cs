using System;
using System.Collections.Generic;
using System.ComponentModel;
using Core.Common.Tests.TestClasses;
using Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Common.Tests
{
    [TestClass]
    public class ObjectBaseTests
    {
        [TestMethod]
        public void test_clean_property_change()
        {
            var objTest = new TestClass();
            var propertyChanged = false;

            objTest.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "CleanProp")
                    propertyChanged = true;
            };

            objTest.CleanProp = "test value";
            Assert.IsTrue(propertyChanged, "The property should have triggered change notification");
        }

        [TestMethod]
        public void test_dirty_set()
        {
            var objTest = new TestClass();
            Assert.IsFalse(objTest.IsDirty, "object should be clean");

            objTest.DirtyProp = "test value";

            Assert.IsTrue(objTest.IsDirty, "Object should be dirty");
        }

        [TestMethod]
        public void test_property_change_single_subscription()
        {
            var objTest = new TestClass();
            int changeCounter = 0;

            var handler1 = new PropertyChangedEventHandler((s, e) => { changeCounter++; });

            var handler2 = new PropertyChangedEventHandler((s, e) => { changeCounter++; });

            objTest.PropertyChanged += handler1;
            objTest.PropertyChanged += handler1; // should not duplicate
            objTest.PropertyChanged += handler1; // should not duplicate
            objTest.PropertyChanged += handler2;
            objTest.PropertyChanged += handler2;// should not duplicate

            objTest.CleanProp = "test value";
            Assert.IsTrue(changeCounter == 2, "Property change notifiction should only have ");

        }

        [TestMethod]
        public void test_child_dirty_tracking()
        {
            TestClass objTest = new TestClass();

            Assert.IsFalse(objTest.IsAnythingDirty(), "Nothing in the object graph should be dirty.");

            objTest.Child.ChildName = "test value";

            Assert.IsTrue(objTest.IsAnythingDirty(), "The object graph should be dirty.");

            objTest.CleanAll();

            Assert.IsFalse(objTest.IsAnythingDirty(), "Nothing in the object graph should be dirty.");
        }

        [TestMethod]
        public void test_dirty_object_aggregating()
        {
            var objTest = new TestClass();

            List<IDirtyCapable> dirtyObjects = objTest.GetDirtyObjects();

            Assert.IsTrue(dirtyObjects.Count == 0, "There should be no dirty object returned.");

            objTest.Child.ChildName = "test value";
            dirtyObjects = objTest.GetDirtyObjects();

            Assert.IsTrue(dirtyObjects.Count == 1, "There should be one dirty object.");

            objTest.DirtyProp = "test value";
            dirtyObjects = objTest.GetDirtyObjects();

            Assert.IsTrue(dirtyObjects.Count == 2, "There should be two dirty object.");

            objTest.CleanAll();
            dirtyObjects = objTest.GetDirtyObjects();

            Assert.IsTrue(dirtyObjects.Count == 0, "There should be no dirty object returned.");
        }


        [TestMethod]
        public void test_object_validation()
        {
            var objTest = new TestClass();
            Assert.IsFalse(objTest.IsValid, "Object should not be valid as one its rules should be broken.");

            objTest.StringProp = "Some value";

            Assert.IsTrue(objTest.IsValid, "Object should be valid as its property has been fixed.");
        }
    }
}
