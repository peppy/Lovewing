using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lovewing.Beatmaps.Loaders
{
    public class AsyncFileUtils
    {
        /// <summary>
        /// The max buffer size to use for async file operations
        /// </summary>
        public const int AsyncBufferSize = 4096;

        /// <summary>
        /// Asyncronously reads the content of a text file.
        /// </summary>
        /// <param name="path">Path to the text file.</param>
        /// <param name="encoding">The encoding that the text file uses.</param>
        /// <returns>The contents of the text file.</returns>
        public static async Task<string> ReadTextFile(string path)
        {
            // Just read the data, then convert it to a string.
            byte[] buffer = await ReadBinaryFile(path);
            int index = 0;
            int count = buffer.Length;

            // Skip BOM characters at the start.
            if (buffer[0] == 239 && buffer[1] == 187 && buffer[2] == 191)
            {
                index = 3;
                count -= 3;
            }

            return Encoding.UTF8.GetString(buffer, index, count);
        }

        /// <summary>
        /// Asyncronously reads the content of a binary file.
        /// </summary>
        /// <param name="path">Path to the binary file.</param>
        /// <returns>The contents of the binary file.</returns>
        public static async Task<byte[]> ReadBinaryFile(string path)
        {
            FileInfo info = new FileInfo(path);
            byte[] buffer = new byte[info.Length];

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, AsyncBufferSize, true))
            {
                await stream.ReadAsync(buffer, 0, (int)info.Length);
            }

            return buffer;
        }
    }
}
