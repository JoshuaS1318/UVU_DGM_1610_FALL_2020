extends Node

var player


func _ready():
	player = get_node("../Player")



func _process(delta):
	$MainCamera.position = player.position
