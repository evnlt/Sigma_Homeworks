using QueueLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueueLib.Factories
{
    internal interface IPeopleFactory
    {
        Client CreateInstance();
    }
}
