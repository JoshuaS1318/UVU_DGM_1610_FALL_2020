extends KinematicBody2D

export (int) var health = 20
var player_in_range = false
var player
var vision_range = 750
var rotation_speed = 20
var speed = 200
var velocity = Vector2()
var cooldown = true
var lazer

func _ready():
	lazer = preload("res://GameObjects/Weapons/lazer.tscn")

func _process(_delta):
	if health <= 0:
		death()

func _physics_process(_delta):
	if player_in_range:
		attack_player()
	else:
		look_for_player()
	
	# Move
	velocity = move_and_slide(velocity)

func attack_player():
	# Look at the player
	var dir = get_angle_to(player.position)
	
	if abs(dir) < rotation_speed:
		rotation += dir
	else:
		if dir > 0: rotation += rotation_speed
		if dir < 0: rotation -= rotation_speed

	# Move towards the player
	velocity = Vector2(speed, 0).rotated(rotation)

	if global_position.distance_to(player.position) < 400:
		velocity = Vector2(0, 0)

	# Lose interest if the player runs away
	if position.distance_to(player.position) > vision_range * 1.5:
		player_in_range = false
		velocity = Vector2(0, 0)

	if cooldown == false:
		fire_weapon()
	

func look_for_player():
	pass

func _found_player(body):
	# check if body is the player
	if body.is_in_group("Player"):
		player_in_range = true
		# Get a reference to the player
		player = body
		# Start weapons cooldown
		$CoolDownTimer.start()

func _on_CoolDownTimer_timeout():
	cooldown = false
	
func fire_weapon():
	# Get a lazer instance
	var weapon = lazer.instance()
	# Set the lazer the the players position
	weapon.position = position
	weapon.rotation = rotation
	weapon.add_to_group("EnemyLazer")
	# Add the lazer to the scene
	get_parent().add_child(weapon)
	
	# Start cooldown
	cooldown = true
	$CoolDownTimer.start()

func death():
	queue_free()

func hit(weapon):
	if weapon.is_in_group("PlayerLazer"):
		if player:
			player_in_range = true
		else:
			player = get_node_or_null("../Player")
			if player:
				player_in_range = true

		health -= 10
		weapon.queue_free()
