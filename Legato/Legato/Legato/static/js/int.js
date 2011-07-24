
    Cufon.replace('nav ', 
    { color: '#ffffff', 
      hover: {color: '#fb9c36'}
    } );
	
	
    Cufon.replace('.LMidBlock ul li', 
    {  
      hover: {color: '#d7282f'}
    } );
	
	Cufon.replace('.LMidBlock ul li.first', 
    {  
      color: '#d7282f'
    } );
	
	
 
	 
Cufon.replace('h1:not(.contentHeaderText,.notCufon)', {
    fontWeight: 'bold',
    hover: {
        fontWeight: 'bold'
    }
});

Cufon.replace('.Advertiser h2' );
Cufon.replace('.NavContent ul li' );


Cufon.replace('.TopHdg');    

// pop up video script

				function MyPopUpWin() {
				var iMyWidth;
				var iMyHeight;
				//half the screen width minus half the new window width (plus 5 pixel borders).
				iMyWidth = (window.screen.width/2) - (480 + 10);
				//half the screen height minus half the new window height (plus title and status bars).
				iMyHeight = (window.screen.height/2) - (250 + 50);
				//Open the window.
				var win2 = window.open("slider.html","Window2","status=no,height=336,width=600,resizable=no,left=" + iMyWidth + ",top=" + iMyHeight + ",screenX=" + iMyWidth + ",screenY=" + iMyHeight + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
				win2.focus();
				}
