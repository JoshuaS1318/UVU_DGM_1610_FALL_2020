extends KinematicBody2D

export (float) var speed = 400
export (float) var rotation_speed = 20
export (float) var health = 100

# Players velocity
var velocity = Vector2()
# Players lazer weapon
var lazer
# Player weapon cooldown
var cooldown = false

var in_bounds = true

func _ready():
	# Load the lazer scene so you can instance it later
	lazer = preload("res://GameObjects/Weapons/lazer.tscn")
	$FireTrail.

func _process(delta):
	if health <= 0:
		death()
	
	if not in_bounds:
		print(health)
		health -= 10 * delta

func _physics_process(_delta):
	# Handle player input
	get_input()
	# Move the player
	velocity = move_and_slide(velocity)

# Handle the inputs for the player
func get_input():
	# Look at the mouse
	var dir = get_angle_to(get_global_mouse_position())
	
	if abs(dir) < rotation_speed:
		rotation += dir
	else:
		if dir > 0: rotation += rotation_speed
		if dir < 0: rotation -= rotation_speed

	# If one of the actions under forward is pressed move forward
	if Input.is_action_pressed('forward'):
		velocity = Vector2(speed, 0).rotated(rotation)
	else:
		# stop moving
		velocity = Vector2(0, 0)

	# Fire the weapon if the player hits a fire weapon button
	if Input.is_action_pressed("fire_weapon") and cooldown == false:
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