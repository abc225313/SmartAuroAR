using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace SmartAutoAR.VirtualObject.Base
{
	public class Mesh : IDisposable
	{
		protected readonly int VAO, VBO, EBO;
		protected bool dis_EBO = false, dis_texture = false;

		public Vertex[] Vertices { get; set; }
		public Material Material { get; set; }
		public uint[] Indices { get; private set; }
		public bool UseIndices { get; set; }
		public Texture Texture { get; private set; }
		public bool UseTexture { get; set; }

		public Mesh(Vertex[] vertices)
		{
			this.Vertices = vertices;

			// 生成
			VAO = GL.GenVertexArray();
			VBO = GL.GenBuffer();
			EBO = GL.GenBuffer();

			// 設定 VBO 規格
			GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
			GL.NamedBufferData(VBO, Vertex.Size * Vertices.Length, Vertices, BufferUsageHint.StaticDraw);

			GL.BindVertexArray(VAO);
			// 設定 VAO 屬性 0 (position)
			GL.VertexArrayAttribBinding(VAO, 0, 0);
			GL.EnableVertexArrayAttrib(VAO, 0);
			GL.VertexArrayAttribFormat(VAO, 0, 3, VertexAttribType.Float, false, 0);

			// 設定 VAO 屬性 1 (textureCoord)
			GL.VertexArrayAttribBinding(VAO, 1, 0);
			GL.EnableVertexArrayAttrib(VAO, 1);
			GL.VertexArrayAttribFormat(VAO, 1, 2, VertexAttribType.Float, false, 12);

			// 設定 VAO 屬性 2 (normal)
			GL.VertexArrayAttribBinding(VAO, 2, 0);
			GL.EnableVertexArrayAttrib(VAO, 2);
			GL.VertexArrayAttribFormat(VAO, 2, 3, VertexAttribType.Float, false, 20);

			// 將 VAO 與 VBO 串起來
			GL.VertexArrayVertexBuffer(VAO, 0, VBO, IntPtr.Zero, Vertex.Size);

			// 設定材質
			Material = Material.Common;

			UseIndices = false;
			UseTexture = false;
		}

		public void SetIndices(uint[] indices)
		{
			this.Indices = indices;

			// 設定 EBO
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
			GL.NamedBufferStorage(EBO, Indices.Length * sizeof(uint), Indices, BufferStorageFlags.MapWriteBit);

			UseIndices = true;
			dis_EBO = true;
		}

		public void SetTexture(Texture texture)
		{
			this.Texture = texture;
			UseTexture = true;
			dis_texture = true;
		}

		public void Resize(float percent)
		{
			for (int i = 0; i < Vertices.Length; i++)
			{
				Vertices[i].position = new Vector3(
					Vertices[i].position.X * percent,
					Vertices[i].position.Y * percent,
					Vertices[i].position.Z * percent
				);
			}
			// 設定 VBO 規格
			GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
			GL.NamedBufferData(VBO, Vertex.Size * Vertices.Length, Vertices, BufferUsageHint.StaticDraw);
		}

		public void Render(Shader shader)
		{
			shader.Use();
			if (UseTexture)
			{
				GL.Uniform1(shader.GetUniformLocation("useTexture"), (uint)1);
				Texture.Use();
			}
			else
			{
				GL.Uniform1(shader.GetUniformLocation("useTexture"), (uint)0);
			}
			GL.Uniform4(shader.GetUniformLocation("material.ambient"), Material.Ambient);
			GL.Uniform4(shader.GetUniformLocation("material.diffuse"), Material.Diffuse);
			GL.Uniform4(shader.GetUniformLocation("material.specular"), Material.Specular);
			GL.Uniform1(shader.GetUniformLocation("material.shininess"), Material.Shininess);
			GL.BindVertexArray(VAO);
			if (UseIndices) GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
			else GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Length);
		}

		public void Dispose()
		{
			GL.DeleteVertexArray(VAO);
			GL.DeleteBuffer(VBO);
			if (dis_EBO) GL.DeleteBuffer(EBO);
			if (dis_texture) this.Texture.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
