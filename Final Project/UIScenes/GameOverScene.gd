extends Control


func _ready():
	print(GameManager.score)
	$Score.text = "Score: " + str(GameManager.score)
