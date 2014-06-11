using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pts.models;

namespace pts.domain
{
    public interface IProfileDomain
    {
        Profile Get(int profileId);
    }
}
