extends Area2D

signal player_out_of_bounds
signal player_in_bounds

func _ready():
	pass


func _on_BorderFog_body_entered(body):
	if body.is_in_group("Player"):
		emit_signal("player_out_of_bounds")


func _on_BorderFog_body_exited(body):
	if body.is_in_group("Player"):
		emit_signal("player_in_bounds")
