using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Mliybs
{
    namespace MliybsToolKit
    {
        /// <summary>
        /// 用于衔接Linq的整型类
        /// </summary>
        public class MliybsIntObject : IEnumerable<int>
        {
            int _object;

            /// <summary>
            /// 传入可处理的整型变量并实例化
            /// </summary>
            /// <param name="self"></param>
            public MliybsIntObject(int self) => _object = self;

            /// <summary>
            /// 实现GetEnumerator
            /// </summary>
            /// <returns></returns>
            public IEnumerator<int> GetEnumerator() => _object.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => _object.GetEnumerator();

            /// <summary>
            /// 实现隐式转换
            /// </summary>
            /// <param name="self"></param>
            public static implicit operator MliybsIntObject(int self) => new(self);
        }
        
        /// <summary>
        /// 用于衔接Linq的元组类
        /// </summary>
        public class MliybsTupleObject : IEnumerable<int>
        {
            (int begin, int end, int step) _object;
            
            /// <summary>
            /// 传入可处理的元组对象并实例化
            /// </summary>
            /// <param name="self"></param>
            public MliybsTupleObject((int, int) self)
            {
                var (begin, end) = self;

                _object = (begin, end, 1);
            }
            
            /// <summary>
            /// 传入可处理的元组对象并实例化
            /// </summary>
            /// <param name="self"></param>
            public MliybsTupleObject((int, int, int) self) => _object = self;

            /// <summary>
            /// 实现GetEnumerator
            /// </summary>
            /// <returns></returns>
            public IEnumerator<int> GetEnumerator() => _object.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => _object.GetEnumerator();

            /// <summary>
            /// 自定义索引器
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public int this[Index index]
            {
                get
                {
                    if (index.IsFromEnd)
                        return _object.Get(_object.Count() - index.Value - 1);

                    else
                        return _object.Get(index.Value);
                }
            }

            /// <summary>
            /// 自定义索引器 此处range的用法用.NET常规用法不同 索引末尾值是包含在返回集合里的
            /// </summary>
            /// <param name="range"></param>
            /// <returns></returns>
            public IEnumerable<int> this[Range range]
            {
                get
                {
                    if (range.Start.Value < 0 || range.End.Value < 0)
                        throw new MliybsEnumeratorIndexOutOfRangeException();

                    var start = range.Start.IsFromEnd ? _object.Count() - range.Start.Value - 1 : range.Start.Value;
                    
                    var end = range.End.IsFromEnd ? _object.Count() - range.End.Value - 1 : range.End.Value;

                    if (start > end)
                        throw new MliybsEnumeratorBeginIsBiggerException();

                    for (int i = start; i <= end; i++)
                        yield return _object.Get(i);
                }
            }

            /// <summary>
            /// 自定义Count属性
            /// </summary>
            /// <returns></returns>
            public int Count
            {
                get
                {
                    if (_object.step == 1)
                        return (_object.begin, _object.end).Count();

                    else
                        return _object.Count();
                }
            }

            /// <summary>
            /// 实现隐式转换
            /// </summary>
            /// <param name="self"></param>
            public static implicit operator MliybsTupleObject((int, int) self) => new(self);

            /// <summary>
            /// 实现隐式转换
            /// </summary>
            /// <param name="self"></param>
            public static implicit operator MliybsTupleObject((int, int, int) self) => new(self);
        }

        /// <summary>
        /// MliybsToolKit的扩展方法类
        /// </summary>
        public static class StaticExtensionMethods
        {
            /// <summary>
            /// 遍历集合并使用Console.Write方法输出
            /// </summary>
            /// <param name="self"></param>
            public static void Print(this IEnumerable self)
            {
                foreach (var item in self)
                    Console.Write(item);
            }

            /// <summary>
            /// 遍历集合并使用Console.WriteLine方法输出
            /// </summary>
            /// <param name="self"></param>
            public static void PrintLine(this IEnumerable self)
            {
                foreach (var item in self)
                    Console.WriteLine(item);
            }

            /// <summary>
            /// 产生一个0到所给整数（非负数）的整数集合
            /// </summary>
            /// <param name="num"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            public static IEnumerator<int> GetEnumerator(this int num)
            {
                if (num <= 0)
                    throw new MliybsEnumeratorBeginIsBiggerException();

                else
                    for (int i = 0; i <= num; i++)
                        yield return i;
            }

            /// <summary>
            /// 返回一个可处理对象
            /// </summary>
            /// <param name="num"></param>
            /// <returns></returns>
            public static MliybsIntObject Transfer(this int num) => new MliybsIntObject(num);

            /// <summary>
            /// 产生一个所给整数1到所给整数2的整数集合 1必须小于2
            /// </summary>
            /// <param name="tuple"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            public static IEnumerator<int> GetEnumerator(this (int begin, int end) tuple)
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

                    if (index < 0 || index >= i - tuple.begin)
                        throw new MliybsEnumeratorIndexOutOfRangeException();

                    else
                        return tuple.begin + index;
                }
            }

            /// <summary>
            /// 返回一个可处理对象
            /// </summary>
            /// <param name="tuple"></param>
            /// <returns></returns>
            public static MliybsTupleObject Transfer(this (int, int) tuple) => new MliybsTupleObject(tuple);

            /// <summary>
            /// <para>产生一个整数集合 三个整数分别为起始值 结束值与步长</para>
            /// <para>步长决定每隔几个值进行取值 不可为0 步长可为负</para>
            /// </summary>
            /// <param name="tuple"></param>
            /// <returns></returns>
            /// <exception cref="MliybsEnumeratorStepIsZeroException"></exception>
            /// <exception cref="MliybsEnumeratorBeginIsBiggerException"></exception>
            /// <exception cref="MliybsEnumeratorEndIsBiggerException"></exception>
            public static IEnumerator<int> GetEnumerator(this (int begin, int end, int step) tuple)
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

                    int length = 0;

                    for (int i = tuple.begin; i <= tuple.end; i += tuple.step)
                        length++;

                    return length;
                }

                else
                {
                    if (tuple.begin < tuple.step)
                        throw new MliybsEnumeratorEndIsBiggerException();

                    int length = 0;

                    for (int i = tuple.begin; i >= tuple.end; i += tuple.step)
                        length++;

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

                    int length = 0;

                    for (int i = tuple.begin; i <= tuple.end; i += tuple.step)
                        length++;

                    if (index < 0 || index >= length)
                        throw new MliybsEnumeratorIndexOutOfRangeException();

                    else
                        return tuple.begin += tuple.step * index;
                }

                else
                {
                    if (tuple.begin < tuple.step)
                        throw new MliybsEnumeratorEndIsBiggerException();

                    int length = 0;

                    for (int i = tuple.begin; i >= tuple.end; i += tuple.step)
                        length++;

                    if (index < 0 || index >= length)
                        throw new MliybsEnumeratorIndexOutOfRangeException();

                    else
                        return tuple.begin += tuple.step * index;
                }
            }

            /// <summary>
            /// 返回一个可处理对象
            /// </summary>
            /// <param name="tuple"></param>
            /// <returns></returns>
            public static MliybsTupleObject Transfer(this (int, int, int) tuple) => new MliybsTupleObject(tuple);
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