using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc_back_end_puc.Infrastructure.Common
{
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using tcc_back_end_puc.Domain;

    public class Aes256 : IDisposable
    {
        private RijndaelManaged Engine;
        private SHA256CryptoServiceProvider HashProvider;
        private byte[] HashBytes;

        private string _Password;
        public Aes256()
        {
            _Password = Parametros.Token;
        }

        /// <summary>
        /// Criptografa um buffer de bytes
        /// </summary>
        /// <param name="Buffer"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] Buffer)
        {
            using (ICryptoTransform Encryptor = Engine.CreateEncryptor())
                return Encryptor.TransformFinalBlock(Buffer, 0, Buffer.Length);

        }

        /// <summary>
        /// Descriptografa um buffer de bytes
        /// </summary>
        /// <param name="Buffer"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] Buffer)
        {
            using (ICryptoTransform Decryptor = Engine.CreateDecryptor())
                return Decryptor.TransformFinalBlock(Buffer, 0, Buffer.Length);
        }

        /// <summary>
        /// Criptografa um texto
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public string Encrypt(string Str)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(Str)));
        }

        /// <summary>
        /// Descriptografa um texto
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public string Decrypt(string Str)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(Str)));
        }

        public void Dispose()
        {
            Engine.Dispose();
            HashProvider.Dispose();
            HashBytes = null;
            _Password = null;
        }
    }
}
