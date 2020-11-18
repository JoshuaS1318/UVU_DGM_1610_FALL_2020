extends KinematicBody2D

export (float) var speed = 200
export (float) var rotation_speed = 1

var velocity = Vector2()

func _ready():
	pass # Replace with function body.

	
func _physics_process(delta):
	get_input()
	velocity = move_and_slide(velocity)

# Handle the inputs for the player
func get_input():
	# Look at the mouse
	var m = get_global_mouse_position()

	rotation_speed = deg2rad(rotation_speed)
	if get_angle_to(m) > 0:
		rotation += rotation_speed
	else:
		rotation -= rotation_speed
	
	rotation = lerp_angle(rotation, get_angle_to(get_global_mouse_position()), 0.1)

	# If one of the actions under forward is pressed move forward
	if Input.is_action_pressed('forward'):
		velocity = Vector2(speed, 0).rotated(rotation)
	else:
		# stop moving
		velocity = Vector2(0, 0)
