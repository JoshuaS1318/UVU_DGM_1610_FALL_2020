extends KinematicBody2D

# The speed that the planet spins at
var rotation_speed = 0.3;

func _process(delta):
	# Rotate the planet every frame
	rotate(rotation_speed * delta)

func hit(weapon):
	"""Called if the planet is hit by a lazer"""
	# If a lazer hits the planet delete it
	weapon.queue_free()
