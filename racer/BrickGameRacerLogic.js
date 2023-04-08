class BorderRect
{
	width;
	length;
	position;
	direction;

	constructor(width,length,position)
	{
		this.width = width;
		this.length = length;
		this.position = position;
		this.direction = new Vector2(0,1);
	}

	updatePos(timeBetweenFrames)
	{
		var movement = this.direction.scale(globalSpeed).scale(timeBetweenFrames);
		this.position = this.position.add(movement.xPos,movement.yPos);

	}

	draw()
	{
		//this.updatePos();
		context.fillStyle = 'black';
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
	constructor(width,length,position,line,icon)
	{
		this.position  = position;
		this.direction = new Vector2(0,1);
		this.line = line;
		this.icon = icon;
	}

	draw()
	{
		//context.fillStyle = 'black';
		//context.fillRect(this.position.xPos,this.position.yPos,carWidth,carHeight);
		context.drawImage(this.icon,this.position.xPos,this.position.yPos,carWidth,carHeight);
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
	constructor(changeLineSpeed,position,line,rightMostRestriction,leftMostRestriction,color,icon)
	{
		this.changeLineSpeed = changeLineSpeed;
		this.position = position;
		this.line = line;
		this.color = color;
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
		context.fillStyle = this.color;
		//context.fillRect(this.position.xPos,this.position.yPos,carWidth,carHeight);
		context.drawImage(this.icon,this.position.xPos,this.position.yPos,carWidth ,carHeight);
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
	constructor(carMinVerDis,canvasHeight,minCarLine,maxCarLine)
	{
		this.minDis = carMinVerDis;
		this.maxDis = canvasHeight;
		this.minCarLine = minCarLine;
		this.maxCarLine = maxCarLine;
		this.carLine =  this.getRandomCarLine();
		this.distance = this.getRandomDis();
	}

	getRandomDis()
	{
		var randomDis = Math.floor(Math.random() * (this.maxDis	- this.minDis + 1) + this.minDis);
		return randomDis;
	}

	getRandomCarLine()
	{
		var randomCarLine = Math.floor(Math.random() * (this.maxCarLine - this.minCarLine + 1) + this.minCarLine )
		return randomCarLine;
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
	if (lastBorderRect.position.yPos >= distance)
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
		console.log('time to remove');
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
			new Vector2(canvasWidth - borderRectWidth,elements[i].position.yPos));

		rightElement.draw();
	}

	if (isTimeForNewBorderRect(borderRect.length))
	{
		var newBorderRect = new BorderRect(borderRectWidth,borderRectHeight,new Vector2(0,-borderRectHeight));
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

	if (checkCarCallision())
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

function generateCar(carLine)
{
	var nextCarPos = new Vector2(0,- carHeight);
	if (carLine == 1)
	{
		nextCarPos = new Vector2(borderRectWidth + horiItemDis,- carHeight);
	}
	else
	{
		nextCarPos = new Vector2(canvasWidth - borderRectWidth - horiItemDis - carWidth, - carHeight);
	}

	var nextCar = new Car (carWidth,carHeight,nextCarPos,carLine,carIcon);
	carQueue.enqueue(nextCar);
	carLineQueue.dequeue();
	carDistanceQueue.dequeue();
	lastGeneratedCar = nextCar;
}

function isLastGeneratedCarFarEnough()
{
	if (lastGeneratedCar.position.yPos >= getRandomDist())
	{
		return true;
	}
	return false;
}

function getRandomDist()
{
	if (carDistanceQueue.isEmpty())
	{
		fillCarDisQueue(carMinVerDis,canvasHeight);
	}
	var randomDis = carDistanceQueue.peek();
	return randomDis;
}

function getRandomCarLine(min , max)
{
	if (carLineQueue.isEmpty())
	{
		fillCarLineQueue(min,max);
	}
	var randomCarLine = carLineQueue.peek();
	return randomCarLine;
}
function fillCarLineQueue(min , max)
{
	for(var i = 0 ; i < 50 ; i++)
	{
		var randomCarLine = Math.floor(Math.random() * (max - min + 1) + min );
		carLineQueue.enqueue(randomCarLine);
	}
}

function fillCarDisQueue(min,max)
{
	for (var i = 0 ; i < 50 ; i++)
	{
		var randomDis = Math.floor(Math.random() * (max - min + 1) + min);
		carDistanceQueue.enqueue(randomDis);
	}
}

function isLineFeasible(carLine)
{
	if (carLine == lastGeneratedCar.line)
	{
		return true;
	}
	else if (carLine != lastGeneratedCar.line)
	{
		if (lastGeneratedCar.position.yPos >= playerCarHeight + carMinVerDis )
		{
			return true;
		}
		return false;
	}
}

function checkCarGeneration()
{
	if (isLastGeneratedCarFarEnough())
	{
		var carLine = getRandomCarLine(1,2);
		if (isLineFeasible(carLine))
		{
			generateCar(carLine);
		}

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

function checkCarCallision()
{
	var cars = carQueue.getElements();
	for(var i = 0 ; i < cars.length;i++)
	{
		var currentCar = cars[i];
		var leftCar = currentCar;
		var rightCar = currentCar;
		var upperCar = currentCar;
		var lowerCar = currentCar;

		if (currentCar.position.xPos >= player.position.xPos)
		{
			rightCar = currentCar;
			leftCar = player;
		}
		else
		{
			rightCar = player;
			leftCar = currentCar;
		}

		if (currentCar.position.yPos >= player.position.yPos)
		{
			upperCar = player;
			lowerCar = currentCar;
		}
		else
		{
			upperCar = currentCar;
			lowerCar = player;
		}

		if (rightCar.position.xPos >= leftCar.position.xPos
			&& rightCar.position.xPos <= leftCar.position.xPos + carWidth)
		{
			if (lowerCar.position.yPos >= upperCar.position.yPos
				&& lowerCar.position.yPos <= upperCar.position.yPos + carHeight)
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
	endPoint.xPos = event.changedTouches[event.changedTouches.length-1].clientX;
	endPoint.yPos = event.changedTouches[event.changedTouches.length-1].clientY;
	console.log(`Ended : (${endPoint.xPos},${endPoint.yPos})`);
	specifyMovementDirec();

}

function handleTouchStart(event)
{
	event.preventDefault();
	startPoint.xPos = event.touches[0].clientX;
	startPoint.yPos = event.touches[0].clientY;
	console.log(`Started : (${startPoint.xPos},${startPoint.yPos})`)
}

function specifyMovementDirec()
{
	var dir = new Vector2(endPoint.xPos - startPoint.xPos,endPoint.yPos - startPoint.yPos);
	console.log(`Direction : (${dir.xPos},${dir.yPos})`);
	handleTouchInput(dir);
}

function handleTouchInput(dir)
{
	if (player.playerState == PlayerState.Stable)
	{
		if (Math.abs(dir.xPos)>= 10 && dir.xPos >= 0 && player.line != 2)
		{
			player.setState(PlayerState.GoingRight);
		}

		else if (Math.abs(dir.xPos)>= 10 && dir.xPos <= 0 && player.line != 1)
		{
			player.setState(PlayerState.GoingLeft);
		}
	}
}



var lastFrameTime = Date.now();

var canvas = document.querySelector('canvas');
console.log(canvas);

specifyCanvasSize(window.innerWidth,window.innerHeight);

let canvasHeight = canvas.height;
let canvasWidth = canvas.width;

var globalSpeed = Math.floor(canvasHeight/7);
var initialSpeed = globalSpeed;
var speedIncreasePerSec = Math.floor(canvasHeight / 10);
console.log('globalSpeed speed ' + globalSpeed);

var context = canvas.getContext('2d');

const borderRectWidth = Math.floor(0.05 * canvasWidth);
const borderRectHeight = Math.floor(canvasHeight * 3/20);

const carWidth = Math.floor(0.35 * canvasWidth);
console.log("car width is " + carWidth);
const carHeight = carWidth;
//const carHeight = Math.floor(canvasHeight / 10);
console.log("car height is " + carHeight);
const carMinVerDis = Math.floor(canvasHeight/5);

const horiItemDis = Math.floor (0.05 * canvasWidth);

const playerCarHeight = carHeight; //TODO maybe I change this later.

var borderRectQueue = new Queue(15);
var borderRect = new BorderRect(borderRectWidth,borderRectHeight,new Vector2(0,0));
var lastBorderRect = borderRect;
borderRectQueue.enqueue(borderRect);

const carIcon = new Image();
carIcon.src = "Obstacle.svg";
var carDistanceQueue = new Queue(60);
var carQueue = new Queue(15);
var carLineQueue = new Queue (60);
var firstCar = new Car(carWidth,carHeight,new Vector2(borderRectWidth + horiItemDis,-carHeight),1,carIcon);
var lastGeneratedCar = firstCar;
carQueue.enqueue(lastGeneratedCar);


const changeLineSpeed = canvasWidth * 5;
const leftMostRestriction = borderRectWidth + horiItemDis ;
const rightMostRestriction = canvasWidth - borderRectWidth - horiItemDis - carWidth;
var initialPlayerPos = new Vector2(canvasWidth - borderRectWidth - horiItemDis - carWidth,
	canvasHeight - carMinVerDis - carHeight);
const playerIcone = new Image();
playerIcone.src = "Player.svg";
var playerColor = 'blue';
var player = new Player(changeLineSpeed,initialPlayerPos,2,rightMostRestriction,leftMostRestriction,playerColor,playerIcone);

var isGameOver = false;

var playerScore = 0;


var speedElement = document.getElementById("speedElement");
var scoreElement = document.getElementById("scoreElement");

speedElement.style.width = `${Math.floor(canvasWidth/2) - horiItemDis}px`;
scoreElement.style.width = `${Math.floor(canvasWidth/2) - horiItemDis}px`;

var startPoint = new Vector2(0,0);
var endPoint = new Vector2(0,0);
window.addEventListener("keydown",(event)=>{
	handleInput(event);
});

window.addEventListener("touchend",handleTouchEnd,{passive : false});
window.addEventListener("touchstart",handleTouchStart,{passive : false});

play();