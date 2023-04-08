var gameOverElement = document.getElementById("gameOverScreen");
var canvas = document.querySelector('canvas');
var playerScoreElement = document.getElementById("playerScore");
var playerSpeedElement = document.getElementById("playerSpeed");
var overlayUiElement = document.getElementById("overlayUiContainer");



function popGameOverScreen()
{
	overlayUiElement.style.display = "none";
	gameOverElement.style.display = "inline";
	var canvasWidth = canvas.width;
	var canvasHeight = canvas.height;
	const gameOverElementWidth = 0.8* canvasWidth;
	const gameOverElementHeight = 0.5 * canvasHeight;

	gameOverElement.style.width = `${gameOverElementWidth}px`;
	//gameOverElement.style.height = `${gameOverElementHeight}px` ;

	playerScoreElement.innerHTML = "Speed : " + Math.floor(playerScore);
	playerSpeedElement.innerHTML = "Score : " + speedElement.innerHTML;

	window.addEventListener("keydown",(event)=>{reloadPage(event)})
}

function reloadPage(event)
{
	
	if (event.key == "r")
	{
		window.location.reload();
	}
}


