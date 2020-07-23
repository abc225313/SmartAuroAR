using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SmartAutoAR.VirtualObject.Base;

namespace SmartAutoAR.VirtualObject.Lights
{
	public class AmbientLight : ILight
	{
		public Color4 Color { get; set; }
		public float Main_strength { get; set; }

		public AmbientLight(Color4 color, float main_strength)
		{
			Color = color;
			Main_strength = main_strength;
		}

		public void SetShader(Shader shader, int index)
		{
			GL.Uniform4(shader.GetUniformLocation($"ambient_lights[{index}].color"), Color);
			GL.Uniform1(shader.GetUniformLocation($"ambient_lights[{index}].main_strength"), Main_strength);
		}
	}
}
