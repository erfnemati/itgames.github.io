class BorderRect
{
	width;
	length;
	position;
	direction;
	color

	constructor(width,length,position,color)
	{
		this.width = width;
		this.length = length;
		this.position = position;
		this.direction = new Vector2(0,1);
		//this.borderRectIcon = new Image();
		this.color = color;
		//this.borderRectIcon.src = borderRectIconSrc;
	}

	updatePos(timeBetweenFrames)
	{
		var movement = this.direction.scale(globalSpeed).scale(timeBetweenFrames);
		this.position = this.position.add(movement.xPos,movement.yPos);

	}

	draw()
	{
		//this.updatePos();
		context.fillStyle = this.color;
		context.fillRect(this.position.xPos,this.position.yPos,this.width,this.length);
	}

	move(timeBetweenFrames)
	{
		//this.draw();
		this.updatePos(timeBetweenFrames);
		this.draw();
	}
}

class Vector2
{
	xPos;
	yPos;
	constructor(x,y)
	{
		this.xPos = x;
		this.yPos = y;
	}

	add(xOffset,yOffset)
	{
		var x = this.xPos + xOffset;
		var y = this.yPos	+ yOffset;

		return new Vector2(x,y);
	}

	scale(scaleFactor)
	{
		var x = this.xPos * scaleFactor;
		var y = this.yPos * scaleFactor;

		return new Vector2(x,y);
	}
}

class Queue
{
	
	constructor(size)
	{
		this.innerArray = Array(size).fill(-1);
		this.front = 0;
		this.back = 0;
	}
	enqueue(item)
	{
		if (this.isFull())
		{
			console.log('Array is full');
			return;
		}
		this.innerArray[this.back] = item;
		this.back = (this.back + 1) % this.innerArray.length;

	}

	dequeue()
	{
		if (this.isEmpty())
		{
			console.log('Queue is empty');
			return;
		}

		var item = this.innerArray[this.front];
		this.front = (this.front + 1) % this.innerArray.length;
		return item;

	}

	peek()
	{
		if (this.isEmpty())
		{
			console.log('queue is empty');
			return;
		}
		return this.innerArray[this.front];
	}

	isFull()
	{
		if (((this.back + 1) % this.innerArray.length ) == this.front)
		{
			return true;
		}
		return false;
	}

	isEmpty()
	{
		if (this.back == this.front)
		{
			return true;
		}
		return false;
	}

	getCount()
	{
		if (this.isEmpty())
		{
			return 0;
		}
		return Math.abs(this.front - this.back);
	}

	getElements()
	{
		if (this.isEmpty())
		{
			return;
		}
		var elements = [];
		for (var i = this.front;i != this.back ; i = (i+1) % this.innerArray.length)
		{
			elements.push(this.innerArray[i]);
		}

		return elements;
	}
}

class Car
{
	constructor(width,height,position,line,icon)
	{
		this.width = width;
		this.height = height;
		this.position  = position;
		this.direction = new Vector2(0,1);
		this.line = line;
		this.icon = icon;
	}

	draw()
	{
		//context.fillStyle = 'black';
		//context.fillRect(this.position.xPos,this.position.yPos,carWidth,carHeight);
		context.drawImage(this.icon,this.position.xPos,this.position.yPos,this.width,this.height);
	}

	updatePos(timeBetweenFrames)
	{
		var movement = this.direction.scale(globalSpeed).scale(timeBetweenFrames);
		this.position = this.position.add(movement.xPos	,movement.yPos);
	}

	move(timeBetweenFrames)
	{
		this.updatePos(timeBetweenFrames);
		this.draw();
	}

}
const PlayerState = 
{
	Stable : Symbol(1),
	GoingRight : Symbol(2),
	GoingLeft : Symbol(3),
}

class Player
{
	constructor(changeLineSpeed,position,line,rightMostRestriction,leftMostRestriction,icon)
	{
		this.changeLineSpeed = changeLineSpeed;
		this.position = position;
		this.line = line;
		//this.color = color;
		this.playerState = PlayerState.Stable;
		this.rightMostRestriction = rightMostRestriction;
		this.leftMostRestriction = leftMostRestriction;
		this.icon = icon;


	}
	
	goRight(timeBetweenFrames)
	{
		var rightDirection = new Vector2(1,0);
		var movement = rightDirection.scale(changeLineSpeed).scale(timeBetweenFrames);
		this.position = this.position.add(movement.xPos,movement.yPos);
		this.checkRightRestriction();
		this.draw();
	}

