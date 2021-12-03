using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.DAL
{
    public interface IRepository<T>
    {
        bool Create(T model);
        bool Read();
        bool Update(T model);
        bool Delete(T model);


    }
}
