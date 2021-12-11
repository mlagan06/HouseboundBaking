using System;
using System.Collections.Generic;
using System.Text;

namespace HouseboundBaking.Services
{
    public interface IHashService
    {
        string GenerateHashkey();
        void StartSMSRetriverReceiver();
    }
}
