var gameOverElement = document.getElementById("gameOverScreen");
var canvas = document.querySelector('canvas');
var playerScoreElement = document.getElementById("playerScore");
var playerSpeedElement = document.getElementById("playerSpeed");
var overlayUiElement = document.getElementById("overlayUiContainer");

var speedElement = document.getElementById("speedElement");
var scoreElement = document.getElementById("scoreElement");

speedElement.style.width = `${Math.floor(canvasWidth/2) - horiItemDis}px`;
scoreElement.style.width = `${Math.floor(canvasWidth/2) - horiItemDis}px`;



function popGameOverScreen()
{
	overlayUiElement.style.display = "none";
	gameOverElement.style.display = "inline";

	playerScoreElement.innerHTML = "Speed : " +speedElement.innerHTML;
	playerSpeedElement.innerHTML = "Score : " + Math.floor(playerScore);

	//window.addEventListener("keydown",reloadGame);
}

function hidGameOverScreen()
{
	console.log("hello" + overlayUiElement);
	overlayUiElement.style.display = "inline";
	gameOverElement.style.display = "none";
}



//! From here start menu code begine : 
var gameplayScreen = document.getElementById("gameplayScreen");
var startMenuScreen = document.getElementById("startMenu");
var selectedCarIcon = null;


function hidStartMenu()
{

    gameplayScreen.style.display="block";
	startMenuScreen.style.display="none";
	startGame();
}





