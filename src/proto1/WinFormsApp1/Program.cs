using System;
using System.Runtime.InteropServices;

namespace WinFormsApp1;


class Program
{
    // 定数の定義
    private const int ENUM_CURRENT_SETTINGS = -1;
    private const int CDS_UPDATEREGISTRY = 0x01;
    private const int CDS_TEST = 0x02;
    private const int DISP_CHANGE_SUCCESSFUL = 0;
    private const int DM_POSITION = 0x00000020;
    private const int DM_PELSWIDTH = 0x00080000;
    private const int DM_PELSHEIGHT = 0x00100000;

    // DEVMODE 構造体の定義
    [StructLayout(LayoutKind.Sequential)]
    private struct DEVMODE
    {
        private const int CCHDEVICENAME = 0x20;
        private const int CCHFORMNAME = 0x20;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;
        public short dmLogPixels;
        public short dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    // DLL インポート
    [DllImport("user32.dll")]
    private static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

    [DllImport("user32.dll")]
    private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

    static void Main(string[] args)
    {
        DEVMODE dm = new DEVMODE();
        dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
        bool result = EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref dm);

        if (!result)
        {
            Console.WriteLine("現在のディスプレイ設定の取得に失敗しました。");
            return;
        }
        
        dm.dmPelsWidth *= 2;  // 横幅を2倍にして複製モードを実現
        dm.dmFields = DM_PELSWIDTH | DM_PELSHEIGHT;

        int resultChange = ChangeDisplaySettings(ref dm, CDS_TEST);

        if (resultChange != DISP_CHANGE_SUCCESSFUL)
        {
            Console.WriteLine("テスト変更に失敗しました。");
            return;
        }
        
        resultChange = ChangeDisplaySettings(ref dm, CDS_UPDATEREGISTRY);

        if (resultChange != DISP_CHANGE_SUCCESSFUL)
        {
            Console.WriteLine("ディスプレイモードの変更に失敗しました。");
            return;
        }
        
        Console.WriteLine("ディスプレイモードを複製モードに変更しました。");
    }
}
