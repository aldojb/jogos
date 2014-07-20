﻿/*
 * Copyright (c) 2013 Tomasz Hachaj
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * Adaptacao 2014 Aldo JB
 * Classe utilizada para a descricao dos modelos das espaconaves
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace WindowsGame2
{
    class ModelDescription
    {
        // Vetor para posicionamento da camera
        public Vector3 CameraPosition = new Vector3();

        // Modelo da espaconave
        public Model ShipModel = null;

        // Cores para os modelos
        public ArrayList ModelColors = new ArrayList();
        public float RotationX = 0;
        public float RotationY = 0;
        public float RotationZ = 0;
        public float Scale = 0;
    }
}
