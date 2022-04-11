using OpenGLGame.AssetsGenerators;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGLGame
{
    public class WindowedGame : GameWindow
    {
        #region FPS
        private float _second = 0;
        private int _fps = 0;
        #endregion

        private static List<SqareInstance> _sqares = new();

        #region Constructor
        public WindowedGame(int width, int height) : base(GameWindowSettings.Default, new NativeWindowSettings
        {
            Size = new Vector2i(width, height),
            Location = new Vector2i(370, 300),
            StartFocused = true,
            WindowState = WindowState.Fullscreen,
            Profile = ContextProfile.Any,
            WindowBorder = WindowBorder.Resizable
        })
        {
            VSync = VSyncMode.On;
            //CursorVisible = false;
        }
        #endregion

        #region OnLoad and OnUnload

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.PaleGoldenrod);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);

        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            foreach (var square in _sqares)
            {
                GL.DeleteBuffer(square._VBOIndex);
                GL.DeleteVertexArray(square._VAOIndex);

                GL.DeleteProgram(square._shader.Handle);
            }

            base.OnUnload();
        }

        #endregion

        #region Useless Crap

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            _second += (float)args.Time;
            _fps++;

            if (_second >= 1.0f)
            {
                Title = "FPS = " + _fps.ToString();
                _second = 0.0f;
                _fps = 0;
            }

            base.OnUpdateFrame(args);
        }

        #endregion

        #region OnRender

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (var square in _sqares)
            {
                square._shader.Use();
                GL.BindBuffer(BufferTarget.ArrayBuffer, square._VBOIndex);
                GL.BindVertexArray(square._VAOIndex);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            }

            SwapBuffers();
        }

        #endregion

        #region Interaction
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButton.Left:
                    {
                        Square.CreateSquare(ref _sqares, new Vector2((MousePosition.X - 970) / 970, (MousePosition.Y - 540) / -540), new Vector2(0.2f, 0.2f));
                    }
                    break;
            }
            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.R:
                    {
                        OnUnload();
                        OnLoad();
                    }
                    break;
                case Keys.Escape:
                    {
                        Close();
                    }
                    break;
            }
            base.OnKeyDown(e);
        }
        #endregion
    }
}