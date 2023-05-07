var gameOverElement = document.getElementById("gameOverScreen");
var canvas = document.querySelector('canvas');
var playerScoreElement = document.getElementById("playerScore");
var playerSpeedElement = document.getElementById("playerSpeed");
var overlayUiElement = document.getElementById("overlayUiContainer");

var speedBarContainer = document.getElementById("speed-meter-container");
var speedBar = document.getElementById("speed-meter");
var speedBarText = document.getElementById("speed-meter-text");

//var speedElement = document.getElementById("speedElement");
var scoreElement = document.getElementById("scoreElement");

//speedElement.style.width = `${Math.floor(canvasWidth/2) - horiItemDis}px`;
scoreElement.style.width = `${Math.floor(canvasWidth/2) - horiItemDis}px`;


//speedbar container variables : 
const xDiffOfCanvas = 40;
var speedBarHeight = 0;




function popGameOverScreen()
{
	console.log("global speed is " + globalSpeed);
	overlayUiElement.style.display = "none";
	gameOverElement.style.display = "inline";

	playerScoreElement.innerHTML = "Score : " + Math.floor(playerScore);
	playerSpeedElement.innerHTML = "Speed : " + speedBarText.innerHTML;

	//window.addEventListener("keydown",reloadGame);
}

function hidGameOverScreen()
{
	overlayUiElement.style.display = "inline";
	gameOverElement.style.display = "none";
}



//* From here start menu code begine : 
var gameplayScreen = document.getElementById("gameplayScreen");
var startMenuScreen = document.getElementById("startMenu");
var playerCarImage = document.getElementById("playerCarImage");
var selectedCarIcon = "./PlayerCarImages/PlayerCar1.svg";

let playerImages = [];
var playerImagesIndex = 0;

function setPlayerImages()
{
	playerImages.push("./PlayerCarImages/PlayerCar1.svg");
	//playerImages.push("./PlayerCarImages/PlayerCar2.svg");
	//playerImages.push("./PlayerCarImages/PlayerCar3.svg");
	//playerImages.push("./PlayerCarImages/PlayerCar4.svg");
	playerImages.push("./PlayerCarImages/PlayerCar5.svg");
	playerImages.push("./PlayerCarImages/PlayerCar6.svg");


}
function hidStartMenu()
{
    gameplayScreen.style.display="block";
	startMenuScreen.style.display="none";
	setPlayerCarIcon(selectedCarIcon,playerCarImage.naturalWidth,playerCarImage.naturalHeight);
	startGame();
	initialiseSpeedBarContainer();
}


function scrollRight()
{
	playerImagesIndex = (playerImagesIndex+1)%playerImages.length;
	playerCarImage.src = playerImages[playerImagesIndex];
	selectedCarIcon = playerCarImage.src;
	playerCarImage.style.width="70%";
	playerCarImage.style.height="60%";
	
}

function leftScroll()
{
	if (playerImagesIndex <= 0)
	{
		playerImagesIndex = playerImages.length - 1;
	}
	else
	{
		playerImagesIndex--;
	}

	playerCarImage.src = playerImages[playerImagesIndex];
	selectedCarIcon = playerCarImage.src
	playerCarImage.style.width="70%";
	playerCarImage.style.height="60%";
}

function initialiseSpeedBarContainer()
{
	speedBarContainer.style.display = "inline-block";
	var canvasXPos = window.scrollY + canvas.getBoundingClientRect().left;
	
	speedBarContainer.style.left = `${canvasXPos - xDiffOfCanvas}px`;

	speedBarHeight = speedBarContainer.offsetHeight;
}

function updateSpeedBar(percentage)
{
	speedBar.setAttribute("style",`height=${percentage * speedBarHeight}px`);
	speedBar.style.height = `${percentage * speedBarHeight}px`;

	updateColor(percentage);

	var delta = speedBarHeight - speedBar.offsetHeight;
	speedBar.style.top = `${delta}px`;
}

function updateColor(percentage)
{
	if (percentage <= 0.2)
	{
		speedBar.style.backgroundColor = "green";
		speedBarText.style.backgroundColor = "green";
		updateText("1G");
	}
	else if (percentage <=0.4)
	{
		speedBar.style.backgroundColor = "#FFDF00";
		speedBarText.style.backgroundColor="#FFDF00";
		updateText("2G");
	}
	else if(percentage <= 0.6)
	{
		speedBar.style.backgroundColor="orange";
		speedBarText.style.backgroundColor="orange";
		updateText("3G");
	}
	else if (percentage <= 0.8)
	{
		speedBar.style.backgroundColor= "#FA5F55";
		speedBarText.style.backgroundColor="#FA5F55";
		updateText("4G");
	}
	else
	{
		speedBar.style.backgroundColor= "red";
		speedBarText.style.backgroundColor="red";
		updateText("5G");
	}
}

function updateText(speedText)
{
	speedBarText.innerHTML = speedText;
}


setPlayerImages();







