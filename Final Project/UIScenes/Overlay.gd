extends CanvasLayer

var player

func _ready():
	if len(get_tree().get_nodes_in_group("Player")) > 0:
		player = get_tree().get_nodes_in_group("Player")[0]
	else:
		 player = null
	
func _process(delta):
	if player:
		transform.origin = player.position
	else:
		if len(get_tree().get_nodes_in_group("Player")) > 0:
			player = get_tree().get_nodes_in_group("Player")[0]
