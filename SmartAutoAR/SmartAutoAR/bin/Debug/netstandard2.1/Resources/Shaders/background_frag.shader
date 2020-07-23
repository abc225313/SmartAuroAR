#version 450 core

in vec2 vs_textureCoordinate;

uniform sampler2D texture0;

out vec4 color;

void main(void)
{
	color = texture(texture0, vs_textureCoordinate);
}