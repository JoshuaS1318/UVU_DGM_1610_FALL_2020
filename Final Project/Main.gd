extends Node2D

var world_width = 10
var world_height = 10

var cell_width = 500
var world_objects = {
	-1: "player",
	0: "none",
	1: "enemy",
	2: "planet", 
	3: "resource",
}
var player = preload("res://GameObjects/Player.tscn")
var planet = preload("res://GameObjects/Objects/Planet.tscn")
var enemy = preload("res://GameObjects/enemies/enemy.tscn")
var resource = preload("res://GameObjects/Objects/Resource.tscn")

var world = []

func _ready():
	
	
	build_galaxy(build_galaxy_map())

func build_galaxy(map):
	"""Place world objects on the world based on the map"""
	print(GameManager.galaxy_seed)
	for line in map:
		print(line)

	for y in range(world_height-1):
		for x in range(world_width-1):
			match world[y][x]:
				-1:
					var object = player.instance()
					object.position = get_cell_pos(x, y)
				0:
					continue
				1:
					"place enemy"
				2:
					"place planet"
				3:
					"place resource"

func build_galaxy_map():
	"""Return a nested list representing the objects of the galaxy"""
	var rng = RandomNumberGenerator.new()
	rng.randomize()
	
	GameManager.galaxy_seed = rng.get_seed()

	for _y in range(world_height):
		var line = []
		for _x in range(world_width):
			line.append(rng.randi_range(0, 2))
		world.append(line)

	var player_spawn = Vector2(rng.randi_range(0, world_width-1), rng.randi_range(0, world_height-1))
	world[player_spawn[1]][player_spawn[0]] = -1

	return world

func get_cell_pos(x, y):
	"""Return the world coordinates of the virtual x y pos in the world list"""
	return Vector2(x * cell_width, y * cell_width)