	goLeft(timeBetweenFrames)
	{
		var leftDirecion = new Vector2(-1,0);
		var movement = leftDirecion.scale(changeLineSpeed).scale(timeBetweenFrames);
		this.position = this.position.add(movement.xPos,movement.yPos);
		this.checkLeftRestriction();
		this.draw();
	}

	draw()
	{
		//context.fillStyle = this.color;
		//context.fillRect(this.position.xPos,this.position.yPos,carWidth,carHeight);
		context.drawImage(this.icon,this.position.xPos,this.position.yPos,playerCarWidth ,playerCarHeight);
	}

	checkRightRestriction()
	{
		if (this.position.xPos >= rightMostRestriction)
		{
			this.position.xPos = rightMostRestriction;
			this.playerState = PlayerState.Stable;
			this.line = this.line + 1;

		}
	}

	checkLeftRestriction()
	{
		if(this.position.xPos <= leftMostRestriction)
		{
			this.position.xPos = leftMostRestriction;
			this.playerState = PlayerState.Stable;
			this.line = this.line -1;
		}
	}

	
	setLine(line)
	{
		this.line = line;
	}

	setState(playerState)
	{
		this.playerState = playerState;
	}


}

class RandomisedCar
{
	static lastCarLine = 0;
	constructor()
	{
		this.minCarLine = 1;
		this.maxCarLine = 2;
		this.maxDis = 1 * canvasHeight; //TODO :Think about changing this parameter
		this.minDis = carMinVerDis;
		this.carLine = this.getRandCarLine();
		this.distance = this.getRandomDis();//TODO : rename this method and refactor random functions.
		this.carIcon = new Image();
		this.carIcon.src = this.getRandomCarIcon();
		this.width = carWidth;
		this.height = this.setObstacleHeight();
		
	}

	getRandomDis()
	{
		if (this.lastCarLine != this.carLine)
		{
			var randomDis = Math.floor(Math.random() * (this.maxDis - (this.minDis + playerCarHeight) + 1) + this.minDis + playerCarHeight);
		}
		else
		{
			var randomDis = Math.floor(Math.random() * (this.maxDis	- this.minDis + 1) + this.minDis);

		}
		RandomisedCar.lastCarLine = this.carLine;
		return randomDis;
	}

	getRandCarLine()
	{
		var randomCarLine = Math.floor(Math.random() * (this.maxCarLine - this.minCarLine + 1) + this.minCarLine );
		return randomCarLine;
	}

	getRandomCarIcon()
	{
		var randomObstacleCarIndex = Math.floor(Math.random() * ((obstacleCarImages.length-1) - 0 + 1) + 0 );
		var randomCarIcon = obstacleCarImages[randomObstacleCarIndex];
		return randomCarIcon;
	}

	setObstacleHeight()
	{
		var ratio = 1.6//= this.carIcon.naturalHeight / this.carIcon.naturalWidth;
		carHeight = carWidth * ratio;
		return carHeight;
	}

}

function getRandomisedCars(numberOfCars)
{
	var randomCars = new Queue(numberOfCars + 5);

	for(var i = 0 ; i < numberOfCars ; i++)
	{
		var tempCar = new RandomisedCar(carMinVerDis,canvasHeight - carMinVerDis,1,2);
		randomCars.enqueue(tempCar);
	}
	return randomCars;
}

function getTimeBetweenFrames()
{
	//return 0.2;
	var presentTime = Date.now();
	var deltaTime = (presentTime - lastFrameTime)/1000;
	lastFrameTime = presentTime;
	return (deltaTime);
}


function isTimeForNewBorderRect(distance)
{
	if (lastBorderRect.position.yPos >= -0.5)
	{
		return true;
	}
	return false;
}



function isTimeForDeleteOldRect()
{
	if (borderRectQueue.peek().position.yPos >= canvasHeight )
	{
		return true;
	}
	return false;
}

