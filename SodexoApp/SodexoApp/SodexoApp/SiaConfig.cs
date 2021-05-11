using SodexoApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace SodexoApp
{
    public class SiaConfig
    {

        public static string Decompress(byte[] data)
        {
            byte[] lengthBuffer = new byte[4];
            Array.Copy(data, data.Length - 4, lengthBuffer, 0, 4);
            int uncompressedSize = BitConverter.ToInt32(lengthBuffer, 0);

            var buffer = new byte[uncompressedSize];
            using (var ms = new MemoryStream(data))
            {
                using (var gzip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    gzip.Read(buffer, 0, uncompressedSize);
                }
            }
            return Encoding.Unicode.GetString(buffer);
        }


        public static bool ValidAcceso(string idacceso)
        {
            try
            {
                bool flag = false;
                var found = App.Acc.ContainsKey(idacceso);
                if (found)
                {
                    AccDir Accesos = App.Acc[idacceso];
                    if (Accesos.lRun) flag = true;                    
                }
                
                return flag;
            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRROR en la funcion ValidAcceso():" + w);
                return false;
            }
        }






    }
}
