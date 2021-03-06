﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using static InjectionScript.Tests.TestHelpers;

namespace InjectionScript.Tests.Interpretation
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public void Sum()
        {
            TestExpression("'asdf'+'qwer'", "asdfqwer");
            TestExpression("'asdf'+'qwer'+'zxcv'", "asdfqwerzxcv");
            TestExpression("''+''", "");
        }

        [TestMethod]
        public void Str()
        {
            TestExpression("str(1)", "1");
            TestExpression("str(0x1abc)", "6844");
            TestExpression("str(4.99)", "4.99");
            TestExpression("str('')", "0");
            TestExpression("str('str')", "0");
        }

        [TestMethod]
        public void Comparison()
        {
            TestExpression("'asdf' == 'qwer'", 0);
            TestExpression("'asdf' == 'asdf'", 1);
            TestExpression("'ASDF' == 'asdf'", 0);
            TestExpression("'asdf' <> 'qwer'", 1);
            TestExpression("'asdf' <> 'asdf'", 0);
            TestExpression("'ASDF' <> 'asdf'", 1);
        }

        [TestMethod]
        public void Val()
        {
            TestExpression("val('123')", 123);
            TestExpression("val('4.99')", 4.99);
            TestExpression("val('asdf')", 0);
            TestExpression("val('abc')", 0);
            TestExpression("val('0x123')", 0);
        }

        [TestMethod]
        public void Len()
        {
            TestExpression("len('asdf')", 4);
            TestExpression("len('')", 0);
            TestExpression("len(123)", 0);
            TestExpression("len(4.99)", 0);
        }

        [TestMethod]
        public void Left()
        {
            TestExpression("left('abcdefgh', 4)", "abcd");
            TestExpression("left('abcdefgh', 0)", "");
            TestExpression("left('abcd', 6)", "abcd");
        }

        [TestMethod]
        public void Right()
        {
            TestExpression("right('abcdefgh', 4)", "efgh");
            TestExpression("right('abcdefgh', 0)", "");
            TestExpression("right('abcd', 6)", "abcd");
        }

        [TestMethod]
        public void Mid()
        {
            //TestExpression("mid('abcdefgh', 2, 4)", "cdef");
            //TestExpression("mid('abcdefgh', 2, 0)", "");
            //TestExpression("mid('abcdefgh', 0, 4)", "abcd");
            //TestExpression("Mid('abcd', 2, 6)", "cd");
            TestExpression("Mid('abcd', 6, 6)", "");
        }
    }
}
