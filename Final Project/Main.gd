extends Node2D

var world_width = 30
var world_height = 30

var cell_width = 1500
#var world_objects = {
#	-1: "player",
#	0: "none",
#	1: "enemy",
#	2: "planet", 
#	3: "resource",
#}
onready var player = preload("res://GameObjects/Player.tscn")
onready var planet = preload("res://GameObjects/Objects/Planet.tscn")
onready var enemy = preload("res://GameObjects/enemies/enemy.tscn")
onready var resource = preload("res://GameObjects/Objects/Resource.tscn")

var rng
var world = []

func _ready():
	rng = RandomNumberGenerator.new()
	rng.randomize()
	
	GameManager.galaxy_seed = rng.get_seed()
	
	build_galaxy(build_galaxy_map())

func build_galaxy(map):
	"""Place world objects on the world based on the map"""
	print(GameManager.galaxy_seed)
	for line in map:
		print(line)

	for y in range(world_height):
		for x in range(world_width):
			match world[y][x]:
				-1:
					var object = player.instance()
					add_child(object)
					object.position = get_cell_pos(x, y)
					object.get_node("Camera").make_current()
				0:
					continue
				1:
					new_enemy(x, y)
				2:
					new_planet(x, y)
				3:
					new_resource(x, y)

func build_galaxy_map():
	"""Return a nested list representing the objects of the galaxy"""
	

	for _y in range(world_height):
		var line = []
		for _x in range(world_width):
			line.append(rng.randi_range(0, 3))
		world.append(line)

	var player_spawn = Vector2(rng.randi_range(0, world_width-1), rng.randi_range(0, world_height-1))
	world[player_spawn[1]][player_spawn[0]] = -1
	
	return world

func new_planet(x, y):
	# Create a planet object
	var object = planet.instance()
	# Get the position for the cell for the planet
	var cell_pos = get_cell_pos(x, y)
	# get the size of the object
	var size = object.get_node("Sprite").texture.get_size()
	# Set a random Position within the cell
	object.position = Vector2(rng.randi_range(cell_pos[0] + 50 + (size[0] / 2), cell_pos[0] + cell_width - 50 - (size[0] / 2)), rng.randi_range(cell_pos[1] + 50 + (size[1] / 2), cell_pos[1] + cell_width - 50 - (size[1] / 2)))
	object.get_node("Sprite").modulate = Color(rng.randf_range(0, 1), rng.randf_range(0, 1), rng.randf_range(0, 1))
	add_child(object)

func new_enemy(x, y):
	# Create a planet object
	var object = enemy.instance()
	# Get the position for the cell for the enemy
	var cell_pos = get_cell_pos(x, y)
	# get the size of the object
	var size = object.get_node("Sprite").texture.get_size()
	# Set a random Position within the cell
	object.position = Vector2(rng.randi_range(cell_pos[0] + 50 + (size[0] / 2), cell_pos[0] + cell_width - 50 - (size[0] / 2)), rng.randi_range(cell_pos[1] + 50 + (size[1] / 2), cell_pos[1] + cell_width - 50 - (size[1] / 2)))
	add_child(object)

func new_resource(x, y):
	# Create a planet object
	var object = resource.instance()
	# Get the position for the cell for the enemy
	var cell_pos = get_cell_pos(x, y)
	# get the size of the object
	var size = object.get_node("Sprite").texture.get_size()
	# Set a random Position within the cell
	object.position = Vector2(rng.randi_range(cell_pos[0] + 50 + (size[0] / 2), cell_pos[0] + cell_width - 50 - (size[0] / 2)), rng.randi_range(cell_pos[1] + 50 + (size[1] / 2), cell_pos[1] + cell_width - 50 - (size[1] / 2)))
	add_child(object)


func get_cell_pos(x, y):
	"""Return the world coordinates of the virtual x y pos in the world list"""
	return Vector2(x * cell_width, y * cell_width)
