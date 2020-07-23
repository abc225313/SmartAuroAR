#version 450 core

struct AmbientLight {
	vec4 color;
	float main_strength;
};

struct PointLight {
	vec4 color;
	vec3 position;
	float main_strength;
	float specular_strength;
};

struct DirectionalLight {
	vec4 color;
	vec3 direction;
	float main_strength;
	float specular_strength;
};

struct Light {
	vec4 color;
	vec3 position;
	float ambient_strength;
	float diffuse_strength;
	float specular_strength;
};

struct Material {
	vec4 ambient;
	vec4 diffuse;
	vec4 specular;

	float shininess;
};

in vec3 vs_normal;
in vec2 vs_texCoord;
in vec3 frag_position;

uniform vec3 lights_num;
uniform AmbientLight ambient_lights[8];
uniform PointLight point_lights[8];
uniform DirectionalLight directional_lights[8];

uniform vec3 view_position;
uniform sampler2D texture0;
uniform uint useTexture;
uniform Material material;

out vec4 color;


vec4 CalcAmbLight(AmbientLight light) {
	vec4 ambient = light.main_strength * light.color * material.ambient;
	return ambient;
}

vec4 CalcPoiLight(PointLight light, vec3 normal, vec3 viewDir) {
	// diffuse
	vec3 lightDir = normalize(light.position - frag_position);
	float diff = max(dot(normal, lightDir), 0.0);

	// specular
	vec3 reflectDir = reflect(-lightDir, normal);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	vec4 diffuse = (light.main_strength * light.color) * (diff * material.diffuse);
	vec4 specular = (light.color * light.specular_strength) * (spec * material.specular);

	return (diffuse + specular);
}

vec4 CalcDirLight(DirectionalLight light, vec3 normal, vec3 viewDir) {
	// diffuse
	vec3 lightDir = normalize(-light.direction);
	float diff = max(dot(normal, lightDir), 0.0);

	// specular
	vec3 reflectDir = reflect(-lightDir, normal);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	vec4 diffuse = light.main_strength * light.color * diff * material.diffuse;
	vec4 specular = (light.color * light.specular_strength) * (spec * material.specular);
	return (diffuse + specular);
}


void main(void)
{
	vec4 result = vec4(0.0, 0.0, 0.0, 0.0);
	vec3 norm = normalize(vs_normal);
	vec3 viewDir = normalize(view_position - frag_position);

	for (int i = 0; i < lights_num.x; i++) {
		result += CalcAmbLight(ambient_lights[i]);
	}

	for (int i = 0; i < lights_num.y; i++) {
		result += CalcPoiLight(point_lights[i], norm, viewDir);
	}

	for (int i = 0; i < lights_num.z; i++) {
		result += CalcDirLight(directional_lights[i], norm, viewDir);
	}

	if (useTexture > 0) {
		color = result * texture(texture0, vs_texCoord);
	}
	else {
		color = result;
	}
}