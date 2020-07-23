using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SmartAutoAR.VirtualObject.Base;

namespace SmartAutoAR.VirtualObject.Lights
{
	public class DirectionalLight : ILight
	{
		public Color4 Color { get; set; }
		public Vector3 Direction { get; set; }
		public float Main_strength { get; set; }
		public float Specular_strength { get; set; }

		public DirectionalLight(Color4 color, Vector3 direction, float main_strength, float specular_strength)
		{
			Color = color;
			Direction = direction;
			Main_strength = main_strength;
			Specular_strength = specular_strength;
		}

		public void SetShader(Shader shader, int index)
		{
			GL.Uniform4(shader.GetUniformLocation($"directional_lights[{index}].color"), Color);
			GL.Uniform3(shader.GetUniformLocation($"directional_lights[{index}].direction"), Direction);
			GL.Uniform1(shader.GetUniformLocation($"directional_lights[{index}].main_strength"), Main_strength);
			GL.Uniform1(shader.GetUniformLocation($"directional_lights[{index}].specular_strength"), Specular_strength);
		}
	}
}
