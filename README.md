# EasyWechatWeb

## 项目简介

EasyWechatWeb 是一个基于 ASP.NET Core 6.0 的分层 WebAPI 项目，采用 SqlSugar 作为 ORM，集成 AutoMapper、Redis、内存缓存、NPOI Excel 导入导出等常用企业级开发组件。项目结构清晰，便于扩展和维护，适合中后台管理系统或接口服务开发。

## 主要技术栈

- ASP.NET Core 6.0
- SqlSugar
- AutoMapper
- Redis
- NPOI（Excel 导入导出）
- Autofac（依赖注入）
- Swagger（API 文档）

## 项目结构


```
├── EasyWechatWeb/                // WebAPI 项目入口
│   ├── Controllers/              // 控制器
│   ├── Program.cs                // 启动配置
│   └── appsettings.json          // 配置文件
├── CommonManager/                // 通用基础组件
│   ├── Base/                     // 通用服务、仓储、接口
│   ├── Helper/                   // 工具类
│   ├── Utity/                    // AutoMapper、Autofac等配置
│   └── Cache/                    // 缓存相关
├── EasyWechatModels/             // 实体与DTO
│   ├── Entitys/                  // 数据库实体
│   └── Dto/                      // 传输对象
├── BusinessManager/              // 业务服务层
│   ├── IService/                 // 业务接口
│   └── Service/                  // 业务实现
└── BasicDataManager/             // 基础数据业务实现

```

## 主要功能

- 用户、角色、菜单、地址等基础数据的增删改查
- Session、内存缓存、Redis 缓存操作
- Excel 导入导出（NPOI）
- 文件上传
- 自动映射（AutoMapper）
- Swagger API 文档

## 典型接口示例

以 `TestController` 为例，提供了如下测试接口：

- `GET /api/Test/Test`  
  测试 Session、缓存、配置读取等功能。

- `GET /api/Test/TestExcel`  
  导出地址列表为 Excel。

- `GET /api/Test/TestExportAreas`  
  导出地址列表为 Excel（同上）。

- `GET /api/Test/TestChangeArea`  
  导出用户列表为 Excel。

- `GET /api/Test/TestImportExcel`  
  导入 Excel 文件，解析为地址数据。

- `POST /api/Test/Upload`  
  上传 Excel 文件，解析并批量导入地址数据。

## 快速开始

1. **克隆项目**
   
```shell
   git clone https://github.com/Lijiawang-01/EasyProject.git
   
```

2. **配置数据库和 Redis**
   - 修改 `EasyWechatWeb/appsettings.json`，配置数据库连接字符串和 Redis 连接信息。

3. **还原依赖并运行**
   
```shell
   dotnet restore
   dotnet run --project EasyWechatWeb
   
```

4. **访问 Swagger 文档**
   
```
   http://localhost:5000/swagger
   
```

## 依赖注入与扩展

- 所有服务、仓储、工具类均通过依赖注入注册，便于单元测试和扩展。
- 支持自定义扩展业务服务，只需实现对应接口并注册即可。

## 代码规范

- 实体、DTO、服务、控制器分层清晰，便于维护。
- 统一异常处理与日志记录（可在中间件扩展）。
- 支持异步编程模式。

## 贡献与反馈

如有建议或问题，欢迎提交 Issue 或 Pull Request。