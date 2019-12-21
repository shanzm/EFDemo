using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _007公共父类
{
    class CommonCURD<T> where T : EntityBase//泛型类，指定泛型继续于BaseEntity类，换言之我们的这个类的操作仅针对BaseEntity类的子类对象
    {
        private MyDbContext cxt;
        public CommonCURD(MyDbContext cxt)
        {
            this.cxt = cxt;
        }

        public CommonCURD()
        {
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