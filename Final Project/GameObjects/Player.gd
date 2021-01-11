extends KinematicBody2D

# Player speeds
var speed = 400
var rotation_speed = 20
# Player information
var health = 100
var fuel = 100
var energy = 100

# Players velocity
var velocity = Vector2() 
# Players lazer weapon
var lazer = preload("res://GameObjects/Weapons/lazer.tscn")
# Player weapon cooldown
var cooldown = false
# Whether or not the player should be warned about being out of bounds
var warning = false

func _process(delta):
	# If the player stats go over 100 set them back to 100
	if health > 100:
		health = 100
	if energy > 100:
		energy = 100
	if fuel > 100:
		fuel = 100
	
	if health <= 0:
		# If the player runs out of health kill them
		death()

	# Check if the player is in the games bounds
	bounds()
	
	# Warn and damage the player if they are out of bounds
	if warning:
		$Warning.global_rotation = 0
		health -= 3 * delta
		$Warning.visible = true
	else:
		$Warning.visible = false

func _physics_process(delta):
	# Handle player input
	get_input(delta)
	# Move the player
	velocity = move_and_slide(velocity)
	
	# If the player runs out of health kill them
	if fuel < 0:
		health -= 10 * delta

# Handle the inputs for the player
func get_input(delta):
	# Look at the mouse
	var dir = get_angle_to(get_global_mouse_position())
	if abs(dir) < rotation_speed:
		rotation += dir
	else:
		if dir > 0: rotation += rotation_speed
		if dir < 0: rotation -= rotation_speed

	# If one of the actions under forward is pressed move forward
	if Input.is_action_pressed('forward') and fuel > 0:
		# Activate fire trail
		$FireTrail.set_emitting(true)
		# Lose fuel
		fuel -= 1 * delta

		velocity = Vector2(speed, 0).rotated(rotation)
	else:
		$FireTrail.set_emitting(false)
		
		# stop moving
		velocity = Vector2(0, 0)

	# Fire the weapon if the player hits a fire weapon button and they have energy
	if Input.is_action_pressed("fire_weapon") and cooldown == false and energy > 0:
		energy -= 2
		fire_weapon()

func fire_weapon():
	# Get a lazer instance
	var weapon = lazer.instance()
	# Set the lazers postion to the the players position
	weapon.position = position
	weapon.rotation = rotation
	# Add the weapon to the PlayerLazer group
	weapon.add_to_group("PlayerLazer")
	# Add the lazer to the scene
	get_parent().add_child(weapon)

	# Start cooldown
	cooldown = true
	$CooldownTimer.start()

func _on_Timer_timeout():
	# Stop the cooldown
	cooldown = false

func hit(weapon):
	"""Called when the player is hit"""
	if weapon.is_in_group("EnemyLazer"):
		# If the player is hit by an enemy lazer lose 10 health and delete the lazer
		health -= 10
		weapon.queue_free()

	if weapon.is_in_group("Melee"):
		# If the player collides with the melee enemy lose 0.3 health
		health -= 0.3

func death():
	# Hide the player and change the scene
	visible = false
	var _err = get_tree().change_scene("res://UIScenes/GameOverScene.tscn")

func bounds():
	# If the player goes out of bounds set warning to true
	if position.x < 0 or position.x > GameManager.world_width * GameManager.cell_width or position.y < 0 or position.y > GameManager.world_height * GameManager.cell_width:
		warning = true
	# Set warning to false if the player goes back in bounds
	else:
		warning = false
