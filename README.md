# Sketchtoar
View your hand drawn sketches in Augmented reality


### Project Overview
This app detects the object drawn on a sheet of paper and displays the respective 3d Object on top of it.
The AI system runs on a server and the output is communicated to the app through firebase so that the app can be accessed from anywhere in the world using less device resources.

### Tech Used
* Google Arcore for displaying 3d object in AR.
* Google Firebase for realtime communication through the internet.
* Google Quick Draw dataset to train our CNN model to understand specific sketches.
* OpenCV to detect coordinates of each sketch and preprocess for CNN model
* Unity Environment used for developing the app
