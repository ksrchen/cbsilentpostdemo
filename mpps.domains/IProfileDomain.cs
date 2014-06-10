using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mpts.models;

namespace mpts.domain
{
    public interface IProfileDomain
    {
        Profile Get(int profileId);
    }
}
