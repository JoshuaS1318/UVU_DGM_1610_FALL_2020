"""Code for enemy1 or the lazer enemy"""
extends KinematicBody2D

export (int) var health = 20
# Whether or not the player is in the enemies range
var player_in_range = false
# Reference to the player
var player
# The size of the enemies field of view
var vision_range = 750
# Enemies movement speed
var rotation_speed = 20
var speed = 200
var velocity = Vector2()
# Whether or not the enemies weapons are on cooldown
var cooldown = true
# A reference to the enemies weapons
var lazer = preload("res://GameObjects/Weapons/lazer.tscn")

func _process(_delta):
	# If the enemy runs out of health delete it
	if health <= 0:
		death()

func _physics_process(_delta):
	# If the player in range attack it
	if player_in_range:
		attack_player()
		
	# Move
	velocity = move_and_slide(velocity)

func attack_player():
	# Look at the player
	var dir = get_angle_to(player.position)
	
	# Rotate to face the player
	if abs(dir) < rotation_speed:
		rotation += dir
	else:
		if dir > 0: rotation += rotation_speed
		if dir < 0: rotation -= rotation_speed

	# Move towards the player
	velocity = Vector2(speed, 0).rotated(rotation)

	# If the enemy starts getting close enough the player it will stop getting closer
	if global_position.distance_to(player.position) < 400:
		velocity = Vector2(0, 0)

	# Lose interest if the player runs away
	if position.distance_to(player.position) > vision_range * 1.5:
		player_in_range = false
		velocity = Vector2(0, 0)

	# Fire weapons if the cooldown is over
	if cooldown == false:
		fire_weapon()

func _found_player(body):
	# check if body is the player
	if body.is_in_group("Player"):
		player_in_range = true
		# Get a reference to the player
		player = body
		# Start weapons cooldown
		$CooldownTimer.start()

func _on_CoolDownTimer_timeout():
	# When the weapons cooldown ends set cooldown to false
	cooldown = false
	
func fire_weapon():
	# Get a lazer instance
	var weapon = lazer.instance()
	# Set the lazers position to the the enemies position
	weapon.position = position
	weapon.rotation = rotation
	# Add the lazer to the EnemyLazer group
	weapon.add_to_group("EnemyLazer")
	# Add the lazer to the scene
	get_parent().add_child(weapon)
	
	# Start cooldown
	cooldown = true
	$CooldownTimer.start()

func death():
	# Delete the enemy and add 10 to the players score
	GameManager.score += 10
	queue_free()

func hit(weapon):
	"""If the enemy is hit by a lazer lose health and look for the player"""
	# If the lazer is a player lazer
	if weapon.is_in_group("PlayerLazer"):
		# If there is a reference to the player set player in range to true
		if player:
			player_in_range = true
		# If there is no reference to the player try to get one
		else:
			player = get_node_or_null("../Player")
			# If a reference to the player is obtained successfully set player in range to true
			if player:
				player_in_range = true

		# Decrease health and delete the lazer
		health -= 10
		weapon.queue_free()
