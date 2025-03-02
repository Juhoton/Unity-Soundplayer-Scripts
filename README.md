C# code I've written for a private unity game project. I pulled these out to showcase the code I wrote. Everything expect the CreatePlayerGeneratedSound method under collisionSoundPlayer script is written by me.

SoundPlayer.cs works as a parent class that has the shared functionality for soundplayers to store and play sounds. The classes that inherit it then decide when to play those sounds. This way allows me to write specific uses for different needs, while avoiding copying the same functionality which all of the scripts have.

