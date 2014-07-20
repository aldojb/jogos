/*
 * Copyright (c) 2013 Tomasz Hachaj
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * Adaptacao 2014 Aldo JB
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Net;

namespace Series3D2
{
    [Serializable]
    public class XwingPosition
    {
        public Vector3 position;
        // Declaracao do Quaternion para rotacao.
        public Quaternion rotation;
        public Vector4 color;
        public int uid;
        public Bullet newBullet = null;
        public int killedByUid = -1;
        // Se collision! = 0, em seguida, o jogador foi morto por colisao com killedByUid e jogador com uid = killedByUid
        // tambem deve ser morto.
        //public byte collision = 0;
        public int lastUpdate = 0;
        public byte shipModel = 0;
        public int frags = 0;

        // Converte e adicionar para um Array
        private static void ConverAndAddToArray(byte[]data, float number, ref int actualPosition, bool reverse)
        {
            byte []helpArray = BitConverter.GetBytes(number);
            if (reverse)
                Array.Reverse(helpArray);
            for (int a = 0; a < helpArray.Length; a++)
                data[actualPosition + a] = helpArray[a];
            actualPosition += helpArray.Length;
        }

        // Converte e passa para um Vetor um tipo numero inteiro.
        private static void ConverAndAddToArray(byte[]data, int number, ref int actualPosition, bool reverse)
        {
            byte []helpArray = BitConverter.GetBytes(number);
            if (reverse)
                Array.Reverse(helpArray);
            for (int a = 0; a < helpArray.Length; a++)
                data[actualPosition + a] = helpArray[a];
            actualPosition += helpArray.Length;
        }

        // Converte e passa para um Vetor um tipo numero byte.
        private static void ConverAndAddToArray(byte[] data, byte number, ref int actualPosition, bool reverse)
        {
            data[actualPosition] = number;
            actualPosition ++;
        }

        // Passa para um Array do tipo Byte.
        public static byte[] ToByteArray(XwingPosition Data)
        {
            // posicao da rotacao da cor uid.
            byte[]outputData = new byte[(3 * sizeof(float)) + (4 * sizeof(float)) + (4 * sizeof(float)) + sizeof(int) + 
                // posicao da rotacao uid.
                (3 * sizeof(float)) + (4 * sizeof(float)) + sizeof(int) +
                // uid.
                sizeof(int) +
                // modelo da nave.
                sizeof(byte)];
            int actualPosition = 0;
            bool reverse = BitConverter.IsLittleEndian;
            // posicao.
            ConverAndAddToArray(outputData, Data.position.X, ref actualPosition, reverse);
            ConverAndAddToArray(outputData, Data.position.Y, ref actualPosition, reverse);
            ConverAndAddToArray(outputData, Data.position.Z, ref actualPosition, reverse);
            // rotacao.
            ConverAndAddToArray(outputData, Data.rotation.W, ref actualPosition, reverse);
            ConverAndAddToArray(outputData, Data.rotation.X, ref actualPosition, reverse);
            ConverAndAddToArray(outputData, Data.rotation.Y, ref actualPosition, reverse);
            ConverAndAddToArray(outputData, Data.rotation.Z, ref actualPosition, reverse);
            // cor.
            ConverAndAddToArray(outputData, Data.color.W, ref actualPosition, reverse);
            ConverAndAddToArray(outputData, Data.color.X, ref actualPosition, reverse);
            ConverAndAddToArray(outputData, Data.color.Y, ref actualPosition, reverse);
            ConverAndAddToArray(outputData, Data.color.Z, ref actualPosition, reverse);    
            // uid.
            ConverAndAddToArray(outputData, Data.uid, ref actualPosition, reverse);
            // tem nova bala.

            if (Data.newBullet != null)
            {
                // posicao da bala.
                ConverAndAddToArray(outputData, Data.newBullet.position.X, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, Data.newBullet.position.Y, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, Data.newBullet.position.Z, ref actualPosition, reverse);
                // rotacao da bala.
                ConverAndAddToArray(outputData, Data.newBullet.rotation.W, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, Data.newBullet.rotation.X, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, Data.newBullet.rotation.Y, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, Data.newBullet.rotation.Z, ref actualPosition, reverse);
                // bala id.
                ConverAndAddToArray(outputData, Data.newBullet.ownerUid, ref actualPosition, reverse);
            }
            else
            {
                ConverAndAddToArray(outputData, 0, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, 0, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, 0, ref actualPosition, reverse);

                ConverAndAddToArray(outputData, 0, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, 0, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, 0, ref actualPosition, reverse);
                ConverAndAddToArray(outputData, 0, ref actualPosition, reverse);

                ConverAndAddToArray(outputData, -1, ref actualPosition, reverse);
            }
            // morto por um matador.
            ConverAndAddToArray(outputData, Data.killedByUid, ref actualPosition, reverse);
            // modelo da nave.
            ConverAndAddToArray(outputData, Data.shipModel, ref actualPosition, reverse); 

            return outputData;
        }

        // Converte e retorna de um Array um Float. 
        private static float ConvertFloatFromArray(byte[]data,ref int actualPosition, bool reverse)
        {
            byte []helpArray = new byte[sizeof(float)];
            for (int a = 0; a < sizeof(float);a ++)
                helpArray[a] = data[actualPosition + a];
            if (reverse)
                Array.Reverse(helpArray);
            actualPosition += helpArray.Length;
            return BitConverter.ToSingle(helpArray, 0);
        }

        // Converte e retorna de um inteiro um Array.
        private static int ConvertIntFromArray(byte[]data,ref int actualPosition, bool reverse)
        {
            byte []helpArray = new byte[sizeof(int)];
            for (int a = 0; a < sizeof(int);a ++)
                helpArray[a] = data[actualPosition + a];
            if (reverse)
                Array.Reverse(helpArray);
            actualPosition += helpArray.Length;
            return BitConverter.ToInt32(helpArray, 0);
        }

        // Converte de um Array um Byte.
        private static byte ConvertByteFromArray(byte[] data, ref int actualPosition, bool reverse)
        {
            byte returnValue = data[actualPosition];
            actualPosition++;
            return returnValue;
        }

        // Converte de um Array um Byte.
        public static XwingPosition FromByteArray(byte[] Data)
        {
            XwingPosition xwp = new XwingPosition();
            int actualPosition = 0;
            bool reverse = BitConverter.IsLittleEndian;

            xwp.position.X = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.position.Y = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.position.Z = ConvertFloatFromArray(Data, ref actualPosition, reverse);

            xwp.rotation.W = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.rotation.X = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.rotation.Y = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.rotation.Z = ConvertFloatFromArray(Data, ref actualPosition, reverse);

            xwp.color.W = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.color.X = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.color.Y = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.color.Z = ConvertFloatFromArray(Data, ref actualPosition, reverse);

            xwp.uid = ConvertIntFromArray(Data, ref actualPosition, reverse);

            xwp.newBullet = new Bullet();

            xwp.newBullet.position.X = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.newBullet.position.Y = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.newBullet.position.Z = ConvertFloatFromArray(Data, ref actualPosition, reverse);

            xwp.newBullet.rotation.W = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.newBullet.rotation.X = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.newBullet.rotation.Y = ConvertFloatFromArray(Data, ref actualPosition, reverse);
            xwp.newBullet.rotation.Z = ConvertFloatFromArray(Data, ref actualPosition, reverse);

            xwp.newBullet.ownerUid= ConvertIntFromArray(Data, ref actualPosition, reverse);

            xwp.killedByUid = ConvertIntFromArray(Data, ref actualPosition, reverse);

            xwp.shipModel = ConvertByteFromArray(Data, ref actualPosition, reverse);

            return xwp;
        }
    }
}
