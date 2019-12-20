using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005配置一对多关系
{
    public class Student
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public long ClassId { get; set; }  //注意数据库的T_Students表中是有ClassId这一个字段的
        //我们新建一个Student类型的对象的时候，我们给Class类型的属性赋值，则EF自动帮我们提取该Class类型的对象的Id，填充到数据库T_Students表中的ClassId中
        //看上去好像没有必要定义这个ClassId属性，只要下面的Class属性即可，但是事实是不可以的，这个属性必须有，因为EF生成的SQL语句可能需要

        public virtual Class Class { get; set; }
        //通过Student类中添加这样一个Class类型的属性就将Student和Class类联系起来了，称之为对象之间的关联属性，又称之为导航属性
        //注意这个属性是virtual的，防止报NullReferenceException

        //同样我们可以在Class类中添加一个Student集合属性，具体见Class.cs,注意其实是没必要的
        //两个类中都有对方类型的属性，称之为"双向关系"

        //若是一个表中有两个指向同一张表的外键（注意是指向同一张表），具体实现是一样的
        //如T_Students表中还有一列是InterestClass（允许为空）,指的是学校的兴趣班，这个兴趣班也是放在T_Class中的
        //则我们在Student类中添加
        //public long InterestClassId{get;set}
        //public virtual Class InterestClassI{get;set}

        //在StudentConfig.cs中配置：this. HasOptional (s => s.XZClass).WithMany().HasForeignKey(s => s.XZClassId);
        //注意有两个指向同一张表的外键，我们必须自己写上面的配置代码，不能使用默认的约定
    }
}