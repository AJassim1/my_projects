using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComToTCP
{
    class RegisterConvertor
    {
        public static float ToSingle(ushort[] registers)
        {
            if (registers.Length > 2) return 0;

            int value = registers[0] << 16;
            value = value | registers[1];
            float res = BitConverter.ToSingle(BitConverter.GetBytes(value), 0);

            return res;
        }

        public static float[] ToSingleArray(ushort[] registers)
        {
            float[] res = new float[registers.Length / 2];
            for (int i = 0; i < registers.Length / 2; i++)
            { 
                res[i] = RegisterConvertor.ToSingle(new ushort[] {registers[i*2], registers[i*2+1]});
            }
            return res;
        }

        public static int ToInt(ushort[] registers)
        {
            if (registers.Length > 2) return 0;

            int value = registers[0] << 16;
            value = value | registers[1];
            int res = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);

            return res;
        }

        public static int[] ToIntArray(ushort[] registers)
        {
            int[] res = new int[registers.Length / 2];
            for (int i = 0; i < registers.Length / 2; i++)
            {
                res[i] = RegisterConvertor.ToInt(new ushort[] { registers[i * 2], registers[i * 2 + 1] });
            }
            return res;
        }

        public static ushort[] GetRegisters(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            ushort[] res = new ushort[2];
            res[0] = BitConverter.ToUInt16(bytes, 2);
            res[1] = BitConverter.ToUInt16(bytes, 0);

            return res;
        }

        public static ushort[] GetRegisters(float[] value)
        {
            ushort[] res = new ushort[value.Length * 2 + 1];
            for (int i = 0; i < value.Length; i++)
            {
                ushort[] tmp_res = RegisterConvertor.GetRegisters(value[i]);
                res[i * 2] = tmp_res[0];
                res[i * 2 + 1] = tmp_res[1];
            }

            return res;
        }
    }
}