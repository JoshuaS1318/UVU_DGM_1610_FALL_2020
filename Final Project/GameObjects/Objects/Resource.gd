extends KinematicBody2D

export (int) var health = 30


# Check that the resource is not destroyed
func _process(_delta):
	if health <= 0:
		collect()

# Decrease the health of the resource if it is hit by a player lazer and destroy the player lazer
func hit(weapon):
	if weapon.is_in_group("PlayerLazer"):
		health -= 10
		weapon.queue_free()

# If the resource is destroyed add to the player score and destroy the resource
func collect():
	GameManager.score += 10
	queue_free()
