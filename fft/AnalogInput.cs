using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fft
{
    public class AnalogInput
    {
        List<byte> buffer = new List<byte>();

        /// <summary>
        /// Метод для добавления байта к сообщению
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public int? Add(byte bt)
        {
            buffer.Add(bt);

            if (buffer.Count > 4)
            {
                buffer.RemoveAt(0);
            }

            if (buffer.Count < 4)
            {
                return null;
            }

            if (buffer[0] == buffer[2])
            {
                int value = (int)(buffer[1]) + (int)(buffer[3] << 8);
                return value;
            }

            return null;
        }

        public int[] AddArray(byte [] bt)
        {
            List<int> response = new List<int>();

            for (int i = 0; i < bt.Length; i++)
            {
                buffer.Add(bt[i]);

                if (buffer.Count > 4)
                {
                    buffer.RemoveAt(0);
                }

                if (buffer.Count < 4)
                {
                    continue;
                }

                if (buffer[0] == buffer[2])
                {
                    int value = (int)(buffer[1]) + (int)(buffer[3] << 8);
                    response.Add(value);
                }
            }

            return response.ToArray();
        }
    }
}
