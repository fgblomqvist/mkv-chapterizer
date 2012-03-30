using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace MKV_Chapterizer
{
    public delegate CopyFileCallbackAction CopyFileCallback(string source, string destination, object state, long totalFileSize, long totalBytesTransferred);

    public enum CopyFileCallbackAction
    {
        Continue = 0,
        Cancel = 1,
        Stop = 2,
        Quiet = 3
    }

    [Flags]
    public enum CopyFileOptions
    {
        None = 0x0,
        FailIfDestinationExists = 0x1,
        Restartable = 0x2,
        AllowDecryptedDestination = 0x8,
        All = FailIfDestinationExists | Restartable | AllowDecryptedDestination
    }

    public sealed class AdvancedFileHandling
    {
        private delegate int CopyProgressRoutine(long totalFileSize, long TotalBytesTransferred, long streamSize, long streamBytesTransferred, int streamNumber, int callbackReason, IntPtr sourceFile, IntPtr destinationFile, IntPtr data);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName, CopyProgressRoutine lpProgressRoutine, IntPtr lpData, ref bool pbCancel, int dwCopyFlags);

        public static void CopyFile(string source, string destination)
        {
            CopyFile(source, destination, CopyFileOptions.None);
        }

        public static void CopyFile(string source, string destination, CopyFileOptions options)
        {
            CopyFile(source, destination, options, null);
        }

        public static void CopyFile(string source, string destination, CopyFileOptions options, CopyFileCallback callback)
        {
            CopyFile(source, destination, options, callback, null);
        }

        public static void CopyFile(string source, string destination, CopyFileOptions options, CopyFileCallback callback, object state)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if ((options & ~CopyFileOptions.All) != 0)
            {
                throw new ArgumentOutOfRangeException("options");
            }

            new FileIOPermission(FileIOPermissionAccess.Read, source).Demand();
            new FileIOPermission(FileIOPermissionAccess.Write, destination).Demand();

            CopyProgressRoutine cpr = callback == null ? null : new CopyProgressRoutine(new CopyProgressData(source, destination, callback, state).CallbackHandler);

            bool cancel = false;
            if (!CopyFileEx(source, destination, cpr, IntPtr.Zero, ref cancel, (int)options))
            {
                throw new IOException(new Win32Exception().Message);
            }
        }

        private class CopyProgressData
        {
            private string _source;
            private string _destination;
            private CopyFileCallback _callback;
            private object _state;

            public CopyProgressData(string source, string destination,
                                    CopyFileCallback callback, object state)
            {
                _source = source;
                _destination = destination;
                _callback = callback;
                _state = state;
            }

            public int CallbackHandler(
                long totalFileSize, long totalBytesTransferred,
                long streamSize, long streamBytesTransferred,
                int streamNumber, int callbackReason,
                IntPtr sourceFile, IntPtr destinationFile, IntPtr data)
            {
                return (int)_callback(_source, _destination, _state,
                                      totalFileSize, totalBytesTransferred);
            }
        }
    }
}