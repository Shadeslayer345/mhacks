$(document).ready(function () {
	// Parse the url to determine which player is calling the shots.
	var playerNum = location.pathname[1];

	// The initial communication with Unity through EA API
	conn = new Connection();
	conn.sendMessage({"type": "connect"});

	// Process incoming game messages for two interaction
	$(document).on("game_message", function (e, message) {
		console.log("Received Message: " + JSON.stringify(message));
		var payload = message.payload;
		//switch (payload.type) {
			//your code here
		//}
	});

	/**
	 * On the user's first touch
	 * @param  {DOM event} e
	 */
	document.addEventListener('touchstart', function(e) {
		var imgCursor = document.getElementById('pacman_cursor');
		var widthCursor = imgCursor.width;
		var heightCursor = imgCursor.height;

		xFirst = e.touches[0].pageX;
		yFirst = e.touches[0].pageY;

		ClickClick = true;

		$("#pacman_cursor").css({'top': yFirst- (heightCursor/2), 'left' : xFirst- (widthCursor/2)});
		$("#pacman_cursor").show();
	});

	/**
	 * As the user swipes, let's take the angle
	 * @param  {Dom element} e)
	 */
	document.addEventListener('touchmove', function(e) {
		e.preventDefault();
		var imgCursor = document.getElementById('pacman_cursor');
		var widthCursor = imgCursor.width;
		var heightCursor = imgCursor.height;

		xCurrent = e.touches[0].pageX;
		yCurrent = e.touches[0].pageY;
		$("#pacman_cursor").css({'top': yCurrent-(heightCursor/2), 'left' :
				xCurrent-(widthCursor/2)});
		$("#pacman_cursor").show();

		xdiff = xCurrent - xFirst;
		ydiff = yCurrent - yFirst;

		if ( xdiff > 1 || xdiff < -1 || ydiff > 1 || ydiff < -1)
			ClickClick = false;

		var angle = (Math.atan2((xCurrent - xFirst),(yCurrent - yFirst)) *
				(180/Math.PI))+180;

		if ((angle > 330) || (angle < 30)) {
			console.log("Swiped Up");
			conn.sendMessage({"type": "movement", "movement": "Up", "player": playerNum}, 0);
		} else if((angle > 240) && (angle < 300)) {
			console.log("Swiped Right");
			conn.sendMessage({"type": "movement", "movement": "Right", "player": playerNum}, 0);
		} else if ((angle > 150) && (angle < 210)) {
			console.log("Swiped Down");
			conn.sendMessage({"type": "movement", "movement": "Down", "player": playerNum}, 0);
		} else if ((angle > 60) && (angle < 120)) {
			console.log("Swiped Left");
			conn.sendMessage({"type": "movement", "movement": "Left", "player": playerNum}, 0);
		}
	});

	/**
	 * When the user is finished with an interaction, let's reset our
	 * 			positions.
	 * @param  {DOM element} e)
	 */
	document.addEventListener('touchend', function(e) {
		if(ClickClick == true) {
			console.log("tapped");
			ClickClick = false;
		}

		xCurrent = null;
		yCurrent = null;
		xFirst = null;
		yFirst = null;
		xdiff = null;
		ydiff = null;

		$("#pacman_cursor").hide();
	});
});
