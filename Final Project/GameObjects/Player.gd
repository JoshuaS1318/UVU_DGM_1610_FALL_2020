extends KinematicBody2D

export (float) var speed = 300
export (float) var rotation_speed = 20

var velocity = Vector2()

func _ready():
	pass

	
func _physics_process(_delta):
	get_input()
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


