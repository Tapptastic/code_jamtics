extends KinematicBody

var speed = 8
onready var sprite = get_node("AnimatedSprite3D")
onready var level = get_node("../Level boi")
var ontop = preload("PlayerOnTop.tres")
var defaultMaterial = preload("res://default.tres")

# Called when the node enters the scene tree for the first time.
func _ready():
	#Fix height of the player
	translation = Vector3(0, get_node("CollisionShape").shape.height, 0)
	sprite.playing = true
	pass

var lastDir = 0
func move(dir):
	sprite.flip_h = dir.normalized().x < 0.1
	lastDir = dir.normalized().x
	
	if dir.normalized() == Vector3.FORWARD:
		if sprite.animation != "run_up":
			sprite.animation = "run_up"
	elif dir.normalized() == Vector3.BACK:
		if sprite.animation != "run_down":
			sprite.animation = "run_down"
	elif(sprite.animation != "run"):
		sprite.animation = "run"
		
	move_and_collide(dir)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	
	if level.flipping:
		sprite.material_override = ontop
		sprite.animation = "summon"
		return
	elif(sprite.material_override != defaultMaterial):
		sprite.material_override = defaultMaterial
	
	var move = Vector3.ZERO
	
	if(Input.is_action_pressed("ui_up")):
		move += Vector3.FORWARD * speed * delta
	if(Input.is_action_pressed("ui_down")):
		move += Vector3.BACK * speed * delta
	if(Input.is_action_pressed("ui_left")):
		move += Vector3.LEFT * speed * delta
	if(Input.is_action_pressed("ui_right")):
		move += Vector3.RIGHT * speed * delta
		
	if(move == Vector3.ZERO):
		if sprite.animation != "idle":
			sprite.animation = "idle"
		sprite.flip_h = lastDir < 0.1
	else:
		move(move)
		
	pass
