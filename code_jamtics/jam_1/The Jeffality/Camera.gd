extends Camera

var height = 4.5
var distance = 5
var angle = 30

# Called when the node enters the scene tree for the first time.
func _ready():
	rotation = Vector3(deg2rad(-angle), 0, 0)
	pass

var ro = -1

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	translation = get_node("../Player").translation + Vector3(0, height, distance)
	pass