"""Stores globals for the entire game"""
extends Node


# The players score
export (int) var score
# The wold generations random seed
export (int) var galaxy_seed
# The width and height of the world in cells
export (int) var world_width
export (int) var world_height
# The size of a world cell
export (int) var cell_width
# Whether or not the same seed should be used again
export (bool) var play_again

func _ready():
	score = 0
	world_width = 30
	world_height = 30
	cell_width = 1500
	galaxy_seed = null
	play_again = false
