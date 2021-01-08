extends KinematicBody2D

export (float) var speed
export (float) var rotation_speed
export (float) var health
export (float) var fuel
export (float) var energy

# Players velocity
var velocity = Vector2()
# Players lazer weapon
var lazer
# Player weapon cooldown
var cooldown = false

var in_bounds = true

func _ready():
	speed = 400
	rotation_speed = 20
	health = 100
	fuel = 100
	energy = 100
	
	# Load the lazer scene so you can instance it later
	lazer = preload("res://GameObjects/Weapons/lazer.tscn")
	$FireTrail.set_emitting(false)

func _process(delta):
	if health <= 0:
		death()
	
	if not in_bounds:
		print(health)
		health -= 10 * delta

func _physics_process(delta):
	# Handle player input
	get_input(delta)
	# Move the player
	velocity = move_and_slide(velocity)
	
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
		$FireTrail.set_emitting(true)
		fuel -= 1 * delta
		
		velocity = Vector2(speed, 0).rotated(rotation)
	else:
		$FireTrail.set_emitting(false)
		
		# stop moving
		velocity = Vector2(0, 0)

	# Fire the weapon if the player hits a fire weapon button
	if Input.is_action_pressed("fire_weapon") and cooldown == false and energy > 0:
		energy -= 2
		fire_weapon()

func fire_weapon():
	# Get a lazer instance
	var weapon = lazer.instance()
	# Set the lazer the the players position
	weapon.position = position
	weapon.rotation = rotation
	weapon.add_to_group("PlayerLazer")
	# Add the lazer to the scene
	get_parent().add_child(weapon)

	# Start cooldown
	cooldown = true
	$Timer.start()

func _on_Timer_timeout():
	cooldown = false

func hit(weapon):
	if weapon.is_in_group("EnemyLazer"):
		health -= 10
		weapon.queue_free()

func death():
	visible = false
	var _err = get_tree().change_scene("res://UIScenes/GameOverScene.tscn")

func _on_Player_player_in_bounds():
	in_bounds = true

func _on_Player_player_out_of_bounds():
	in_bounds = false
