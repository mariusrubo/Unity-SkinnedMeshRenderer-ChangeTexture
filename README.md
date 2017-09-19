# Purpose
These scripts allow you to display your heartbeat live on your avatar, changing its skin's texture with every beat and fading back between beats.

![alt tag](https://github.com/mariusrubo/Unity-DisplayHeartbeat/blob/master/characterChangeColor.png)

# Installation
The installation of this functionality consists of two steps: 
* Enabling your character to change its color 
* Link color change to ECG data
## Enable your character to change its color
* If you merely wish your entire character to turn red, including its hair and clothes, you can simply use this line of code (with 'character' being the GameObject of your character and 'color' a float between 0 and 1): character.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red, color); 
* Changing the color of only some parts of your mesh, like the skin or perhaps only the hands, is more complex. First find the skin texture of your character in your project folder (it looks like the rolled out wrapping of a chocolate Santa Claus), create a folder ".../Assets/Ressources" and copy it there. Copy this image again and colorize it as you like. Find both these images inside the Unity project tab. In the inspector, set "Read/Write Enable" to true and change Format to "RGBA 32 Bit". 
* Attach the script "ChangeColor.cs" to your character and drag the skin carrying the mesh renderer into "skin". Inside "ChangeColor.cs", set the names of your two skin files. After starting, this script will create 5 different blendings of the two images you provided, and store these to dynamically fade the color intensity at runtime. If you need smoother blending, you can change the amount of blended images in here. If Unity takes too long to start your scene (as blending of images is computationally heavy), I recommend setting up and storing various blendings using a graphics program, or looking into the Assets "Texture Mixer" or "Colorify" on the Asset Store.
## Link color change to ECG data
* Attach "ChangeColorInterface.cs" to any object in the scene. This file will call "ChangeColor.cs", but also provides code to handle ECG data and change the color accordingly.
* Place the example file "ecg_data.csv" anywhere on your computer and set the path in "ChangeColorInterface.cs" accordingly. Unity will simulate livestreaming the ECG data from that file.
* Press play. Character will start displaying the heartbeat after 5 seconds.

# Detecting R peaks
* Detecting R peaks (the "beat") in ECG data is not trivial, and the research behind it is not covered here. The simple approach used here works well for very clean ecg recordings as like the provided data and serves as a quick demonstration. For an introduction to science-grade detection algorithms, one may start here: https://de.mathworks.com/help/dsp/examples/real-time-ecg-qrs-detection.html
* The below image shows the approach taken for this script. ECG data can usually be recorded at a high temporal precision of 500Hz or more (a), making the R curve easy to find visually in artefact-free recordings. Plotting the change in voltage rather than the absolute voltage (b) can even increase the signal(R-curve)-to-noise(all other components)-ratio. A lower frequency of 60Hz makes the R curve less obvious (c), but still detectable when looking at voltage changes (d).
* "ChangeColorInterface.cs" parses the 60Hz-ECG data provided in the example and looks at the voltage changes as in (d). It first collects data for 5 seconds, then determines the 95th percentile drawn as a line in (d). From that point, new data located beyond this threshold will turn the character's skin red, while otherwise color fades back to its original value. 

![alt tag](https://github.com/mariusrubo/Unity-DisplayHeartbeat/blob/master/detectRcurve.png)

# License
These scripts run under the GPLv3 license.

