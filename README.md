# object angle detection
pivot_angle_script
pivot_angle_script is a Unity MonoBehaviour script designed to manage and manipulate the Z-axis rotation (angle) of a GameObject. It provides methods to set, add, and smoothly animate the Z angle, while also exposing the current Z angle as a public variable for use by other scripts.

Features
•	Set Z Angle: Instantly set the GameObject's Z rotation to a specific value.
•	Add to Z Angle: Increment or decrement the Z rotation by a given amount.
•	Smooth Z Angle Animation: Animate the Z rotation smoothly over a specified duration.
•	Keyboard Control: Increase or decrease the Z angle by 40 degrees using the 1 and 2 keys.
•	Public Z Angle Value: The current Z angle (normalized between 0-360) is always available via the pivotZValue public variable for use in other scripts.

How It Works
•	The script keeps track of the initial and last set Z angles.
•	All angle values are normalized to the 0-360 range and rounded to the nearest integer.
•	The Z angle can be changed directly, incrementally, or smoothly via coroutine.
•	The pivotZValue variable is automatically updated whenever the angle changes.

Usage
1.	Attach the Script: Add pivot_angle_script to any GameObject whose Z rotation you want to control.
2.	Control via Code:
•	Use SetZAngle(float z) to set the Z angle directly.
•	Use AddZAngle(float delta) to add or subtract from the current Z angle.
•	Use SetZAngleSmooth(float z, float duration) to animate the Z angle.
3.	Keyboard Shortcuts:
•	Press 1 to increase the Z angle by 40 degrees.
•	Press 2 to decrease the Z angle by 40 degrees.
4.	Access Current Angle:
•	Other scripts can read the current Z angle from the pivotZValue public variable.

Current usage
Since it is written to manage an ammeter in the game, the needle is moved by listening to the 1, 2, 3 and 4 keys.
Depending on the usage area, it can only be enabled to detect the angle
