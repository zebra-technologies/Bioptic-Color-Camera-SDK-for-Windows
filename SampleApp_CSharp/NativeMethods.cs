using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CameraSDKSampleApp
{
    /// <summary>
    /// This class contains native declarations used int the CameraSDK C# sample application.
    /// </summary>
    class NativeMethods
    {
        /// <summary>
        /// Enables the application to access the window menu (also known as the system menu or the control menu) for copying and modifying.
        /// </summary>
        /// <param name="windowHandle">A handle to the window that will own a copy of the window menu.</param>
        /// <param name="revertAction">The action to be taken. If this parameter is FALSE, GetSystemMenu returns a handle to the copy of the window menu currently in use. The copy is initially identical to the window menu, but it can be modified. If this parameter is TRUE, GetSystemMenu resets the window menu back to the default state. The previous window menu, if any, is destroyed.</param>
        /// <returns>If the bRevert parameter is FALSE, the return value is a handle to a copy of the window menu. If the bRevert parameter is TRUE, the return value is NULL.</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetSystemMenu(IntPtr windowHandle, bool revertAction);

        /// <summary>
        /// Appends a new item to the end of the specified menu bar, drop-down menu, sub-menu, or shortcut menu. You can use this function to specify the content, appearance, and behavior of the menu item.
        /// </summary>
        /// <param name="menueHandle">A handle to the menu bar, drop-down menu, sub-menu, or shortcut menu to be changed.</param>
        /// <param name="flags">Controls the appearance and behavior of the new menu item.</param>
        /// <param name="idNewItem">The identifier of the new menu item or, if the uFlags parameter is set to MF_POPUP, a handle to the drop-down menu or submenu.</param>
        /// <param name="newItem">The content of the new menu item.</param>S
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool AppendMenu(IntPtr menueHandle, int flags, int idNewItem, string newItem);


        ///  Bounding box detection image processing library native methods
        /// <summary>
        /// Initialize Bounding Box Detector
        /// </summary>
        /// <param name="background_filter_type">background_filter_type</param>
        [DllImport("image_proc.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void InitBoundingBoxDetector(int background_filter_type);

        /// <summary>
        /// Set Background Frame
        /// </summary>
        /// <param name="background_frame_buffer">Background frame image buffer</param>
        /// <param name="length">length of background frame buffer</param>
        /// <param name="image_width">image width</param>
        /// <param name="image_height">image height</param>
        /// <param name=image_type">image type (reserved BMP:0/JPEG:1)</param>
        [DllImport("image_proc.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetBackgroundFrame(IntPtr background_frame_buffer, int length, int image_width, int image_height, int image_type);

        /// <summary>
        /// Detect Bounding Box
        /// </summary>
        /// <param name="input_frame_buffer">input image frame buffer to detect bounding box</param>
        /// <param name="length">length of image frame buffer</param>
        /// <param name="image_width">input image width</param>
        /// <param name="image_height">input image height</param>
        /// <param name=image_type">input image type (reserved BMP:0/JPEG:1)</param>
        /// <param name="x1">detected bounding box location top left x</param>
        /// <param name="y1">detected bounding box location top left y</param>
        /// <param name="x2">detected bounding box location bottom right x</param>
        /// <param name="y2">detected bounding box location bottom right y</param>
        /// <returns>Returns true if bounding box detected</returns>
        [DllImport("image_proc.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DetectBoundingBox(IntPtr input_frame_buffer, int length, int image_width, int image_height, int image_type, ref int x1, ref int y1, ref int x2, ref int y2);
    }
}
