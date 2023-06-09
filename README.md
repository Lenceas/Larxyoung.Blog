# Larxyoung.Blog —— 个人博客 服务端接口 .NET API
[![sdk](https://img.shields.io/badge/sdk-7.0.4-d.svg)](#)  
-------------------------------
`Larxyoung.Blog.Application`：业务应用层（业务代码主要编写层）  
`Larxyoung.Blog.Core`：核心层（实体，仓储，其他核心代码）  
`Larxyoung.Blog.Web.Core`：Web 核心层（存放 Web 公共代码，如 过滤器、中间件、Web Helpers 等）  
`Larxyoung.Blog.Web.Entry`：Web 入口层/启动层  

#### 框架模块：  
- [x] 采用`Furion`后台基础框架；
- [x] 采用`仓储+服务+接口`的形式封装框架；
- [x] 异步`async/await`开发；
- [x] 接入国产数据库ORM组件 ——`SqlSugar`，封装数据库操作，支持级联操作；
- [x] 实现项目启动，自动生成种子数据`CodeFirst` ✨； 

组件模块：
- [x] 使用`Swagger`做api文档；
- [x] 使用`MiniProfiler`做接口性能分析 ✨；
- [x] 使用`Mapster`处理对象映射；  
- [x] 支持`CORS`跨域；
- [x] 封装`JWT`自定义策略授权；
- [x] 提供`MemoryCache`做缓存处理；
- [x] 提供`Redis`做缓存处理；
- [x] 添加`IpRateLimiting`做API限流处理;
- [x] 添加`雪花算法ID`工具类;
- [x] 添加`验证码生成器`工具类;

微服务模块：
- [x] 可配合`Docker`实现容器化；
