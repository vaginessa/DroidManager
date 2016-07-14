﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharpAdbClient.Tests
{
    internal static class ExceptionTester<T>
        where T : Exception
    {
        public static void TestEmptyConstructor(Func<T> constructor)
        {
            var ex = constructor();
        }

        public static void TestMessageConstructor(Func<string, T> constructor)
        {
            var message = "Hello, World";
            var ex = constructor(message);

            Assert.AreEqual(message, ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        public static void TestMessageAndInnerConstructor(Func<string, Exception, T> constructor)
        {
            var message = "Hello, World";
            var inner = new Exception();
            var ex = constructor(message, inner);

            Assert.AreEqual(message, ex.Message);
            Assert.AreEqual(inner, ex.InnerException);
        }

        public static void TestSerializationConstructor(Func<SerializationInfo, StreamingContext, T> constructor)
        {
            var info = new SerializationInfo(typeof(T), new FormatterConverter());
            var context = new StreamingContext();

            info.AddValue("ClassName", string.Empty);
            info.AddValue("Message", string.Empty);
            info.AddValue("InnerException", new ArgumentException());
            info.AddValue("HelpURL", string.Empty);
            info.AddValue("StackTraceString", string.Empty);
            info.AddValue("RemoteStackTraceString", string.Empty);
            info.AddValue("RemoteStackIndex", 0);
            info.AddValue("ExceptionMethod", string.Empty);
            info.AddValue("HResult", 1);
            info.AddValue("Source", string.Empty);

            var ex = constructor(info, context);
        }
    }
}
