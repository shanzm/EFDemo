using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _007公共父类
{
    class CommonCURD<T> where T : EntityBase
        //泛型类，使用where约束泛型，指定泛型继续于BaseEntity类，换言之我们的这个类的操作仅针对BaseEntity类的子类对象
    {
        private MyDbContext cxt;//注意我们不是在下面的那些增删改查函数中new MyDBContext（）对象，而是在这个类外new MyDBContext（）再通过构造函数传入类中，防止数据库连接失效
        public CommonCURD(MyDbContext cxt)
        {
            this.cxt = cxt;
        }

        /// <summary>
        /// 根据Id删除数据（软删除）
        /// </summary>
        /// <param name="id"></param>
        public void MarkDeleted(long id)
        {
            //cxt.Persons书写方式等价于cxt.Set<Person>()
            T item = cxt.Set<T>().Where(e => e.Id == id).SingleOrDefault();
            if (item == null)
            {
                throw new ArgumentException($"数据库中没有找到Id={id}的数据 ");
            }
            item.IsDeleted = true;
            item.DeleteDateTime = DateTime.Now;

            cxt.SaveChanges();//注意对数据库的修改是要保存的
        }

        /// <summary>
        /// 根据Id查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            try
            {
                // T item = cxt.Set<T>().Where(e => e.Id == id && e.IsDeleted != true).SingleOrDefault();
                T item = cxt.Set<T>().SingleOrDefault(e => e.Id == id && e.IsDeleted != true);
                return item;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IQueryable<T> GetAll(int start, int count)
        {
            return cxt.Set<T>().OrderBy(e => e.CreateDateTime).Skip(start).Take(count).Where(e => e.IsDeleted == false);
        }
        ///注意这里的返回值类型是IQueryable<T>类型，他继承与IEnumerable<T>类型，但是我们不要使用IEnumberable类型
        ///为什么呢，假如我们的查询的数据的类中有ICollection<T>类型的属性，我们需要使用Include（）函数避免延迟加载
        ///注意我们这里的查询结果是IQueryable<T>类型的对象，他的Include（）函数的命名空间是：System.Data.Entity




        /// <summary>
        /// 查询数据库的数据总量
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()//因为我们的Id类型就是long，所以我们的一张表中的数据理论上最多有long类型取值范围的条数
        {
            return cxt.Set<T>().Where(e => e.IsDeleted == false).LongCount();
        }

    }
}