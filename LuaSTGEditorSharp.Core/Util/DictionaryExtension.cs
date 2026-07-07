using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LuaSTGEditorSharp.Util
{
    public static class DictionaryExtension
    {
        public static U GetOrDefault<T, U>(this Dictionary<T, U> dict, T inValue, U defaultValue)
        {
            U value = defaultValue;
            if (dict.TryGetValue(inValue, out U val)) value = val;
            return value;
        }
    }

    public static class FileExtension
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private extern static bool DeleteFile(string lpFileName);

        extension(File)
        {
            /// <summary>
            /// Unlocks a DLL by deleting the MOTW.
            /// </summary>
            /// <param name="filePath">Full path to DLL.</param>
            /// <returns>True if the unlocking was sucessful. False if the file wasn't locked.</returns>
            /// <exception cref="FileNotFoundException">File is not found.</exception>
            public static bool UnblockDll(string filePath)
            {
                Console.WriteLine($"Unblocking DLL \"{filePath}\"");

                if (!File.Exists(filePath))
                    throw new FileNotFoundException("Specified file doesn't exist.", filePath);

                string zoneId = $"{filePath}:Zone.Identifier";
                return DeleteFile(zoneId);
            }
        }
    }
}
