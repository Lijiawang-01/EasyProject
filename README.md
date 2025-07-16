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

---


## PCWeb

PCWeb 是基于 Vue 3 + TypeScript + Vite 的企业级后台管理系统前端项目，集成了丰富的组件和工具，适合中大型管理后台开发。

### 主要技术栈

- **Vue 3**：渐进式前端框架
- **TypeScript**：类型安全的 JavaScript 超集
- **Vite**：极速前端构建工具
- **Element Plus**：UI 组件库
- **Pinia**：新一代状态管理
- **Vue Router**：前端路由
- **Axios**：HTTP 请求库
- **ECharts**：数据可视化
- **WangEditor**：富文本编辑器
- **@vue-office**：Office 文档预览
- **Day.js**：日期处理
- **SignalR**：实时通信
- **Prettier / ESLint / Stylelint**：代码风格与质量保障
- **Sass / Less**：CSS 预处理
- **Vite Plugin Compression**：打包压缩

### 常用命令


```shell
# 本地开发
npm run dev

# 生产构建
npm run build

# 预览构建产物
npm run preview

# 代码格式化
npm run format

# 代码检查
npm run lint:eslint
npm run lint:style

```

---

## MobileWeb

MobileWeb 是基于 Vue 3 + TypeScript + Vite + Vant 的移动端项目，适合移动端业务开发，支持多语言、状态持久化等特性。

### 主要技术栈

- **Vue 3**：渐进式前端框架
- **TypeScript**：类型安全的 JavaScript 超集
- **Vite**：极速前端构建工具
- **Vant 4**：移动端组件库
- **Pinia**：新一代状态管理
- **Vue Router**：前端路由
- **Axios**：HTTP 请求库
- **Day.js**：日期处理
- **SignalR**：实时通信
- **reset-css**：样式重置
- **PostCSS px-to-viewport**：移动端适配
- **Prettier / ESLint / Stylelint**：代码风格与质量保障
- **Sass / Less**：CSS 预处理

### 常用命令


```shell
# 本地开发
npm run dev

# 生产构建
npm run build

# 预览构建产物
npm run preview

```
---
# WechatWeb 小程序前端

## 项目简介

WechatWeb 是基于微信小程序生态开发的鲜花电商项目，适用于微信小程序平台。项目集成了 Vant Weapp 组件库、mobx 状态管理等主流技术，支持 npm 构建和模块化开发。

## 主要技术栈

- **微信小程序原生开发**
- **Vant Weapp**：高质量小程序 UI 组件库
- **mobx-miniprogram**：响应式状态管理
- **mobx-miniprogram-bindings**：mobx 与小程序数据自动绑定
- **npm 构建**：支持模块化依赖管理

## 快速开始

1. **安装依赖**

   
```
   npm install
   
```

2. **微信开发者工具构建 npm**

   - 打开微信开发者工具，导入项目根目录
   - 执行 `工具 -> 构建 npm`

3. **配置 appid**

   - 打开 `project.config.json`，将 `appid` 替换为你自己的小程序 appid

4. **配置服务器白名单**

   - 在微信小程序后台 `开发/开发管理/开发设置/服务器域名`，添加后端接口域名

## 目录结构


```
WechatWeb/
├── miniprogram/         # 小程序主目录
├── package.json         # npm 依赖配置
├── project.config.json  # 小程序项目配置
├── README.md            # 项目说明
└── ...                  # 其他文件

```

## 常用命令


```
# 安装依赖
npm install

# 构建 npm 包（在微信开发者工具中操作）
# 工具 -> 构建 npm

```

## 相关文档

- [微信小程序官方文档](https://developers.weixin.qq.com/miniprogram/dev/framework/)
- [Vant Weapp 文档](https://vant-contrib.gitee.io/vant-weapp/#/home)
- [mobx-miniprogram 文档](https://github.com/wuba/mobx-miniprogram)

---
## 开源协议

本项目基于 MIT 协议开源，欢迎学习和参与贡献。
## 贡献与反馈

如有建议或问题，欢迎提交 Issue 或 Pull Request。
以下是根据 PCWeb 和 MobileWeb 下的 package.json 自动生成的 README.md，分别介绍了两端的技术栈和启动方式：