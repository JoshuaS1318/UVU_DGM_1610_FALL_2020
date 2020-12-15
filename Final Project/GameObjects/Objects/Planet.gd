extends KinematicBody2D


var rotation_speed = 0.3;


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

func _process(delta):
	rotate(rotation_speed * delta)

func hit(weapon):
	weapon.queue_free()
