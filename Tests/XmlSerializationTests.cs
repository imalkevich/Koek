﻿using Koek;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class XmlSerializationTests : BaseTestClass
    {
        public class MyClass
        {
            public string? MyProperty { get; set; }
        }

        [TestMethod]
        public void BasicSeriAndDeseriSeemsToWork()
        {
            #region Doc example: basic seri/deseri
            MyClass input = new MyClass
            {
                MyProperty = "MyValue"
            };

            string xml = Helpers.XmlSerialization.XmlSerialize(input);

            MyClass? result = Helpers.XmlSerialization.XmlDeserialize<MyClass>(xml);

            // If you do not know the type beforehand, you can provide a Type parameter.
            Type dataType = typeof(MyClass); // Pretend this comes from configuration or something.
            object? alternativeResult = Helpers.XmlSerialization.XmlDeserialize(xml, dataType);
            #endregion

            Assert.IsNotNull(result);
            Assert.AreEqual(input.MyProperty, result!.MyProperty);

            Assert.IsNotNull(alternativeResult);
            Assert.IsInstanceOfType(alternativeResult, typeof(MyClass));
            Assert.AreEqual(input.MyProperty, ((MyClass)alternativeResult!).MyProperty);
        }

        [TestMethod]
        public void OutputIsUtf8()
        {
            string xml = Helpers.XmlSerialization.XmlSerialize(new MyClass());

            Assert.IsTrue(xml.Contains(@"encoding=""utf-8"""));
        }
    }
}