# mapbaidu
通过访问百度地图api定位本机详细位置，本机ip高精度定位

想知道目标服务器（PC）具体地址？使用这个小工具，在目标机器上运行，获取高精度定位信息

使用 VS2013 C# 开发，源码结构如下

    C:.
    │  mapbaidu.sln
    │  说明.txt
    │
    └─mapbaidu
        │  App.config
        │  mapbaidu.csproj
        │  mapbaidu.csproj.user
        │  Program.cs                   //  C#源代码
        │
        ├─bin
        │  └─Release
        │          mapbaidu-4.5.exe     //  已经编译好的.net4.5版本
        │          mapbaidu-net2.0.exe  //  已经编译号的.net2.0版本
        │
        ├─obj
        └─Properties
                AssemblyInfo.cs
            
运行exe后会先输出本机IP列表，再返回unicode编码的json字符串，需要unicode转中文
