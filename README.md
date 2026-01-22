# 3-DOF-Inverse-kinematic-robotic-manipulator

This project is an attempt at making a 3 degree of freedom robotic manipulator and implementing basic inverse kinematics

### Project contributors : 

- **Electronics and Programming :**
    -  Anirudh Pranesh - Eletronics and communication engineering department, National Institute of Technology Karnataka,  batch of 29'
- **Mathematical analsyses and assistance :** 
    - Neo Anil Dodti - Electronics and communication engineering department, National Institute of Technology Karnataka, batch of 29'
    - Aman Tandon - Electronics and communication engineering department, National Institute of Technology Karnataka, batch of 29'
- **Mechanical design :**
    - Maxon David Nazereth - Mechanical engineering department, National Institute of Technology Karnataka, batch of 29'

## The robotic manipulator and degrees of freedom 

The arm like structure of an industrial robot which is used to manipulate materials without direct physical ccontact by the operator is known as a robotic manipulator. A 3 degree of freedom robotic manipulator 
is designed to mimic the movement of a human arm. The motions achieved by the shoulder, elbows and wrists in a human arm is replicated in robotics using **joints**, **links**, **actuators**, and **end-effectors**

The image below depicts what an industrial grade robotic manipulator looks like

<img width="176" height="286" alt="image" src="https://github.com/user-attachments/assets/51f7078f-255e-482f-a9b9-8a1af4e2ce40" />

The degrees of freedom (DOF) of a manipulator is one of its defining features and is an important measure of the manipulators mobility and flexibility.

The degree of freedom is the number of independent moements the robot can make. 

## Introduction to the components of a manipulator and the initial design

The components that together constitute a robotic manipulator are **joints**, **links**, **actuators**, and **end-effectors**

1. Joint: A connection between two links that allows relative motion
    - Prismatic joint : Translation / sliding along one axis. Actuating the joint makes it longer/shorter
    - Revolute joint : A rotational joint (analogous to an elbow) that rotates about one degree of freedom
    - Spherial joint : A spherical joint can move in multiple degrees of freedom around a single point (analgous to the shoulder)
2. Link: A rigid body that connects joints and transmits motion and force
3. Actuator: A device that provides motion or force to drive a joint
4. End-effector: The terminal device attached to the last link that performs the task (for example a gripper / a scientific tool)

**The initial design :**

For a 3 DOF robotic manipulator, we fixated on 3 revolute joints. One to act as the shoulder and rotate about the y-axis. The 2nd to again act as a shoulder and rotate about the x-axis and the 3rd to act as the elbow, hence giving us access to any point in 3D space

We chose two, SG90 9g servos and one Futaba S3003 sero motor as the actuators. Ideally an actuator with a cycloidal gearbox should have been used. It offers very little backlash, and converts the high speeds of motors into higher torque to prevent the arm on collapsing on itself (An issue which was encountered later in the development process). However we went with the servo motors due to budget and time constraints.

For the links, we designed two I-beams in Fusion 360 that have a slot to attach the gear of the servo motor, and a hole on the other end to attach the end-effector / the servo motor for the elbow. 

In the end, after multiple centre of mass analsyes in Fusion 360, we determined the arm was stable and went with 3D printing it. We chose our material to be PETG with a 25% infill. 

