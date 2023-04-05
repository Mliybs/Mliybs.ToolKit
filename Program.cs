using System;
using System.Collections;
using System.Collections.Generic;

namespace Mliybs
{
    namespace MliybsToolKit
    {
        public static class ExtensionMethods
        {
            public static IEnumerator GetEnumerator(this int num)
            {
                if (num < 0) throw new MliybsEnumeratorBeginIsBiggerException();

                else
                {
                    for (int i = 0; i <= num; i++) yield return i;
                }
            }

            public static IEnumerator GetEnumerator(this (int, int) tuple)
            {
                if (tuple.Item1 > tuple.Item2) throw new MliybsEnumeratorBeginIsBiggerException();

                else
                {
                    for (int i = tuple.Item1; i <= tuple.Item2; i++) yield return i;
                }
            }
        }

        internal class MliybsEnumeratorBeginIsBiggerException : ApplicationException
        {
            internal MliybsEnumeratorBeginIsBiggerException() : base("起始值比结束值更大！") { }
        }
    }
}