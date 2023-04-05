using System;
using System.Collections;
using System.Collections.Generic;

namespace Mliybs
{
    namespace MliybsToolKit
    {
        public static class ExtensionMethods
        {
            /// <summary>
            /// 产生一个0到所给整数（非负数）的整数集合
            /// </summary>
            /// <param name="num"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            public static IEnumerator GetEnumerator(this int num)
            {
                if (num <= 0)
                    throw new MliybsEnumeratorBeginIsBiggerException();

                else
                {
                    for (int i = 0; i <= num; i++)
                        yield return i;
                }
            }

            /// <summary>
            /// 产生一个所给整数1到所给整数2的整数集合 1必须小于2
            /// </summary>
            /// <param name="tuple"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            public static IEnumerator GetEnumerator(this (int begin, int end) tuple)
            {
                if (tuple.begin > tuple.end)
                    throw new MliybsEnumeratorBeginIsBiggerException();

                else
                {
                    for (int i = tuple.begin; i <= tuple.end; i++)
                        yield return i;
                }
            }

            /// <summary>
            /// <para>产生一个整数集合 三个整数分别为起始值 结束值与步长</para>
            /// <para>步长决定每隔几个值进行取值 不可为0 步长可为负</para>
            /// </summary>
            /// <param name="tuple"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorStepIsZeroException"></exception>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            /// <exception cref="MliybsEnumeratorEndIsBiggerException"></exception>
            public static IEnumerator GetEnumerator(this (int begin, int end, int step) tuple)
            {
                if (tuple.step == 0)
                    throw new MliybsEnumeratorStepIsZeroException();

                else if (tuple.step > 0)
                {
                    if (tuple.begin > tuple.end)
                        throw new MliybsEnumeratorBeginIsBiggerException();

                    for (int i = tuple.begin; i <= tuple.end; i += tuple.step)
                        yield return i;
                }

                else
                {
                    if (tuple.begin < tuple.step)
                        throw new MliybsEnumeratorEndIsBiggerException();

                    for (int i = tuple.begin; i >= tuple.end; i += tuple.step)
                        yield return i;
                }
            }
        }

        internal class MliybsEnumeratorBeginIsBiggerException : ApplicationException
        {
            internal MliybsEnumeratorBeginIsBiggerException() : base("起始值比结束值更大！") {}
        }

        internal class MliybsEnumeratorEndIsBiggerException : ApplicationException
        {
            internal MliybsEnumeratorEndIsBiggerException() : base("起始值比结束值更小！") {}
        }

        internal class MliybsEnumeratorStepIsZeroException : ApplicationException
        {
            internal MliybsEnumeratorStepIsZeroException() : base("步长不可为零！") {}
        }
    }
}