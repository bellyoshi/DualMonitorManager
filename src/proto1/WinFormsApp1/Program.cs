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
    /// �T�u���j�^���\���ɂ���B
    /// </summary>
    public void SetDisplayModeToInternal()
    {
        SetDisplayConfig(0, IntPtr.Zero, 0, IntPtr.Zero, 0x00000001 | 0x00000080);
    }

    /// <summary>
    /// �T�u���j�^�Ƀ��C�����j�^�Ɠ������e��\������B
    /// </summary>
    static public void SetDisplayModeToClone()
    {
        SetDisplayConfig(0, IntPtr.Zero, 0, IntPtr.Zero, 0x00000002 | 0x00000080);
    }

    static public void SetDisplayModeToExtend()
    {
        SetDisplayConfig(0, IntPtr.Zero, 0, IntPtr.Zero, 0x00000004 | 0x00000080);
    }

    static void Main(string[] args)
    {
        if(args.Length == 0)
        {
            Console.WriteLine("����������܂���B");
            return;
        }
        //���� extends �Ŋg�����[�h
        if(args[0] == "extends")
        {
            SetDisplayModeToExtend();
            return;
        }
        //���� clone �ŃN���[�����[�h
        if(args[0] == "clone")
        {
            SetDisplayModeToClone();
            return;
        }
        
    }
}
