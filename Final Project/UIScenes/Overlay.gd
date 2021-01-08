extends CanvasLayer

var player

func _ready():
	if len(get_tree().get_nodes_in_group("Player")) > 0:
		player = get_tree().get_nodes_in_group("Player")[0]
	else:
		 player = null
	
func _process(_delta):
	if player:
		transform.origin = player.position
		$HealthBar.value = player.health
		$EnergyBar.value = player.energy
		$FuelBar.value = player.fuel
	else:
		if len(get_tree().get_nodes_in_group("Player")) > 0:
			player = get_tree().get_nodes_in_group("Player")[0]
