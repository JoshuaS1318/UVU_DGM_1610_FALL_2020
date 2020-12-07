extends KinematicBody2D

export (int) var health = 30

func _ready():
	pass

func _process(_delta):
	if health <= 0:
		collect()

func hit(weapon):
	if weapon.is_in_group("PlayerLazer"):
		health -= 10
		weapon.queue_free()

func collect():
	var player = get_node_or_null("../Player")

	if player:
		GameManager.score += 10
	queue_free()
