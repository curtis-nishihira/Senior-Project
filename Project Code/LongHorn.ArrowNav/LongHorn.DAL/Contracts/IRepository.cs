using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.DAL
{
    public interface IRepository<T>
    {
        bool Create(T model);
        List<T> Read(T model);
        bool Update(T model);
        bool Delete(T model);

    }
}
