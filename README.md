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
  配置方式：在StudentConfig中`this.HasRequired(e=>e.Class).WithMany().HasForeignKey(e=>e.Key)`

* 双向设计：不经最在Student类中添加一个Class类型属性，而且在Class类中添加一个ICollection<Student>类型的对象
配置方式：在StudentConfig中`this.HasRequired(e=>e.Class).WithMany().HasForeignKey(e=>e.Key)`
同时在ClassConfig中`HasMany(e => e.Students).WithRequired().HasForeignKey(e=>e.ClassId);`

## 006配置多对多关系
* 不建表，使用EF自动生成
* 在TeacherConfig中添加多对多的配置

```cs
this.HasMany(t => t.Students).WithMany(s => s.Teachers)
    .Map(m=>m.ToTable ("T_TeachersStudentRelations")
    .MapLeftKey ("TeacherId").MapRightKey ("StudentId"));
```            
