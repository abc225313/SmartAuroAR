using System.Collections.Generic;
using System.IO;
using Assimp;
using Assimp.Unmanaged;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SmartAutoAR.VirtualObject.Base;
using Mesh = SmartAutoAR.VirtualObject.Base.Mesh;

namespace SmartAutoAR.VirtualObject
{
	public class Model
	{
		public List<Mesh> Meshes { get; }
		public Matrix4 ModelMatrix { get; set; }
		protected string filepath;

		public Model()
		{
			Meshes = new List<Mesh>();
			ModelMatrix = Matrix4.Identity;
		}

		public void Rotation(float x = 0f, float y = 0f, float z = 0f)
		{
			ModelMatrix = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(x)) * ModelMatrix;
			ModelMatrix = Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(y)) * ModelMatrix;
			ModelMatrix = Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(z)) * ModelMatrix;
		}

		public void Move(float x = 0f, float y = 0f, float z = 0f)
		{
			ModelMatrix *= Matrix4.CreateTranslation(x, y, z);
		}

		public void Resize(float percent)
		{
			foreach (Mesh mesh in Meshes)
			{
				mesh.Resize(percent);
			}
		}

		public void Render(Shader shader)
		{
			Matrix4 temp = ModelMatrix;
			GL.UniformMatrix4(shader.GetUniformLocation("model"), false, ref temp);
			foreach (Mesh mesh in Meshes)
			{
				mesh.Render(shader);
			}
		}

		public static Model LoadModel(string path)
		{
			AssimpContext importer = new AssimpContext();
			Assimp.Scene aiScene = importer.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs | PostProcessSteps.GenerateNormals);
			if (aiScene.Equals(null) || aiScene.SceneFlags == SceneFlags.Incomplete || aiScene.RootNode.Equals(null))
			{
				throw new FileLoadException();
			}

			Model model = new Model()
			{
				filepath = path
			};
			ProcessNode(aiScene.RootNode, aiScene, ref model);
			return model;
		}

		private static void ProcessNode(Node node, Assimp.Scene scene, ref Model model)
		{
			for (int i = 0; i < node.MeshCount; i++)
			{
				Assimp.Mesh mesh = scene.Meshes[node.MeshIndices[i]];
				model.Meshes.Add(ProcessMesh(mesh, scene, model.filepath));
			}
			for (int i = 0; i < node.ChildCount; i++)
			{
				ProcessNode(node.Children[i], scene, ref model);
			}
		}

		private static Mesh ProcessMesh(Assimp.Mesh mesh, Assimp.Scene scene, string filepath)
		{
			List<Vertex> vertices = new List<Vertex>();
			List<uint> indices = new List<uint>();

			// vertex
			for (int i = 0; i < mesh.VertexCount; i++)
			{
				Vertex vertex = new Vertex()
				{
					position = new Vector3(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z),
					normal = new Vector3(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z)
				};
				if (mesh.TextureCoordinateChannelCount > 0)
				{
					vertex.texCoord = new Vector2(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y);
				}
				vertices.Add(vertex);
			}
			Mesh output = new Mesh(vertices.ToArray());

			// indices
			if (mesh.HasFaces)
			{
				for (int i = 0; i < mesh.FaceCount; i++)
				{
					Face face = mesh.Faces[i];
					for (int j = 0; j < face.IndexCount; j++)
					{
						indices.Add((uint)face.Indices[j]);
					}
				}
				output.SetIndices(indices.ToArray());
			}

			// material
			if (mesh.MaterialIndex >= 0)
			{
				Assimp.Material material = scene.Materials[mesh.MaterialIndex];
				output.Material = new Base.Material(
					material.HasColorAmbient ? new Color4(material.ColorAmbient.R, material.ColorAmbient.G, material.ColorAmbient.B, material.ColorAmbient.A) : Color4.White,
					material.HasColorDiffuse ? new Color4(material.ColorDiffuse.R, material.ColorDiffuse.G, material.ColorDiffuse.B, material.ColorDiffuse.A) : Color4.White,
					material.HasColorSpecular ? new Color4(material.ColorSpecular.R, material.ColorSpecular.G, material.ColorSpecular.B, material.ColorSpecular.A) : Color4.White,
					material.HasShininess ? material.Shininess : 32
				);
				if (material.HasTextureDiffuse)
				{
					material.GetMaterialTexture(TextureType.Diffuse, 0, out TextureSlot texture1);
					output.SetTexture(Texture.FromFile(@$"{filepath}\..\{texture1.FilePath}"));
				}
			}

			return output;
		}

		public void Dispose()
		{
			foreach (Mesh mesh in Meshes)
			{
				mesh.Dispose();
			}
		}
	}
}
