void setup() {
  // put your setup code here, to run once:
 Serial.begin(115200);
}

void loop() {
  // put your main code here, to run repeatedly:

 Serial.print(random(1,1024));
 Serial.print(","); 
 Serial.print(random(700,900));
 Serial.print(",");
 Serial.print(random(1,124));
 Serial.print(",");
 Serial.println(random(300,400));
delay(50);

 Serial.print(random(1,1024));
 Serial.print(","); 
 Serial.print(random(700,900));
 Serial.print(",");
 Serial.print(random(1,124));
 Serial.print(" ");
 Serial.println(random(300,400));
 
delay(50);
}
