extends CanvasLayer


# Reference to the player
var player

func _ready():
	# Attempt to get a copy of the player
	if len(get_tree().get_nodes_in_group("Player")) > 0:
		player = get_tree().get_nodes_in_group("Player")[0]
	else:
		 player = null
	
func _process(_delta):
	# If there is a copy of the player set the bars to the players stats
	if player:
		$HealthBar.value = player.health
		$EnergyBar.value = player.energy
		$FuelBar.value = player.fuel
		# Show the players score
		$ScoreLabel.text = str(GameManager.score)
	# Else try to get a copy of the player again
	else:
		if len(get_tree().get_nodes_in_group("Player")) > 0:
			player = get_tree().get_nodes_in_group("Player")[0]
