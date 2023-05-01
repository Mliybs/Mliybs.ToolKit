# MliybsToolKit
Mliybs的C#扩展工具包 写了一堆逆天类和逆天扩展方法

使用dotnet add package MliybsToolKit安装此包

# 扩展方法
## 整型（非负数）
### GetEnumerator()
为整型扩展了GetEnumerator方法 有了该方法后整型就可以使用foreach进行遍历 具体作用相当于产生一个0到该整型的整数集合

示例代码：
```CSharp
using System;
using Mliybs.MliybsToolKit;

namespace Namespace
{
    class Program
    {
        public static void Main(string[] args)
        {
            foreach (var item in 5) Console.WriteLine(item);
        }
    }
}
```

输出结果：0 1 2 3 4 5

## 元组
### GetEnumerator
为ValueTuple&lt;int begin, int end&gt;和ValueTuple&lt;int begin, int end, int step&gt;扩展了GetEnumerator方法 可以进行遍历 其中begin表示起始值 end表示结束值 step表示步长 指每隔几个值进行取值 不可为0 可为负

示例代码：
```CSharp
using System;
using Mliybs.MliybsToolKit;

namespace Namespace
{
    class Program
    {
        public static void Main(string[] args)
        {
            foreach (var item in (20, 5, -3)) Console.WriteLine(item);
        }
    }
}
```

输出结果：20 17 14 11 8 5

### Count()和Get()
同上 Count()和Get()可以看做是上述集合的方法 用来获得该集合的元素数量和特定位置的元素

_这个我觉得还有点用 比那个0到n都要靠专门方法来计算的强多了_

## MliybsIntObject和MliybsTupleObject

### Count属性和this[]索引器
采用特殊的方法对Count和Get方法进行了封装（nmd不是单纯加壳子 很累的）

this[]索引器支持反向索引（如^0）和范围索引（如0..^0） 同常规Range不同 自定义类型在使用索引器时结尾索引的值会包含在集合中不会排除 需注意

### ToString
现在使用MliybsIntObject和MliybsTupleObject的ToString方法会自动把其中的元素全部串联成一整个字符串了

## IEnumerable
### Print和PrintLine
简单来说就是所有集合都会实现的接口 _（MliybsToolKit使用的整型和元组没有继承该接口 所以需要转换至自定义对象）_ 为该接口引用类型扩展了Print和PrintLine方法 可以将集合快速地用Console.Write和Console.WriteLine进行输出

_（虽然是套壳子 但是我是不会道歉的）_

## LINQ兼容
上述数据类型在v1.0.6兼容了LINQ（v1.0.8修复BUG 建议使用v1.0.8以上版本） 由于数据转换受限 _（想要兼容LINQ必须实现IEnumerable&lt;T&gt;接口或者实现到IEnumerable&lt;T&gt;的隐式或显式转换 前者受限于int和ValueTuple无法实现后者受限于扩展方法无法实现）_ 所以采用了自定义类MliybsIntObject和MliybsTupleObject

如果想要使用LINQ 必须使用显式转换 _（但是定义的是隐式转换 方便后续版本开发）_ 或Transfer方法 之后才能使用LINQ

示例代码：
```CSharp
using System;
using System.Linq;
using Mliybs.MliybsToolKit;

namespace Namespace
{
    class Program
    {
        public static void Main(string[] args)
        {
            // 此处(MliybsIntObject)10外层的括号不可省略
            foreach (var item in ((MliybsIntObject)10).Where(i => i % 2 == 0)) Console.WriteLine(item);

            foreach (var item in (24, 1, -3).Transfer().Where(i => i % 2 == 0)) Console.WriteLine(item);
        }
    }
}
```

输出结果：
0 2 4 6 8 10
24 18 12 6

_（v1.0.6我愿称之为史上最强 兼容了LINQ之后可操作能力指数级增长 这波薄纱了）_

# 更新公告
## v1.0.8
引入了更新公告（不然我自己都不知道自己写了什么）

加入了Print和PrintLine方法 加入了Count属性和索引器 修复了命名BUG 修复了元组索引值可以比最大值大一的BUG（忘了倒数第一是个数减一了 我是笨比） 修复了一系列BUG

## v1.0.9
更新了Print和PrintLine方法 现在输出元素的时候前面会加上“元素类型：”了 覆写了ToString方法 修复了BUG