function animateBorderRects(timeBetweenFrames)
{
	var elements = borderRectQueue.getElements();
	for (var i = 0 ; i < elements.length ; i++)
	{
		
		elements[i].move(timeBetweenFrames);

		var rightElement = new BorderRect(borderRectWidth,borderRectHeight,
			new Vector2(canvasWidth - borderRectWidth,elements[i].position.yPos),elements[i].color);

		rightElement.draw();
	}

	if (isTimeForNewBorderRect(canvasHeight/5))
	{
		var nextColor = "black";
		if (isBlack)
		{
			nextColor = "#FFFF9F";
			isBlack = false;
		}
		else
		{	
			nextColor = "black";
			isBlack = true;
		}

		var newBorderRect = new BorderRect(borderRectWidth,borderRectHeight,new Vector2(0,-borderRectHeight),nextColor);
		lastBorderRect = newBorderRect;
		borderRectQueue.enqueue(newBorderRect);
	}

	if (isTimeForDeleteOldRect())
	{
		borderRectQueue.dequeue();
	}
}

function animateCars(timeBetweenFrames)
{
	
	checkCarGeneration();

	var cars = carQueue.getElements();
	for (var i = 0 ; i < cars.length ; i++) 
	{
		cars[i].move(timeBetweenFrames);
	}

	checkCarDeletion();
}

function animatePlayer(player,timeBetweenFrames)
{
	if (player.playerState == PlayerState.Stable)
	{
		player.draw();
	}

	else if (player.playerState == PlayerState.GoingLeft)
	{
		player.goLeft(timeBetweenFrames);
	}

	else if (player.playerState == PlayerState.GoingRight)
	{
		player.goRight(timeBetweenFrames);
	}

	if (checkCarCollision())
	{
		isGameOver = true;
		popGameOverScreen();
	}
}

function handleInput(event)
{
	if (player.playerState == PlayerState.Stable)
	{
		if (event.key == "ArrowRight" && player.line != 2)
		{
			player.setState(PlayerState.GoingRight);

		}

		else if (event.key == "ArrowLeft" && player.line != 1)
		{
			player.setState(PlayerState.GoingLeft);
		}
	}
	
}

function generateCar()
{
	var nextCarPos = new Vector2(0,- carHeight);
	var nextCar = randomObstacleCars[randomObsCarIndex];
	if (nextCar.carLine == 1)
	{
		nextCarPos = new Vector2(borderRectWidth + horiItemDis,- carHeight);
	}
	else
	{
		nextCarPos = new Vector2(canvasWidth - borderRectWidth - horiItemDis - carWidth, - carHeight);
	}

	var nextCar = new Car (nextCar.width,nextCar.height,nextCarPos,nextCar.carLine,nextCar.carIcon);
	carQueue.enqueue(nextCar);
	randomObsCarIndex = (randomObsCarIndex + 1) % randomObstacleCars.length;
	lastGeneratedCar = nextCar;
}

function isLastGeneratedCarFarEnough()
{
	if (lastGeneratedCar.position.yPos >= randomObstacleCars[randomObsCarIndex].distance)
	{
		return true;
	}
	return false;
}


function checkCarGeneration()
{
	if (isLastGeneratedCarFarEnough())
	{
		generateCar();
	}

}


function checkCarDeletion()
{
	if (carQueue.isEmpty())
	{
		return;
	}
	var furthestCar = carQueue.peek();
	if (furthestCar.position.yPos - canvasHeight >= Number.EPSILON)
	{
		carQueue.dequeue();
	}
}

function checkCarCollision()
{
	
	const xcollisionDetectionForgiveness = Math.floor(carHeight/6) ;
	const ycollisionDetectionForgiveness = Math.floor(carHeight/8);
	var cars = [];
	cars = carQueue.getElements();
	for(var i = 0 ; i < cars.length;i++)
	{
		var currentCar = cars[i];
		var leftCar = currentCar;
		var rightCar = currentCar;
		var upperCar = currentCar;
		var lowerCar = currentCar;
		var upperCarHeight = 0;
		var leftCarWidth = 0;

		if (currentCar.position.xPos >= player.position.xPos)
		{
			rightCar = currentCar;
			leftCar = player;
			leftCarWidth = playerCarWidth;
		}
		else
		{
			rightCar = player;
			leftCar = currentCar;
			leftCarWidth = carWidth;
		}

		if (currentCar.position.yPos >= player.position.yPos)
		{
			upperCar = player;
			lowerCar = currentCar;
			upperCarHeight =  playerCarHeight;
		}
		else
		{
			upperCar = currentCar;
			lowerCar = player;
			upperCarHeight = currentCar.height;
		}

		if (rightCar.position.xPos >= leftCar.position.xPos
			&& rightCar.position.xPos <= leftCar.position.xPos + carWidth - xcollisionDetectionForgiveness)
		{
			if (lowerCar.position.yPos >= upperCar.position.yPos
				&& lowerCar.position.yPos <= upperCar.position.yPos + upperCarHeight - ycollisionDetectionForgiveness)
			{
				return true;
			}
		}
	}
	return false;
}

