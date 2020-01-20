using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


//Class to convert and storage data.
namespace SzyfratorTekstu.Model
{
    class TextFiller
    {
        [DllImport("ASM_dll.dll")]
        public static extern void EncryptionASM(byte[] data , int dataStart);

        [DllImport("CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncryptionCPP(byte[] data, int dataStart);

        string textToConvert;
        int matrixSize = 16;
        int dataToThreadLenght = 0;
        byte[] textToConvertB;
        int threadsNumber = 0;
        public void encryptionCpp()
        {
            Parallel.For(0, threadsNumber, iteration =>
            {
                int startIteration = iteration * dataToThreadLenght;
                int length = dataToThreadLenght;
                while (length > 0)
                {
                    if (startIteration + 16 <= textToConvertB.Length)
                    {
                        EncryptionCPP(textToConvertB, startIteration);
                        startIteration += 16;
                    }
                    length -= 16;
                }
            });
        }

        public void encryptionAsm()
        {
            Parallel.For(0, threadsNumber, iteration =>
            {
                int startIteration = iteration * dataToThreadLenght;
                int length = dataToThreadLenght;
                while (length > 0)
                {
                    if (startIteration + 16 <= textToConvertB.Length)
                    {
                        EncryptionASM(textToConvertB, startIteration);
                        startIteration += 16;
                    }
                    length -= 16;
                }
            });
        }

        public byte[] getByteArray()
        {
            return textToConvertB;
        }
        public void setText(string text)
        {
            textToConvert = text;
        }

        public string getText()
        {
            return textToConvert;
        }

        public void fillGapInText(int threads)
        {
            threadsNumber = threads;
            while(textToConvert.Length / matrixSize == 0)
            {
                textToConvert += " ";
            }
            while (textToConvert.Length % matrixSize != 0)
            {
                textToConvert += " ";
            }

            if (textToConvert.Length % threads != 0)
            {
                dataToThreadLenght = (textToConvert.Length / threads) + 1;
            }

            else
            {
                dataToThreadLenght = textToConvert.Length / threads;
            }

            dataToThreadLenght += matrixSize - (dataToThreadLenght % matrixSize);
            if (textToConvertB != null)
            {
                Array.Clear(textToConvertB, 0, textToConvertB.Length);
            }
                textToConvertB = Encoding.ASCII.GetBytes(textToConvert);
        }
    }
}
