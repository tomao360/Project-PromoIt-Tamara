using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public interface ILogger
    {
        void Init();
        void LogEvent(string msg);
        void LogError(string msg);
        void LogException(string msg, Exception exce);
        void LogCheckHoseKeeping();
    }
}
