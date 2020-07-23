using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SmartAutoAR.VirtualObject.Base;

namespace SmartAutoAR.VirtualObject.Lights
{
	public class PointLight : ILight
	{
		public Color4 Color { get; set; }
		public Vector3 Position { get; set; }
		public float Main_strength { get; set; }
		public float Specular_strength { get; set; }

		public PointLight(Color4 color, Vector3 position, float main_strength, float specular_strength)
		{
			Color = color;
			Position = position;
			Main_strength = main_strength;
			Specular_strength = specular_strength;
		}

		public void SetShader(Shader shader, int index)
		{
			GL.Uniform4(shader.GetUniformLocation($"point_lights[{index}].color"), Color);
			GL.Uniform3(shader.GetUniformLocation($"point_lights[{index}].position"), Position);
			GL.Uniform1(shader.GetUniformLocation($"point_lights[{index}].main_strength"), Main_strength);
			GL.Uniform1(shader.GetUniformLocation($"point_lights[{index}].specular_strength"), Specular_strength);
		}
	}
}
