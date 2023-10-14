# RollChannelControl

## Description
This is a small but powerful PID (Proportional, Integral, Derivative) controller specifically designed and adapted for an airplane simulation model. It's primarily used for controlling the roll rate of the aircraft, which is crucial in stabilizing the aircraft's bank rotation through the movement of the ailerons. In addition to the usual PID terms, it incorporates the third derivative of the bank angle (jerk) to improve the realism and relevance of the simulator.

## Theoretical concepts
The modelled controller aims to stabilize the roll rate of an aircraft (Airbus: gamma dot) to a value proportional to the deviation of the side-stick, by driving the aielron positions. Below is an approximation of the expected behavior of roll channel right after sidestick deflection:
![image](https://github.com/CristianMazilu/RollChannelControl/assets/43795897/1ee569eb-56ac-42e8-96ba-3aab3131ba21)


## Installation
The code provided is written in C# and primarily relies on the LiveCharts.Wpf library for graphical representation. Make sure you have the necessary packages installed in your development environment.

## Usage

This controller's core is contained in the `Form1` class. Here's a brief overview of its most important elements:

### Class Variables
- `X`: The roll rate (deg/s) of the aircraft.
- `XDotIn` and `XDotOut`: Input and output values for the roll rate respectively.
- `XDotDot`: Represents the acceleration (the derivative of `XDotOut`).
- `XDotDotSetPoint`: The desired acceleration (deg/s^2) calculated from PID.
- `DeltaXDotDot`: The difference between the desired and the actual acceleration.
- `XDotIntegral` and `XDotDerivative`: Integral and derivative terms for the PID controller.

### Constants
- `Kp`, `Ki`, `Kd`: These are the proportional, integral, and derivative gains respectively. Adjust these to tune the PID controller.
- `DerivativeSampleSize`, `IntegralSampleSize`: The number of samples to use when calculating the integral and derivative.
- `maxAngularJerk`: The maximum angular jerk (deg/s^3) that can be expected of the aircraft.

### Queue
- `XDotDerivativeQueue` and `XDotIntegralQueue`: Queues for storing past samples of the integral and derivative.

### GUI Components
- `rollRateCartesianChart` and `accelerationRateCartesianChart`: Charts for displaying the roll rate and its derivative.

### Methods
- `EvaluateX()`, `EvaluateXDotOut()`, `EvaluateXDotDerivative()`, `EvaluateIntegral()`, `EvaluateXDotDot()`, `EvaluateXDotDotDot()`: These functions are responsible for calculating the PID terms and their derivatives.

## Contributing
Contributions are welcome. If you have a feature to request or a bug to report, please do so through the GitHub issues page.

## License
This project is licensed under the MIT License. See `LICENSE` for more information.