![9AA72045-951F-4C17-9807-37681B96CB96](https://github.com/user-attachments/assets/af468727-5776-42bb-9396-febcf6eed133)

![759E43AC-6026-4F62-A465-EB66088A091E](https://github.com/user-attachments/assets/4f77d3f6-1087-4b03-9bed-8fc2b300bf7b)

https://github.com/user-attachments/assets/e654b260-f733-496b-b24b-751bba9cb4f4

## Forward vs Inverse kinematics

In forward kinematics, the operator manually sets the angles at which each joint should be rotated to, in order for its end-effector to reach a particular orientation/location in space.

However in inverse kinematics, the robot is aware of the final orientation/location its end-effector should be at, and it accordingly calculates at what angle each joint should be rotated to. 

For a 3-DOF manipulator, the math is relatively simple, involving inverse trigonometric functions and the cosine rule. However upon increasing the degrees of freedom the math becomes much more complex, involving linear algebra and transformations. 

## Real world applications 

A robotic manipulator is a quintessential piece of technology in the medical field. They are used in surgical robots and prosthetics. 

These are also often seen in industrial manufacturing processes and also used in space exploration missions. 

All these applications use inverse kinematics and hence it is clearly evident why understanding IK is cornerstone for working towards a career in robotics.

## Electronics and Programming - Inverse kinemtaics implementation 

The entire system is controlled using an arduino uno microcontroller. A unity simulation was set up to form the logical layer. Hence the rotation of each joint calculated via inverse kinematics in unity is obtained and sent to arduino using serial communication, and hence the angles are set to the servo motors using the Servo.h library. 

Here is the specific part of the code that performs the inverse kinematics calculations : 

```C#
// Obtaining traget position 
xf = Target.position.x;
yf = Target.position.y;
zf = Target.position.z;

r = Mathf.Sqrt(Mathf.Pow(xf, 2) + Mathf.Pow(zf, 2)); // Magnitude of the projection of the position vector of final point on the XZ plane
l = Mathf.Sqrt(Mathf.Pow(r, 2) + Mathf.Pow(yf, 2)); // Magnitude of the position vector of the final point

//l1 and l2 are the link lenghts

phi =  Mathf.Acos(((l*l)+(l1*l1)-(l2*l2))/(2*l1*l)) * Mathf.Rad2Deg; // Angle of the first link with respect the the position vector of the final point using cosine rule
theta = Mathf.Atan2(yf, r) * Mathf.Rad2Deg; // angle of the position vector of the final point with the XZ plane

Quaternion a = Quaternion.Euler(0, (180 + Mathf.Atan2(xf, zf) * Mathf.Rad2Deg), 0); // Setting angle for the base (shoulder) joint
Quaternion b = Quaternion.Euler(phi + theta, 0, 0); // Setting angle for the first link 
Quaternion c = Quaternion.Euler(-2 * phi, 0, 0); // Setting angle for the 2nd link (2 * phi assuming the two lengths are equal which it is in our case by design)

// Spherical linear interpolation for a smooth look
transform.localRotation = Quaternion.Slerp(transform.localRotation, a, t * Time.deltaTime);
base_joint.localRotation = Quaternion.Slerp(base_joint.localRotation, b, t * Time.deltaTime);
elbow_joint.localRotation = Quaternion.Slerp(elbow_joint.localRotation, c, t * Time.deltaTime);
```

Here is the fully working simulation : 

https://github.com/user-attachments/assets/3f60e0b6-7de5-4177-973a-fe10abcc836b

## Assembly

Once the 3D printed parts arrived, several **MAJOR** flaws in the mechanical design were immediately obvious.

First and foremost the parts were much heavier than anticipated. In hindsight it would have been a smarter move to make the links thinner and lesser in length
Secondly, the holes made for the gear of the servo motor to slot in were missing gear teeth, hence it was impossible to make the link rotate with the servo motor
Thirdly, the holes made for the servo motor to slot in at the joints had too less clearance

All in all the design was not optimal for the actuators we chose. 

![A29553D9-5106-4DBB-A082-6044CEA94357](https://github.com/user-attachments/assets/835ab70d-60cf-47be-be68-dd737c0f9b2d)

With some clever engineering, the plastic servo horns that came with the motors were attached to the links using strong rubber bands, and thankfully the link was able to rotate with the motor.

This however led to another issue. Due to the weight of the print being much higher than expected, our initial torque calculations went wrong. The SG90 servo could not provide enough torque at the shoulder to support the first link along with the 2nd link and the elbow joint. Hence, we used the S3003 servo motor with a much higher torque capacity to be the actuator at the shoulder. 

This proved to work and we had achieved 2 degrees of freedom for the robotic manipulator.

However we could not achieve the 3rd degree of freedom. The entire arm was attached to a circular disk which was mounted on to a singular servo motor for the rotation of the manipulator about the y axis. 

The motor could not sustain the weight of the entire system and the disk toppled. Many measures were taken in order to mitigate the ordeal. For example adding counter weights using metal. 
These efforts proved to be futile and we could not achieve the 3rd DOF. The gripper could not be attached either due to further excessive weight.  

We decided to work with the 2 DOF's we had and tweaked the code a little bit to achieve IK with it.

Here is the code for the unity simulation for 2 DOF manipulator : 

```C#

yf = Target.position.y;
zf = -Target.position.z;

r = Mathf.Sqrt(Mathf.Pow(yf, 2) + Mathf.Pow(zf, 2));

phi = Mathf.Atan2(yf , zf) * Mathf.Rad2Deg;
theta = Mathf.Acos(((r * r) + (l1 * l1) - (l2 * l2)) / (2 * l1 * r)) * Mathf.Rad2Deg;

base_joint.localRotation = Quaternion.Euler(phi + theta, 0, 0);
elbow_joint.localRotation = Quaternion.Euler(-2*theta, 0, 0);

intAngle1 = (int)(phi+theta)-11;
intAngle2 = (int)(2*theta);
strAngle1 = intAngle1.ToString();
strAngle2 = intAngle2.ToString();
relay = strAngle1 + " " + strAngle2;
serial.WriteLine(relay);

```

Here is the image of the disk barely balancing on the servo motor. It may be used as a proof of concept for a 3 DOF manipulator with our design : 

![IMG_2548](https://github.com/user-attachments/assets/99afc59b-44fd-4f67-a9bd-eb07a080f534)


# Final project showcase : 

https://github.com/user-attachments/assets/3d56ba8f-9d47-44a3-8794-81a7bd2c54c0

# Learning outcomes 

- The mechanical design itself has no flaw. However more rigorous testing must be done before going ahead with the 3D print. For instance performing mechanical simulations like finite element analsysis.
- A basic, but in depth understanding of robotic manipulators and the math lying behind it
- A basic introduction to DH parameters
- Exposure to alternative robotic simulation software like coppeliaSim (was not used due to logistical and time constraints)
- Achieved an understanding of practical applications of robotic manipulators in everyday life and how they can be integrated with other robotic systems





