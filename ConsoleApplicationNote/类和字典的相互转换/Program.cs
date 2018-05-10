using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ConsoleApplicationNote
{
    class Program
    {
       //注意：如果在主函数入口调用同一类下的方法，需要该方法为static
        static void Main(string[] args)
        {

            Student stu = new Student
          {
              Id = "1111",
              Name = "test",
              Age = 18

          };
            Oper oper = new Oper();
            var result = oper.RetrieveProperties(stu);
            Student result2 = oper.DicToObject<Student>(result);
            Console.ReadLine();
        }


    }
    public class Student
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

    }

    public class Oper
    {
        /// <summary>
        /// 将对象转换成字典 using System.Reflection;
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>Instance of Dictionary{string, object}.</returns>
        public Dictionary<string, object> RetrieveProperties(object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                if (property.CanRead)
                {
                    result.Add(property.Name, property.GetValue(source, null));
                }
            }

            return result;
        }

        // <summary>  
        /// 字典类型转化为对象  
        /// </summary>  
        /// <param name="dic"></param>  
        /// <returns></returns>  
        public T DicToObject<T>(Dictionary<string, object> dic) where T : new()
        {
            var md = new T();
            foreach (var d in dic)
            {
                var filed = d.Key;
                try
                {
                    var value = d.Value;
                    md.GetType().GetProperty(filed).SetValue(md, value);
                }
                catch (Exception e)
                {
                    throw new Exception("字典转换成对象出错", e.InnerException);
                }
            }
            return md;
        }

    }
}
