#include <Servo.h>

Servo base;
Servo elbow;
String input = "";
int angle1 = 0;
int angle2 = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  Serial.setTimeout(100);
  base.attach(6);
  elbow.attach(5);
  //base.write(0);
  //elbow.write(0);
}

void loop() {
  input = Serial.readStringUntil('\n');   // read full line
  int spaceIndex = input.indexOf(' ');    // find space
  String first = input.substring(0, spaceIndex);
  String second = input.substring(spaceIndex + 1);

  angle1 = first.toInt();
  angle2 = second.toInt();

  base.write(30+angle1);
  elbow.write(180-angle2);


}
