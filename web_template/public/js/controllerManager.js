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
});

