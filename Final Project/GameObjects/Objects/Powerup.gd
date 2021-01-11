extends KinematicBody2D


# The powerup has 1 health so that it can be gathered in one hit
export (int) var hp = 1

# Load the sprites for the three different powerups
var health = preload("res://GameObjects/Objects/health.tres")
var energy = preload("res://GameObjects/Objects/energy.tres")
var fuel = preload("res://GameObjects/Objects/fuel.tres")

# The number for what type of powerup it is
var type

func _process(_delta):
	# Check the health of the powerup every frame to make sure it is not destroyed
	if hp <= 0:
		collect()

func set_random_powerup(index):
	"""Should be called by the world generator to randomly assign a powerup type"""
	type = index

	# If type is = to 0 then it is a health powerup
	if type == 0:
		$Sprite.texture = health

	# If type is = to 1 then it is a energy powerup 
	elif type == 1:
		$Sprite.texture = energy

	# If type is = to 2 then it is a fuel powerup
	elif type == 2:
		$Sprite.texture = fuel

func hit(weapon):
	"""If the powerup is hit by a player lazer then break it"""
	if weapon.is_in_group("PlayerLazer"):
		hp -= 1
		weapon.queue_free()

# If the powerup is destroyed heal the player
func collect():
	# Attempt to get a copy of the play
	var player
	if len(get_tree().get_nodes_in_group("Player")) > 0:
		player = get_tree().get_nodes_in_group("Player")[0]
	else:
		 player = null

	# If the player was successfully found heal it
	if player:
		if type == 0:
			player.health += 50
		elif type == 1:
			player.energy += 50
		elif type == 2:
			player.fuel += 50

	# Delete the powerup
	queue_free()
