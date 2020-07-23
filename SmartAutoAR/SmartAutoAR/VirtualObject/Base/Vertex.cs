using OpenTK;

namespace SmartAutoAR.VirtualObject.Base
{
	public struct Vertex
	{
		// The size of a vertex
		public const int Size = (3 + 2 + 3) * sizeof(float);

		public Vector3 position;

		// This field could be the color(r,g,b,a) of the vertex
		// or the coordinate of textures
		public Vector2 texCoord;

		public Vector3 normal;

		public Vertex(Vector3 position, Vector2 texCoord, Vector3 normal)
		{
			this.position = position;
			this.texCoord = texCoord;
			this.normal = normal;
		}
	}
}
