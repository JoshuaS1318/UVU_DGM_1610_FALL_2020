extends Control


func _ready():
	# Set the score label to the players score
	$Score.text = "Score: " + str(GameManager.score)


func _on_PlayAgainButton_pressed():
	# Start the game over with the same seed
	GameManager.play_again = true
	var _err = get_tree().change_scene("res://WorldGeneration.tscn")
	
func _on_New_Galaxy_pressed():
	# Start the game over
	var _err = get_tree().change_scene("res://WorldGeneration.tscn")
