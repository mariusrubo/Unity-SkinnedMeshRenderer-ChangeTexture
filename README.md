# Purpose
Change the texture of your character at runtime. In this example, the character's skin will turn red and fade back to its normal color in the rythm of an attached heartbeat recording. 

![alt tag](https://github.com/mariusrubo/Unity-DisplayHeartbeat/blob/master/characterChangeColor.png)

# Installation
* If you merely wish your entire character to turn red, including its hair and clothes, you can simply use this line of code (with 'character' being the GameObject of your character and 'color' a float between 0 and 1): character.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red, color); 
* Changing the color of only some parts of your mesh, like the skin or perhaps only the hands, is more complex. First find the skin texture of your character in your project folder (it looks like the rolled out wrapping of a chocolate Santa Claus), create a folder ".../Assets/Ressources" and copy it there. Copy this image again and colorize it as you like. Find both these images inside the Unity project tab. In the inspector, set "Read/Write Enable" to true and change Format to "RGBA 32 Bit". 
* Attach the script "ChangeColor.cs" to your character and drag the skin carrying the mesh renderer into "skin". Inside "ChangeColor.cs", set the names of your two skin files. After starting, this script will create 5 different blendings of the two images you provided, and store these to dynamically fade the color intensity at runtime. If you need smoother blending, you can change the amount of blended images in here. If Unity takes too long to start your scene (as blending of images is computationally heavy), I recommend setting up and storing various blendings using a graphics program, or looking into the Assets "Texture Mixer" or "Colorify" on the Asset Store.
* Attach "ChangeColorInterface.cs" to any object in the scene. This file will call "ChangeColor.cs", but also provides code to handle the attached ECG data. 
* Place the example file "ecg_data.csv" anywhere on your computer and set the path in "ChangeColorInterface.cs" accordingly. Unity will simulate livestreaming the ECG data from that file.
* Press play. Character will start displaying the heartbeat after 5 seconds.

# License
These scripts run under the GPLv3 license.

