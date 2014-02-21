namespace Conway.OpenToolKit
{
    using System;
    using System.Collections.ObjectModel;
    using Game;
    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Input;
    using Color = System.Drawing.Color;

    public class MyApplication
    {
        [STAThread]
        public static void Main()
        {
            var diehard = new[]
            {
                new Vector(3, 2),
                new Vector(4, 4),
                new Vector(3, 4),
                new Vector(2, 4),
                new Vector(-2, 3),
                new Vector(-2, 4),
                new Vector(-3, 3),
            }.ToHashSet();

            using (var game = new GameWindow())
            {
                var conway = new Game<Vector>();
                var generation = 0;
                var gameOfLife = conway.Run(diehard, true).GetEnumerator();

                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }

                    gameOfLife.MoveNext();
                    generation++;
                    Console.WriteLine("Generation: {0}", generation);
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(-30.0, 30.0, -30.0, 30.0, 0.0, 10.0);
                    GL.PointSize(5.0f);

                    GL.Begin(PrimitiveType.Points);

                    foreach (var cell in gameOfLife.Current)
                    {
                        GL.Color3(Color.SteelBlue);
                        GL.Vertex2(cell.X, cell.Y);
                    }

                    GL.End();

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(2);
            }
        }
    }
}