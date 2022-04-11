using OpenGLGame.AssetsGenerators.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpenGLGame.AssetsGenerators
{
    public static class Square
    {
        public static void CreateSquare(ref List<SqareInstance> sqares, Vector2 mousePosition, Vector2 size)
        {
            sqares.Add(new SqareInstance(GL.GenBuffer(), GL.GenVertexArray(), mousePosition, size, new Shader("./Shaders/shader.vert", "./Shaders/shader.frag")));
            int elementIndex = sqares.Count - 1;
            GL.BindBuffer(BufferTarget.ArrayBuffer, sqares[elementIndex]._VBOIndex);
            GL.BufferData(BufferTarget.ArrayBuffer, sqares[elementIndex]._vertices.Length * sizeof(float), sqares[elementIndex]._vertices, BufferUsageHint.StaticDraw);
            GL.BindVertexArray(sqares[elementIndex]._VAOIndex);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            sqares[elementIndex]._shader.Use();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }
    }

    #region Square Instance
    public class SqareInstance
    {
        public readonly int _VBOIndex;
        public readonly int _VAOIndex;
        public Vector2 _position;
        public Vector2 _size;

        public readonly float[] _vertices;

        public Shader _shader;

        internal SqareInstance(int VBOIndex, int VAOIndex, Vector2 position, Vector2 size, Shader shader)
        {
            _VBOIndex = VBOIndex;
            _VAOIndex = VAOIndex;
            _position = position;
            _size = size;

            _vertices = new float[]
            {
                position.X - size.X*0.28125f, position.Y - size.Y*0.5f, 0f, //D L
                position.X - size.X*0.28125f, position.Y + size.Y*0.5f, 0f, //U L 
                position.X + size.X*0.28125f, position.Y + size.Y*0.5f, 0f, //U R
                position.X + size.X*0.28125f, position.Y + size.Y*0.5f, 0f, //U R
                position.X + size.X*0.28125f, position.Y - size.Y*0.5f, 0f, //D R
                position.X - size.X*0.28125f, position.Y - size.Y*0.5f, 0f, //D L
            };

            _shader = shader;
        }
    }
    #endregion
}
