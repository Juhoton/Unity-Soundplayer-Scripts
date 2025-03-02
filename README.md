C# code I've written for a private unity game project. I pulled these out to showcase the code I wrote. Everything expect the CreatePlayerGeneratedSound method under collisionSoundPlayer script is written by me.

SoundPlayer.cs works as a parent class that has the shared functionality for soundplayers to store and play sounds. The classes that inherit it then decide when to play those sounds. This way allows for writing code to specific uses when needed, while still having control on high level.

At first I made scripts that have all the functionality needed for specific game objects in a single script, like physics objects that had collision, dragging, and pickup. It was simpler for the editor side to have a single script attached to the object. After some time though I realised that I was rewriting a lot of stuff, and the code wasn't simple with having the same script do so many different things. I then rewrote all the scripts into the current system.
