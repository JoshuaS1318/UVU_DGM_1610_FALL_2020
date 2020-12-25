extends Control


func _ready():
	$Score.text = "Score: " + str(GameManager.score)
