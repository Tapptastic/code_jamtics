extends Spatial

var brick = preload("res://world/brick.tres")
var brick_wall = preload("res://world/brick-wall.tres")
var dark = preload("res://world/dark.tres")
var rabbit = preload("res://rabbit.gd")

func box(width, height, depth, pos, mode = CSGBox.OPERATION_UNION, material = null, bunny = true):
	var box = CSGBox.new()
	box.width = width
	box.height = height
	box.depth = depth
	box.translation = pos
	box.operation = mode
	box.use_collision = true
	box.material = material
	box.collision_layer = 1 | 2
	
	if bunny && randi() % 5 == 1:
		box.add_child(rabbit.new())
	
	return box
		
func createGround(width, height, center):
	var ground = box(width, 0.01, height, center, CSGBox.OPERATION_UNION, brick)
	ground.name = "Ground"
	self.add_child(ground)

var tall = 4
var mainBox = null
	
func checkBox(room):
	for c in mainBox.get_children():
		if(room.get_aabb().intersects(c.get_aabb())):
			return true
	return false
	
func recurse(box, depth, oldDir):
	var size = box.depth / 2
		
	if(depth >= 5):
		return
	
	var roomSize = rand_range(3, 6)
	
	for dir in range(0, 4):
		if randi() % 2 == 1:
			if dir == 0 && dir != oldDir:
				var room = box(roomSize, tall + 0.01, roomSize, box.translation - Vector3(0.2, 0, size + 0.2 + (roomSize / 2)), CSGBox.OPERATION_SUBTRACTION, brick_wall)
				
				if !checkBox(room):
					mainBox.add_child(box(2, tall + 0.01, 1, box.translation - Vector3(0, 0, size), CSGBox.OPERATION_SUBTRACTION, brick_wall))
					mainBox.add_child(room)
					recurse(room, depth + 1, 2)
			if dir == 1 && dir != oldDir:#left
				var room = box(roomSize, tall + 0.01, roomSize, box.translation - Vector3(size + 0.2 + (roomSize / 2), 0, 0.2), CSGBox.OPERATION_SUBTRACTION, brick_wall)
				
				if !checkBox(room):
					mainBox.add_child(box(1, tall + 0.01, 2, box.translation - Vector3(size, 0, 0), CSGBox.OPERATION_SUBTRACTION, brick_wall))
					mainBox.add_child(room)
					recurse(room, depth + 1, 3)
			if dir == 2 && dir != oldDir:#bottom
				var room = box(roomSize, tall + 0.01, roomSize, box.translation + Vector3(0.2, 0, size + 0.2 + (roomSize / 2)), CSGBox.OPERATION_SUBTRACTION, brick_wall)
				
				if !checkBox(room):
					mainBox.add_child(box(2, tall + 0.01, 1, box.translation + Vector3(0, 0, size), CSGBox.OPERATION_SUBTRACTION, brick_wall))
					mainBox.add_child(room)
					recurse(room, depth + 1, 0)
			if dir == 3 && dir != oldDir:#right
				var room = box(roomSize, tall + 0.01, roomSize, box.translation + Vector3(size + 0.2 + (roomSize / 2), 0, 0.2), CSGBox.OPERATION_SUBTRACTION, brick_wall)
				
				if !checkBox(room):
					mainBox.add_child(box(1, tall + 0.01, 2, box.translation + Vector3(size, 0, 0), CSGBox.OPERATION_SUBTRACTION, brick_wall))
					mainBox.add_child(room)
					recurse(room, depth + 1, 1)

# Called when the node enters the scene tree for the first time.
func _ready():
	randomize()
	createGround(100, 100, Vector3.ZERO)
	
	var roomSize = rand_range(5, 10)
	
	mainBox = box(100, tall, 100, Vector3.ZERO, CSGBox.OPERATION_UNION, dark, false)
	
	var middle = box(roomSize, tall + 0.01, roomSize, Vector3(0, 0, 0), CSGBox.OPERATION_SUBTRACTION, brick_wall, false)
	mainBox.add_child(middle)
	self.add_child(mainBox)
	
	recurse(middle, 0, -1)
	
	pass
	
var flipping = false
var flip = -1
var time = 0
var flipped = false

onready var light = get_node("../DirectionalLight")

func _process(delta):
	if flipping && (flip - OS.get_ticks_msec()) > 0:
		#half way
		if (flip - OS.get_ticks_msec()) < 500:
			light.light_negative = flipped
		
		time += delta
		rotation.x = deg2rad(lerp(0, 360, time))
	else:
		flipping = false
	pass
	
func _input(input):
	if input.is_action_pressed("switch"):
		flip = OS.get_ticks_msec() + 1000
		time = 0
		flipping = true
		flipped = !flipped
		
	pass