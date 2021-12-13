using System;
using System.Collections.Generic;
using System.Text;
using LongHorn.ArrowNav.Models;

namespace LongHorn.ArrowNav.Services
{
    public interface IEnableService
    {
        string EnableAccount(AccountInfo info);

    }
}