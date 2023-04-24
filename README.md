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

## Linq兼容
上述数据类型在v1.0.6兼容了Linq 由于数据转换受限 _（想要兼容Linq必须实现IEnumerable&lt;T&gt;接口或者实现到IEnumerable&lt;T&gt;的隐式或显式转换 前者受限于int和ValueTuple无法实现后者受限于扩展方法无法实现）_ 所以采用了自定义类IntMliybsObject和TupleMliybsObject

_（一开始用的是MliybsIntObject和MliybsTupleObject 后来才发现改过来之后字好打多了）_

如果想要使用Linq 必须使用显式转换 _（但是定义的是隐式转换 方便后续版本开发）_ 之后才能使用Linq

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
            // 此处(IntMliybsObject)10外层的括号不可省略 下一条语句同理
            foreach (var item in ((IntMliybsObject)10).Where(i => i % 2 == 0)) Console.WriteLine(item);

            foreach (var item in ((TupleMliybsObject)(24, 1, -3)).Where(i => i % 2 == 0)) Console.WriteLine(item);
        }
    }
}
```

输出结果：
0 2 4 6 8 10
24 18 12 6

_（v1.0.6我愿称之为史上最强 兼容了Linq之后可操作能力指数级增长 这波薄纱了）_