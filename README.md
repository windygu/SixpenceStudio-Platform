# SixpenceStudio-Platform

## Description

该平台基于`.Net`的`ORM`二次封装后端平台，封装操作类型包括基本的 CRUD。Package 均上传至 🌍[Nuget](http://www.dumiaoxin.top:8001/)。
前端使用`Vue`基于`Element`二次封装的组件库，组件库已上传至 🌍[NPM](https://www.npmjs.com/package/sixpence.platform.pc.vue)。

## 后端

### SixpenceStudio.BaseSite

- V 1.0.0
  - 新增功能
    - 用户信息（UserInfo）
    - 用户权限（AuthUser）
    - 文件管理（SysFile）
    - 菜单管理（SysMenu）
    - 参数管理（SysParam）
    - 系统实体（SysEntity）

### SixpenceStudio.Platform

- V 1.0.0
  - 新增功能
    - 实体操作（EntityCommand）
    - 数据库对象（DbClient）
    - 实体（Entity）
    - 日志（Logging）
    - 实体服务（Service）
    - 帮助类（Utils）
    - API（WebApi）

## 前端

### Sixpence.platform.pc.vue

- V1.0.0
  - 组件库
    - 按钮组（spButtonList）
    - 头部（spHeader）
    - icon（spIcon）
    - 列表（spList）
    - 菜单（spMenu）
    - 区域（spSection）
    - 表格（spTable）
  - 混入
    - edit
    - pagination
  - 帮助类
    - axios
    - common
    - http
    - uuid
