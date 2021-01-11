extends Area2D


# The travel speed of the lazer
var speed = 1000

func _physics_process(delta):
	# Move the lazer forward every physics frame
	position += Vector2(speed * delta, 0).rotated(rotation)

func _on_Timer_timeout():
	# After the lazers timer runs out it should be deleted
	queue_free()

func _hit(body):
	# If the lazer hits something it should call that things hit function
	if body.has_method("hit"):
		body.hit(self)
