using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.DAL
{
    public interface IRepository<T>
    {
        string Create(T model);
        List<string> Read(T model);
        string Update(T model);
        string Delete(T model);
    }
}
