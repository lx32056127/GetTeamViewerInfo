using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GetTeamViewerInfo.Commands
{
    public static class Command
    {
        public static void ReleaseMemory(bool removePages)
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            if (removePages)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle,
                    (UIntPtr) 0xFFFFFFFF,
                    (UIntPtr) 0xFFFFFFFF);
            }
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process,
            UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
    }
}