function increaseGloablSpeed(timeBetweenFrames)
{
	
	globalSpeed = globalSpeed + (speedIncreasePerSec * timeBetweenFrames);
	if (globalSpeed >= maxSpeed)
	{
		globalSpeed = maxSpeed;
	}
	updateSpeedBar(globalSpeed/maxSpeed);
}

function updatePlayerScore(timeBetweenFrames)
{
	playerScore += (globalSpeed * timeBetweenFrames);
	scoreElement.innerHTML = Math.round(playerScore);
}

function updateSpeedUi()
{
	if (globalSpeed >= 8 * initialSpeed)
	{
		speedElement.innerHTML = "5G";
	}
	else if (globalSpeed >= 6 * initialSpeed)
	{
		speedElement.innerHTML = "4G";
	}
	else if (globalSpeed >= 4 * initialSpeed)
	{
		speedElement.innerHTML = "3G";
	}
	else if (globalSpeed >= 3 * initialSpeed)
	{
		speedElement.innerHTML = "2G";
	}
	else
	{
		speedElement.innerHTML = "1G";
	}
}



function play()
{
	if (isGameOver)
	{
		return;

	}

	requestAnimationFrame(play);
	context.clearRect(0,0,canvasWidth,canvasHeight);

	var timeBetweenFrames = getTimeBetweenFrames();
	animateBorderRects(timeBetweenFrames);
	animateCars(timeBetweenFrames);
	animatePlayer(player,timeBetweenFrames);
	increaseGloablSpeed(timeBetweenFrames);
	updatePlayerScore(timeBetweenFrames);
	updateSpeedUi();
}

function specifyCanvasSize(windowWidth,windowHeight)
{
	if (windowWidth/2 >= windowHeight)
	{
		canvas.width = windowHeight/2;

	}
	else
	{
		canvas.width = windowHeight/3;
	}

	canvas.height = windowHeight;
	
}

function handleTouchEnd(event)
{
	event.preventDefault();
	if (isTapped == true)
	{
		isTapped = false;
	}

}

function handleTouchStart(event)
{
	event.preventDefault();
	if (isTapped == false)
	{
		isTapped = true;
		handleTouchInput();
	}
}

function specifyMovementDirec()
{
	var dir = new Vector2(endPoint.xPos - startPoint.xPos,endPoint.yPos - startPoint.yPos);
	console.log(`Direction : (${dir.xPos},${dir.yPos})`);
	handleTouchInput(dir);
}

function handleTouchInput()
{
	if (player.playerState == PlayerState.Stable)
	{
		if (player.line == 1)
		{
			player.setState(PlayerState.GoingRight);
		}
		else if (player.line == 2)
		{
			player.setState(PlayerState.GoingLeft);
		}
	}
}

function addListeners()
{
	window.addEventListener("keydown",handleInput);
	window.addEventListener("touchend",handleTouchEnd,{passive : false});
	window.addEventListener("touchstart",handleTouchStart,{passive : false});
	window.addEventListener("keydown",handleRestart);

}


function initialiseBorderRects()
{
	borderRectQueue = new Queue(15);
	borderRect = new BorderRect(borderRectWidth,borderRectHeight,new Vector2(0,0),"black");
	lastBorderRect = borderRect;
	borderRectQueue.enqueue(borderRect);
}

function initialiseObstacleCars()
{
	carDistanceQueue = new Queue(60);
	carQueue = new Queue(15);
	carLineQueue = new Queue (60);
	fillrandomObstacleCars();
	randomObsCarIndex = (randomObsCarIndex + 1) % randomObstacleCars.length;
	firstRanObsCar = randomObstacleCars[randomObsCarIndex];
	firstCar = new Car(firstRanObsCar.width,firstRanObsCar.height,
						new Vector2(borderRectWidth + horiItemDis,-carHeight),
						firstRanObsCar.carLine,firstRanObsCar.carIcon);

	lastGeneratedCar = firstCar;
	carQueue.enqueue(lastGeneratedCar);
}

