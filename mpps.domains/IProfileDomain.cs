using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mpps.models;

namespace mpps.domain
{
    public interface IProfileDomain
    {
        Profile Get(int profileId);
    }
}
