# Purpose
These scripts allow you to display your heartbeat live on your avatar, turning its skin red with every beat. 

# Installation
The installation of this functionality consists of two steps: 
* Enabling your character to change its color 
* Link color change to ECG data
## Enable your character to change its color
* If you merely wish your entire character to turn red, including its hair and clothes, you can simply use this line of code (with 'character' being the GameObject of your character and 'color' a float between 0 and 1): character.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red, color*0.1f); 
* Changing the color of only some parts of your mesh, like the skin or perhaps only the hands, is more complex. First find the skin texture of your character in your project folder (it looks like the rolled out wrapping of a chocolate Santa Claus), create a folder ".../Assets/Ressources" and copy it there. Copy this image again and colorize it as you like. Find both these images inside the Unity project tab. In the inspector, set "Read/Write Enable" to true and change Format to "RGBA 32 Bit". 
* Attach the script "ChangeColor.cs" to your character and drag the skin carrying the mesh renderer into "skin". Inside "ChangeColor.cs", set the names of your two skin files. After starting, this script will create 5 different blendings of the two images you provided, and store these to dynamically fade the color intensity at runtime. If you need smoother blending, you can change the amount of blended images in here. If Unity takes too long to start your scene (as blending of images is computationally heavy), I recommend setting up and storing various blendings using a graphics program, or looking into the Assets "Texture Mixer" or "Colorify" on the Asset Store.
## Link color change to ECG data
* Attach "ChangeColorInterface.cs" to any object in the scene. This file will call "ChangeColor.cs", but also provides code to handle ECG data and change the color accordingly.
* Place the example file "ecg_data.csv" anywhere on your computer and set the path in "ChangeColorInterface.cs" accordingly. Unity will simulate livestreaming the ECG data from that file.
* Press play. Character will start displaying the heartbeat after 5 seconds.

# Background: Detecting R curves
* Robustly detecting R curves (the "beat") in ECG data is not trivial, and when small changes in frequency are important, ECG data is typically analyzed offline and only semi-automatically. To display ECG data live of course requires online processing. 
* The below image shows the approach taken for this script. ECG data can usually be recorded at a high temporal precision of 500Hz or more (a), making the R curve easy to find visually, at least in artefact-free recordings like the one displayed here. Plotting the change in voltage rather than the absolute voltage (b) can even increase the signal(R-curve)-to-noise(all other components)-ratio. Recording and evaluating ECG data at Unity's native frequency of 60Hz makes the R curve  less obvious (c), although looking at differences rather than absolute values may again facilitate detecting (d).
* "ChangeColorInterface.cs" parses the 60Hz-ECG data provided in the example, and only looks at the voltage changes as in (d). It first collects data for 5 seconds, and then determines to 95th percentile of these datapoints drawn as a line in (d). Note that choosing a lower percentile will reduce the chance of missing an R curve, but increase the risk of false positives, and vice verca. From that point on, any new data point located beyond this threshold will trigger a sudden change of the character's skin to red, while datapoints smaller than the threshold will allow the skin to slowly fade back to its original color. Since only fast changes in voltage are processed, this approach is robust against slow changes of the ECG baseline which may occur in the course of an experiment. 
* Depending on your ECG measurement system's API, you will be able to use other procedures to detect R curves. This is just a basic example using raw data provided ata low frequency of 60 Hz. 

# License
These scripts run under the GPLv3 license.

