/*
Copyright (C) 2015 Electronic Arts Inc.  All rights reserved.

This software is solely licensed pursuant to the Hackathon License Agreement,
Available at:  www.eapathfinders.com/license
All other use is strictly prohibited.
*/

$(document).ready(function () {

	console.log("Document Loaded");

	// INIT..
	conn = new Connection();
	conn.sendMessage({"type": "connect"});

	// Process incoming game messages
	$(document).on("game_message", function (e, message) {
		console.log("Received Message: " + JSON.stringify(message));
		var payload = message.payload;
		//switch (payload.type) {
			//your code here
		//}
	});

	document.addEventListener('touchstart',function(e)
				{
					xFirst = e.touches[0].pageX;
					yFirst = e.touches[0].pageY;

					ClickClick = true;
					console.log("x location = " + xFirst);
					console.log("y location = " + yFirst);
				});

				document.addEventListener('touchmove',function(e){
					e.preventDefault();

						if(xFirst != null)
						{
							xCurrent = e.touches[0].pageX;
							yCurrent = e.touches[0].pageY;

							xdiff = xCurrent - xFirst;
							ydiff = yCurrent - yFirst;
							if ( xdiff > 1 || xdiff < -1 || ydiff > 1 || ydiff < -1)
								ClickClick = false;
							if (xdiff > 1 && xdiff > ydiff) {
								conn.sendMessage({"direction": "right"});
							} else if (xdiff < 1 && Math.abs(xdiff) > ydiff) {
								conn.sendMessage({"direction": "left"});
							} else if (ydiff > 1 && ydiff > xdiff) {
								conn.sendMessage({"direction": "up"});
							} else if (ydiff < 1 && Math.abs(ydiff) > xdiff) {
								conn.sendMessage({"direction": "down"});
							}
						}

				});

				document.addEventListener('touchend',function(e)
				{
						if(ClickClick == true)
						{
							//send click info
							console.log("tapped");
							ClickClick = false;
						}
						else
						{
							console.log("End");
							//conn.sendMessage({"type": "End"}, 0 );	//when joystick end4
						}


						xCurrent = null;
						yCurrent = null;
						xFirst = null;
						yFirst = null;
						xdiff = null;
						ydiff = null;
				});

	document.addEventListener('touchstart',function(e){
		var imgCursor = document.getElementById('pacman_cursor');
		var widthCursor = imgCursor.width;
		var heightCursor = imgCursor.height;

		//console.log(widthCursor);
		//console.log(heightCursor);

		xFirst = e.touches[0].pageX;
		yFirst = e.touches[0].pageY;

		ClickClick = true;
		//console.log("x location = " + xFirst);
		//console.log("y location = " + yFirst);
		
		$("#pacman_cursor").css({'top': yFirst- (heightCursor/2), 'left' : xFirst- (widthCursor/2)});
		$("#pacman_cursor").show();
	});

	document.addEventListener('touchmove',function(e){
		e.preventDefault();
		var imgCursor = document.getElementById('pacman_cursor');
		var widthCursor = imgCursor.width;
		var heightCursor = imgCursor.height;

		//if(xFirst != null){
			xCurrent = e.touches[0].pageX; 
			yCurrent = e.touches[0].pageY;
			$("#pacman_cursor").css({'top': yCurrent-(heightCursor/2), 'left' : xCurrent-(widthCursor/2)});
			$("#pacman_cursor").show();

			xdiff = xCurrent - xFirst;
			ydiff = yCurrent - yFirst;

			if ( xdiff > 1 || xdiff < -1 || ydiff > 1 || ydiff < -1)
				ClickClick = false;
				
			var angle = (Math.atan2((xCurrent - xFirst),(yCurrent - yFirst)) * (180/Math.PI))+180;
			//console.log("angle: " + angle);
			//conn.sendMessage({"type": "angle", "angle": angle}, 0);	//Angle of joystick1
		//}
		//console.log("x diff = " + xdiff);
		
		//console.log("current location = " + xCurrent +", " + yCurrent);
		//console.log("y diff = " + ydiff);
		if ((angle > 330) || (angle < 30))
		{
			console.log("Swiped Up");
			conn.sendMessage({"type": "movement", "movement": "Up"}, 0);
		}
		else if((angle > 240) && (angle < 300))
		{
			console.log("Swiped Right");
			conn.sendMessage({"type": "movement", "movement": "Right"}, 0);
		}
		else if ((angle > 150) && (angle < 210))
		{
			console.log("Swiped Down");
			conn.sendMessage({"type": "movement", "movement": "Down"}, 0);
		}
		else if ((angle > 60) && (angle < 120))
		{
			console.log("Swiped Left");
			conn.sendMessage({"type": "movement", "movement": "Left"}, 0);
		}
	});						

	document.addEventListener('touchend',function(e){
		if(ClickClick == true){
			//send click info
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
