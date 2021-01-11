extends Node2D


# How many different cells wide and tall te game should be
var world_width = GameManager.world_width
var world_height = GameManager.world_height

# How large each cell should be
var cell_width = GameManager.cell_width
# The numbers that corespons with certain game objects
# -1: player
# 0: none
# 1: enemy
# 2: planet
# 3: resource
# 4: powerup

# Load references to all the game objects that must be placed in the world
onready var player = preload("res://GameObjects/Player.tscn")
onready var planet = preload("res://GameObjects/Objects/Planet.tscn")
onready var enemy = preload("res://GameObjects/enemies/enemy.tscn")
onready var enemy2 = preload("res://GameObjects/enemies/enemy2.tscn")
onready var resource = preload("res://GameObjects/Objects/Resource.tscn")
onready var powerup = preload("res://GameObjects/Objects/Powerup.tscn")

# Create a random number generator to be used for world generation
var rng = RandomNumberGenerator.new()
# Create an array to store the map of the world
var world = []

func _ready():
	# If the player wants to play the same galaxy again use the old seed
	if GameManager.play_again:
		rng.set_seed(GameManager.galaxy_seed)
		GameManager.play_again = false
	# Else get a new seed
	else:
		rng.randomize()

	# Store the galaxy seed
	GameManager.galaxy_seed = rng.get_seed()

	# Generate a map of the galaxy
	build_galaxy_map()
	# Poplulate the galaxy with game objects
	build_galaxy()

func build_galaxy():
	"""Place world objects on the world based on the map"""

	# Fill every cell in the galaxy
	for y in range(world_height):
		for x in range(world_width):
			match world[y][x]:
				# Place the player
				-1:
					new_player(x, y)
				# Leave the cell empty
				0:
					continue
				# Spawn an enemy
				1:
					new_enemy(x, y)
				# Spawn a planet
				2:
					new_planet(x, y)
				# Spawn a resource
				3:
					new_resource(x, y)
				# Spawn a powerup
				4:
					new_powerup(x, y)

func build_galaxy_map():
	"""Return a nested list representing the objects of the galaxy"""
	
	for _y in range(world_height):
		
		var line = []
		for _x in range(world_width):
			# Add a radom number to the world map to represent a game object
			line.append(rng.randi_range(0, 4))
		# Add a row to the world map
		world.append(line)

	# Get a random position for the play
	var player_spawn = Vector2(rng.randi_range(0, world_width-1), rng.randi_range(0, world_height-1))
	# Set the random position to -1 to represent the player
	world[player_spawn[1]][player_spawn[0]] = -1
	
	return world

func new_player(x, y):
	"""Spawn the player in the x, y cell of the galaxy"""
	# Get an insance of the player
	var object = player.instance()
	# Get the pixel coordinates of the players cell
	var cell_pos = get_cell_pos(x, y)
	# Get the size of the players sprite
	var size = object.get_node("Sprite").texture.get_size()
	# Add the player to the scene
	add_child(object)
	# Set the players position to a random spot in its cell
	object.position = Vector2(rng.randi_range(cell_pos[0] + 50 + (size[0] / 2), cell_pos[0] + cell_width - 50 - (size[0] / 2)), rng.randi_range(cell_pos[1] + 50 + (size[1] / 2), cell_pos[1] + cell_width - 50 - (size[1] / 2)))
	# Set the players camera as the main camera
	object.get_node("Camera").make_current()

func new_planet(x, y):
	"""Spawn a new planet at the x, y cell of the galaxy"""
	# Create a planet object
	var object = planet.instance()
	# Get the position for the cell for the planet
	var cell_pos = get_cell_pos(x, y)
	# get the size of the object
	var size = object.get_node("Sprite").texture.get_size()
	# Set a random Position within the cell
	object.position = Vector2(rng.randi_range(cell_pos[0] + 50 + (size[0] / 2), cell_pos[0] + cell_width - 50 - (size[0] / 2)), rng.randi_range(cell_pos[1] + 50 + (size[1] / 2), cell_pos[1] + cell_width - 50 - (size[1] / 2)))
	# Set a random color for the planet
	object.get_node("Sprite").modulate = Color(rng.randf_range(0, 1), rng.randf_range(0, 1), rng.randf_range(0, 1))
	# Add the planet to the scene
	add_child(object)

func new_enemy(x, y):
	"""Spawn a new enemy at the x, y cell of the galaxy"""
	# Create a random enemy object
	var object
	# If the random number is 0 use enemy 1 if it is 1 use enemy 2
	if rng.randi_range(0, 1) == 0:
		object = enemy.instance()
	else:
		object = enemy2.instance()
	# Get the position for the cell for the enemy
	var cell_pos = get_cell_pos(x, y)
	# get the size of the object
	var size = object.get_node("Sprite").texture.get_size()
	# Set a random Position within the cell
	object.position = Vector2(rng.randi_range(cell_pos[0] + 50 + (size[0] / 2), cell_pos[0] + cell_width - 50 - (size[0] / 2)), rng.randi_range(cell_pos[1] + 50 + (size[1] / 2), cell_pos[1] + cell_width - 50 - (size[1] / 2)))
	# Add the enemy to hte scene
	add_child(object)

func new_resource(x, y):
	"""Spawn a new resource in the galaxy at the x, y cell"""
	# Create a resource object
	var object = resource.instance()
	# Get the position of the cell for the resource
	var cell_pos = get_cell_pos(x, y)
	# get the size of the object
	var size = object.get_node("Sprite").texture.get_size()
	# Give the resource a random rotation
	object.rotate(deg2rad(rng.randi_range(0, 360)))
	# Set a random Position within the cell
	object.position = Vector2(rng.randi_range(cell_pos[0] + 50 + (size[0] / 2), cell_pos[0] + cell_width - 50 - (size[0] / 2)), rng.randi_range(cell_pos[1] + 50 + (size[1] / 2), cell_pos[1] + cell_width - 50 - (size[1] / 2)))
	# Add the resource to the scene
	add_child(object)

func new_powerup(x, y):
	"""Spawn a new powerup at the x, y cell in the galaxy"""
	# Create a powerup object
	var object = powerup.instance()
	# Select a random powerup
	# 0 = health 1 = energy 2 = fuel
	object.set_random_powerup(rng.randi_range(0, 2))
	# Get the position of the cell for the powerup
	var cell_pos = get_cell_pos(x, y)
	# get the size of the object
	var size = object.get_node("Sprite").texture.get_size()
	# Give the powerup a random rotation
	object.rotate(deg2rad(rng.randi_range(0, 360)))
	# Set a random Position within the cell
	object.position = Vector2(rng.randi_range(cell_pos[0] + 50 + (size[0] / 2), cell_pos[0] + cell_width - 50 - (size[0] / 2)), rng.randi_range(cell_pos[1] + 50 + (size[1] / 2), cell_pos[1] + cell_width - 50 - (size[1] / 2)))
	# Add the poweup to the scene
	add_child(object)


func get_cell_pos(x, y):
	"""Return the world coordinates of the virtual x y pos in the world array"""
	return Vector2(x * cell_width, y * cell_width)
