using System;
using System.Collections.Generic;
using System.Text;
using LongHorn.ArrowNav.Models;

namespace LongHorn.ArrowNav.Services
{
    public interface IDeleteService
    {
        string DeleteAccount(AccountInfo info);

    }
}