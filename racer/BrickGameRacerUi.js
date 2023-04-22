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
	overlayUiElement.style.display = "inline";
	gameOverElement.style.display = "none";
}



//* From here start menu code begine : 
var gameplayScreen = document.getElementById("gameplayScreen");
var startMenuScreen = document.getElementById("startMenu");
var playerCarImage = document.getElementById("playerCarImage");
var selectedCarIcon = "/PlayerCarImages/PlayerCar1.svg";

let playerImages = [];
var playerImagesIndex = 0;

function setPlayerImages()
{
	playerImages.push("/PlayerCarImages/PlayerCar1.svg");
	playerImages.push("/PlayerCarImages/PlayerCar2.svg");
	playerImages.push("/PlayerCarImages/PlayerCar3.svg");
	playerImages.push("/PlayerCarImages/PlayerCar4.svg");
	playerImages.push("/PlayerCarImages/PlayerCar5.svg");
	playerImages.push("/PlayerCarImages/PlayerCar6.svg");


}
function hidStartMenu()
{
    gameplayScreen.style.display="block";
	startMenuScreen.style.display="none";
	setPlayerCarIcon(selectedCarIcon,playerCarImage.naturalWidth,playerCarImage.naturalHeight);
	startGame();
}


function scrollRight()
{
	playerImagesIndex = (playerImagesIndex+1)%playerImages.length;
	playerCarImage.src = playerImages[playerImagesIndex];
	selectedCarIcon = playerCarImage.src;
	playerCarImage.style.width="100%";
	playerCarImage.style.height="100%";
	
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
	playerCarImage.style.width="100%";
	playerCarImage.style.height="100%";
}


setPlayerImages();







