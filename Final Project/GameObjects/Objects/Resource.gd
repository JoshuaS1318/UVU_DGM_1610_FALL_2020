extends KinematicBody2D


export (int) var health = 30

func _process(_delta):
	# If the resource is out of health call its collect function
	if health <= 0:
		collect()

func hit(weapon):
	"""Called if the resource is hit by a lazer"""
	if weapon.is_in_group("PlayerLazer"):
		health -= 10
		weapon.queue_free()

func collect():
	# Increase the players score by 50 and delete the resource
	GameManager.score += 50
	queue_free()
