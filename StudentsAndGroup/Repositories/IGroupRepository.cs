using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Models;

namespace University.Repositories
{
    interface IGroupRepository
    {
        List<Group> GetAllGroups();
        void AddGroup(Group group);
        List<Group> GetGroupByName(string name);
    }
}