function initialisePlayer()
{
	player = new Player(changeLineSpeed,initialPlayerPos,2,rightMostRestriction,leftMostRestriction,playerIcone);
	playerScore = 0;
}

function initialiseGlobalGameSettings()
{
	lastFrameTime = Date.now();
	initialSpeed = Math.floor(canvasHeight/7)
	globalSpeed = initialSpeed;
	speedIncreasePerSec = Math.floor(canvasHeight / 10);
	isGameOver = false;
	isTapped = false;
}
function restartGame()
{
	hidGameOverScreen()
	turnOffGameOverScreen();
	initialiseGlobalGameSettings();
	initialiseBorderRects();
	initialiseObstacleCars();
	initialisePlayer();
	play();
}
function turnOffGameOverScreen()
{
	if (gameOverScreen.style.display != 'none')
	{
		gameOverScreen.style.display = 'none';
	}
	return;
}
 

function startGame()
{
	initialiseGlobalGameSettings();
	initialiseBorderRects();
	initialiseObstacleCars();
	initialisePlayer();
	addListeners();
	play();
}

function handleRestart(event)
{
	if (event.key == 'r')
	{
		hidGameOverScreen();
		restartGame();
	}

}

function setPlayerCarIcon(img,originalImageWidth,originalImageLength)
{
	playerIcone.src = img;
	var ratio = originalImageLength/originalImageWidth;
	playerCarHeight = ratio * playerCarWidth;
		
}

function setObstacleCarImages()
{
	obstacleCarImages.push("/ObstacleCarImages/ObstacleCar1.svg");
	obstacleCarImages.push("/ObstacleCarImages/ObstacleCar2.svg");
	obstacleCarImages.push("/ObstacleCarImages/ObstacleCar3.svg");
}

function fillrandomObstacleCars()
{
	randomObstacleCars.length = 0;
	randomObsCarIndex = 0;
	for(var i = 0 ; i< numOfObstacleCars; i++)
	{
		randomObstacleCars.push(new RandomisedCar());
	}
}

//Canvas variables : 
var canvas = document.querySelector('canvas');
specifyCanvasSize(window.innerWidth,window.innerHeight);
let canvasHeight = canvas.height;
let canvasWidth = canvas.width;
var context = canvas.getContext('2d');

//Global game variables : 
var lastFrameTime;
var initialSpeed;
var globalSpeed;
var speedIncreasePerSec;
var isGameOver;
var isTapped;
var obstacleCarImages = [];
var randomObstacleCars= [];
var randomObsCarIndex = 0;

const maxSpeed = 1000;

//BorderRect constants : 
const borderRectWidth = Math.floor(0.05 * canvasWidth);
const borderRectHeight = Math.floor(canvasHeight * 3/20);

//BorderRect variables :
var borderRectSrc = "/SideroadBlocks/SideroadBlock1.svg"; 
var borderRectQueue;
var borderRect;
var lastBorderRect;
var isBlack = false;



//Obstacle cars const : 
const numOfObstacleCars = 100;
const carWidth = Math.floor(0.30 * canvasWidth);
const carMinVerDis = Math.floor(canvasHeight/5);
const horiItemDis = Math.floor (0.07 * canvasWidth);




//Obstacle cars variables :
var carIcon = new Image();
carIcon.src = "/ObstacleCarImages/ObstacleCar1.svg";
var carHeight = carWidth;
var carDistanceQueue;
var carQueue;
var carLineQueue;
var firstCar;
var lastGeneratedCar;



//Player variables : 
var playerCarHeight = canvasHeight/20;
var player;
var playerScore;
var playerIcone = new Image();
playerIcone.src = "Player.svg";

//Player constants :
const playerCarWidth = carWidth; 
const changeLineSpeed = canvasWidth * 5;
const leftMostRestriction = borderRectWidth + horiItemDis ;
const rightMostRestriction = canvasWidth - borderRectWidth - horiItemDis - playerCarWidth;
const initialPlayerPos = new Vector2(canvasWidth - borderRectWidth - horiItemDis - playerCarWidth,
	canvasHeight - carMinVerDis - playerCarHeight);
	
	

	
var gameOverScreen = document.getElementById('gameOverScreen');

document.getElementById('restartButton').ontouchstart = function(event){
	restartGame();
}

setObstacleCarImages();

//startGame();