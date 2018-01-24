using GuardianV4_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianV4_Repository.Unit_of_Work
{
    interface IUnitOfWork
    {
        void SaveChanges();
    }
}
