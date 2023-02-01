using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    interface INotifier
    {
        void Notify();

        bool CanRun();
    }
}
