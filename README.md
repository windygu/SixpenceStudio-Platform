# SixpenceStudio+Platform

## Description

该平台基于`.Net`的`ORM`二次封装后端平台，封装操作类型包括基本的 CRUD。Package 均上传至 🌍[Nuget](http://nuget.karldu.cn/)。
前端使用`Vue`基于`Ant design`二次封装的组件库，组件库已上传至 🌍[NPM](https://www.npmjs.com/package/sixpence.platform.pc.vue)。

## Backend

### SixpenceStudio.Encryption

数据库加密解密工具

### SixpenceStudio.BaseSite

- 用户信息（UserInfo）
- 用户权限（AuthUser）
- 文件管理（SysFile）
- 菜单管理（SysMenu）
- 参数管理（SysParam）
- 系统实体（SysEntity）
- 作业管理（Job）
- 系统配置（SysConfig）

### SixpenceStudio.Platform

- 实体操作（EntityCommand）
- 数据库对象（DbClient）
- 实体（Entity）
- 日志（Logging）
- 实体服务（Service）
- 帮助类（Utils）
- API（WebApi）
- 存储服务（Store）
- 作业（Job）
- Configs（系统配置）

## Frontend

### Sixpence.platform.pc.vue

- Components
  - 按钮组（spButtonList）
  - 头部（spHeader）
  - icon（spIcon）
  - 列表（spList）
  - 菜单（spMenu）
  - 区域（spSection）
  - 表格（spTable）
  - 标签（spTag）
- Website modules
  - 404
  - 系统管理
    - 修改密码
    - 作业管理
    - 系统配置
    - 实体管理
    - 系统菜单
    - 选项集
    - 用户信息
- Mixins
  - edit
  - pagination
- Utils
  - axios
  - common
  - http
  - uuid
  - encryption
