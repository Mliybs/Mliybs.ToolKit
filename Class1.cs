using System;
using System.Collections;
using System.Collections.Generic;

namespace Mliybs
{
    namespace MliybsToolKit
    {
        /// <summary>
        /// MliybsToolKit的扩展方法类
        /// </summary>
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
                    for (int i = 0; i <= num; i++)
                        yield return i;
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
                    for (int i = tuple.begin; i <= tuple.end; i++)
                        yield return i;
            }

            /// <summary>
            /// 获取该整数集合的元素数量
            /// </summary>
            /// <param name="tuple"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            public static int Count(this (int begin, int end) tuple)
            {
                if (tuple.begin > tuple.end)
                    throw new MliybsEnumeratorBeginIsBiggerException();

                else
                {
                    int i = tuple.begin;

                    while (i <= tuple.end)
                        i++;

                    i -= tuple.begin;

                    return i;
                }
            }

            /// <summary>
            /// 获取该集合特定位置的元素
            /// </summary>
            /// <param name="tuple"></param>
            /// <param name="index"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            /// <exception cref="MliybsEnumeratorIndexOutOfRangeException"></exception>
            public static int Get(this (int begin, int end) tuple, int index)
            {
                if (tuple.begin > tuple.end)
                    throw new MliybsEnumeratorBeginIsBiggerException();

                else
                {
                    int i = tuple.begin;

                    while (i <= tuple.end)
                        i++;

                    if (i < 0 || i > tuple.end)
                        throw new MliybsEnumeratorIndexOutOfRangeException();

                    else
                        return i;
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
            
            /// <summary>
            /// 获取该整数集合的元素数量
            /// </summary>
            /// <param name="tuple"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorStepIsZeroException"></exception>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            /// <exception cref="MliybsEnumeratorEndIsBiggerException"></exception>
            public static int Count(this (int begin, int end, int step) tuple)
            {
                if (tuple.step == 0)
                    throw new MliybsEnumeratorStepIsZeroException();

                else if (tuple.step > 0)
                {
                    if (tuple.begin > tuple.end)
                        throw new MliybsEnumeratorBeginIsBiggerException();

                    int i = tuple.begin;

                    int length = 0;

                    while (i <= tuple.end)
                    {
                        i += tuple.step;

                        length++;
                    }

                    return length;
                }

                else
                {
                    if (tuple.begin < tuple.step)
                        throw new MliybsEnumeratorEndIsBiggerException();

                    int i = tuple.begin;

                    int length = 0;

                    while (i >= tuple.end)
                    {
                        i += tuple.step;

                        length++;
                    }

                    return length;
                }
            }

            /// <summary>
            /// 获取该集合特定位置的元素
            /// </summary>
            /// <param name="tuple"></param>
            /// <param name="index"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorStepIsZeroException"></exception>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            /// <exception cref="MliybsEnumeratorIndexOutOfRangeException"></exception>
            /// <exception cref="MliybsEnumeratorEndIsBiggerException"></exception>
            public static int Get(this (int begin, int end, int step) tuple, int index)
            {
                if (tuple.step == 0)
                    throw new MliybsEnumeratorStepIsZeroException();

                else if (tuple.step > 0)
                {
                    if (tuple.begin > tuple.end)
                        throw new MliybsEnumeratorBeginIsBiggerException();

                    int i = tuple.begin;

                    int length = 0;

                    while (i <= tuple.end)
                    {
                        i += tuple.step;

                        length++;
                    }

                    if (index < 0 || index > length)
                        throw new MliybsEnumeratorIndexOutOfRangeException();

                    else
                        return tuple.begin += tuple.step * index;
                }

                else
                {
                    if (tuple.begin < tuple.step)
                        throw new MliybsEnumeratorEndIsBiggerException();

                    int i = tuple.begin;

                    int length = 0;

                    while (i >= tuple.end)
                    {
                        i += tuple.step;

                        length++;
                    }

                    if (index < 0 || index > length)
                        throw new MliybsEnumeratorIndexOutOfRangeException();

                    else
                        return tuple.begin += tuple.step * index;
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

        internal class MliybsEnumeratorIndexOutOfRangeException : ApplicationException
        {
            internal MliybsEnumeratorIndexOutOfRangeException() : base("索引超出集合范围！") {}
        }
    }
}