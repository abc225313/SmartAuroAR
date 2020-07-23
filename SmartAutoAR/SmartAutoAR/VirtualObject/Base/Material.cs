using OpenTK.Graphics;

namespace SmartAutoAR.VirtualObject.Base
{
	public struct Material
	{
		public Color4 Ambient;
		public Color4 Diffuse;
		public Color4 Specular;
		public float Shininess;

		public Material(Color4 ambient, Color4 diffuse, Color4 specular, float shininess)
		{
			Ambient = ambient;
			Diffuse = diffuse;
			Specular = specular;
			Shininess = shininess;
		}

		public static Material Common { get { return new Material(Color4.White, Color4.White, Color4.Black, 32); } }
	}
}
