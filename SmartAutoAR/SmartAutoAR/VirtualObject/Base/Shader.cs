using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;

namespace SmartAutoAR.VirtualObject.Base
{
	public class Shader : IDisposable
	{
		public static Shader StandardShader
		{
			get
			{
				return new Shader(@"Resources\Shaders\standard_vert.shader",
								  @"Resources\Shaders\standard_frag.shader");
			}
		}

		public static Shader BackgroundShader
		{
			get
			{
				return new Shader(@"Resources\Shaders\background_vert.shader",
								  @"Resources\Shaders\background_frag.shader");
			}
		}

		protected readonly int handle;

		public Shader(string vertexPath, string fragmentPath)
		{
			// vertex shader
			string vertexShaderSource = File.ReadAllText(vertexPath);
			int vertexShader = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vertexShader, vertexShaderSource);
			GL.CompileShader(vertexShader);
			string infoLogVert = GL.GetShaderInfoLog(vertexShader);
			if (infoLogVert != string.Empty) throw new Exception(infoLogVert);

			// fragment shader
			string fragmentShaderSource = File.ReadAllText(fragmentPath);
			int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(fragmentShader, fragmentShaderSource);
			GL.CompileShader(fragmentShader);
			string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);
			if (infoLogFrag != string.Empty) throw new Exception(infoLogFrag);

			handle = GL.CreateProgram();
			GL.AttachShader(handle, vertexShader);
			GL.AttachShader(handle, fragmentShader);
			GL.LinkProgram(handle);

			GL.DetachShader(handle, vertexShader);
			GL.DetachShader(handle, fragmentShader);
			GL.DeleteShader(vertexShader);
			GL.DeleteShader(fragmentShader);
		}

		public void Use()
		{
			GL.UseProgram(handle);
		}

		public int GetUniformLocation(string name)
		{
			Use();
			return GL.GetUniformLocation(handle, name);
		}

		public void Dispose()
		{
			GL.DeleteProgram(handle);
			GC.SuppressFinalize(this);
		}
	}
}
