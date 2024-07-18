using System;
using System.Runtime.InteropServices;

namespace WinFormsApp1;


class Program
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern UInt32 SetDisplayConfig(
        UInt32 numPathArrayElements,
        IntPtr pathArray,
        UInt32 numModeInfoArrayElements,
        IntPtr modeInfoArray,
        UInt32 flags
    );

    /// <summary>
    /// サブモニタを非表示にする。
    /// </summary>
    public void SetDisplayModeToInternal()
    {
        SetDisplayConfig(0, IntPtr.Zero, 0, IntPtr.Zero, 0x00000001 | 0x00000080);
    }

    /// <summary>
    /// サブモニタにメインモニタと同じ内容を表示する。
    /// </summary>
    static public void SetDisplayModeToClone()
    {
        SetDisplayConfig(0, IntPtr.Zero, 0, IntPtr.Zero, 0x00000002 | 0x00000080);
    }

    static void Main(string[] args)
    {
        SetDisplayModeToClone();
    }
}
