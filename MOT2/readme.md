# 刀具管理系统（工人端）

## 环境搭建

### 1. VS安装成功后，双击运行MOT.sln

### 2. 读卡器、扫描器依赖

1. 项目运行后会产生 debug目录
2. 将根目录下的WSR.dll和vbar.dll放到debug目录下。

### 3. Dapper和mysql.data引入

项目中使用了Dapper第三方ORM框架和数据库连接库mysql.data.

直接通过工具栏中的NuGet包管理器搜索引入即可。