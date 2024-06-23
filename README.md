# Satellite-Orbit-Simulation
[System Model](https://github.com/hamdyekiz/SatelliteOrbitSimulation/edit/main/README.md#system-model)

[Theories](https://github.com/hamdyekiz/SatelliteOrbitSimulation/edit/main/README.md#system-model)

## System Model
In this project a software is written to simulate the space-systems. The space system described here includes a collection of satellites and ground stations. The schematic of this system is represented below:

![Representation of the System](https://github.com/hamdyekiz/SatelliteOrbitSimulation/blob/main/Project%20Documentations/Images/Picture21.png)

Two types of satellites are assumed to exist in this system: 
1)	Satellites that only receive data from the ground stations 
2)	Satellites that only send data to the ground stations
   
We have also three types of ground stations: 
1)	Ground stations that only communicate with the satellites 
2)	Ground stations that only track the satellites 
3)	Ground stations that both communicate with and track the satellites.

## Theories
To simulate the motion of the satellite, numerical integration methods (Runge-Kutta 4th Order and Euler Integration) is used to solve the equations of motion. In figure below you can see the satellite and grand station representations:

![Visualization of the System](https://github.com/hamdyekiz/SatelliteOrbitSimulation/blob/main/Project%20Documentations/Images/satellite%20ground%20station.png)

Visualization of the satellite's position.

![Visualization of the satellite's position](https://github.com/hamdyekiz/SatelliteOrbitSimulation/blob/main/Project%20Documentations/Images/visualization%20of%20the%20satellite%20position.png)

Visualization of the satellite's velocity.

![Visualization of the satellite's velocity](https://github.com/hamdyekiz/SatelliteOrbitSimulation/blob/main/Project%20Documentations/Images/visualization%20of%20the%20satellite%20velocity.png)

Visualization of the satellite's dynamic model. W represent as Gravity Force, and D represent as Aerodynamic Force.

![Visualization of the satellite's dynamic model](https://github.com/hamdyekiz/SatelliteOrbitSimulation/blob/main/Project%20Documentations/Images/dynamic%20model.png)

Visualization of the satellite's gravity force.

![Visualization of the satellite's gravity force](https://github.com/hamdyekiz/SatelliteOrbitSimulation/blob/main/Project%20Documentations/Images/visualization%20of%20the%20gravity%20force%20comp..png)

Visualization of the satellite's aerodynamic force.

![Visualization of the satellite's aerodynamic force](https://github.com/hamdyekiz/SatelliteOrbitSimulation/blob/main/Project%20Documentations/Images/visualization%20of%20the%20aerodynamic%20force%20comp..png)

Visualization of the ground station's position.

![Visualization of the ground station's position](https://github.com/hamdyekiz/SatelliteOrbitSimulation/blob/main/Project%20Documentations/Images/visualization%20of%20the%20ground%20station%20position.png)

Visualization of the ground station's field of view.

![Visualization of the ground station's field of view](https://github.com/hamdyekiz/SatelliteOrbitSimulation/blob/main/Project%20Documentations/Images/Visualization%20of%20the%20ground%20%20station's%20field%20of%20view%20angle.png)




