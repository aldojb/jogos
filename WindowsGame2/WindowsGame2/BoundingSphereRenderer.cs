/*
 * Source: 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame2
{
    /// <summary>
    /// Fornece um conjunto de metodos para rendering BoundingSpheres.
    /// </summary>
    public static class BoundingSphereRenderer
    {
        static VertexBuffer vertBuffer;
        static VertexDeclaration vertDecl;
        static BasicEffect effect;
        static int sphereResolution;

        /// <summary>
        /// Inicializa os objetos graficos para rendenrizar a esfera. Se este metodos nao e
        /// correr manualmente, ele ira ser chamado no primeiro momento do render da esfera.
        /// </summary>
        /// <param name="graphicsDevice"> O dispositivo grafico para ser usando quando renderizar. </param>
        /// <param name="sphereResolution"> O numero de segmentos de linhas
        /// para usar em cadas dos tres circulos. </param>
        public static void InitializeGraphics(GraphicsDevice graphicsDevice, int sphereResolution)
        {
            BoundingSphereRenderer.sphereResolution = sphereResolution;

            //vertDecl = new VertexDeclaration(
            effect = new BasicEffect(graphicsDevice);
            effect.LightingEnabled = false;
            effect.VertexColorEnabled = false;

            VertexPositionColor[] verts = new VertexPositionColor[(sphereResolution + 1) * 3];

            int index = 0;

            float step = MathHelper.TwoPi / (float)sphereResolution;

            // Cria um loop sobre as coordenadas XY do primeiro plano
            for (float a = 0f; a <= MathHelper.TwoPi; a += step)
            {
                verts[index++] = new VertexPositionColor(
                    new Vector3((float)Math.Cos(a), (float)Math.Sin(a), 0f),
                    Color.White);
            }

            // proximo no plano XZ
            for (float a = 0f; a <= MathHelper.TwoPi; a += step)
            {
                verts[index++] = new VertexPositionColor(
                    new Vector3((float)Math.Cos(a), 0f, (float)Math.Sin(a)),
                    Color.White);
            }

            // finalmente no plano YZ
            for (float a = 0f; a <= MathHelper.TwoPi; a += step)
            {
                verts[index++] = new VertexPositionColor(
                    new Vector3(0f, (float)Math.Cos(a), (float)Math.Sin(a)),
                    Color.White);
            }

            vertBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), verts.Length, BufferUsage.None);
            vertBuffer.SetData(verts);
        }

        /// <summary>
        /// Renderiza uma esfera em seu limite usando diferentes cores para cada eixo.
        /// </summary>
        /// <param name="sphere"> A esfera para renderizar. </param>
        /// <param name="graphicsDevice"> O dispositivo grafico para usar quando renderizar. </param>
        /// <param name="view"> A visao corrente da matriz. </param>
        /// <param name="projection"> A projecao da matriz. </param>
        /// <param name="xyColor"> A cor para o circulo XY. </param>
        /// <param name="xzColor"> A cor para o circulo XZ. </param>
        /// <param name="yzColor"> A cor para o circulo YZ. </param>
        public static void Render(
            BoundingSphere sphere,
            GraphicsDevice graphicsDevice,
            Matrix view,
            Matrix projection,
            Color xyColor,
            Color xzColor,
            Color yzColor)
        {
            if (vertBuffer == null)
                InitializeGraphics(graphicsDevice, 30);

            graphicsDevice.SetVertexBuffer(vertBuffer);

            effect.World =
                Matrix.CreateScale(sphere.Radius) *
                Matrix.CreateTranslation(sphere.Center);
            effect.View = view;
            effect.Projection = projection;
            effect.DiffuseColor = xyColor.ToVector3();

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                // renderizar para cada circulo individualmente
                graphicsDevice.DrawPrimitives(
                      PrimitiveType.LineStrip,
                      0,
                      sphereResolution);
                pass.Apply();
                effect.DiffuseColor = xzColor.ToVector3();
                graphicsDevice.DrawPrimitives(
                      PrimitiveType.LineStrip,
                      sphereResolution + 1,
                      sphereResolution);
                pass.Apply();
                effect.DiffuseColor = yzColor.ToVector3();
                graphicsDevice.DrawPrimitives(
                      PrimitiveType.LineStrip,
                      (sphereResolution + 1) * 2,
                      sphereResolution);
                pass.Apply();

            }

        }

        public static void Render(BoundingSphere[] spheres,
           GraphicsDevice graphicsDevice,
           Matrix view,
           Matrix projection,
           Color xyColor,
            Color xzColor,
            Color yzColor)
        {
            foreach (BoundingSphere sphere in spheres)
            {
                Render(sphere, graphicsDevice, view, projection, xyColor, xzColor, yzColor);
            }
        }

        public static void Render(BoundingSphere[] spheres,
            GraphicsDevice graphicsDevice,
            Matrix view,
            Matrix projection,
            Color color)
        {
            foreach (BoundingSphere sphere in spheres)
            {
                Render(sphere, graphicsDevice, view, projection, color);
            }
        }

        /// <summary>
        /// Renderizar uma superficide da esfera usando apenas uma cor para todos os tres eixos.
        /// </summary>
        /// <param name="sphere"> A esfera para renderizar. </param>
        /// <param name="graphicsDevice"> O dispositivo grafico para usar quando renderizar. </param>
        /// <param name="view"> A corrente visao da matriz. </param>
        /// <param name="projection"> A projecao corrente da matriz. </param>
        /// <param name="color"> A cor para ser usada para renderizar os circulos. </param>
        public static void Render(
            BoundingSphere sphere,
            GraphicsDevice graphicsDevice,
            Matrix view,
            Matrix projection,
            Color color)
        {
            //BlendState oldBS = graphicsDevice.BlendState;
            //graphicsDevice.BlendState = BlendState.AlphaBlend;
            if (vertBuffer == null)
                InitializeGraphics(graphicsDevice, 30);

            graphicsDevice.SetVertexBuffer(vertBuffer);

            effect.World =
                  Matrix.CreateScale(sphere.Radius) *
                  Matrix.CreateTranslation(sphere.Center);
            effect.View = view;
            effect.Projection = projection;
            effect.DiffuseColor = color.ToVector3();

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                // renderizar cada circulo individualmente
                graphicsDevice.DrawPrimitives(
                      PrimitiveType.LineStrip,
                      0,
                      sphereResolution);
                graphicsDevice.DrawPrimitives(
                      PrimitiveType.LineStrip,
                      sphereResolution + 1,
                      sphereResolution);
                graphicsDevice.DrawPrimitives(
                      PrimitiveType.LineStrip,
                      (sphereResolution + 1) * 2,
                      sphereResolution);

            }
            //graphicsDevice.BlendState = oldBS;
        }
    }
}