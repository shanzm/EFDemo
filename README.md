# EFDemo
关于Entity Framework

## 001EFDemo
一个关于使用EF的简单Demo
* 建立数据库：EFTest,
        表：T_Persons（Id，Name，CreateDateTime,Age)
* NuGet: 安装EF
        PM> Install-Package EntityFramework -Version 6.4.0
* 开发模式： Database First
* 配置模式： Data Annotation

## 002FluentAPI
* 配置方式： Fluent API
* 使用默认的约定

## 003CURD
* 使用LINQ to EF,简单的CURD
* 使用原生SQL语句的CURD
* EF对象状态（EntityState）管理
* EF简单的优化技巧--AsNoTracking()


## 004FluentAPI2
* 开发模式：Code First
* 其他的一些配置方式

## 005配置一对多关系
* 新建两个表
T_Students(PK:Id，Name，Age，FK:ClassId)
T_Classes(PK:Id ,Name)

* 单向设计：通过在Student类中添加一个Class类型属性，将两张表联系起
  配置方式：在StudentConfig中:
```cs
  this.HasRequired(e=>e.Class).WithMany().HasForeignKey(e=>e.Key)`
```
* 双向设计：不经最在Student类中添加一个Class类型属性，而且在Class类中添加一个ICollection<Student>类型的对象

配置方式：在StudentConfig中：

```cs
this.HasRequired(e=>e.Class).WithMany().HasForeignKey(e=>e.Key)
```
同时在ClassConfig中:
```cs
HasMany(e => e.Students).WithRequired().HasForeignKey(e=>e.ClassId);
```

## 006配置多对多关系
* 不建表，使用EF自动生成
* 在TeacherConfig中添加多对多的配置

```cs
this.HasMany(t => t.Students).WithMany(s => s.Teachers)
    .Map(m=>m.ToTable ("T_TeachersStudentRelations")
    .MapLeftKey ("TeacherId").MapRightKey ("StudentId"));
```            
---
## EF配置总结

| 实体关系 | Has          | With          | ForeignKey                     |
| -------- | ------------ | ------------- | ------------------------------ |
| 一对一   | .HasRequired | .WithMany     | .HasForeignKey                 |
| 一对多   | .HasMany     | .WithRequired |                                |
| 多对多   | .HasMany     | .WithMany     | MapLeftKey,MapRightKey,ToTable |

## 补充：延迟加载
见：在思维导图笔记中详细说明

## 007公共父类
* 新建一EntityBase.cs作为其他实体类的父类
* 封装一个泛型类对继承于EntityBase类的子类进行CURD


## 008 EFEntities & 008MVC+EF
* 新建一类库项目008EFEntities
* 添加一个Student、Class、Nation类
* PM>Install-Package EntityFramework
* 配置好StudentConfig、ClassConfig、NationConfig
* 新建类-MyDBContext：DBContext
<hr>

*  新建008MVC+EF项目，一个MVC空模版项目
*  添加对008EFEntities的引用
*  PM>Install-Package EntityFramework
（注意两个项目中都要安装EntityFramework）
* **在Web.config中配置数据库连接字符串**
（注意连接字符串是写在启动项目的配置文件中的，
  这里008EFEntities项目是一个类库项目，启动项目是008MVC+EF）
<hr>

*  新建一个HomeController控制器，添加一个名为Index（）的Action

* 简单测试EF是否有效，是否可以连接到数据库
<hr>

* 在HomeController控制器，添加一个名为Retrieve（）的Action
* 查询Name为“三年级二班”班级和其中的学生，
* 新建一个名为HomeRetrieveModel的ViewModel，封装数据后传递到View中
<hr>

## MVC+EF+三层

008MVC+EF

008EFEntities 

008EFDAL

008EFBLL

008EFDTO

* EFEntity(即EF实体类）、DAL（调用EF）、BLL（调用DAL）、UI(即MVC，Controller调用BLL)
* 以上除UI（MVC）以外都是类库项目
* 1.在DAL层中调用EF，对数据库中的数据操作，查询返回的数据封装在DTO中
* 2.在BLL层调用DAL，数据也封装在DTO中
* 3.在UI层，MVC中的Controller调用BLL，获取数据为DTO
* 4.将DTO数据封装为ViewModel传输到MVC中的View中（此步骤省略，直接使用了DTO作为ViewModel）

## 009 UI+Service
* 假设一个关一下学校的项目
* 新建一个名为SchoolService类库项目->添加EF，并新建实体类，并配置
* 新建一个名为SchoolDTO类库项目->添加StudentDTO，ClassDTO，NationDTO
* 在SchoolService中添加一个StudentService类实现对数据库中的学生数据的操作，返回StudentDTO类型数据
* 新建一个名为SchoolWeb的MVC项目，添加EF，配置数据库字符串
* 新建一个StudentController控制器，在Index（）中调用StudentService.GetByClass()查询学生，查询数据为IEnumber<StudentDTO>类型
* 将查询的数据就按照IEnumber<StudentDTO>类型传递给View，在View中添加相应的命名空间：@using SchoolDTO，在Index.cshtml中展示数据