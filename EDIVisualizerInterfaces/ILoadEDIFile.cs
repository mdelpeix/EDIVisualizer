using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace EDIVisualizer.Interfaces
{
    public interface ILoadEDIFile
    {
        void EDILoadFile(string fileName);
        void EDIReset();
        string EDITag { get; set; }
        string EDISession { get; set; }
        string EDIMapping { get; set; }
        string EDITrace { get; set; }
        string EDIEnvironnement { get; set; }
        string SPLogin { get; }
        SecureString SPPassword { get; }
        bool isCommandLine { get; set; }
    }
}
