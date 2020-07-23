using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace SmartAutoAR.VirtualObject.Base
{
	public class Texture : IDisposable
	{
		protected readonly int Handle;
		public double Width { get; protected set; }
		public double Height { get; protected set; }

		public Texture()
		{
			Handle = GL.GenTexture();
			Width = 0;
			Height = 0;
		}

		public static Texture FromFile(string file)
		{
			Texture texture = new Texture();
			using (var image = new Bitmap(file))
			{
				texture.SetImage(image);
			}
			return texture;
		}

		public void SetImage(Bitmap image)
		{
			Use();

			Width = image.Width;
			Height = image.Height;

			try
			{
				var data = image.LockBits(
					new Rectangle(0, 0, image.Width, image.Height),
					ImageLockMode.ReadOnly,
					System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				GL.TexImage2D(TextureTarget.Texture2D,
					0,
					PixelInternalFormat.Rgba,
					image.Width,
					image.Height,
					0,
					PixelFormat.Bgra,
					PixelType.UnsignedByte,
					data.Scan0);

				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

				GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			}
			catch { }
		}

		public void Use(TextureUnit unit = TextureUnit.Texture0)
		{
			GL.ActiveTexture(unit);
			GL.BindTexture(TextureTarget.Texture2D, Handle);
		}

		public void Dispose()
		{
			GL.DeleteTexture(Handle);
			GC.SuppressFinalize(this);
		}
	}
}
