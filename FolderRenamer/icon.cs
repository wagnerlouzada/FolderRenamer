using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FolderRenamer
{


    public class AppIcon
    {


        private const Int32 MAX_PATH = 260;
        private const Int32 SHGFI_ICON = 0x100;
        private const Int32 SHGFI_USEFILEATTRIBUTES = 0x10;
        private const Int32 FILE_ATTRIBUTE_NORMAL = 0x80;

        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public Int32 iIcon;
            public Int32 dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        public enum IconSize
        {
            SHGFI_LARGEICON = 0,
            SHGFI_SMALLICON = 1
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private extern static IntPtr SHGetFileInfo(string pszPath, Int32 dwFileAttributes, ref SHFILEINFO psfi, Int32 cbFileInfo, Int32 uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private extern static bool DestroyIcon(IntPtr hIcon);

        // get associated icon (as bitmap).
        public static Bitmap GetFileIcon(string fileExt, IconSize ICOsize = IconSize.SHGFI_SMALLICON)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            shinfo.szDisplayName = new string('\0', MAX_PATH);
            shinfo.szTypeName = new string('\0', 80);
            SHGetFileInfo(fileExt, FILE_ATTRIBUTE_NORMAL, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | (int)ICOsize | SHGFI_USEFILEATTRIBUTES);
            Bitmap bmp = System.Drawing.Icon.FromHandle(shinfo.hIcon).ToBitmap();
            DestroyIcon(shinfo.hIcon); // must destroy icon to avoid GDI leak!
            return bmp; // return icon as a bitmap
        }


    }
}
