"""Contains the code for enemy2 or the melee enemy"""
extends KinematicBody2D


export (int) var health = 50
# Whether or not the enemy is close enough to the player to lock on
var player_in_range = false
# A reference to the player
var player
# The range that the enemy can see the player in
var vision_range = 750
# Movement speeds
var rotation_speed = 20
var speed = 250
var velocity = Vector2()

func _process(_delta):
	# Delete the enemy if it runs out of health
	if health <= 0:
		death()

func _physics_process(_delta):
	if player_in_range:
		# Attack the player
		attack_player()
		# Spin blades
		$Blade1.rotate(deg2rad(3))
		$Blade2.rotate(deg2rad(3))

	# Move
	velocity = move_and_slide(velocity)

func attack_player():
	# Look at the player
	var dir = get_angle_to(player.position)
	
	# Rotateto face the player
	if abs(dir) < rotation_speed:
		rotation += dir
	else:
		if dir > 0: rotation += rotation_speed
		if dir < 0: rotation -= rotation_speed

	# Move towards the player
	velocity = Vector2(speed, 0).rotated(rotation)

	# If the distance to the player is small enough stop moving closer and start doing damage
	if global_position.distance_to(player.position) < 160:
		velocity = Vector2(0, 0)
		player.hit(self)

	# Lose interest if the player runs away
	if position.distance_to(player.position) > vision_range * 1.5:
		player_in_range = false
		velocity = Vector2(0, 0)

func _found_player(body):
	# check if body is the player
	if body.is_in_group("Player"):
		player_in_range = true
		# Get a reference to the player
		player = body

func death():
	# Increase the player score by 10 and delete the enemy
	GameManager.score += 10
	queue_free()

func hit(weapon):
	"""Called if the enemy is hit by a lazer"""
	# If the weapon is a player lazer
	if weapon.is_in_group("PlayerLazer"):
		# If there is a reference to the player set player in range to true
		if player:
			player_in_range = true
		else:
			# If not try to get a reference and if successful et player in range to true
			player = get_node_or_null("../Player")
			if player:
				player_in_range = true

		# Decrase health by 10 and delete the lazer
		health -= 10
		weapon.queue_free()
