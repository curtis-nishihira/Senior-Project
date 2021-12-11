using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.DAL
{
    public interface IRepository<T>
    {
        string Create(T model);
        List<string> Read(T model);
        bool Update(T model);
        bool Delete(T model);
    }
}
