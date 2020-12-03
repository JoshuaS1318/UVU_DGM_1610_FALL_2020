extends Area2D

var speed = 1000
var damage = 10

func _ready():
	pass
	
func _physics_process(delta):
	position += Vector2(speed * delta, 0).rotated(rotation)

func _on_Timer_timeout():
	queue_free()
	
func _hit(body):
	if body.has_method("hit"):
		body.hit(self)
