extends KinematicBody

var anim = preload("res://rabbit/rabbit_animations.tres")
var defaultMaterial = preload("res://default.tres")
var evilMaterial = preload("res://evil.tres")
var animation = null
var level = null

# Called when the node enters the scene tree for the first time.
func _ready():
	animation = AnimatedSprite3D.new()
	animation.frames = anim
	animation.playing = true
	animation.animation = "idle"
	animation.pixel_size = 0.03
	add_child(animation)
	
	var shape = CollisionShape.new()
	var capsule = CapsuleShape.new()
	capsule.height = 0.02
	capsule.radius = 0.4
	shape.shape = capsule
	add_child(shape)
	
	collision_mask = 2
	collision_layer = 2
	translation.y = 0.5
	level = get_node("/root/Spatial/Level boi")
	pass # Replace with function body.

var nextMove = 0
var speed = 6
var move = null
var moveTime = null
var lastDir = 1

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	#move randomly
	if randi() % 5 == 1 && nextMove < OS.get_ticks_msec():
		nextMove = OS.get_ticks_msec() + rand_range(1000, 3000)
		moveTime = OS.get_ticks_msec() + 400
		
		#direction
		var dir = randi() % 4
		lastDir = dir
		
		if dir == 0:
			animation.animation = "up"
			move = Vector3.FORWARD * speed * delta
		elif dir == 1:
			animation.animation = "right"
			animation.flip_h = false
			move = Vector3.RIGHT * speed * delta
		elif dir == 2:
			animation.animation = "down"
			move = Vector3.BACK * speed * delta
		elif dir == 3:
			animation.animation = "right"
			animation.flip_h = true
			move = Vector3.LEFT * speed * delta
	
	if move != null && moveTime > OS.get_ticks_msec():
		move_and_collide(move)
	else:
		animation.animation = "idle"
		animation.flip_h = lastDir == 1
		
	if level.flipped:
		animation.material_override = evilMaterial
	else:
		animation.material_override = defaultMaterial
	translation.y = 0.5
	pass
