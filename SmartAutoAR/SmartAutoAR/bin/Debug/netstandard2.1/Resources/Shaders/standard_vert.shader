#version 450 core

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 texCoord;
layout(location = 2) in vec3 normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec2 vs_texCoord;
out vec3 vs_normal;
out vec3 frag_position;

void main(void)
{
	gl_Position = projection * view * model * vec4(position, 1.0);
	vs_texCoord = texCoord;
	vs_normal = transpose(inverse(mat3(model))) * normal;
	frag_position = (model * vec4(position, 1.0)).xyz;
}