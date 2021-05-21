using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.NETReact.Domain.Contracts
{
    public interface IQuery <T>
    {
        Task<T> ExecuteQuery();
    }
}
